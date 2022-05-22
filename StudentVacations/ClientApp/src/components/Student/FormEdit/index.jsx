import React from "react";

class FormEdit extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      firstName: props.rowDataSelected.firstName, 
      email: props.rowDataSelected.email
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
        <input type="hidden" id="studentId" defaultValue={this.props.rowDataSelected.id} />
        <div className="form-group">
          <label htmlFor="firstName">Name</label>
          <input className="form-control" id="firstName" value={this.state.firstName} onChange={this.handleChange}
            />
        </div>
        <div className="form-group">
          <label htmlFor="email">Email address</label>
          <input
            type="email"
            className="form-control"
            id="email"
            placeholder="name@example.com"
            value={this.state.email} onChange={this.handleChange}
          />
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
