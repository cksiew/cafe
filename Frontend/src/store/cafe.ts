import { createSlice } from '@reduxjs/toolkit';
import { ICafe } from '../models/Cafe';


export interface ICafesState {
  cafes: ICafe[],
  filteredCafes: ICafe[],
  showCafes: boolean,
  changed: boolean,
  filterBy: string,
  filterQuery: string,
  showCreateOrUpdateForm: boolean,
  showDeleteDialog: boolean,
  cafeToBeEdited: ICafe | undefined,
  cafeToBeDeletedId: string | undefined
}

const cafesInitialState: ICafesState = {
  cafes: [],
  filteredCafes: [],
  showCafes: false,
  changed: false,
  filterBy: "location",
  filterQuery: "",
  showCreateOrUpdateForm: false,
  showDeleteDialog: false,
  cafeToBeEdited: undefined,
  cafeToBeDeletedId: undefined
}

const cafesSlice = createSlice({
  name: "cafe",
  initialState: cafesInitialState,
  reducers: {
    replaceCafes(state: ICafesState, action) {
      state.cafes = action.payload.cafes;
      state.filteredCafes = [...state.cafes];
      state.showCafes = action.payload.showCafes;
      state.showCreateOrUpdateForm = false;

    },
    filterCafes(state: ICafesState, action) {
      let filterQuery = action.payload.filterQuery;
      if (filterQuery !== '') {
        switch (state.filterBy) {
          case "location":
            state.filteredCafes = state.filteredCafes.filter((item) => item.location.toLowerCase().includes(filterQuery.toLowerCase()));
            break;
        }

      } else {
        state.filteredCafes = [...state.cafes];
      }
    },
    beginCreateOrUpdate(state: ICafesState, action) {
      state.showCreateOrUpdateForm = true;
      state.cafeToBeEdited = action.payload.cafeToBeEdited;
    },
    endCreateOrUpdate(state: ICafesState, _action) {
      state.showCreateOrUpdateForm = false;
    },
    beginDelete(state: ICafesState, action) {
      state.showDeleteDialog = true;
      state.cafeToBeDeletedId = action.payload.cafeToBeDeletedId;
    },
    endDelete(state: ICafesState, _action) {
      state.showDeleteDialog = false;
      state.cafeToBeDeletedId = undefined;
    }
  }
});


export default cafesSlice.reducer;

export const cafesSliceActions = cafesSlice.actions;
