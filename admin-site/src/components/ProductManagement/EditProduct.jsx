import React, { useState } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { Box, Tooltip } from "@mui/material";
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import { useEffect } from "react";
import { getCategoryById } from "../../apis/useApi";

export default function EditCategory(props) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const { editCategory, selectedId } = props

    useEffect(() => {
        getCategoryById(selectedId).then((response) => {
            setName(response.data.name)
            setDescription(response.data.description)
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
        setDescription("")
    };

    const handleSubmit = () => {
        editCategory({ id: selectedId, name: name, description: description });
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
        <Tooltip title="Edit">
            <IconButton onClick={handleClickOpen}>
                <EditIcon />
            </IconButton>
        </Tooltip>
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