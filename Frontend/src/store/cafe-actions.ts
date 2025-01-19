import { uiSliceActions } from './ui';
import { cafesSliceActions } from './cafe';
import { ICafe } from '../models/Cafe';

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const fetchCafes = () => {
  return async (dispatch: any) => {
    const fetchRequest = async () => {
      // const response = await fetch(`${BASE_URL}/cafes?pageIndex=0&pageSize=10`);
      const response = await fetch(`${BASE_URL}/cafes?pageIndex=0&pageSize=10`);
      if (!response.ok) {
        throw new Error('Failed to fetch cafes');
      }
      const data = await response.json();
      return data.cafes.data;
    };

    try {
      const data = await fetchRequest();
      dispatch(
        cafesSliceActions.replaceCafes({
          cafes: data || [],
          showCafes: true
        })
      );
    } catch (error) {
      dispatch(
        uiSliceActions.showNotification({
          status: "error",
          title: "Error!",
          message: "Failed to fetch cafes"
        })
      )
    }
  }
}

export const deleteCafe = (cafeId: string) => {
  return async (dispatch: any) => {
    const deleteRequest = async () => {
      const response = await fetch(`${BASE_URL}/cafes/${cafeId}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Failed to delete cafe");
      }
      const data = await response.json();
      dispatch(uiSliceActions.showNotification({
        status: (data.isSuccess ? "success" : "error"),
        title: "Delete Cafe",
        message: (data.isSuccess ? "Cafe is deleted successfully." : "Deletion of cafe fail.")
      }));

    };

    try {
      await deleteRequest();
      dispatch(cafesSliceActions.endDelete({}));
      dispatch(fetchCafes());
    } catch (error) {
      console.error("Request failed:", error);
      dispatch(
        uiSliceActions.showNotification({
          status: "error",
          title: "Error!",
          message: "Deletion of cafe failed"
        })
      )
    }

  }
}

export const updateCafe = (cafe: ICafe, isNew: boolean) => {
  return async (dispatch: any) => {
    const postRequest = async () => {
      const formData = new FormData();

      formData.append("logoFile", cafe.logoFile!);
      formData.append("name", cafe.name);
      formData.append("description", cafe.description);
      formData.append("location", cafe.location);
      formData.append("id", cafe.id!);

      const response = await fetch(`${BASE_URL}/cafes`, {
        method: isNew ? "POST" : "PUT",
        body: formData
      });


      if (!response.ok) {
        throw new Error(`Failed to ${isNew ? 'create' : 'update'} cafe`);
      }
      const data = await response.json();
      const isSuccess = (isNew && data.id) || (!isNew && data.isSuccess);
      dispatch(uiSliceActions.showNotification({
        status: (isSuccess ? "success" : "error"),
        title: `${isNew ? 'Create' : 'Update'} Cafe`,
        message: (isSuccess ? `Cafe is ${isNew ? 'created' : 'updated'} successfully.` : `${isNew ? 'Create' : 'Update'} cafe failed.`)
      }));
    };

    try {
      await postRequest();
      dispatch(cafesSliceActions.endCreateOrUpdate(undefined))
      dispatch(fetchCafes());
    } catch (error) {
      console.error("Request failed:", error);
      dispatch(
        uiSliceActions.showNotification({
          status: "error",
          title: "Error!",
          message: `${isNew ? "Create" : "Update"} Cafe failed`
        })
      )
    }
  }
}