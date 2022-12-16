import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Box, FormControl, IconButton, InputBase, MenuItem, Select, Stack, Typography } from '@mui/material';
import PaginationControlled from './Pagination';
import { StyledTableCell, StyledTableRow } from './Table.styled';
import { useEffect } from 'react';
import { useState } from 'react';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { styled } from '@mui/system';
import { GetDateDMY } from 'utils';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

const BootstrapInput = styled(InputBase)(({ theme }) => ({
    'label + &': {
        marginTop: theme.spacing(3),
    },
    '& .MuiInputBase-input': {
        borderRadius: 4,
        position: 'relative',
        backgroundColor: theme.palette.background.paper,
        fontSize: 16,
        padding: '10px 26px 10px 12px',
        transition: theme.transitions.create(['border-color', 'box-shadow']),
        // Use the system font instead of the default Roboto font.
        fontFamily: [
            '-apple-system',
            'BlinkMacSystemFont',
            '"Segoe UI"',
            'Roboto',
            '"Helvetica Neue"',
            'Arial',
            'sans-serif',
            '"Apple Color Emoji"',
            '"Segoe UI Emoji"',
            '"Segoe UI Symbol"',
        ].join(','),
        '&:focus': {
            borderRadius: 4,
            borderColor: '#80bdff',
            boxShadow: '0 0 0 0.2rem rgba(0,123,255,.25)',
        },
    },
}));

const CustomTable = ({ getData, fields, headers }) => {
    const [currentPage, setCurrentPage] = useState(1);
    const [rows, setRows] = useState([])
    const [totalItem, setTotalItem] = useState(5);
    const [rowsPerPage, setRowPerPage] = useState(5);
    const [selectedProduct, setSelectedProduct] = useState(null)

    const handleChange = (event) => {
        setRowPerPage(event.target.value);
    };

    const [open, setOpen] = React.useState(false);

    const handleClickOpen = (id) => {
        console.log(id)
        setSelectedProduct(id)
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setSelectedProduct(null)

    };

    const deleteProduct = () => {
        setOpen(false);
        setSelectedProduct(null)
    };

    useEffect(() => {
        getData(rowsPerPage, currentPage).then((response) => {
            setTotalItem(response.data.totalItems)
            let rows = response.data.items.map((row) =>
                ({ ...row, createdDate: GetDateDMY(new Date(row.createdDate)), updatedDate: GetDateDMY(new Date(row.updatedDate)) }))
            setRows(rows)
        }
        ).catch((error) => console.log(error))
    }, [currentPage, getData, rowsPerPage])

    return (
        <Box>
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                    {"Delete This Product?"}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        Are you sure to delete this product?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Disagree</Button>
                    <Button onClick={() => deleteProduct()} autoFocus>
                        Agree
                    </Button>
                </DialogActions>
            </Dialog>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 700 }} aria-label="customized table">
                    <TableHead>
                        <TableRow>
                            {headers.map((header, index) => (
                                <StyledTableCell align="left" key={index}>{header}</StyledTableCell>
                            ))}
                            <StyledTableCell></StyledTableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row) => (
                            <StyledTableRow key={row.id}>
                                {fields.map((field) => (
                                    <StyledTableCell key={field} align="left">
                                        {row[field]}
                                    </StyledTableCell>

                                ))}
                                <StyledTableCell>
                                    <Stack direction="row">
                                        <IconButton>
                                            <EditIcon />
                                        </IconButton>
                                        <IconButton onClick={() => handleClickOpen(row.id)} >
                                            <DeleteIcon />
                                        </IconButton>
                                    </Stack>
                                </StyledTableCell>
                            </StyledTableRow>
                        ))}
                    </TableBody>
                </Table>
                <Stack direction="row" spacing={2} alignItems="center" justifyContent="end">
                    <Stack direction="row" spacing={2} alignItems="center">
                        <Typography>Rows per page:</Typography>
                        <FormControl sx={{ m: 1 }}>
                            <Select
                                labelId="demo-customized-select-label"
                                id="demo-customized-select"
                                value={rowsPerPage}
                                onChange={handleChange}
                                input={<BootstrapInput />}
                            >
                                <MenuItem value={5}>5</MenuItem>
                                <MenuItem value={10}>10</MenuItem>
                                <MenuItem value={15}>15</MenuItem>
                            </Select>
                        </FormControl>
                    </Stack>
                    <PaginationControlled
                        totalPage={Math.ceil(totalItem / rowsPerPage)}
                        currentPage={currentPage}
                        setCurrentPage={setCurrentPage} />
                </Stack>
            </TableContainer>
        </Box>
    )
}

export default CustomTable;