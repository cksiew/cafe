import { ColDef, ICellRendererComp, ICellRendererParams } from 'ag-grid-community';
import { useEffect, useState } from 'react';
import Button from '@mui/material/Button';

import classes from './Cafes.module.css';
import { TextField, Box, Typography, CircularProgress } from '@mui/material';
import { ICafe } from '../../models/Cafe';
import DataGridActionsComponent from '../../components/UI/Data/DataGridActions';
import DataGrid from '../../components/UI/Data/DataGrid';
import { deleteCafe, fetchCafes } from '../../store/cafe-actions';
import { useDispatch, useSelector } from 'react-redux';
import { cafesSliceActions } from '../../store/cafe';
import CafeForm from '../../components/Cafe/CafeForm';
import CafeDeleteConfirmationDialog from '../../components/Cafe/CafeDeleteConfirmationDialog';
import StatusNotification from '../../components/UI/Notification/StatusNotification';



class LogoCellRenderer implements ICellRendererComp {

  eGui!: HTMLElement;

  init(params: ICellRendererParams) {
    const logoElement = document.createElement('img');
    logoElement.src = params.data.logo;
    logoElement.title = params.data.name;
    logoElement.width = 75;
    // logoElement.height = 100;
    this.eGui = logoElement;
  }

  getGui() {
    return this.eGui;
  }

  refresh(_params: ICellRendererParams): boolean {
    return false;
  }
}

class EmployeesCountCellRenderer implements ICellRendererComp {

  eGui!: HTMLAnchorElement;

  init(params: ICellRendererParams) {

    // logoElement.height = 100;
    this.eGui = document.createElement('a');
    this.eGui.href = `/employees?cafeId=${params.data.id}`;
    this.eGui.textContent = params.data.employeesCount;
  }

  getGui() {
    return this.eGui;
  }

  refresh(_params: ICellRendererParams): boolean {
    return false;
  }
}

const CafesPage = () => {


  const { cafes, showCafes, filteredCafes, filterQuery, showCreateOrUpdateForm, showDeleteDialog, cafeToBeDeletedId } = useSelector((state: any) => state.cafe);
  const notification = useSelector((state: any) => state.ui.notification);
  const displayNotification = notification && !showCreateOrUpdateForm && !showDeleteDialog;
  const dispatch = useDispatch<any>();
  const [searchQuery, setSearchQuery] = useState(filterQuery);

  const [isEdit, setIsEdit] = useState<boolean>(false);

  const handleEditClick = (rowData: ICafe) => {
    setIsEdit(true);
    dispatch(cafesSliceActions.beginCreateOrUpdate({ cafeToBeEdited: rowData }));
  };

  const handleDeleteClick = (rowData: ICafe) => {
    dispatch(cafesSliceActions.beginDelete({ cafeToBeDeletedId: rowData.id }));
  };


  const handleDeleteCafe = () => {
    dispatch(deleteCafe(cafeToBeDeletedId));
  }

  const handleCloseDeleteDialog = () => {
    dispatch(cafesSliceActions.endDelete({}));
  }


  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const query = event.target.value;
    setSearchQuery(query);
    dispatch(
      cafesSliceActions.filterCafes({
        filterQuery: query
      })
    );
  }

  const handleAddCafe = () => {
    setIsEdit(false);
    dispatch(cafesSliceActions.beginCreateOrUpdate({}));
  }

  const handleCloseForm = () => {
    dispatch(cafesSliceActions.endCreateOrUpdate({}));
  }

  useEffect(() => {
    dispatch(fetchCafes());
  }, [dispatch]);

  const [colDefs] = useState<ColDef<ICafe>[]>([
    { field: "logo", cellRenderer: LogoCellRenderer },
    { field: "name" },
    { field: "description", wrapText: true, autoHeight: true },
    { field: "employeesCount", cellRenderer: EmployeesCountCellRenderer },
    { field: "location" },
    { field: "id", hide: true },
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
      <h2>Cafes</h2>

      <Box sx={{ padding: 2 }}>
        <Typography variant="h6" gutterBottom sx={{ color: '#2d3319', textAlign: 'left' }}>
          Search Cafes
        </Typography>

        <TextField
          label="Filter by location"
          variant="outlined"
          fullWidth
          value={searchQuery}
          onChange={handleSearchChange}
          sx={{ marginBottom: 2 }}
        />
      </Box>

      {displayNotification && <StatusNotification />}

      {!showCafes && !displayNotification && <div className="loading"><CircularProgress /><span>Loading cafes...</span></div>}

      {showCafes && <><div className={classes.listContainerByCol}>
        <Button variant="contained" onClick={handleAddCafe}>Add Cafe</Button>
      </div>
        <div
          // define a height because the Data Grid will fill the size of the parent container
          style={{ height: 1000 }}
        >
          <DataGrid data={cafes} filteredData={filteredCafes} colDefs={colDefs} />
        </div>

        <CafeForm open={showCreateOrUpdateForm} handleClose={handleCloseForm} isEdit={isEdit} />
        <CafeDeleteConfirmationDialog open={showDeleteDialog} handleDelete={handleDeleteCafe} handleClose={handleCloseDeleteDialog} />
      </>
      }
    </div>
  )

}

export default CafesPage;


