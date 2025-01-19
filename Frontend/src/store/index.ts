import { configureStore } from "@reduxjs/toolkit";
import cafeReducer from './cafe';
import employeeReducer from './employee';
import uiReducer from './ui';

const store = configureStore({
  reducer: {
    cafe: cafeReducer,
    employee: employeeReducer,
    ui: uiReducer,
  }
});

export default store;