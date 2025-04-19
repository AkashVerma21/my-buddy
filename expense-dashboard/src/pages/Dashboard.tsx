import React from 'react';
import AddExpenseForm from '../components/AddExpenseForm';
import ExpenseList from '../components/ExpenseList';

const Dashboard: React.FC = () => {
    const [expenses, setExpenses] = React.useState([]);

    const addExpense = (expense: { title: string; amount: number; date: string }) => {
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