import React, { useState, useEffect } from 'react';

const ExpenseTable = () => {
    const [expenses, setExpenses] = useState([]);
    const [editMode, setEditMode] = useState(false);
    const [currentExpense, setCurrentExpense] = useState({ id: null, name: '', amount: '', date: '' });

    // Fetch expenses from API
    useEffect(() => {
        fetch('http://localhost:5047/api/expenses')
            .then(response => response.json())
            .then(data => setExpenses(data))
            .catch(error => console.error('Error fetching data:', error));
    }, []);

    // Handle delete
    const deleteExpense = (id) => {
        fetch(`http://localhost:5047/api/expenses/${id}`, {
            method: 'DELETE',
        })
        .then(() => setExpenses(expenses.filter(expense => expense.id !== id)))
        .catch(error => console.error('Error deleting expense:', error));
    };

    // Handle edit
    const startEditing = (expense) => {
        setEditMode(true);
        setCurrentExpense(expense);
    };

    const updateExpense = (id, updatedExpense) => {
        fetch(`http://localhost:5047/api/expenses/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updatedExpense)
        })
        .then(response => response.json())
        .then((updatedData) => {
            setExpenses(expenses.map(exp => (exp.id === id ? updatedData : exp)));
            setEditMode(false);
            setCurrentExpense({ id: null, name: '', amount: '', date: '' });
        })
        .catch(error => console.error('Error updating expense:', error));
    };

    // Handle form submission for update
    const handleFormSubmit = (e) => {
        e.preventDefault();
        updateExpense(currentExpense.id, currentExpense);
    };

    // Handle form change
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setCurrentExpense({ ...currentExpense, [name]: value });
    };

    // Calculate total expense
    const totalExpense = expenses.reduce((total, expense) => total + expense.amount, 0);

    return (
        <div>
            <h2>Expense List</h2>

            {editMode ? (
                <form onSubmit={handleFormSubmit}>
                    <h3>Edit Expense</h3>
                    <label>
                        Name:
                        <input type="text" name="name" value={currentExpense.name} onChange={handleInputChange} required />
                    </label>
                    <label>
                        Amount:
                        <input type="number" name="amount" value={currentExpense.amount} onChange={handleInputChange} required />
                    </label>
                    <label>
                        Date:
                        <input type="datetime-local" name="date" value={currentExpense.date} onChange={handleInputChange} required />
                    </label>
                    <button type="submit">Update Expense</button>
                    <button onClick={() => setEditMode(false)}>Cancel</button>
                </form>
            ) : null}

            <h3>Total Expense: Rs. {totalExpense.toFixed(2)}</h3> {/* Display total expense */}

            <table border="1" cellPadding="10" cellSpacing="0">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Amount</th>
                        <th>Date</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {expenses.map((expense) => (
                        <tr key={expense.id}>
                            <td>{expense.id}</td>
                            <td>{expense.description}</td>
                            <td>Rs. {expense.amount.toFixed(2)}</td>
                            <td>{new Date(expense.date).toLocaleString()}</td>
                            <td>
                                <button onClick={() => startEditing(expense)}>Edit</button>
                                <button onClick={() => deleteExpense(expense.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ExpenseTable;
