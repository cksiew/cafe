import { NavLink } from "react-router-dom";
import classes from "./MainNavigation.module.css";
import { Divider, Stack } from "@mui/material";



const MainNavigation = () => {
  return (
    <header className={classes.header}>
      <nav>
        <Stack
          direction="row"
          divider={<Divider orientation="vertical" flexItem />}
          spacing={2}
        >
          <NavLink
            className={({ isActive }) =>
              isActive ? classes.active : undefined
            }
            to=""
            end
          >
            Home
          </NavLink>
          <NavLink
            className={({ isActive }) =>
              isActive ? classes.active : undefined
            }
            to="cafes"
          >
            Cafes
          </NavLink>
          <NavLink
            className={({ isActive }) =>
              isActive ? classes.active : undefined
            }
            to="employees"
          >
            Employees
          </NavLink>
        </Stack>

        {/* <ul className={classes.list}>
          <li>
            <NavLink
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              to=""
              end
            >
              Home
            </NavLink>
          </li>
          <li>
            <NavLink
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              to="cafes"
            >
              Cafes
            </NavLink>

          </li>
          <li>
            <NavLink
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              to="employees"
            >
              Employees
            </NavLink>

          </li>
        </ul> */}
      </nav>
    </header>
  );
};

export default MainNavigation;