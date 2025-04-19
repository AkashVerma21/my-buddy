import React, { useState, useEffect } from 'react';
import '../css/ExpenseHistory.css'; // Import the CSS file

export function ExpenseHistory() {
   const [expenses, setExpenses] = useState([]);
  
  // Fetch expenses from API
      useEffect(() => {
          fetch('http://localhost:5047/api/expenses')
              .then(response => response.json())
              .then(data => setExpenses(data))
              .catch(error => console.error('Error fetching data:', error));
      }, []);

    const totalExpenses = expenses.reduce((sum, expense) => sum + expense.amount, 0);


  return (
    <div className="expense-history-container">
      <div className="header-section">
        <h2 className="history-title">Expense History</h2>
        <div className="total-expenses">
          Total: {totalExpenses.toFixed(2)}
        </div>
      </div>
      <div className="table-container">
        <table className="expense-table">
          <thead className="table-header">
            <tr>
              <th className="header-cell">Date</th>
              <th className="header-cell">Amount</th>
              <th className="header-cell">Description</th>
              <th className="header-cell">Category</th>
            </tr>
          </thead>
          <tbody className="table-body">
            {expenses.map((expense) => (
              <tr key={expense.id}>
                <td className="data-cell">
                  {expense.date}
                </td>
                <td className="data-cell">
                  {expense.amount.toFixed(2)}
                </td>
                <td className="data-cell">{expense.description}</td>
                <td className="data-cell">
                  <span className={`category-badge category-${expense.category}`}>
                    {expense.category}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
