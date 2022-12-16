import { Box, Card, CardContent, Container, Stack, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import Alert from '@mui/material/Alert';
import { useForm } from "react-hook-form";
import { getAllCategories } from "services";
import { useNavigate } from "react-router-dom";

const CreateProduct = (props) => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const [name, setName] = useState("");
    const [author, setAuthor] = useState("");
    const [categoryId, setCategoryId] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [image, setImage] = useState("");
    const [quantity, setQuantity] = useState("");
    const [categoryList, setCategoryList] = useState([])
    const { addProduct } = props
    const navigate = useNavigate()

    useEffect(() => {
        getAllCategories().then((response) => {
            setCategoryList(response.data)
        })
    }, [])

    const handleCancel = () => {
        navigate("/")
    };

    const onSubmit = (data) => {
        addProduct(data);
    };

    const handleChange = (e) => {
        const { name, value } = e.target
        if (name === "name")
            setName(value)
        else if (name === "author")
            setAuthor(value)
        else if (name === "category")
            setCategoryId(value)
        else if (name === "description")
            setDescription(value)
        else if (name === "price")
            setPrice(value)
        else if (name === "quantity")
            setQuantity(value)
        else
            setImage(value)
    }

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
                            Create Product
                        </Typography>
                    </Box>
                    <Box sx={{ mt: 3 }}>
                        <Card>
                            <CardContent>
                                <form onSubmit={handleSubmit(onSubmit)}>
                                    {/* <UploadImage /> */}
                                    <TextField
                                        id="outlined-textarea"
                                        label="Product Name"
                                        placeholder="Name"
                                        multiline
                                        fullWidth
                                        sx={{ marginTop: "30px" }}
                                        name="name"
                                        value={name}
                                        {...register("name", { required: true, onChange: handleChange })}
                                    />
                                    {errors.name && <Alert severity="error">Product name is required</Alert>}
                                    <TextField
                                        id="outlined-textarea"
                                        label="Author"
                                        placeholder="Author"
                                        multiline
                                        fullWidth
                                        sx={{ margin: "30px 0" }}
                                        name="author"
                                        value={author}
                                        {...register("author", { onChange: handleChange })}
                                    />
                                    <FormControl fullWidth sx={{ margin: "30px 0" }}>
                                        <InputLabel id="demo-simple-select-label">Category</InputLabel>
                                        <Select
                                            labelId="demo-simple-select-label"
                                            id="demo-simple-select"
                                            name="category"
                                            value={categoryId}
                                            label="Category"
                                            {...register("category", { onChange: handleChange })}
                                        >
                                            <MenuItem value={""}>None</MenuItem>
                                            {categoryList.map((category, index) =>
                                                <MenuItem key={category.id} value={category.id}>{category.name}</MenuItem>
                                            )}
                                        </Select>
                                    </FormControl>
                                    <TextField
                                        id="outlined-multiline-static"
                                        label="Description"
                                        multiline
                                        fullWidth
                                        sx={{ margin: "30px 0" }}
                                        rows={4}
                                        name="description"
                                        value={description}
                                        {...register("description", { onChange: handleChange })}
                                    />
                                    <TextField
                                        id="outlined-number"
                                        type="number"
                                        label="Price"
                                        fullWidth
                                        name="price"
                                        value={price}
                                        {...register("price", { required: true, min: 0, onChange: handleChange })}
                                    />
                                    {errors.price && <Alert severity="error">Product price is required and non-negative</Alert>}

                                    <TextField
                                        id="outlined-textarea"
                                        label="Image"
                                        multiline
                                        fullWidth
                                        sx={{ margin: "30px 0" }}
                                        name="image"
                                        value={image}
                                        {...register("image", { onChange: handleChange })}
                                    />
                                    <TextField
                                        id="outlined-number"
                                        label="Quantity"
                                        type="number"
                                        fullWidth
                                        sx={{ marginTop: "30px" }}
                                        name="quantity"
                                        value={quantity}
                                        {...register("quantity", { required: true, min: 0, onChange: handleChange })}
                                    />
                                    {errors.quantity && <Alert severity="error">Product quantity is required and non-negative</Alert>}
                                    <Stack direction="row" justifyContent="end" sx={{ mt: 3 }} spacing={2}>
                                        <Button
                                            type="submit"
                                            color="primary"
                                            variant="contained"
                                        >
                                            Create
                                        </Button>
                                        <Button type="submit"
                                            color="primary"
                                            variant="contained"
                                            onClick={() => handleCancel()}>
                                            Cancel
                                        </Button>
                                    </Stack>
                                </form>
                            </CardContent>
                        </Card>
                    </Box>
                </Container>
            </Box>
        </div>
    )
}

export default CreateProduct;