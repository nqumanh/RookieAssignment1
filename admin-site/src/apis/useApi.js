import axios from "axios";

// const BASE_URL = "https://localhost:7133/";

// Category
const getAllCategories = () =>
    axios.get("https://localhost:7133/Category/GetAll");
const addCategory = (category) =>
    axios.post("https://localhost:7133/Category/Create", category);
const getCategoryById = (id) =>
    axios.get(`https://localhost:7133/Category/Get/${id}`);
const updateCategory = (id, category) =>
    axios.put(`https://localhost:7133/Category/Update/${id}`, category);
const deleteCategoryApi = (id) =>
    axios.delete(`https://localhost:7133/Category/Delete/${id}`);

// Product
const getProducts = () =>
    axios.get("https://localhost:7133/Product/GetAllProducts");

// Customer
const getCustomers = () =>
    axios.get("https://localhost:7133/User/GetAllUsers");

export {
    getAllCategories,
    addCategory,
    getCategoryById,
    updateCategory,
    deleteCategoryApi,
    
    getProducts,
    getCustomers
};