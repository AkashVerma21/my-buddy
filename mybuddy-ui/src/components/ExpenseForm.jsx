import React, { useState } from 'react';
import '../css/ExpenseForm.css'; // Import the CSS file
import axios from "axios";

export function ExpenseForm({expenses, setExpenses}) {
  const [amount, setAmount] = useState('');
  const [description, setDescription] = useState('');
  const [category, setCategory] = useState('');
  const [date, setDate] = useState("");


  const handleSubmit = (event) => {
    event.preventDefault();
    const newExpense = {
      description,
      amount: parseFloat(amount),
      date,
      category,
    };

    axios
      .post("http://localhost:5047/api/expenses", newExpense)
      .then((response) => {
        console.log("Expense added successfully:", response.data);
        setExpenses([...expenses, response.data])
        // Clear the form
        setDescription("");
        setAmount("");
        setDate("");
        setCategory("");
      })
      .catch((error) =>
        console.error("There was an error adding the expense!", error)
      );
  };

  return (
    <form onSubmit={handleSubmit} className="expense-form">
      <h2 className="form-title">Add New Expense</h2>
      <div className="form-fields">
        <div className="form-group">
          <label className="form-label">Amount</label>
          <input
            type="number"
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
            className="form-input"
            required
            step="0.01"
          />
        </div>
        <div className="form-group">
          <label className="form-label">Description</label>
          <input
            type="text"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="form-input"
            required
          />
        </div>
        <div className="form-group">
          <label className="form-label">Category</label>
          <select
            value={category}
            onChange={(e) => setCategory(e.target.value)}
            className="form-input"
            required
          >
            <option value="">Select a category</option>
            <option value="food">Food</option>
            <option value="transportation">Transportation</option>
            <option value="utilities">Utilities</option>
            <option value="entertainment">Entertainment</option>
            <option value="other">Other</option>
          </select>
        </div>
        <div className="form-group">
          <label className="form-label">Date</label>
          <input
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
            className="form-input"
            required
            step="0.01"
          />
        </div>
        <button type="submit" className="submit-button">
          Add Expense
        </button>
      </div>
    </form>
  );
}
