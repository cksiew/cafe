import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import './App.css'
import CafesPage from './pages/Cafes/Cafes';
import RootLayout from './pages/Layouts/RootLayout';
import ErrorPage from './pages/Error/ErrorPage';
import EmployeesPage from './pages/Employees/Employees';

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <CafesPage /> },
      { path: "cafes", element: <CafesPage /> },
      { path: "employees/:cafeId", element: <EmployeesPage /> },
      { path: "employees", element: <EmployeesPage />, },

    ]
  }
])


function App() {

  return <RouterProvider router={router} />;
}

export default App
