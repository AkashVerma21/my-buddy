import React, { useState } from "react";
import axios from "axios";

const AddExpense = ({expenses, setExpenses}) => {
  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState("");
  const [date, setDate] = useState("");
  const [category, setCategory] = useState("");

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
    <div>
      <h2>Add Expense</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Description"
        />
        <input
          type="number"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          placeholder="Amount"
        />
        <input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
        <input
          type="text"
          value={category}
          onChange={(e) => setCategory(e.target.value)}
          placeholder="Category"
        />
        <button type="submit">Add Expense</button>
      </form>
    </div>
  );
};

export default AddExpense;
