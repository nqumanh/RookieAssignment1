import { useRoutes } from "react-router-dom";
import Login from "pages/Login";
import ManageProduct from "pages/ManageProducts";
import Dashboard from "layout";
import CreateProduct from "pages/CreateProduct";
import ManageCustomer from "pages/ManageCustomer";
import ManageCategory from "pages/ManageCategory";

const Routes = () => {
    const routes = [
        {
            path: "/login",
            element: <Login />
        },
        {
            path: "/",
            element: <Dashboard />,
            children: [
                {
                    path: "products",
                    element: <ManageProduct />
                },
                {
                    path: "create-product",
                    element: <CreateProduct />
                },
                {
                    path: "customers",
                    element: <ManageCustomer />
                },
                {
                    path: "categories",
                    element: <ManageCategory />
                },
            ]
        },
    ]

    return (
        useRoutes(routes)
    )
}

export default Routes;