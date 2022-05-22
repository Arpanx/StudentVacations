import React, { useState } from "react";
import { PageLayout } from "./components/PageLayout";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { loginRequest } from "./authConfig";
import Button from "react-bootstrap/Button";
import { ProfileData } from "./components/ProfileData";
import { callMsGraph } from "./graph";
import { StudentComponent } from "./components/Student/StudentComponent";
import { CourseComponent } from "./components/Course/CourseComponent";
import { VacationComponent } from "./components/Vacation/VacationComponent";
import './App.css';

import 'ag-grid-community/dist/styles/ag-grid.css'; // Core grid CSS, always needed
import 'ag-grid-community/dist/styles/ag-theme-alpine.css'; // Optional theme CSS

function App() {
    const [studentIdSelected, setStudentIdSelected] = useState(0);
  return (
      <PageLayout>
         <AuthenticatedTemplate>
            <ProfileContent />
        </AuthenticatedTemplate>
        <UnauthenticatedTemplate>
            <div className="container-xxl">
                <div className="row">
                    <div className="col-md-5">
                        <StudentComponent studentIdSelected={setStudentIdSelected} />
                    </div>
                    <div className="col-md-6">
                        <CourseComponent studentIdSelected={studentIdSelected}/>
                    </div>
                </div>
                <div className="row margin-grid">
                    <div className="col-md-5">                        
                    </div>
                    <div className="col-md-6">
                        <VacationComponent studentIdSelected={studentIdSelected}/>
                    </div>
                </div>
            </div>
        </UnauthenticatedTemplate>
      </PageLayout>
  );
}

function ProfileContent() {
  const { instance, accounts } = useMsal();
  const [graphData, setGraphData] = useState(null);

  const name = accounts[0] && accounts[0].name;

  function RequestProfileData() {
      const request = {
          ...loginRequest,
          account: accounts[0]
      };

      // Silently acquires an access token which is then attached to a request for Microsoft Graph data
      instance.acquireTokenSilent(request).then((response) => {
          callMsGraph(response.accessToken).then(response => setGraphData(response));
      }).catch((e) => {
          instance.acquireTokenPopup(request).then((response) => {
              callMsGraph(response.accessToken).then(response => setGraphData(response));
          });
      });
  }

  return (
      <>
          <h5 className="card-title">Welcome {name}</h5>
          {graphData ? <ProfileData graphData={graphData} />  :  <Button variant="secondary" onClick={RequestProfileData}>Request Profile Information</Button>
          }
      </>
  );
};


export default App;