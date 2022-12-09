import * as React from 'react';
import Pagination from '@mui/material/Pagination';

export default function PaginationControlled({ totalPage, setCurrentPage, currentPage }) {

    const handleChange = (event, value) => {
        setCurrentPage(value);
    };

    return (
        <Pagination count={totalPage} page={currentPage} onChange={handleChange} />
    );
}