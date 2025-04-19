import React, { useState } from 'react';

const AddExpenseForm: React.FC<{ onAddExpense: (expense: { title: string; amount: number; date: string }) => void }> = ({ onAddExpense }) => {
    const [title, setTitle] = useState('');
    const [amount, setAmount] = useState('');
    const [date, setDate] = useState('');

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        if (title && amount && date) {
            onAddExpense({ title, amount: parseFloat(amount), date });
            setTitle('');
            setAmount('');
            setDate('');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Title</label>
                <input 
                    type="text" 
                    value={title} 
                    onChange={(e) => setTitle(e.target.value)} 
                    required 
                />
            </div>
            <div>
                <label>Amount</label>
                <input 
                    type="number" 
                    value={amount} 
                    onChange={(e) => setAmount(e.target.value)} 
                    required 
                />
            </div>
            <div>
                <label>Date</label>
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