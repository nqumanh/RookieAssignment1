import { Box, Container, Typography } from "@mui/material";
import DataTable from "components/ViewCustomer";

const ManageCustomer = () => {
    return (
        <Box
            component="main"
            sx={{
                flexGrow: 1,
                py: 8
            }}
        >
            <Box
                sx={{
                    alignItems: 'center',
                    display: 'flex',
                    justifyContent: 'space-between',
                    flexWrap: 'wrap',
                    m: -1
                }}
            >
                <Typography
                    sx={{ m: 1 }}
                    variant="h4"
                >
                    Customers
                </Typography>
            </Box>
            <Container maxWidth={false}>
                <Box sx={{ mt: 3 }}>
                    <DataTable />
                </Box>
            </Container>
        </Box>
    )
}

export default ManageCustomer;