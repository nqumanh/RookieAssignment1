import { Divider, List } from '@mui/material';
import React from 'react';
import { mainListItems } from './MainListItems';
import SecondaryListItems from './SecondaryListItem';

export default function SideBar(props) {
    return <List component="nav">
        {mainListItems}
        <Divider sx={{ my: 1 }} />
        <SecondaryListItems logOut={props.logOut} />
    </List>
}

