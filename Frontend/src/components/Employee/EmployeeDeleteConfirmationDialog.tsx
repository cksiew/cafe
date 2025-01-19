import { Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from '@mui/material';
import { FC } from 'react';
import { useSelector } from 'react-redux';
import { IEmployeesState } from '../../store/employee';
import { IEmployee } from '../../models/Employee';

const EmployeeDeleteConfirmationDialog: FC<{ open: boolean, handleDelete: () => void, handleClose: () => void }> = ({ open, handleDelete, handleClose }) => {

  const employeeToBeDeleted = useSelector<IEmployeesState, IEmployee>((state: any) => state.employee.employeeToBeDeleted);

  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {"Delete Employee"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to delete this employee?
            <br /><br />
            {employeeToBeDeleted && <span>{employeeToBeDeleted.name} ({employeeToBeDeleted.id})</span>}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDelete}>Yes</Button>
          <Button onClick={handleClose} autoFocus>
            No
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default EmployeeDeleteConfirmationDialog;