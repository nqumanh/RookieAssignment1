import { Box, Container, Typography } from "@mui/material";
import CategoryManagement from "components/CategoryManagement";

const ManageCategory = () => {
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
                    Categories
                </Typography>
            </Box>
            <Container maxWidth={false}>
                <Box sx={{ mt: 3 }}>
                    <CategoryManagement />
                </Box>
            </Container>
        </Box>
    )
}

export default ManageCategory;