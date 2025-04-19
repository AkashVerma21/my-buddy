import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import AddExpense from './pages/AddExpense';
import './styles/App.css';

const App: React.FC = () => {
  return (
    <Router>
      <Switch>
        <Route path="/" exact component={Dashboard} />
        <Route path="/add-expense" component={AddExpense} />
      </Switch>
    </Router>
  );
};

export default App;