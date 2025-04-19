import React, { useState } from 'react';
import ExpenseList from '../components/ExpenseList';

const Dashboard = () => {
    const [expenses, setExpenses] = useState([]);

    const addExpense = (expense) => {
        setExpenses([...expenses, expense]);
    };

    return (
        <div>
            <h1>Expense Dashboard</h1>
            <AddExpenseForm onAddExpense={addExpense} />
            <ExpenseList expenses={expenses} />
        </div>
    );
};

export default Dashboard;