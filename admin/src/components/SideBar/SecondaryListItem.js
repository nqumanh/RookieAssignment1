import React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import LogoutIcon from '@mui/icons-material/Logout';

export default function SecondaryListItems(props) {
    const handleClick = () => {
        props.logOut();
    }

    return <React.Fragment>
        <ListSubheader component="div" inset>
            Account
        </ListSubheader>
        <ListItemButton onClick={handleClick}>
            <ListItemIcon>
                <LogoutIcon />
            </ListItemIcon>
            <ListItemText primary="Logout" />
        </ListItemButton>
    </React.Fragment>
}    