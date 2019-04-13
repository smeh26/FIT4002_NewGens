import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Accordion from '../Components/ContentItems/Accordion';
import MarkupBlock from '../Components/ContentItems/MarkupBlock';
import Heading from '../Components/ContentItems/Heading';
import RolesLinkList from '../Components/ContentItems/RolesLinkList';


import { connect } from 'react-redux';
import { fetchPageData, fetchFrameworkData, locationLabelUpdate } from '../Actions';

class RolePage extends Component {
  componentWillMount() {
      this.props.onMount();
       
  }
  
  render() {
    var roleId = this.props.match.params.id;
    var role;
    if (this.props.content){
        role = this.props.content.find((r) => { return r.roleId == roleId });
    }
    
    if (this.props.loading || !role){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else if (this.props.error){
      return <div><h1>Error</h1></div>
    } else {   
      var headingContent = {
        title: role.name
      };
      var headingAccordionTitle = 'What is a ' + role.linkName+'?'
      
      var whatIsContent = {
        text: role.whatIs
      };
      var whatIs = <MarkupBlock content={whatIsContent} />
      
      var whatIsTheirRoleContent = {
        text: role.whatIsTheirRole
      };
      var whatIsTheirRole = <MarkupBlock content={whatIsTheirRoleContent} />  
      
      var furtherInformationContent = {
        text: role.furtherInformation
      }
      var furtherInformation = <MarkupBlock content={furtherInformationContent} />

      var careerPathwaysContent = {
          text: role.pathways
      }
      var careerPathways = <MarkupBlock content={careerPathwaysContent} />

       return (
        <div className="role-page-wrapper">
          <Heading content={headingContent}/>
          <Accordion child={whatIs} title={headingAccordionTitle} open="true"/>
          <Accordion child={whatIsTheirRole} title="What is their role within primary health care? " />
          <Accordion child={furtherInformation} title="Further information " />
          <Accordion child={careerPathways} title="Career pathways" />
          <hr />
          <RolesLinkList filter={[roleId]}/>
        </div>
      )
    }
  }
  
}

const mapStateToProps = (state) => {
  return {
    content: state.app.framework.roles,
    loading: state.app.framework.rolesDataLoading,
    error: state.app.content.error
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function(title) {
      dispatch(fetchFrameworkData('roles'));
      dispatch(locationLabelUpdate('Sectors & Roles'));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(RolePage);

