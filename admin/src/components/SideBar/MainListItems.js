import React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import PeopleIcon from '@mui/icons-material/People';
import CategoryIcon from '@mui/icons-material/Category';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import { Link } from 'react-router-dom';


export const mainListItems = (
    <React.Fragment>
        <ListSubheader component="div" inset>
            Management
        </ListSubheader>
        <Link style={{ textDecoration: 'none', color: "#000" }} to="/dashboard/categories">
            <ListItemButton>
                <ListItemIcon>
                    <CategoryIcon />
                </ListItemIcon>
                <ListItemText primary="Categories" />
            </ListItemButton>
        </Link>
        <Link style={{ textDecoration: 'none', color: "#000" }} to="/dashboard/products">
            <ListItemButton>
                <ListItemIcon>
                    <MenuBookIcon />
                </ListItemIcon>
                <ListItemText primary="Products" />
            </ListItemButton>
        </Link>
        <Link style={{ textDecoration: 'none', color: "#000" }} to="/dashboard/customers">
            <ListItemButton>
                <ListItemIcon>
                    <PeopleIcon />
                </ListItemIcon>
                <ListItemText primary="Customers" />
            </ListItemButton>
        </Link>
        {/* <Link style={{ textDecoration: 'none', color: "#000" }} to="/dashboard/orders">
            <ListItemButton>
                <ListItemIcon>
                    <ShoppingCartIcon />
                </ListItemIcon>
                <ListItemText primary="Orders" />
            </ListItemButton>
        </Link> */}
    </React.Fragment>
);