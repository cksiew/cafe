import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { ColDef } from "ag-grid-community";
import { Button, CircularProgress } from "@mui/material";
import { useSearchParams } from 'react-router-dom';
import { employeesSliceActions, IEmployeesState } from "../../store/employee";
import { IEmployee } from "../../models/Employee";
import { deleteEmployee, fetchEmployees } from "../../store/employee-actions";
import DataGridActionsComponent from "../../components/UI/Data/DataGridActions";
import DataGrid from "../../components/UI/Data/DataGrid";
import classes from './Employees.module.css';
import EmployeeDeleteConfirmationDialog from "../../components/Employee/EmployeeDeleteConfirmationDialog";
import EmployeeForm from "../../components/Employee/EmployeeForm";
import StatusNotification from "../../components/UI/Notification/StatusNotification";


const EmployeesPage = () => {

  const { employees, showEmployees, showCreateOrUpdateForm, showDeleteDialog, employeeToBeDeleted } = useSelector<IEmployeesState, IEmployeesState>((state: any) => state.employee);
  const notification = useSelector((state: any) => state.ui.notification);
  const displayNotification = notification && !showCreateOrUpdateForm && !showDeleteDialog;
  const [searchParams] = useSearchParams();
  const cafeId = searchParams.get('cafeId');

  const dispatch = useDispatch<any>();

  const [isEdit, setIsEdit] = useState<boolean>(false);

  const handleEditClick = (rowData: IEmployee) => {
    setIsEdit(true);
    dispatch(employeesSliceActions.beginCreateOrUpdate({ employeeToBeEdited: rowData }));
  };

  const handleDeleteClick = (rowData: IEmployee) => {
    dispatch(employeesSliceActions.beginDelete({ employeeToBeDeleted: rowData }));
  };


  const handleDeleteEmployee = () => {
    dispatch(deleteEmployee(employeeToBeDeleted!.id!));
  }

  const handleCloseDeleteDialog = () => {
    dispatch(employeesSliceActions.endDelete({}));
  }

  const handleAddEmployee = () => {
    setIsEdit(false);
    dispatch(employeesSliceActions.beginCreateOrUpdate({}));
  }

  const handleCloseForm = () => {
    dispatch(employeesSliceActions.endCreateOrUpdate({}));
  }

  useEffect(() => {
    dispatch(fetchEmployees(cafeId ?? undefined));
  }, [dispatch]);

  const [colDefs] = useState<ColDef<IEmployee>[]>([
    { field: "id", width: 100 },
    { field: "name" },
    { field: "emailAddress" },
    { field: "phoneNumber" },
    { field: "daysWork", width: 150 },
    { field: "cafeName" },
    {
      headerName: "Actions",
      cellRenderer: DataGridActionsComponent,
      cellRendererParams: {
        editText: "Edit",
        handleEditFunction: handleEditClick,
        deleteText: "Delete",
        handleDeleteFunction: handleDeleteClick
      }
    }
  ]);

  return (
    <div className={classes.listContainerByRow}>
      <h2>Employees</h2>
      {displayNotification && <StatusNotification />}

      {!showEmployees && !displayNotification && <div className="loading"><CircularProgress /><span>Loading employees...</span></div>}

      {showEmployees && <>
        <div className={classes.listContainerByCol}>
          <Button variant="contained" onClick={handleAddEmployee}>Add Employee</Button>
        </div>
        <div
          // define a height because the Data Grid will fill the size of the parent container
          style={{ height: 1000 }}
        >
          <DataGrid data={employees} filteredData={employees} colDefs={colDefs} />
        </div>

        <EmployeeForm open={showCreateOrUpdateForm} handleClose={handleCloseForm} isEdit={isEdit} />
        <EmployeeDeleteConfirmationDialog open={showDeleteDialog} handleDelete={handleDeleteEmployee} handleClose={handleCloseDeleteDialog} />
      </>}
    </div>
  )
}

export default EmployeesPage;