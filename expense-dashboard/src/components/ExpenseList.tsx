import React from 'react';
import ExpenseItem from './ExpenseItem';

interface Expense {
    id: number;
    title: string;
    amount: number;
    date: string;
}

interface ExpenseListProps {
    expenses: Expense[];
}

const ExpenseList: React.FC<ExpenseListProps> = ({ expenses }) => {
    return (
        <div>
            <h2>Expense List</h2>
            <ul>
                {expenses.map(expense => (
                    <ExpenseItem key={expense.id} expense={expense} />
                ))}
            </ul>
        </div>
    );
};

export default ExpenseList;