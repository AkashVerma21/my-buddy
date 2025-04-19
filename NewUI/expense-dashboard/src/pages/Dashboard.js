import React, { useState } from 'react';
import AddExpenseForm from '../components/AddExpenseForm';
import ExpenseList from '../components/ExpenseList';

const Dashboard = () => {
    const [expenses, setExpenses] = useState([]);

    const addExpense = (expense) => {
        setExpenses([...expenses, expense]);
    };

    return (
        <div>
            <h1>Expense Dashboard</h1>
            <AddExpenseForm addExpense={addExpense} />
            <ExpenseList expenses={expenses} />
        </div>
    );
};

export default Dashboard;