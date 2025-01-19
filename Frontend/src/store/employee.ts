import { createSlice } from '@reduxjs/toolkit';
import { IEmployee } from '../models/Employee';

export interface IEmployeesState {
  employees: IEmployee[],
  cafeId: string | undefined,
  showEmployees: boolean,
  showCreateOrUpdateForm: boolean,
  showDeleteDialog: boolean,
  employeeToBeEdited: IEmployee | undefined,
  employeeToBeDeleted: IEmployee | undefined
}

const employeesInitialState: IEmployeesState = {
  employees: [],
  cafeId: undefined,
  showEmployees: false,
  showCreateOrUpdateForm: false,
  showDeleteDialog: false,
  employeeToBeEdited: undefined,
  employeeToBeDeleted: undefined
}

const employeesSlice = createSlice({
  name: "employee",
  initialState: employeesInitialState,
  reducers: {
    replaceEmployees(state: IEmployeesState, action) {
      state.employees = action.payload.employees;
      state.showEmployees = action.payload.showEmployees;
      state.showCreateOrUpdateForm = false;
    },
    beginCreateOrUpdate(state: IEmployeesState, action) {
      state.showCreateOrUpdateForm = true;
      state.employeeToBeEdited = action.payload.employeeToBeEdited;
    },
    endCreateOrUpdate(state: IEmployeesState, _action) {
      state.showCreateOrUpdateForm = false;
    },
    beginDelete(state: IEmployeesState, action) {
      state.showDeleteDialog = true;
      state.employeeToBeDeleted = action.payload.employeeToBeDeleted;
    },
    endDelete(state: IEmployeesState, _action) {
      state.showDeleteDialog = false;
      state.employeeToBeDeleted = undefined;
    }
  }
});

export default employeesSlice.reducer;

export const employeesSliceActions = employeesSlice.actions;