import React, { useState } from 'react';

const AddExpenseForm = ({ onAddExpense }) => {
    const [amount, setAmount] = useState('');
    const [description, setDescription] = useState('');
    const [date, setDate] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!amount || !description || !date) return;

        const newExpense = {
            amount: parseFloat(amount),
            description,
            date: new Date(date).toISOString(),
        };

        onAddExpense(newExpense);
        setAmount('');
        setDescription('');
        setDate('');
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Amount:</label>
                <input
                    type="number"
                    value={amount}
                    onChange={(e) => setAmount(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Description:</label>
                <input
                    type="text"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Date:</label>
                <input
                    type="date"
                    value={date}
                    onChange={(e) => setDate(e.target.value)}
                    required
                />
            </div>
            <button type="submit">Add Expense</button>
        </form>
    );
};

export default AddExpenseForm;