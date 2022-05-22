import React from 'react';
const TriggerStudentEdit = ({ triggerText, buttonRef, showModal }) => {
  return (
    <button
      className="btn btn-sm btn-light center modal-button button-margin"
      ref={buttonRef}
      onClick={showModal}
    >
      {triggerText}
    </button>
  );
};
export default TriggerStudentEdit;