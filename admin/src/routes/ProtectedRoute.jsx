import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children }) => {
    let user = localStorage.getItem("token")
    console.log(user)
    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return children;
};

export default ProtectedRoute;