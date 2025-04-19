import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import AddExpense from './pages/AddExpense';
import './styles/App.css';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/add-expense" element={<AddExpense />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;