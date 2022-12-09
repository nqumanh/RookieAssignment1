import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { FormControl, InputLabel, MenuItem, Select, Stack } from '@mui/material';
import PaginationControlled from './Pagination';
import { StyledTableCell, StyledTableRow } from './Table.styled';
import { useEffect } from 'react';
import { useState } from 'react';

const CustomTable = ({ getData, fields, headers }) => {
    const [currentPage, setCurrentPage] = useState(1);
    const [rows, setRows] = useState([])
    const [totalItem, setTotalItem] = useState(5);
    const [rowsPerPage, setRowPerPage] = useState(5);

    const handleChange = (event) => {
        setRowPerPage(event.target.value);
    };

    useEffect(() => {
        getData(rowsPerPage, currentPage).then((response) => {
            console.log(response.data)
            setTotalItem(response.data.totalItems)
            setRows(response.data.items)
        }
        ).catch((error) => console.log(error))
    }, [currentPage, getData, rowsPerPage])

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 700 }} aria-label="customized table">
                <TableHead>
                    <TableRow>
                        {headers.map((header, index) => (
                            <StyledTableCell key={index}>{header}</StyledTableCell>
                        ))}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <StyledTableRow key={row.id}>
                            {fields.map((field) => (
                                <StyledTableCell key={field} align="center">
                                    {row[field]}
                                </StyledTableCell>
                            ))}
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
            <Stack spacing={2} alignItems="end">
                <FormControl sx={{ m: 1, minWidth: 120 }}>
                    <InputLabel id="rows-per-page">Rows Per Page</InputLabel>
                    <Select
                        labelId="rows-per-page"
                        id="row-per-page"
                        value={rowsPerPage}
                        label="Rows Per Page"
                        onChange={handleChange}
                    >
                        <MenuItem value={5}>5</MenuItem>
                        <MenuItem value={10}>10</MenuItem>
                        <MenuItem value={15}>15</MenuItem>
                    </Select>
                </FormControl>
                <PaginationControlled totalPage={Math.ceil(totalItem / rowsPerPage)} currentPage={currentPage} setCurrentPage={setCurrentPage} />
            </Stack>
        </TableContainer>
    )
}

export default CustomTable;