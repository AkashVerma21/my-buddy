import React from 'react';
import AddExpenseForm from '../components/AddExpenseForm';

const AddExpense = () => {
    return (
        <div>
            <h1>Add New Expense</h1>
            <AddExpenseForm onAddExpense={(expense) => {
                console.log(expense);
            }} />
        </div>
    );
};

export default AddExpense;