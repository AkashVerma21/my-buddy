import React,{useState} from "react";
import { Dashboard } from "./components/Dashboard";
import { Navigation } from "./components/Navigation";


function App() {
  return (
    <div>
      <Navigation />
      <Dashboard />
    </div>
  );
}

export default App;
