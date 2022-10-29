import axios from "axios";

const getCategories = () => axios.get("https://localhost:7133/Category/GetAllCategories");
const getProducts = () => axios.get("https://localhost:7133/Product/GetAllProducts");
const getCustomers = () => axios.get("https://localhost:7133/User/GetAllUsers");

export {
    getCategories,
    getProducts,
    getCustomers
};