import React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import LogoutIcon from '@mui/icons-material/Logout';
import { logoutApi } from '../../apis/useApi';
import { useNavigate } from 'react-router-dom';

export default function SecondaryListItems() {
    let navigate = useNavigate();

    const logout = () => {
        logoutApi().then(function (response) {
            navigate("/");
        }).catch(function (response) {
            alert(response.response.data);
        });
    }

    return <React.Fragment>
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
        <ListItemButton onClick={logout}>
            <ListItemIcon>
                <LogoutIcon />
            </ListItemIcon>
            <ListItemText primary="Logout" />
        </ListItemButton>
    </React.Fragment>
}    