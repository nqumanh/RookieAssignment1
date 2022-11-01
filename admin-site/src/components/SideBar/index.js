import { Divider, List } from '@mui/material';
import React from 'react';
// import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
// import SettingsIcon from '@mui/icons-material/Settings';
import { mainListItems } from './MainListItems';
import SecondaryListItems from './SecondaryListItem';

export default function SideBar() {
    return <List component="nav">
        {mainListItems}
        <Divider sx={{ my: 1 }} />
        <SecondaryListItems />
    </List>
}

