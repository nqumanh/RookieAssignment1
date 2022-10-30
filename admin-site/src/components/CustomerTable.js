import React, { useState, useEffect } from 'react';
import { getCustomers } from '../apis/useApi';
import { DataGrid } from '@mui/x-data-grid';
import { Paper } from '@mui/material';
import './CustomerTable.css'

const columns = [
    { field: 'name', headerName: 'Name', flex: 1 },
    { field: 'username', headerName: 'Username', flex: 1 },
    {
        field: 'email',
        headerName: 'Email'
        , flex: 1
    },
    { field: 'address', headerName: 'Address', flex: 1 },
];
// name,username, email, address

export default function DataTable() {
    const [rows, setRows] = useState([])
    useEffect(() => {
        getCustomers().then((response) => {
            setRows(response.data)
        })
    }, [])

    return (
        <Paper sx={{ width: '100%', mb: 2, height: 400 }}>
            <DataGrid
                rows={rows}
                columns={columns}
                pageSize={5}
                rowsPerPageOptions={[5]}
                hideFooterSelectedRowCount
            />
        </Paper>
    );
}
