import React, { useState, useRef, useEffect, useMemo, useCallback} from 'react';
import { AgGridReact } from 'ag-grid-react'; // the AG Grid React Component
import { Container } from './Container/index';

export const CourseComponent = (props) => {
    const gridRef = useRef(); // Optional - for accessing Grid's API
    const [rowData, setRowData] = useState(); // Set rowData to Array of Objects, one Object per Row
    const rowHeight = 24;
    const [rowDataSelected, setRowDataSelected] = useState(); // Set rowData to Array of Objects, one Object per Row
    const [studentIdSelected, setStudentIdSelected] = useState(props.studentIdSelected); // Set rowData to Array of Objects, one Object per Row
    
    const triggerText = 'Создать';
    const childFunc = React.useRef(null)
    
    useEffect(() => {

        //fetch('https://localhost:7084/api/Course/byStudentId?id=' + props.studentIdSelected)
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Course/byStudentId?id=' + props.studentIdSelected)
        .then(result => result.json())
        .then(rowData => setRowData(rowData))

    }, [props.studentIdSelected]);

    // Each Column Definition results in one Column.
    const [columnDefs, setColumnDefs] = useState([
        {headerName: 'Ид', field: 'id', width: 10, filter: false},
        {headerName: 'Курс', field: 'name', width: 200, editable: false },
        {headerName: 'Начало', field: 'weekNumberStart', width: 140,  filter: true, type: 'rightAligned'},
        {headerName: 'Окончание', field: 'weekNumberEnd', width: 140,  filter: true, type: 'rightAligned'},
    ]);

    // DefaultColDef sets props common to all Columns
    const defaultColDef = useMemo( ()=> ({
        // set every column width
        width: 100,
        sortable: true,
        cellStyle: {fontSize: '12px'}
    }));

    const onRowSelected = useCallback((event) => {
        if(event.node.isSelected()){
            //props.studentIdSelected(Number(event.node.data.id));
            setRowDataSelected(event.node.data);
        }
    }, []);    
    
    const onSubmitCreate = (event) => {
        event.preventDefault(event);
        var course = { 
            studentId: event.target.studentId.value,
            weekNumberStart: event.target.weekNumberStart.value,
            weekNumberEnd: event.target.weekNumberEnd.value,
            name: event.target.name.value,
        }
         
        //fetch('https://localhost:7084/api/Course', {
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Course', {
            method: 'POST', // or 'PUT'
            body: JSON.stringify(course), 
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            }
        })
        .then(res => res.json())
        .then(response => {
            if (response.status == 400){
                alert('Bad Request')

                return;
            }
            //console.log('Success:', JSON.stringify(response))
            childFunc.current()
            //fetch('https://localhost:7084/api/Course/byStudentId?id='+ course.studentId)
            fetch('https://tutorial-v2-react.azurewebsites.net/api/Course/byStudentId?id='+ course.studentId)
            .then(result => result.json())
            .then(rowData => setRowData(rowData))
        })
        .catch(error => console.error('Error:', error));
    };

    const onSubmitEdit = (event) => {
        event.preventDefault(event);
        var course = { 
            id: event.target.id.value,
            studentId: event.target.studentId.value,
            weekNumberStart: event.target.weekNumberStart.value,
            weekNumberEnd: event.target.weekNumberEnd.value,
            name: event.target.name.value,
        }

        //fetch('https://localhost:7084/api/Course/' + course.id , {
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Course/' + course.id , {            
            method: 'PUT', // or 'PUT'
            body: JSON.stringify(course), 
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            }
        })
        //.then(res => res.json())
        .then(response => {
            if (response.status == 400){
                alert('Bad Request')
                
                return;
            }
            
            //console.log('Success:', JSON.stringify(response))
            childFunc.current() // Дергаем метод чтобы закрыть модальное окно
            //fetch('https://localhost:7084/api/Course/byStudentId?id='+ course.studentId)
            fetch('https://tutorial-v2-react.azurewebsites.net/api/Course/byStudentId?id='+ course.studentId)
            .then(result => result.json())
            .then(rowData => setRowData(rowData))
        })
        .catch(error => console.error('Error:', error));
    };

    const onRefresh = (event) => {
        event.preventDefault(event);
        //fetch('https://localhost:7084/api/Course/byStudentId?id='+ course.studentId)
        fetch('https://tutorial-v2-react.azurewebsites.net/api/Course/byStudentId?id='+ props.studentIdSelected)
        .then(result => result.json())
        .then(rowData => setRowData(rowData))
    };

    return (
        <div>
            <Container triggerText={triggerText} 
            onSubmitCreate={onSubmitCreate} 
            onSubmitEdit={onSubmitEdit} 
            childFunc={childFunc} 
            rowDataSelected={rowDataSelected} 
            studentIdSelected={props.studentIdSelected} 
            onRefresh = {onRefresh}
            />

            <div className="ag-theme-alpine" style={{width: 550, height: 300}}>

                <AgGridReact 
                    rowSelection='single'
                    rowHeight={rowHeight}
                    ref={gridRef} // Ref for accessing Grid's API
                    rowData={rowData} // Row Data for Rows
                    columnDefs={columnDefs} // Column Defs for Columns
                    defaultColDef={defaultColDef} // Default Column Properties
                    animateRows={true} // Optional - set to 'true' to have rows animate when sorted
                    onRowSelected={onRowSelected}
                />
            </div>
        </div>
    );
    
};