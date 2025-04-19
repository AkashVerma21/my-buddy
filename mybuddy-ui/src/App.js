import React,{useState} from "react";
import ExpenseList from "./components/ExpenseTable";
import AddExpense from "./components/AddExpense";

function App() {
  const [expenses, setExpenses] = useState([]);
  return (
    <div className="App">
      <h1>Expense Tracker</h1>
      <AddExpense expenses={expenses} setExpenses={setExpenses}/>
      <ExpenseList expenses={expenses} setExpenses={setExpenses}/>
    </div>
  );
}

export default App;
