import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Stack } from '@mui/material';
import PaginationControlled from './Pagination';
import { StyledTableCell, StyledTableRow } from './Table.styled';
import { useEffect } from 'react';
import { getProducts } from 'apis/useApi';
import { useState } from 'react';

const PaginationBackend = () => {
    const [page, setPage] = useState(1);
    const [rows, setRows] = useState([])

    useEffect(() => {
        getProducts().then((response) =>
            setRows(response.data)
        ).catch((error) => console.log(error))
    }, [])

    const fields = ["name", "categoryName", "description", "price", "images", "createdDate", "updatedDate"]
    const headers = ["Name", "Category", "Description", "Price", "Images", "Created Date", "Updated Date"]

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
            <Stack spacing={2}>
                <PaginationControlled page={page} setPage={setPage} />
            </Stack>
        </TableContainer>
    )
}

export default PaginationBackend;