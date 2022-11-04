import React, { useState } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { Box } from "@mui/material";

export default function AddCategory(props) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const { addCategory } = props

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleClear = () => {
        setName("")
        setDescription("")
    };

    const handleSubmit = () => {
        addCategory({ name: name, description: description });
        setOpen(false);
        handleClear();
    };

    const handleOnchange = (e) => {
        const { name, value } = e.target
        if (name === "name") {
            setName(value)
        } else {
            setDescription(value)
        }
    }

    return <>
        <Button size="small" variant="contained" color="success" sx={{ margin: "20px 20px", padding: "10px 40px" }} onClick={handleClickOpen}>
            Add Category +
        </Button>
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Add Category</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Add your Product category and necessary information from here
                </DialogContentText>
                <TextField
                    id="outlined-textarea"
                    label="Category"
                    placeholder="Name"
                    multiline
                    fullWidth
                    sx={{ margin: "30px 0" }}
                    onChange={handleOnchange}
                    name="name"
                    value={name}
                />
                <TextField
                    id="outlined-multiline-static"
                    label="Description"
                    multiline
                    fullWidth
                    rows={4}
                    onChange={handleOnchange}
                    name="description"
                    value={description}
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