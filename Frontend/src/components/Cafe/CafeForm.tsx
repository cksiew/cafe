import { FC, useEffect, useState } from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { ICafe } from '../../models/Cafe';
import { Form, Field } from 'react-final-form'
import classes from './CafeForm.module.css';
import { updateCafe } from '../../store/cafe-actions';
import { useDispatch, useSelector } from 'react-redux';
import { ICafesState } from '../../store/cafe';
import TextField from '../UI/Form/TextField';
import TextAreaField from '../UI/Form/TextAreaField';
import FileInputField from '../UI/Form/FileInputField';
import StatusNotification from '../UI/Notification/StatusNotification';


const CafeForm: FC<{ open: boolean, handleClose: () => void, isEdit: boolean }> = ({ open, handleClose, isEdit }) => {

  const [file, setFile] = useState<File>();
  const cafeToBeEdited: ICafe = useSelector<ICafesState, ICafe>((state: any) => state.cafe.cafeToBeEdited);
  const dispatch = useDispatch<any>();

  useEffect(() => {
    if (open) {
      // Move focus to the dialog when it's opened
      const firstFocusable: HTMLButtonElement | null = document.querySelector('button[type="submit"]');
      if (firstFocusable) {
        firstFocusable.focus();
      }

    }
  }, [open]);


  const handleFileChange = (file: File) => {
    if (file) {
      setFile(file);
    }
  };

  const onSubmit = async (values: any) => {
    const newCafe: ICafe = {
      id: values.id,
      name: values.name,
      description: values.description,
      location: values.location,
      logo: file?.name ?? "",
      logoFile: file ?? undefined,
    };
    dispatch(updateCafe(newCafe, !isEdit));

  }


  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        fullWidth={true}
      >
        <DialogTitle className={classes.dialogTitle}>{(isEdit ? "Edit" : "Add")} Cafe</DialogTitle>
        <DialogContent>
          <Form
            onSubmit={onSubmit}
            initialValues={{
              id: isEdit && cafeToBeEdited ? cafeToBeEdited.id! : '',
              name: isEdit && cafeToBeEdited ? cafeToBeEdited.name : '',
              description: isEdit && cafeToBeEdited ? cafeToBeEdited.description : '',
              location: isEdit && cafeToBeEdited ? cafeToBeEdited.location : '',
              logo: ''
            }}
            render={({ handleSubmit, submitting }) => (
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
                <TextAreaField
                  name="description"
                  label="Description"
                  placeholder="Description"
                  maxLength={256}
                  required
                />
                <TextField
                  name="location"
                  label="Location"
                  placeholder="Name"
                  required
                />
                <FileInputField
                  name="logo"
                  label="Choose Logo"
                  isEdit={isEdit}
                  currentFileName={cafeToBeEdited?.logo}
                  onChangeFile={handleFileChange}
                />
                <div className={classes.buttons}>
                  <Button variant='contained' size="small" type="submit" className={`${classes.button} ${classes.submitButton}`} disabled={submitting}>Submit</Button>
                  <Button variant='contained' size="small" onClick={handleClose} className={`${classes.button} ${classes.cancelButton}`} disabled={submitting}>Cancel</Button>
                </div>
              </form>
            )}
          />

          <StatusNotification />
        </DialogContent>
      </Dialog>
    </>
  );
}

export default CafeForm;

