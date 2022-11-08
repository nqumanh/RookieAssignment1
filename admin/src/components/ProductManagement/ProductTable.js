import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { alpha } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import TableSortLabel from '@mui/material/TableSortLabel';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import Checkbox from '@mui/material/Checkbox';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import FormControlLabel from '@mui/material/FormControlLabel';
import Switch from '@mui/material/Switch';
import DeleteIcon from '@mui/icons-material/Delete';
import Stack from '@mui/material/Stack';
import { visuallyHidden } from '@mui/utils';
import { getAllProducts, addProductApi, updateProductApi, deleteProductApi, getAllCategories } from "../../apis/useApi"
import AddProduct from './AddProduct';
import EditProduct from './EditProduct';

function descendingComparator(a, b, orderBy) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}

function getComparator(order, orderBy) {
    return order === 'desc'
        ? (a, b) => descendingComparator(a, b, orderBy)
        : (a, b) => -descendingComparator(a, b, orderBy);
}

// This method is created for cross-browser compatibility, if you don't
// need to support IE11, you can use Array.prototype.sort() directly
function stableSort(array, comparator) {
    const stabilizedThis = array.map((el, index) => [el, index]);
    stabilizedThis.sort((a, b) => {
        const order = comparator(a[0], b[0]);
        if (order !== 0) {
            return order;
        }
        return a[1] - b[1];
    });
    return stabilizedThis.map((el) => el[0]);
}

const headCells = [
    {
        id: 'name',
        disablePadding: true,
        label: 'Name',
    },
    {
        id: 'author',
        disablePadding: true,
        label: 'Author',
    },
    {
        id: 'category',
        disablePadding: true,
        label: 'Category',
    },
    {
        id: 'description',
        disablePadding: true,
        label: 'Description',
    },
    {
        id: 'price',
        disablePadding: true,
        label: 'Price',
    },
    {
        id: 'image',
        disablePadding: true,
        label: 'Image',
    },
    {
        id: 'quantity',
        disablePadding: true,
        label: 'Quantity',
    },
    {
        id: 'createdDate',
        disablePadding: false,
        label: 'Created Date',
    },
    {
        id: 'updatedDate',
        disablePadding: false,
        label: 'Updated Date',
    }
];

function EnhancedTableHead(props) {
    const { onUncheck, order, orderBy, numSelected, onRequestSort } =
        props;
    const createSortHandler = (property) => (event) => {
        onRequestSort(event, property);
    };

    return (
        <TableHead>
            <TableRow>
                <TableCell padding="checkbox">
                    <Checkbox
                        color="primary"
                        indeterminate={numSelected > 0}
                        checked={numSelected > 0}
                        onChange={onUncheck}
                        inputProps={{
                            'aria-label': 'select all desserts',
                        }}
                    />
                </TableCell>
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align={'center'}
                        padding={headCell.disablePadding ? 'none' : 'normal'}
                        sortDirection={orderBy === headCell.id ? order : false}
                    >
                        <TableSortLabel
                            active={orderBy === headCell.id}
                            direction={orderBy === headCell.id ? order : 'asc'}
                            onClick={createSortHandler(headCell.id)}
                        >
                            {headCell.label}
                            {orderBy === headCell.id ? (
                                <Box component="span" sx={visuallyHidden}>
                                    {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                </Box>
                            ) : null}
                        </TableSortLabel>
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

EnhancedTableHead.propTypes = {
    numSelected: PropTypes.number.isRequired,
    onRequestSort: PropTypes.func.isRequired,
    onUncheck: PropTypes.func.isRequired,
    order: PropTypes.oneOf(['asc', 'desc']).isRequired,
    orderBy: PropTypes.string.isRequired,
    rowCount: PropTypes.number.isRequired,
};

function EnhancedTableToolbar(props) {
    const { selectedId } = props;

    const onDelete = () => {
        props.deleteProduct()
    }

    const onEdit = (category) => {
        props.editProduct(category)
    }

    return (
        <Toolbar
            sx={{
                pl: { sm: 2 },
                pr: { xs: 1, sm: 1 },
                ...(selectedId > 0 && {
                    bgcolor: (theme) =>
                        alpha(theme.palette.primary.main, theme.palette.action.activatedOpacity),
                }),
            }}
        >
            {selectedId > 0 ? (
                <Typography
                    sx={{ flex: '1 1 100%' }}
                    color="inherit"
                    variant="subtitle1"
                    component="div"
                >
                    1 selected
                </Typography>
            ) : (
                <Typography
                    sx={{ flex: '1 1 100%' }}
                    variant="h6"
                    id="tableTitle"
                    component="div"
                >
                    Product Management
                </Typography>
            )}

            {selectedId > 0 ? (
                <Box sx={{ display: "flex" }}>
                    <EditProduct editProduct={onEdit} selectedId={selectedId} />

                    <Tooltip title="Delete">
                        <IconButton onClick={onDelete}>
                            <DeleteIcon />
                        </IconButton>
                    </Tooltip>
                </Box>
            ) : ""}
        </Toolbar>
    );
}

EnhancedTableToolbar.propTypes = {
    selectedId: PropTypes.number.isRequired,
};

export default function EnhancedTable() {
    const [order, setOrder] = React.useState('asc');
    const [orderBy, setOrderBy] = React.useState('calories');
    const [selectedId, setSelectedId] = useState(-1)
    const [page, setPage] = React.useState(0);
    const [dense, setDense] = React.useState(false);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [rows, setRows] = useState([]);
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        getAllProducts().then((response) => {
            setRows(response.data)
        })

        getAllCategories().then((response) => {
            setCategories(response.data)
        })
    }, [])


    const handleRequestSort = (event, property) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    const handleUncheck = (event) => {
        setSelectedId(-1);
        return false
    };

    const handleClick = (event, id) => {
        setSelectedId(id)
    };

    const addProduct = (product) => {
        let productModel = {
            name: product.name,
            description: product.description,
            image: product.image,
            author: product.author,
            price: product.price,
            quantity: product.quantity,
            categoryId: (product.categoryId != null) ? product.categoryId.toString() : null,
        }
        addProductApi(productModel).then((response) => {
            setRows([
                {
                    id: response.data.id,
                    createdDate: response.data.createdDate,
                    updatedDate: response.data.updatedDate,
                    ...productModel
                },
                ...rows
            ])
            setPage(0)
        }).catch(function (response) {
            console.log(response)
            alert(response.response.data.errors.productDTO[0])
        });
    }

    // const readCategory = (category) => {
    // }

    const editProduct = (product) => {
        let productModel = {
            id: product.id,
            name: product.name,
            description: product.description,
            image: product.image,
            author: product.author,
            price: product.price,
            quantity: product.quantity,
            categoryId: (product.categoryId != null) ? product.categoryId.toString() : null
        }
        updateProductApi(selectedId, productModel).then((response) => {
            setRows(rows.map(row => (row.id === selectedId) ?
                { ...productModel, createdDate: response.data.createdDate, updatedDate: response.data.updatedDate } : row))
        })
    }

    const deleteProduct = () => {
        deleteProductApi(selectedId).then((response) => {
            setRows(rows.filter(row => row.id !== selectedId))
            setSelectedId(-1)
        })
    }

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleChangeDense = (event) => {
        setDense(event.target.checked);
    };

    // Avoid a layout jump when reaching the last page with empty rows.
    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    return (
        <Box sx={{ width: '100%', mt: 5 }}>
            <Paper sx={{ width: '100%', mb: 2 }}>
                <EnhancedTableToolbar
                    selectedId={selectedId}
                    deleteProduct={deleteProduct}
                    editProduct={editProduct} />
                <TableContainer>
                    <Table
                        sx={{ minWidth: 750 }}
                        aria-labelledby="tableTitle"
                        size={dense ? 'small' : 'medium'}
                    >
                        <EnhancedTableHead
                            numSelected={selectedId}
                            order={order}
                            orderBy={orderBy}
                            onUncheck={handleUncheck}
                            onRequestSort={handleRequestSort}
                            rowCount={rows.length}
                        />
                        <TableBody>
                            {/* if you don't need to support IE11, you can replace the `stableSort` call with:
                 rows.sort(getComparator(order, orderBy)).slice() */}
                            {stableSort(rows, getComparator(order, orderBy))
                                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                .map((row, index) => {
                                    const labelId = `enhanced-table-checkbox-${index}`;
                                    const categoryId = categories.findIndex(category => category.id === parseInt(row.categoryId))
                                    let category = "";
                                    if (categoryId >= 0) category = categories[categoryId].name
                                    return (
                                        <TableRow
                                            hover
                                            onClick={(event) => handleClick(event, row.id)}
                                            role="checkbox"
                                            tabIndex={-1}
                                            key={row.id}
                                            selected={selectedId === row.id}
                                        >
                                            {/* aria-checked={isItemSelected} */}
                                            <TableCell padding="checkbox">
                                                <Checkbox
                                                    color="primary"
                                                    checked={selectedId === row.id}
                                                    inputProps={{
                                                        'aria-labelledby': labelId,
                                                    }}
                                                />
                                            </TableCell>
                                            <TableCell
                                                component="th"
                                                id={labelId}
                                                scope="row"
                                                padding="none"
                                                align="center"
                                                sx={{ minWidth: "200px" }}
                                            >
                                                {row.name}
                                            </TableCell>
                                            <TableCell align="center">{row.author}</TableCell>
                                            <TableCell align="center">{category}</TableCell>
                                            <TableCell align="center">{row.description}</TableCell>
                                            <TableCell align="center">{row.price}</TableCell>
                                            <TableCell align="center">{row.image}</TableCell>
                                            <TableCell align="center">{row.quantity}</TableCell>
                                            <TableCell align="center">{row.createdDate}</TableCell>
                                            <TableCell align="center">{row.updatedDate}</TableCell>
                                        </TableRow>
                                    );
                                })}
                            {emptyRows > 0 && (
                                <TableRow
                                    style={{
                                        height: (dense ? 33 : 53) * emptyRows,
                                    }}
                                >
                                    <TableCell colSpan={12} />
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
                <Stack direction="row" spacing={2} sx={{ justifyContent: "space-between" }}>
                    <AddProduct addProduct={addProduct} />
                    <TablePagination
                        rowsPerPageOptions={[5, 10, 25]}
                        component="div"
                        count={rows.length}
                        rowsPerPage={rowsPerPage}
                        page={page}
                        onPageChange={handleChangePage}
                        onRowsPerPageChange={handleChangeRowsPerPage}
                        sx={{ display: "flex" }}
                    />
                </Stack>
            </Paper >
            <FormControlLabel
                control={<Switch checked={dense} onChange={handleChangeDense} />}
                label="Dense padding"
            />
        </Box >
    );
}
