import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import { connect } from 'react-redux';
import { fetchPageData, locationLabelUpdate, sidebarClose, fetchUpdateUserDetails, fetchUpdateUserPassword, setUserError, unsetUserError } from '../Actions';

class UserProfile extends Component {
  constructor(props){
    super(props);
    this.handleChangeInput = this.handleChangeInput.bind(this); 
    this.handleToggleEditing = this.handleToggleEditing.bind(this);
    this.handleSaveChange = this.handleSaveChange.bind(this);
    this.handleChangePassword = this.handleChangePassword.bind(this);
    this.state = {
      nameEditing: false,
      emailEditing: false,
      minSalaryEditing: false,
      locationPrefEditing: false,
      isLookingForJobEditing: false,
      passwordEditing: false,
      inputs: {
        name: this.props.user.name,
        email: this.props.user.email,
        newPassword: '',
        oldPassword: '',
        minSalaryReq: '',
        locationPref: '',
        isLookingForJob: ''
      }
    };
  }
  
  handleChangeInput = function(e){
    var name = e.target.name;
    this.setState({inputs: Object.assign({},this.state.inputs,{[name]: e.target.value})});
  }
  
  handleChangePassword = function(){
    if (this.state.inputs.newPassword && this.state.inputs.newPassword.length >= 6){
      if (this.state.inputs.newPassword != this.state.inputs.oldPassword){
        this.props.saveChangePassword(this.state.inputs.oldPassword,this.state.inputs.newPassword);
        this.setState({inputs: Object.assign({},this.state.inputs,{oldPassword:'', newPassword: ''})});
      } else {
        this.props.setUserError("New password must be different.");
      }
    } else {
      this.props.setUserError("Password must be at least 6 characters long.");
    }
  }
  
  handleToggleEditing = function(type){
    switch(type){
      case 'name':
        this.setState({nameEditing: !this.state.nameEditing});
        break;
      case 'email':
        this.setState({emailEditing: !this.state.emailEditing});
        break;
      case 'password':
        if (this.state.passwordEditing){
          if (!this.state.inputs.newPassword ||  this.state.inputs.newPassword.length < 6){ this.props.setUserError("Password must be at least 6 characters long."); break; }
          if (this.state.inputs.newPassword === this.state.inputs.oldPassword){ this.props.setUserError("New password must be different."); break;}
        }
        this.setState({passwordEditing: !this.state.passwordEditing});
        break;
      case 'passwordCancel':
        this.setState({passwordEditing: false});
        this.setState({inputs: Object.assign({},this.state.inputs,{oldPassword: '', newPassword: ''})});
        this.props.unsetUserError();
      case 'minSalaryReq' :
        this.setState({minSalaryEditing: !this.state.minSalaryEditing});
        break;
      case 'locationPref' :
        this.setState({locationPrefEditing: !this.state.locationPrefEditing});
        break;
      case 'isLookingForJob' :
        this.setState({isLookingForJobEditing: !this.state.isLookingForJobEditing});
        break;
      default:
        break;
    }
  }
  
  handleSaveChange = function(){
    if (this.props.user.name != this.state.inputs.name || this.props.user.email != this.state.inputs.email  || this.props.user.locationPref != this.state.inputs.locationPref || this.props.user.minSalaryReq != this.state.inputs.minSalaryReq  || this.props.user.isLookingForJob != this.state.inputs.isLookingForJob){
      this.props.saveChangeToProfile(this.state.inputs.name, this.state.inputs.email, this.state.inputs.minSalaryReq, this.state.locationPref, this.state.isLookingForJob);
    }
  }
  
  componentWillMount() {
    this.props.onMount(this.state.apiEndpoint, 'Account Settings');
  }
  
  render() {
    if (this.props.loading || !this.props.user.name){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else {
      return (
      <div className="profile-page">
        <div className="row">
          <div className="col-12">
            <h1>Name:</h1>
          </div>
          {this.state.nameEditing && 
          <div className="col-12">
            <div className="row">
              <div className="col-6">
                <div className="input-block">
                  <input type="text" value={this.state.inputs.name} name="name" placeholder="Name" onChange={this.handleChangeInput} />
                </div>
              </div>
              <div className="col-6">
                <button className="btn" onClick={() => {this.handleToggleEditing('name');this.handleSaveChange();}}>Save</button>
              </div>
            </div>
          </div>
          }
          {!this.state.nameEditing && 
          <div className="col-12">
            <div className="row">
              <div className="col-6"><p>{this.state.inputs.name}</p></div>
              {!this.props.user.apnaUser && <div className="col-6"><button className="btn" onClick={() => {this.handleToggleEditing('name')}}>Edit</button></div>}
            </div>
          </div>
          }
        </div>
        
        <div className="row">
          <div className="col-12">
            <h1>Email Address:</h1>
          </div>
          {this.state.emailEditing && 
          <div className="col-12">
          <div className="row">
            <div className="col-6">
              <div className="input-block">
                <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
              </div>
            </div>
            <div className="col-6">
              <button className="btn" onClick={() => {this.handleToggleEditing('email');this.handleSaveChange();}}>Save</button>
            </div>
          </div>
          </div>
          }
          {!this.state.emailEditing && 
          <div className="col-12">
            <div className="row">
              <div className="col-6"><p>{this.state.inputs.email}</p></div>
              {!this.props.user.apnaUser && <div className="col-6"><button className="btn" onClick={() => {this.handleToggleEditing('email')}}>Edit</button></div>}
            </div>
          </div>
          }
        </div>
        
        <div className="row">
          <div className="col-12">
            <h1>Minimum Salary Requirement ($):</h1>
          </div>
          {this.state.minSalaryEditing && 
          <div className="col-12">
          <div className="row">
            <div className="col-6">
              <div className="input-block">
                <input type="text" value={this.state.inputs.minSalaryReq} name="minSalaryReq" placeholder="Minimum Salary Requirement in $" onChange={this.handleChangeInput} />
              </div>
            </div>
            <div className="col-6">
              <button className="btn" onClick={() => {this.handleToggleEditing('minSalaryReq');this.handleSaveChange();}}>Save</button>
            </div>
          </div>
          </div>
          }
          {!this.state.minSalaryEditing && 
          <div className="col-12">
            <div className="row">
              <div className="col-6"><p>{this.state.inputs.minSalaryReq}</p></div>
              {!this.props.user.apnaUser && <div className="col-6"><button className="btn" onClick={() => {this.handleToggleEditing('minSalaryReq')}}>Edit</button></div>}
            </div>
          </div>
          }
        </div>
        
        <div className="row">
          <div className="col-12">
            <h1>Are you looking for Job?</h1>
          </div>
          {this.state.minSalaryEditing && 
          <div className="col-12">
          <div className="row">
            <div className="col-6">
              <div className="input-block">
                <input type="checkbox" value={this.state.inputs.isLookingForJob} name="isLookingForJob" onChange={this.handleChangeInput} />
              </div>
            </div>
            <div className="col-6">
              <button className="btn" onClick={() => {this.handleToggleEditing('isLookingForJob');this.handleSaveChange();}}>Save</button>
            </div>
          </div>
          </div>
          }
          {!this.state.isLookingForJobEditing && 
          <div className="col-12">
            <div className="row">
              <div className="col-6"><p>{this.state.inputs.isLookingForJob}</p></div>
              {!this.props.user.apnaUser && <div className="col-6"><button className="btn" onClick={() => {this.handleToggleEditing('isLookingForJob')}}>Edit</button></div>}
            </div>
          </div>
          }
        </div>
        <div className="row">
          {!this.props.user.apnaUser &&<div className="col-12">
            <h1>Password:</h1>
          </div>}
          {this.state.passwordEditing && 
          <div className="col-12">
          <div className="row">
            <div className="col-6">
              <div className="input-block">
                <input type="text" value={this.state.inputs.oldPassword} name="oldPassword" placeholder="Current Password" onChange={this.handleChangeInput} />
                <input type="text" value={this.state.inputs.newPassword} name="newPassword" placeholder="New Password" onChange={this.handleChangeInput} />
              </div>
            </div>
            <div className="col-6">
              <button className="btn" onClick={() => {this.handleChangePassword();this.handleToggleEditing('password');}}>Save</button>
              <button className="btn" onClick={() => {this.handleToggleEditing('passwordCancel');}}>Cancel</button>

            </div>
          </div>
          </div>
          }
          {!this.state.passwordEditing && !this.props.user.apnaUser && 
          <div className="col-12">
            <div className="row">
              <div className="col-6"><p>****</p></div>
               <div className="col-6"><button className="btn" onClick={() => {this.handleToggleEditing('password')}}>Edit</button></div>
            </div>
          </div>
          }
        </div>

        <div className="row">
          <div className="col-12">
          {this.props.error &&
          <p>{this.props.error}</p>
          }
          </div>
        </div>
      </div>
      );
    }
  }
}

const mapStateToProps = (state) => {
  return {
    user: state.app.user.user,
    loading: state.app.user.isLoading,
    error: state.app.user.error,
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: function(endpoint, title) {
      dispatch(unsetUserError());
      dispatch(locationLabelUpdate(title));
      dispatch(sidebarClose());
    },
    saveChangeToProfile: function(name,email){
      dispatch(fetchUpdateUserDetails(name,email));
    },
    saveChangePassword: function(pass,newPass){
      dispatch(fetchUpdateUserPassword(pass,newPass));
    },
    setUserError: function(message){
      dispatch(setUserError(message));
    },
    unsetUserError: function(){
      dispatch(unsetUserError());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);

