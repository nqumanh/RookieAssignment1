import React, { useState, useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Paper } from '@mui/material';
import { getCustomers } from '../../apis/useApi';
import './CustomerTable.css'

const columns = [
    { field: 'name', headerName: 'Name', flex: 1 },
    { field: 'userName', headerName: 'Username', flex: 1 },
    {
        field: 'email',
        headerName: 'Email'
        , flex: 1
    },
    { field: 'phoneNumber', headerName: 'Phone Number', flex: 1 },
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
        <Paper sx={{ width: '100%', mb: 2, height: 400, mt: 5 }}>
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
