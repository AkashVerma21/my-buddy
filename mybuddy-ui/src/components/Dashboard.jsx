import React,{useState} from "react";
import { ExpenseForm } from '../components/ExpenseForm';
import { ExpenseHistory } from '../components/ExpenseHistory';
import '../css/Dashboard.css'; // Import the CSS file

export function Dashboard() {
    const [expenses, setExpenses] = useState([]);
  return (
    <div className="dashboard-container">
      <div className="content-wrapper">
      <h1 className="dashboard-title">Financial Dashboard</h1>
        <div className="header-section">
          <div className="grid-container">
          <ExpenseForm expenses={expenses} setExpenses={setExpenses} />
          <ExpenseHistory expenses={expenses} setExpenses={setExpenses}/>
          </div>
        </div>
      </div>
    </div>
  );
}
