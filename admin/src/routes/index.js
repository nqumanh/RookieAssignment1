import { useRoutes } from "react-router-dom";
import Login from "pages/Login";
import Dashboard from "layout/Dashboard";
import ManageProduct from "pages/ManageProducts";

const Routes = () => {
    const routes = [
        {
            path: "/",
            element: <Login />
        },
        {
            path: "/dashboard",
            element: <Dashboard />,
            children: [
                {
                    path: "products",
                    element: <ManageProduct />
                },
            ]
        },
    ]

    return (
        useRoutes(routes)
    )
}

export default Routes;