import { uiSliceActions } from './ui';
import { employeesSliceActions } from './employee';
import { IEmployee } from '../models/Employee';

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const fetchEmployees = (cafeId?: string) => {
  return async (dispatch: any) => {
    const fetchRequest = async () => {
      const response = await fetch(`${BASE_URL}/employees?${cafeId ? "cafeId=" + cafeId + "&" : ""}pageIndex=0&pageSize=10`);
      if (!response.ok) {
        throw new Error('Failed to fetch employees');
      }
      const data = await response.json();
      return data.employees.data;
    };

    try {
      const data = await fetchRequest();
      dispatch(
        employeesSliceActions.replaceEmployees({
          employees: data || [],
          showEmployees: true
        })
      );
    } catch (error) {
      dispatch(
        uiSliceActions.showNotification({
          status: "error",
          title: "Error!",
          message: "Failed to fetch employees"
        })
      )
    }
  }
}

export const deleteEmployee = (employeeId: string) => {
  return async (dispatch: any) => {
    const deleteRequest = async () => {
      const response = await fetch(`${BASE_URL}/employees/${employeeId}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Failed to delete employee");
      }
      const data = await response.json();
      dispatch(uiSliceActions.showNotification({
        status: (data.isSuccess ? "success" : "error"),
        title: "Delete Employee",
        message: (data.isSuccess ? "Employee is deleted successfully." : "Deletion of employee failed")
      }));

    };

    try {
      await deleteRequest();
      dispatch(employeesSliceActions.endDelete({}));
      dispatch(fetchEmployees());
    } catch (error) {
      console.error("Request failed:", error);
      dispatch(
        uiSliceActions.showNotification({
          status: "error",
          title: "Error!",
          message: "Deletion of employee failed"
        })
      )
    }

  }
}

export const updateEmployee = (employee: IEmployee, isNew: boolean, cafeId: string | undefined) => {
  return async (dispatch: any) => {
    const updateRequest = async () => {
      var body = { "employee": employee };
      const response = await fetch(`${BASE_URL}/employees`, {
        method: isNew ? "POST" : "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
      });

      if (!response.ok) {
        throw new Error(`Failed to ${isNew ? 'create' : 'update'} employee`);
      }
      const data = await response.json();
      const isSuccess = (isNew && data.id) || (!isNew && data.isSuccess);
      dispatch(uiSliceActions.showNotification({
        status: (isSuccess ? "success" : "error"),
        title: `${isNew ? 'Create' : 'Update'} Employee`,
        message: (isSuccess ? `Employee is ${isNew ? 'created' : 'updated'} successfully.` : `${isNew ? 'Create' : 'Update'} employee failed.`)
      }));
    };

    try {
      await updateRequest();
      dispatch(employeesSliceActions.endCreateOrUpdate(undefined));
      dispatch(fetchEmployees(cafeId));
    } catch (error) {
      console.error("Request failed:", error);
      dispatch(
        uiSliceActions.showNotification({
          status: "error",
          title: "Error!",
          message: `${isNew ? "Add" : "Update"} Employee failed`
        })
      )
    }
  }
}

