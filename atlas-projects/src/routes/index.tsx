import React from "react";
import { Routes as Router, Route, BrowserRouter, Navigate } from "react-router-dom";

import Dashboard from "../pages/Dashboard";

const Routes = () => {

   return(
        <BrowserRouter>
            <Router>
                <Route path="/"  element={ <Navigate replace to="/dashboard" /> } /> 
                <Route element={ <Dashboard /> }  path="/dashboard" />
            </Router>
        </BrowserRouter>
   )
}

export default Routes;