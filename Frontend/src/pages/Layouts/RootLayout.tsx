import { NavLink, Outlet } from "react-router-dom"
import { Box, CssBaseline, AppBar, Toolbar, Typography, Drawer, Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import LocalCafeIcon from '@mui/icons-material/LocalCafe';
import BadgeIcon from '@mui/icons-material/Badge';

const drawerWidth = 240;

const RootLayout = () => {
  return (
    <>
      <header>
        {/* <MainNavigation />
        <Breadcrumb /> */}
        <Box sx={{ display: 'flex' }}>
          <CssBaseline />
          <AppBar
            position="fixed"
            sx={{ width: `calc(100% - ${drawerWidth}px)`, ml: `${drawerWidth}px`, backgroundColor: '#d6e5e3' }}
          >
            <Toolbar>
              <Typography variant="h6" noWrap component="div" sx={{ color: '#517664' }}>
                Cafe Management System
              </Typography>
            </Toolbar>
          </AppBar>
          <Drawer
            sx={{
              width: drawerWidth,
              flexShrink: 0,
              '& .MuiDrawer-paper': {
                width: drawerWidth,
                boxSizing: 'border-box',
              },
            }}
            variant="permanent"
            anchor="left"
          >
            <Toolbar />
            <Divider />
            <List>
              {['Cafes', 'Employees'].map((text) => (
                <ListItem key={text} disablePadding>
                  <NavLink
                    to={text === 'Cafes' ? '/cafes' : '/employees'} // Specify the route path for each item
                    style={({ isActive }) => ({
                      textDecoration: 'none',
                      color: isActive ? '#517664' : 'inherit',
                    })}
                  >
                    <ListItemButton>
                      <ListItemIcon>
                        {text === 'Cafes' && <LocalCafeIcon />}
                        {text === 'Employees' && <BadgeIcon />}
                      </ListItemIcon>
                      <ListItemText primary={text} />
                    </ListItemButton>
                  </NavLink>
                </ListItem>
              ))}
            </List>
          </Drawer>
        </Box>
      </header>
      <main>
        <Outlet />
      </main>
    </>
  )
}

export default RootLayout;