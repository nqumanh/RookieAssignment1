import React from "react";
import { Navigate } from "react-router-dom";

const Protected = ({ isLoggedIn, children, redirectPath = "/" }) => {
    if (!isLoggedIn) {
        return <Navigate to={redirectPath} replace />;
    }
    return children;
};
export default Protected;