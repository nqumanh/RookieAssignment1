import { useRoutes } from "react-router-dom";
import Login from "pages/Login";
import Dashboard from "pages/Dashboard";
import PaginationBackend from "features/Table";

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
                    path: "",
                    element: <PaginationBackend />
                },
            ]
        },
    ]

    return (
        useRoutes(routes)
    )
}

export default Routes;