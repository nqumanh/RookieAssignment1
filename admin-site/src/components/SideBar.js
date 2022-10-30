import * as React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import PeopleIcon from '@mui/icons-material/People';
import CategoryIcon from '@mui/icons-material/Category';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import LogoutIcon from '@mui/icons-material/Logout';
// import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
// import SettingsIcon from '@mui/icons-material/Settings';
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

export const secondaryListItems = (
    <React.Fragment>
        <ListSubheader component="div" inset>
            Account
        </ListSubheader>
        {/* <Link style={{ textDecoration: 'none', color: "#000" }} to="/dashboard/setting">
            <ListItemButton>
                <ListItemIcon>
                    <SettingsIcon />
                </ListItemIcon>
                <ListItemText primary="Settings" />
            </ListItemButton>
        </Link> */}
        <Link style={{ textDecoration: 'none', color: "#000" }} to="/">
            <ListItemButton>
                <ListItemIcon>
                    <LogoutIcon />
                </ListItemIcon>
                <ListItemText primary="Logout" />
            </ListItemButton>
        </Link>
    </React.Fragment>
);