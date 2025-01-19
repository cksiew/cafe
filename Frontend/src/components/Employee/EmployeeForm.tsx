import { FC, useEffect } from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { IEmployee } from '../../models/Employee';
import { Form, Field } from 'react-final-form'
import classes from './EmployeeForm.module.css';
import { updateEmployee } from '../../store/employee-actions';
import { useDispatch, useSelector } from 'react-redux';
import { IEmployeesState } from '../../store/employee';
import { ICafesState } from '../../store/cafe';
import { fetchCafes } from '../../store/cafe-actions';
import { Gender } from '../../models/Gender';
import { ICafe } from '../../models/Cafe';
import TextField from '../UI/Form/TextField';
import RadioButtonGroup from '../UI/Form/RadioButtonGroup';
import SelectField from '../UI/Form/SelectField';
import { useSearchParams } from 'react-router-dom';


const EmployeeForm: FC<{ open: boolean, handleClose: () => void, isEdit: boolean }> = ({ open, handleClose, isEdit }) => {

  const employeeToBeEdited: IEmployee = useSelector<IEmployeesState, IEmployee>((state: any) => state.employee.employeeToBeEdited);
  const cafes: ICafe[] = useSelector<ICafesState, ICafe[]>((state: any) => state.cafe.cafes);
  const [searchParams] = useSearchParams();
  const cafeId = searchParams.get('cafeId');
  const dispatch = useDispatch<any>();

  useEffect(() => {

    dispatch(fetchCafes());

    // if (open) {
    //   // Move focus to the dialog when it's opened
    //   const firstFocusable: HTMLButtonElement | null = document.querySelector('button[type="submit"]');
    //   if (firstFocusable) {
    //     firstFocusable.focus();
    //   }

    // }
  }, []);


  const onSubmit = async (values: any) => {
    const newEmployee: IEmployee = {
      id: values.id,
      name: values.name,
      gender: parseInt(values.gender),
      emailAddress: values.emailAddress,
      phoneNumber: values.phoneNumber,
      cafeId: values.cafeId,
    };
    dispatch(updateEmployee(newEmployee, !isEdit, cafeId ?? undefined));

  }



  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        fullWidth={true}
      >
        <DialogTitle className={classes.dialogTitle}>{(isEdit ? "Edit" : "Add")} Employee</DialogTitle>
        <DialogContent>
          <Form
            onSubmit={onSubmit}
            initialValues={{
              id: isEdit && employeeToBeEdited ? employeeToBeEdited.id! : '',
              name: isEdit && employeeToBeEdited ? employeeToBeEdited.name : '',
              gender: isEdit && employeeToBeEdited ? employeeToBeEdited.gender.toString() : Gender.Unknown,
              emailAddress: isEdit && employeeToBeEdited ? employeeToBeEdited.emailAddress : '',
              phoneNumber: isEdit && employeeToBeEdited ? employeeToBeEdited.phoneNumber : '',
              cafeId: (!isEdit ? cafeId : (employeeToBeEdited ? employeeToBeEdited.cafeId : '')),
            }}
            render={({ handleSubmit, submitting, pristine }) => (
              <form onSubmit={handleSubmit} className={classes.formContainer}>
                <Field name="id">
                  {({ input }) => (
                    <input {...input} type="hidden" />
                  )}
                </Field>
                <TextField
                  name="name"
                  label="Name"
                  minLength={6}
                  maxLength={10}
                  placeholder="Name"
                  required
                />
                <RadioButtonGroup
                  name="gender"
                  label="Gender"
                  options={[
                    { value: '1', label: 'Male' },
                    { value: '2', label: 'Female' },
                  ]}
                />
                <TextField
                  name="emailAddress"
                  label="Email Address"
                  placeholder="Email Address"
                  type="email"
                  required
                />
                <TextField
                  name="phoneNumber"
                  label="Phone Number"
                  placeholder="Phone Number"
                  type="phoneNo"
                  required
                />
                <SelectField
                  name="cafeId"
                  label="Cafe"
                  options={cafes.map(cafe => ({
                    value: cafe.id!,
                    label: cafe.name,
                  }))}
                />
                <div className={classes.buttons}>
                  <Button variant='contained' size="small" type="submit" className={`${classes.button} ${classes.submitButton}`} disabled={submitting}>Submit</Button>
                  <Button variant='contained' size="small" onClick={handleClose} className={`${classes.button} ${classes.cancelButton}`} disabled={submitting || pristine}>Cancel</Button>
                </div>
              </form>
            )}
          />


        </DialogContent>


      </Dialog>
    </>
  );
}

export default EmployeeForm;

