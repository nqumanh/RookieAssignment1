import { Box, Container } from "@mui/material";
import CustomTable from "components/Table/Table";
import { ToolbarTable } from "components/Table/ToolbarTable";
import { getProducts } from 'services';

const fields = ["name", "categoryName", "description", "price", "image", "createdDate", "updatedDate"]
const headers = ["Name", "Category", "Description", "Price", "Images", "Created Date", "Updated Date"]

const ManageProduct = () => {
    return (
        <div>
            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    py: 8
                }}
            >
                <Container maxWidth={false}>
                    <ToolbarTable title={"Product"} />
                    <Box sx={{ mt: 3 }}>
                        <CustomTable getData={getProducts} fields={fields} headers={headers} />
                    </Box>
                </Container>
            </Box>
        </div>
    )
}

export default ManageProduct;