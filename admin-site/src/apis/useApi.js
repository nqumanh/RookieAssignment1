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
const getAllProducts = () =>
    axios.get("https://localhost:7133/Product/GetAll");
const getProductById = (id) =>
    axios.get(`https://localhost:7133/Product/Read/${id}`);

// Customer
const getCustomers = () =>
    axios.get("https://localhost:7133/Admin/GetAllUsers");

export {
    getAllCategories,
    addCategory,
    getCategoryById,
    updateCategory,
    deleteCategoryApi,
    
    getAllProducts,
    getProductById,

    getCustomers
};