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
import { DialogContent, DialogContentText, FormControl, InputLabel, MenuItem, Select } from "@mui/material";

const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
});

export default function AddCategory(props) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");
    const [categoryId, setCategoryId] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [image, setImage] = useState("");
    const { addCategory } = props

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleClear = () => {
        setName("")
        setCategoryId("")
        setDescription("")
        setPrice("")
        setImage("")
    };

    const handleSubmit = () => {
        addCategory({ name: name, description: description });
        setOpen(false);
        handleClear();
    };

    const handleChange = (e) => {
        const { name, value } = e.target
        if (name === "name")
            setName(value)
        else if (name === "category")
            setCategoryId(value)
        else if (name === "description")
            setDescription(value)
        else if (name === "price")
            setPrice(value)
        else
            setImage(value)
    }

    return <>
        <Button size="small" variant="contained" color="success" sx={{ margin: "20px 20px", padding: "10px 40px" }} onClick={handleClickOpen}>
            Add Product +
        </Button>

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
                        Add Product
                    </Typography>
                    <div>
                        <Button autoFocus color="inherit" onClick={handleClear}>
                            clear
                        </Button>
                        <Button autoFocus color="inherit" onClick={handleSubmit}>
                            save
                        </Button>
                    </div>
                </Toolbar>
            </AppBar>
            <DialogContent>
                <DialogContentText>
                    Add your Product category and necessary information from here
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
                        <MenuItem value={10}>Ten</MenuItem>
                        <MenuItem value={20}>Twenty</MenuItem>
                        <MenuItem value={30}>Thirty</MenuItem>
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
            </DialogContent>
        </Dialog>
    </>
}