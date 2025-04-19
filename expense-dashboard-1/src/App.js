import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import AddExpense from './pages/AddExpense';
import './styles/App.css';

const App = () => {
    return (
        <Router>
            <div className="App">
                <Switch>
                    <Route path="/" exact component={Dashboard} />
                    <Route path="/add-expense" component={AddExpense} />
                </Switch>
            </div>
        </Router>
    );
};

export default App;