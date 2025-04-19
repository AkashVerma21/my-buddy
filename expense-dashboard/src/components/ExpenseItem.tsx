import React from 'react';

interface ExpenseItemProps {
    title: string;
    amount: number;
    date: string;
}

const ExpenseItem: React.FC<ExpenseItemProps> = ({ title, amount, date }) => {
    return (
        <div className="expense-item">
            <h2>{title}</h2>
            <div className="expense-item__details">
                <span>${amount.toFixed(2)}</span>
                <span>{new Date(date).toLocaleDateString()}</span>
            </div>
        </div>
    );
};

export default ExpenseItem;