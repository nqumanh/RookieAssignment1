import { Box, Divider, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Typography } from '@mui/material';
import React from 'react';
import ShoppingBagIcon from '@mui/icons-material/ShoppingBag';
import { NavLink } from 'react-router-dom';
import GroupIcon from '@mui/icons-material/Group';
import CategoryIcon from '@mui/icons-material/Category';

let activeStyle = {
    color: "#10b981",
    textDecoration: "none",
};

let inactiveStyle = {
    textDecoration: "none",
    color: "#c5d5db"
};

const items = [
    {
        href: '/products',
        icon: (<ShoppingBagIcon fontSize="small" />),
        title: 'Products'
    },
    {
        href: '/customers',
        icon: (<GroupIcon fontSize="small" />),
        title: 'Customers'
    },
    {
        href: '/categories',
        icon: (<CategoryIcon fontSize="small" />),
        title: 'Categories'
    },
];

function Sidebar(props) {
    const { handleDrawerToggle, mobileOpen } = props

    const drawerWidth = 250;

    const drawer = (
        <Box display='flex' flexDirection="column" flex='1' sx={{ backgroundColor: "#111827" }}>
            <Box
                sx={{
                    mx: 2,
                    my: 3,
                    alignItems: 'center',
                    backgroundColor: 'rgba(255, 255, 255, 0.04)',
                    cursor: 'pointer',
                    display: 'flex',
                    justifyContent: 'space-between',
                    px: 4,
                    py: 4,
                    borderRadius: 1
                }}
            >
                <Typography
                    color="white"
                    variant="subtitle1"
                >
                    Book Store
                </Typography>
            </Box>
            <Divider sx={{
                borderColor: '#2D3748',
            }} />
            <List sx={{ flexGrow: 1, color: "#fff" }}>
                {items.map((item) => (
                    <NavLink key={item.title} to={item.href} style={({ isActive }) =>
                        isActive ? activeStyle : inactiveStyle
                    }
                    >
                        <ListItem disablePadding>
                            <ListItemButton>
                                <ListItemIcon>
                                    {item.icon}
                                </ListItemIcon>
                                <ListItemText primary={item.title} />
                            </ListItemButton>
                        </ListItem>
                    </NavLink>
                ))}
            </List>
        </Box>
    );

    return (
        <Box
            sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 }, display: "flex", }}
            aria-label="mailbox folders"
        >
            {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
            <Drawer
                variant="temporary"
                open={mobileOpen}
                onClose={handleDrawerToggle}
                ModalProps={{
                    keepMounted: true, // Better open performance on mobile.
                }}
                sx={{
                    display: { xs: 'block', sm: 'none' },
                    '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
                }}
            >
                {drawer}
            </Drawer>
            <Drawer
                variant="permanent"
                sx={{
                    display: { xs: 'none', sm: 'block' },
                    '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
                }}
                open
            >
                {drawer}
            </Drawer>
        </Box>
    );
}

export default Sidebar;