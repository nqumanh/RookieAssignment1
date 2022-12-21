import React, { useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Paper } from '@mui/material';
import './CustomerTable.css'
import { useDispatch, useSelector } from 'react-redux';
import { fetchUsers } from 'features/user/userSlice';

const columns = [
    { field: 'name', headerName: 'Name', flex: 1 },
    { field: 'userName', headerName: 'Username', flex: 1 },
    {
        field: 'email',
        headerName: 'Email',
        flex: 1
    },
    { field: 'phoneNumber', headerName: 'Phone Number', flex: 1 },
    { field: 'address', headerName: 'Address', flex: 1 },
];

export default function DataTable() {
    const dispatch = useDispatch()
    const users = useSelector(state => state.user.users)

    useEffect(() => {
        dispatch(fetchUsers())
    }, [dispatch])

    return (
        <Paper sx={{ width: '100%', mb: 2, height: 400, mt: 5 }}>
            <DataGrid
                rows={users}
                columns={columns}
                pageSize={5}
                rowsPerPageOptions={[5]}
                hideFooterSelectedRowCount
            />
        </Paper>
    );
}
