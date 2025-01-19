
import { Alert } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { uiSliceActions, IUIState } from '../../../store/ui';
import { useEffect } from 'react';



const StatusNotification = () => {

  useEffect(() => {
    setTimeout(() => {
      handleCloseNotification();
    }, 3000);
  }, [])

  const notification = useSelector<IUIState, any>((state: any) => state.ui.notification);
  const dispatch = useDispatch();
  const handleCloseNotification = () => {
    dispatch(uiSliceActions.clearNotification(undefined));
  }
  return (notification && <div className='notification'>
    <Alert severity={notification.status} onClose={handleCloseNotification}>{notification.message}</Alert>
  </div>);
}

export default StatusNotification;