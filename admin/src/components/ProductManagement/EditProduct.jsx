import React, { useState } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import CloseIcon from '@mui/icons-material/Close';
import Slide from '@mui/material/Slide';
import EditIcon from '@mui/icons-material/Edit';
import { DialogContent, DialogContentText, FormControl, InputLabel, MenuItem, Select, Tooltip } from "@mui/material";
import { getAllCategories, getProductById } from "../../apis/useApi";
import { useEffect } from "react";

const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
});

export default function AddProduct(props) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");
    const [author, setAuthor] = useState("");
    const [categoryId, setCategoryId] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [image, setImage] = useState("");
    const [quantity, setQuantity] = useState("");
    const [categoryList, setCategoryList] = useState([])
    const { editProduct, selectedId } = props

    useEffect(() => {
        getAllCategories().then((response) => {
            setCategoryList(response.data)
        })
    }, [])

    useEffect(() => {
        getProductById(selectedId).then((response) => {
            setName(response.data.name)
            setAuthor(response.data.author)
            setCategoryId(response.data.categoryId == null ? "" : response.data.categoryId)
            setDescription(response.data.description)
            setPrice(response.data.price)
            setImage(response.data.image)
            setQuantity(response.data.quantity)
        })
    }, [selectedId, open])

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleClear = () => {
        setName("")
        setAuthor("")
        setCategoryId("")
        setDescription("")
        setPrice("")
        setImage("")
        setQuantity("")
    };

    const handleSubmit = () => {
        editProduct({
            id: selectedId,
            name: name,
            author: author,
            categoryId: categoryId,
            description: description,
            price: price,
            image: image,
            quantity: quantity
        });
        setOpen(false);
        handleClear();
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

    return <>
        <Tooltip title="Edit">
            <IconButton onClick={handleClickOpen}>
                <EditIcon />
            </IconButton>
        </Tooltip>

        <Dialog
            fullScreen
            open={open}
            onClose={handleClose}
            TransitionComponent={Transition}
        >
            <AppBar sx={{ position: 'relative' }}>
                <Toolbar>
                    <IconButton
                        edge="start"
                        color="inherit"
                        onClick={handleClose}
                        aria-label="close"
                    >
                        <CloseIcon />
                    </IconButton>
                    <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
                        Edit Product
                    </Typography>
                    <div>
                        <Button autoFocus color="inherit" onClick={handleClear}>
                            clear
                        </Button>
                        <Button autoFocus color="inherit" onClick={handleSubmit}>
                            submit
                        </Button>
                    </div>
                </Toolbar>
            </AppBar>
            <DialogContent>
                <DialogContentText>
                    Edit your Product category and necessary information from here
                </DialogContentText>
                <TextField
                    id="outlined-textarea"
                    label="Product Name"
                    placeholder="Name"
                    multiline
                    fullWidth
                    sx={{ margin: "30px 0" }}
                    onChange={handleChange}
                    name="name"
                    value={name}
                />
                <TextField
                    id="outlined-textarea"
                    label="Author"
                    placeholder="Author"
                    multiline
                    fullWidth
                    sx={{ margin: "30px 0" }}
                    onChange={handleChange}
                    name="author"
                    value={author}
                />
                <FormControl fullWidth sx={{ margin: "30px 0" }}>
                    <InputLabel id="demo-simple-select-label">Category</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        name="category"
                        value={categoryId}
                        label="Category"
                        onChange={handleChange}
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
                    onChange={handleChange}
                    name="description"
                    value={description}
                />
                <TextField
                    id="outlined-textarea"
                    label="Price"
                    multiline
                    fullWidth
                    sx={{ margin: "30px 0" }}
                    onChange={handleChange}
                    name="price"
                    value={price}
                />
                <TextField
                    id="outlined-textarea"
                    label="Image"
                    multiline
                    fullWidth
                    sx={{ margin: "30px 0" }}
                    onChange={handleChange}
                    name="image"
                    value={image}
                />
                <TextField
                    id="outlined-textarea"
                    label="Quantity"
                    multiline
                    fullWidth
                    sx={{ margin: "30px 0" }}
                    onChange={handleChange}
                    name="quantity"
                    value={quantity}
                />
            </DialogContent>
        </Dialog>
    </>
}