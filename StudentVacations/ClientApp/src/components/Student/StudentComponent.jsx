import React, { useState, useRef, useEffect, useMemo, useCallback} from 'react';
import { AgGridReact } from 'ag-grid-react';
import { Container } from './Container/index';

const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
    },
  };

export const StudentComponent = (props) => {
    const gridRef = useRef(); // Optional - for accessing Grid's API
    const [rowData, setRowData] = useState(); // Set rowData to Array of Objects, one Object per Row
    const [rowDataSelected, setRowDataSelected] = useState(); // Set rowData to Array of Objects, one Object per Row
    const rowHeight = 24;
    const triggerText = 'Создать';
    const childFuncCloseModal = React.useRef(null)

    const onSubmitCreate = (event) => {
        event.preventDefault(event);
        console.log(event.target.name.value);
        console.log(event.target.email.value);
        var student = { firstName: event.target.name.value, email: event.target.email.value  }
        
        //fetch('https://localhost:7084/api/Student', {
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Student', {
            method: 'POST', // or 'PUT'
            body: JSON.stringify(student), 
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            }
        }).then(res => res.json())
        .then(response => {
            console.log('Success:', JSON.stringify(response))
            childFuncCloseModal.current()
            //fetch('https://localhost:7084/api/Student')
            fetch('https://tutorial-v2-react.azurewebsites.net/api/Student')
            .then(result => result.json())
            .then(rowData => setRowData(rowData))
        })
        .catch(error => console.error('Error:', error));
    };

    const onSubmitEdit = (event) => {
        event.preventDefault(event);
        var student = { 
            id: event.target.studentId.value,
            firstName: event.target.firstName.value,
            email: event.target.email.value 
        }
        
        //fetch('https://localhost:7084/api/Student/' + student.id , {
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Student/' + student.id , {
            method: 'PUT', // or 'PUT'
            body: JSON.stringify(student), 
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            }
        })
        //.then(res => res.json())
        .then(response => {
            console.log('Success:', JSON.stringify(response))
            childFuncCloseModal.current()
            //fetch('https://localhost:7084/api/Student')
            fetch('https://tutorial-v2-react.azurewebsites.net/api/Student/')
            .then(result => result.json())
            .then(rowData => setRowData(rowData))
        })
        .catch(error => console.error('Error:', error));
    };

    // Each Column Definition results in one Column.
    const [columnDefs, setColumnDefs] = useState([
        {headerName: 'Ид', field: 'id', width: 10, filter: false},
        {headerName: 'Имя', field: 'firstName', width: 200,  filter: true},
        {headerName: 'email', field: 'email', width: 250,  filter: true},
        // {headerName: 'курсов, шт.', field: 'coursesCount', width: 110, type: 'rightAligned' },
        // {headerName: 'отпусков, шт.', field: 'vacationsCount', width: 140, type: 'numericColumn'}
    ]);

    // DefaultColDef sets props common to all Columns
    const defaultColDef = useMemo( ()=> ({
        // set every column width
        width: 100,
        sortable: true,
        cellStyle: {fontSize: '12px'}
    }));

    // Example of consuming Grid Event
    const cellClickedListener = useCallback( event => {
        console.log('cellClicked', event);
    }, []);

    // Example load data from sever
    useEffect(() => {
        //fetch('https://localhost:7084/api/Student')
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Student')
        .then(result => result.json())
        .then(rowData => setRowData(rowData))
    }, []);

    // Example using Grid's API
    const buttonListener = useCallback( e => {
    gridRef.current.api.deselectAll();
    }, []);

    const onRowSelected = useCallback((event) => {
        if(event.node.isSelected()){
            props.studentIdSelected(Number(event.node.data.id));
            setRowDataSelected(event.node.data);
        }
    }, []);
    
    const onSelectionChanged = useCallback((event) => {
        var rowCount = event.api.getSelectedNodes().length;
        window.alert('selection changed, ' + rowCount + ' rows selected');
    }, []);

    return (
        <div>
            <Container triggerText={triggerText} onSubmitCreate={onSubmitCreate} onSubmitEdit={onSubmitEdit} childFuncCloseModal={childFuncCloseModal} rowDataSelected={rowDataSelected} />
            <div className="ag-theme-alpine" style={{width: 500, height: 300}}>
                <AgGridReact 
                    rowSelection='single'
                    rowHeight={rowHeight}
                    ref={gridRef} // Ref for accessing Grid's API
                    rowData={rowData} // Row Data for Rows
                    columnDefs={columnDefs} // Column Defs for Columns
                    defaultColDef={defaultColDef} // Default Column Properties
                    animateRows={true} // Optional - set to 'true' to have rows animate when sorted
                    onRowSelected={onRowSelected}
                    //onSelectionChanged={onSelectionChanged}
                    //onCellClicked={cellClickedListener} // Optional - registering for Grid Event
                />
            </div>
        </div>
    );
};