import CustomTable from "components/Table/Table";
import { getProducts } from 'services';

const fields = ["name", "categoryName", "description", "price", "image", "createdDate", "updatedDate"]
const headers = ["Name", "Category", "Description", "Price", "Images", "Created Date", "Updated Date"]

const ManageProduct = () => {
    return (
        <div>
            <h1>Product</h1>
            <CustomTable getData={getProducts} fields={fields} headers={headers} />
        </div>
    )
}

export default ManageProduct;