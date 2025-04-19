import React from 'react';
import AddExpenseForm from '../components/AddExpenseForm';

const AddExpense: React.FC = () => {
    return (
        <div>
            <h1>Add New Expense</h1>
            <AddExpenseForm onAddExpense={(expense) => {
                // handle the added expense
                console.log(expense);
            }} />
        </div>
    );
};

export default AddExpense;