import React from 'react';
import ExpenseItem from './ExpenseItem';

const ExpenseList = ({ expenses }) => {
    return (
        <div>
            <h2>Expense List</h2>
            {expenses.length === 0 ? (
                <p>No expenses found.</p>
            ) : (
                <ul>
                    {expenses.map((expense) => (
                        <ExpenseItem key={expense.id} expense={expense} />
                    ))}
                </ul>
            )}
        </div>
    );
};

export default ExpenseList;