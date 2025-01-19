import DeleteIcon from '@mui/icons-material/Delete';
import ModeEditIcon from '@mui/icons-material/ModeEdit';
import classes from './DataGridActions.module.css';
import { Button } from '@mui/material';


const DataGridActionsComponent = ({ editText, handleEditFunction, deleteText, handleDeleteFunction, node }
  : any) => {

  const handleEditClick = () => {
    handleEditFunction(node.data);
  }

  const handleDeleteClick = () => {
    handleDeleteFunction(node.data);
  }

  return <div className={classes.actionsContainer}>
    <Button size="small" variant="outlined" startIcon={<ModeEditIcon />} onClick={handleEditClick}>{editText}</Button>
    <Button size="small" variant="outlined" startIcon={<DeleteIcon />} onClick={handleDeleteClick}>{deleteText}</Button>
  </div>
}

export default DataGridActionsComponent;