import React, { useState } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { Box } from "@mui/material";
import { useForm } from "react-hook-form";
import Alert from '@mui/material/Alert';

export default function AddCategory(props) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const { addCategory } = props

    const { register, handleSubmit, formState: { errors } } = useForm();

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

    const onSubmit = (data) => {
        addCategory(data);
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
            <form onSubmit={handleSubmit(onSubmit)}>
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
                        sx={{ marginTop: "30px" }}
                        name="name"
                        value={name}
                        {...register("name", { required: true, onChange: handleOnchange })}

                    />
                    {errors.name && <Alert severity="error">Name of category is required</Alert>}
                    <TextField
                        id="outlined-multiline-static"
                        label="Description"
                        multiline
                        sx={{ marginTop: "30px" }}
                        fullWidth
                        rows={4}
                        name="description"
                        value={description}
                        {...register("description", { onChange: handleOnchange })}
                    />
                </DialogContent>
                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                    <DialogActions>
                        <Button onClick={handleClear}>Clear</Button>
                    </DialogActions>

                    <DialogActions>
                        <Button onClick={handleClose}>Cancel</Button>
                        <Button type="submit">Submit</Button>
                    </DialogActions>
                </Box>
            </form>
        </Dialog>
    </>
}