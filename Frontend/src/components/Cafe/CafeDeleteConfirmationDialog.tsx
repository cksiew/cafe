import { Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from '@mui/material';
import { FC } from 'react';
import StatusNotification from '../UI/Notification/StatusNotification';

const CafeDeleteConfirmationDialog: FC<{ open: boolean, handleDelete: () => void, handleClose: () => void }> = ({ open, handleDelete, handleClose }) => {

  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {"Delete Cafe"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to delete this cafe?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDelete}>Yes</Button>
          <Button onClick={handleClose} autoFocus>
            No
          </Button>
        </DialogActions>
        <StatusNotification />
      </Dialog>
    </>
  );
}

export default CafeDeleteConfirmationDialog;