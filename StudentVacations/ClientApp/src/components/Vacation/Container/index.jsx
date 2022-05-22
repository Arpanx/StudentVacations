import React, { Component, useState, useRef, useEffect, useMemo, useCallback } from 'react';
import { Modal } from '../Modal';
import TriggerButtonCreate from '../TriggerButtonStudentCreate';
import TriggerButtonEdit from '../TriggerButtonStudentEdit';
export class Container extends Component {
  constructor(props) {
    super(props);
    // Don't call this.setState() here!
    //this.state = { studentIdSelected: props.studentIdSelected };
    this.state = { 
      isShown: false, 
      isCreate: false, 
      studentIdSelected: props.studentIdSelected
    };

    props.childFunc.current = this.closeModal
  }

  showModalCreate = () => {
    this.setState({ isShown: true }, () => {
      this.closeButton.focus();
    });
    this.setState({ isCreate: true }, () => {
      //this.closeButton.focus();
    });
    this.toggleScrollLock();
  };
  showModalEdit = () => {
    this.setState({ isShown: true }, () => {
      this.closeButton.focus();
    });
    this.setState({ isCreate: false }, () => {
      //
      //this.closeButton.focus();
    });
    this.toggleScrollLock();
  };
  closeModal = () => {
    this.setState({ isShown: false });
    this.TriggerButton.focus();
    this.toggleScrollLock();
  };
  onKeyDown = (event) => {
    if (event.keyCode === 27) {
      this.closeModal();
    }
  };
  onClickOutside = (event) => {
    // if (this.modal && this.modal.contains(event.target)) return;
    // this.closeModal();
  };

  toggleScrollLock = () => {
    document.querySelector('html').classList.toggle('scroll-lock');
  };
  
  render() {

    return (
      <React.Fragment>
        <TriggerButtonCreate
          showModal={this.showModalCreate}
          buttonRef={(n) => (this.TriggerButton = n)}
          triggerText={this.props.triggerText}
        />
        <TriggerButtonEdit
          showModal={this.showModalEdit}
          buttonRef={(n) => (this.TriggerButton = n)}
          triggerText="Edit"
        />
        {this.state.isShown ? (
          <Modal
            onSubmitCreate={this.props.onSubmitCreate}
            onSubmitEdit={this.props.onSubmitEdit}
            modalRef={(n) => (this.modal = n)}
            buttonRef={(n) => (this.closeButton = n)}
            closeModal={this.closeModal}
            onKeyDown={this.onKeyDown}
            onClickOutside={this.onClickOutside}
            isCreate={this.state.isCreate}
            rowDataSelected = {this.props.rowDataSelected}
            studentIdSelected  = {this.props.studentIdSelected}
          />
        ) : null}
      </React.Fragment>
    );
  }
}

export default Container;