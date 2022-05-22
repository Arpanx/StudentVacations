import React from "react";

class FormEdit extends React.Component {
  constructor(props) {
    super(props);

    let id = 0; // courseId
    let studentId = 0;  
    let name = '';
    let weekNumberStart = 0;
    let weekNumberEnd = 0;  
      

    if(props.rowDataSelected && props.rowDataSelected.id){
      id = props.rowDataSelected.id
    }
    
    if(props.rowDataSelected && props.rowDataSelected.studentId){
      studentId = props.rowDataSelected.studentId
    }

    if(props.rowDataSelected && props.rowDataSelected.name){
      name = props.rowDataSelected.name
    }

    if(props.rowDataSelected && props.rowDataSelected.weekNumberStart){
      weekNumberStart = props.rowDataSelected.weekNumberStart
    }
    
    if(props.rowDataSelected && props.rowDataSelected.weekNumberEnd){
      weekNumberEnd = props.rowDataSelected.weekNumberEnd      
    } 

    this.state = {
      id: id, 
      studentId: studentId,
      name: name,
      weekNumberStart: weekNumberStart,
      weekNumberEnd: weekNumberEnd,
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({[event.target.id]: event.target.value});
  }

  handleSubmit(event) {
  }

  render() {

    return (
      <form onSubmit={this.props.onSubmit}>
        <input type="hidden" id="id" defaultValue={this.state.id} />
        <input type="hidden" id="studentId" defaultValue={this.state.studentId} />

        <div className="form-group">
          <label htmlFor="name">Имя</label>
          <input className="form-control" id="name" value={this.state.name} onChange={this.handleChange}/>
        </div>

        <div className="form-group">
          <label htmlFor="weekNumberStart">Номер недели начала</label>
          <input className="form-control" id="weekNumberStart" value={this.state.weekNumberStart} onChange={this.handleChange}/>
        </div>

        <div className="form-group">
          <label htmlFor="weekNumberEnd">Номер недели окончание</label>
          <input className="form-control" id="weekNumberEnd" value={this.state.weekNumberEnd} onChange={this.handleChange}/>
        </div>

        <br />
        <div className="form-group">
          <button className="form-control btn btn-primary" type="submit">
            Edit
          </button>
        </div>
      </form>
    );
  };
}

export default FormEdit;
