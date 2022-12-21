import axios from "axios";

const BASE_URL = "https://localhost:7133";

const getAxios = (url, params = {}) =>
    axios.get(url, {
        params: params,
    });

// Admin
const loginApi = (loginForm) =>
    axios.post(`${BASE_URL}/Admin/Login`, loginForm)
const logoutApi = () =>
    axios.get(`${BASE_URL}/Admin/Logout`);

// Category
const getAllCategories = () =>
    axios.get(`${BASE_URL}/Category/GetAll`);
const addCategory = (category) =>
    axios.post(`${BASE_URL}/Category/Create`, category);
const getCategoryById = (id) =>
    axios.get(`${BASE_URL}/Category/Get/${id}`);
const updateCategory = (id, category) =>
    axios.put(`${BASE_URL}/Category/Update/${id}`, category);
const deleteCategoryApi = (id) =>
    axios.delete(`${BASE_URL}/Category/Delete/${id}`);

// Product
const getProducts = (pageSize, pageNumber) =>
    getAxios(`${BASE_URL}/Product/GetProducts`, {
        PageSize: pageSize,
        PageNumber: pageNumber,
    });

const getAllProducts = () =>
    axios.get(`${BASE_URL}/Product/GetAll`);
const addProductApi = (product) =>
    axios.post(`${BASE_URL}/Product/Create`, product);
const getProductById = (id) =>
    axios.get(`${BASE_URL}/Product/Read/${id}`);
const updateProductApi = (id, product) =>
    axios.put(`${BASE_URL}/Product/Update/${id}`, product);
const deleteProductApi = (id) =>
    axios.delete(`${BASE_URL}/Product/Delete/${id}`);

// Customer
const getCustomers = () => {
    const token = localStorage.getItem("token")
    console.log("getCustomers", token)
    return axios.get(`${BASE_URL}/Admin/GetAllUsers`, {
        headers: { "Authorization": `Bearer ${token}` },
    });
}

export {
    loginApi,
    logoutApi,

    getAllCategories,
    addCategory,
    getCategoryById,
    updateCategory,
    deleteCategoryApi,

    getProducts,
    getAllProducts,
    addProductApi,
    getProductById,
    updateProductApi,
    deleteProductApi,

    getCustomers
};