import * as React from 'react';
import Pagination from '@mui/material/Pagination';

export default function PaginationControlled(props) {
    const { page, setPage } = props

    const handleChange = (event, value) => {
        setPage(value);
    };

    return (
        <Pagination count={10} page={page} onChange={handleChange} />
    );
}