import {
    Box,
    Button,
    Card,
    CardContent,
    TextField,
    InputAdornment,
    SvgIcon, Typography, FormControl, InputLabel, Select, MenuItem, Stack
} from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
// import UploadIcon from '@mui/icons-material/Upload';
import DownloadIcon from '@mui/icons-material/Download';
import { useState } from 'react';
import { useEffect } from 'react';
import { getAllCategories } from 'services';
import { useNavigate } from 'react-router-dom';

export const ToolbarTable = (props) => {
    const { title } = props

    const navigate = useNavigate()

    const [categoryFilter, setCategoryFilter] = useState('');
    const [categories, setCategories] = useState([]);

    const handleChange = (event) => {
        setCategoryFilter(event.target.value);
    };

    useEffect(() => {
        getAllCategories().then((res) => {
            setCategories(res.data)
        }).catch((err) => console.log(err))
    }, [])

    return (
        <Box {...props}>
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
                    {title} List
                </Typography>
                <Box sx={{ m: 1 }}>
                    <Button
                        startIcon={(<DownloadIcon fontSize="small" />)}
                        sx={{ mr: 1 }}
                    >
                        Export
                    </Button>
                    <Button
                        color="primary"
                        variant="contained"
                        onClick={() => navigate("/create-product")}
                    >
                        Add {title}
                    </Button>
                </Box>
            </Box>
            <Box sx={{ mt: 3 }}>
                <Card>
                    <CardContent>
                        <Stack direction="row" justifyContent="space-between">
                            <Box sx={{ minWidth: 120 }}>
                                <FormControl fullWidth>
                                    <InputLabel id="demo-simple-select-label">Category</InputLabel>
                                    <Select
                                        labelId="demo-simple-select-label"
                                        id="demo-simple-select"
                                        value={categoryFilter}
                                        label="Category"
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={'All'}>All Categories</MenuItem>
                                        {categories.map((category, index) =>
                                            <MenuItem key={index} value={category.name}>{category.name}</MenuItem>
                                        )}
                                    </Select>
                                </FormControl>
                            </Box>
                            <Box sx={{ maxWidth: 500 }}>
                                <TextField
                                    fullWidth
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start">
                                                <SvgIcon
                                                    color="action"
                                                    fontSize="small"
                                                >
                                                    <SearchIcon />
                                                </SvgIcon>
                                            </InputAdornment>
                                        )
                                    }}
                                    placeholder={`Search ${title}`}
                                    variant="outlined"
                                />
                            </Box>
                        </Stack>
                    </CardContent>
                </Card>
            </Box>
        </Box>
    )
};
