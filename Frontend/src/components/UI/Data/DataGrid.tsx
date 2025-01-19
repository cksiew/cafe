import { AgGridReact } from 'ag-grid-react';
import { AllCommunityModule, ColDef, ModuleRegistry, GridOptions, themeQuartz } from 'ag-grid-community';

ModuleRegistry.registerModules([AllCommunityModule]);

const myTheme = themeQuartz
  .withParams({
    backgroundColor: "#1f2836",
    browserColorScheme: "dark",
    chromeBackgroundColor: {
      ref: "foregroundColor",
      mix: 0.07,
      onto: "backgroundColor"
    },
    foregroundColor: "#FFF",
    headerFontSize: 14
  });

interface GridProps<T> {
  data: T[];
  filteredData: T[];
  colDefs: ColDef[];
}

const DataGrid = <T,>({ filteredData, colDefs }: GridProps<T>) => {

  const gridOptions: GridOptions<T> = {
    theme: myTheme,
    rowData: filteredData,
    columnDefs: colDefs,
    cellSelection: false,

  };

  return (
    <AgGridReact {...gridOptions} />
  );
}

export default DataGrid;