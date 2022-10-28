import axios from "axios";

export default async function fetchCategories() {
    axios.get("https://localhost:7133/Category/GetAllCategories")
        .then((response) => {
            return response.data;
        })
    return [];
}