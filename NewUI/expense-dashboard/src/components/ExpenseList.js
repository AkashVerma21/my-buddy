import React from 'react';
import ExpenseItem from './ExpenseItem';

const ExpenseList = ({ expenses }) => {
    return (
        <div>
            <h2>Expense List</h2>
            <ul>
                {expenses.map((expense, index) => (
                    <ExpenseItem key={index} expense={expense} />
                ))}
            </ul>
        </div>
    );
};

export default ExpenseList;