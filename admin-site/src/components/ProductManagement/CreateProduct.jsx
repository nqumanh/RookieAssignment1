import React, { useState } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { Box } from "@mui/material";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';

export default function CreateProduct(props) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");
    const [category, setCategory] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [image, setImage] = useState("");
    const [quantity, setQuantity] = useState("");
    const { createProduct } = props

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleClear = () => {
        setName("")
        setCategory("")
        setDescription("")
        setPrice("")
        setImage("")
        setQuantity("")
    };

    const handleSubmit = () => {
        createProduct({ name: name, category: category, description: description, price: price, image: image, quantity: quantity });
        setOpen(false);
        handleClear();
    };

    const handleChange = (e) => {
        const { name, value } = e.target
        if (name === "name") {
            setName(value)
        }
        else if (name === "category") {
            setCategory(value)
        }
        else if (name === "description") {
            setDescription(value)
        }
        else if (name === "price") {
            setPrice(value)
        }
        else if (name === "image") {
            setImage(value)
        }
        else {
            setQuantity(value)
        }
    }

    return <>
        <Button size="small" variant="contained" color="success" sx={{ margin: "20px 20px", padding: "10px 40px" }} onClick={handleClickOpen}>
            Add Product +
        </Button>
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Add Product</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Add your product and necessary information from here
                </DialogContentText>
                <TextField
                    id="outlined-textarea"
                    label="Product Name"
                    placeholder="Name"
                    multiline
                    fullWidth
                    name="name"
                    sx={{ margin: "30px 0" }}
                    value={name}
                    onChange={handleChange}
                />
                <FormControl fullWidth sx={{ marginBottom: "30px" }}>
                    <InputLabel id="demo-simple-select-label">Category</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        label="Category"
                        name="category"
                        value={category}
                        onChange={handleChange}
                    >
                        <MenuItem value={"a"}>Ten</MenuItem>
                        <MenuItem value={"b"}>Twenty</MenuItem>
                        <MenuItem value={"c"}>Thirty</MenuItem>
                    </Select>
                </FormControl>
                <TextField
                    id="outlined-multiline-static"
                    label="Description"
                    multiline
                    fullWidth
                    rows={4}
                    name="description"
                    value={description}
                    onChange={handleChange}
                />
                <TextField
                    id="outlined-textarea"
                    label="Price"
                    placeholder="Price"
                    multiline
                    fullWidth
                    name="price"
                    sx={{ margin: "30px 0" }}
                    value={price}
                    onChange={handleChange}
                />
                <TextField
                    id="outlined-textarea"
                    label="Image"
                    placeholder="Image"
                    multiline
                    fullWidth
                    name="image"
                    sx={{ margin: "30px 0" }}
                    value={image}
                    onChange={handleChange}
                />
                <TextField
                    id="outlined-textarea"
                    label="Quantity"
                    placeholder="Quantity"
                    multiline
                    fullWidth
                    name="quantity"
                    sx={{ margin: "30px 0" }}
                    value={quantity}
                    onChange={handleChange}
                />
            </DialogContent>
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                <DialogActions>
                    <Button onClick={handleClear}>Clear</Button>
                </DialogActions>

                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={handleSubmit}>Submit</Button>
                </DialogActions>
            </Box>
        </Dialog>
    </>
}