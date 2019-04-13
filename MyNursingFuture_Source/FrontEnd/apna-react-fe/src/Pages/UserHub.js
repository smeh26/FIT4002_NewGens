import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import { connect } from 'react-redux';
import { fetchPageData, locationLabelUpdate, sidebarClose } from '../Actions';

class UserHub extends Component {
  constructor(props){
    super(props);
    this.handleChangeInput = this.handleChangeInput.bind(this); 
    this.state = {
      nameEditing: false,
      emailEditing: false,
      passwordEditing: false,
      inputs: {
        name: this.props.user.name,
        email: this.props.user.email
      }
    };
  }
  
  handleChangeInput = function(e){
    var name = e.target.name;
    this.setState({inputs: {[name]: e.target.value}});
  }
  
  componentWillMount() {
    this.props.onMount(this.state.apiEndpoint, 'Account Settings');
  }
  
  render() {
    if (this.props.loading){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else if (this.props.error){
      return <div><h1>Error</h1></div>
    } else if (!this.props.user.isLoggedIn) {
      return (
      <div className="row">
        <div className="col-12">
          <h1>Please log in.</h1>
        </div>
      </div>);
    } else {
      return (
          <div className="user-hub">
            <div className="row list-link-row">
              <div className="col-12">
                <div className=""><Link to={'/user/quizzes/selfAssessment'}>My Self Assessments</Link><span className="oi" data-glyph="chevron-right"></span></div>
              </div>
            </div>
            <div className="row list-link-row">
              <div className="col-12">
                <div className=""><Link to={'/user/quizzes/careerPathways'}>Career Quiz Results</Link><span className="oi" data-glyph="chevron-right"></span></div>
              </div>
            </div>
            <div className="row list-link-row">
              <div className="col-12">
                <div className=""><Link to={'/user/profile'}>Account Settings</Link><span className="oi" data-glyph="chevron-right"></span></div>
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
      dispatch(locationLabelUpdate(title));
      dispatch(sidebarClose());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserHub);

