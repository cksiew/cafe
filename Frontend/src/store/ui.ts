import { createSlice } from "@reduxjs/toolkit";

export interface IUIState {
  notification: { status: string, title: string, message: string } | undefined;
}

const uiInitialState: IUIState = {
  notification: undefined
};

const uiSlice = createSlice({
  name: "ui",
  initialState: uiInitialState,
  reducers: {
    showNotification(state: IUIState, action) {
      const { status, title, message } = action.payload;
      state.notification = { status, title, message };
    },
    clearNotification(state: IUIState, _action) {
      state.notification = undefined;
    }
  }
})

export const uiSliceActions = uiSlice.actions;

export default uiSlice.reducer;