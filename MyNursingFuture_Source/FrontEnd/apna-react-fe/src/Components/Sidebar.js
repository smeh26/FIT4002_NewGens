import React, { Component } from 'react';
import Menu from './Menu';
import { connect } from 'react-redux';
import { sidebarToggle, fetchJobListings } from '../Actions';
import JobListings from '../Reducers/JobListings';

class Sidebar extends Component {
  constructor(props) {
    super(props);
    this.state = {
      userType: null,
      section: 0,
    }
  }
  
  componentDidUpdate() {
    const paths = window.location.pathname.split("/");
    const section = paths[paths.length - 1];
    let userType;
    if (this.props.employerName) {
      userType = 'employer';
    } else if (this.props.name) {
      userType = 'nurse';
    }

    if (this.state.section === section) {
      return;
    }

    this.setState({ section, userType });
    this.props.getJobListings();

  }

  render() {
    var cssClass = 'sidebar';
    if (this.props.sidebarShowing) {
      cssClass += ' showing'
    }
    var loginMenu = [];

    if (this.state.section === '15' && !this.props.loggedIn) {
      return null;
    }

    switch (this.state.section) {
      case '1':
        // Nurse Section
        loginMenu = this.props.loggedIn
          ? [
            { href: "#", title: 'Hi, ' + this.props.name, className: 'no-decoration' },
            { href: "/user/quizzes/selfAssessment", title: 'My self-assessments', className: 'sign-in' },
            { href: "/user/quizzes/careerPathways", title: 'Career quiz results', className: 'sign-in' },
            { href: "/user/profile", title: 'Account settings', className: 'sign-in' },
            { href: "#", title: 'Sign out', className: 'sign-in', action: 'logOut' },
            { href: "", spacer: true }
          ] : [
            { href: "#", title: 'Nurse Sign in', className: 'sign-in', action: 'authModal' },
            { href: "/user/register", title: 'Register', className: 'sign-in' },
          ];
        break;
      case '15':
        // Employer Section
        loginMenu = this.props.loggedIn ? [
          { href: "#", title: this.props.employerName, className: 'no-decoration' },
          // { href: "/user/quizzes/selfAssessment", title: 'My self-assessments', className: 'sign-in'},
          // { href: "/user/quizzes/careerPathways", title: 'Career quiz results', className: 'sign-in'},
          // { href: "/user/profile", title: 'Account settings', className: 'sign-in' },
          { href: "#", title: 'Employer Sign out', className: 'sign-in', action: 'logOut' },
          { href: "", spacer: true }
        ] : [];
        break;
      default:
        loginMenu = [
          { href: "#", title: 'Nurse Sign in', className: 'sign-in', action: 'authModal' },
          { href: "/user/register", title: 'Register', className: 'sign-in' },
        ];
    }

    let sidebarMenuItems = [];
    if (this.props.menus && this.props.menus.length > 0 && this.state.userType === 'nurse') {
      sidebarMenuItems = this.props.menus.filter((i) => { return i.type == 'TOP' });
    }

    return (<div className={cssClass}>
      <div className="close-container" onClick={() => this.props.onMenuIconClick()}>
        <span className="oi" data-glyph="x"></span>
      </div>
      <div className="content-container">
        <Menu menuData={loginMenu} />
        {/* <Menu menuData={employerLoginMenu} /> */}
        <Menu menuData={sidebarMenuItems} />
      </div>
    </div>
    );
  }
}

//({sidebarShowing, sidebarMenu, onMenuIconClick, loggedIn}) => 

const mapStateToProps = (state) => {
  return {
    sidebarShowing: state.app.headerFooterMenus.sidebarShowing,
    menus: state.app.framework.menus,
    loggedIn: state.app.user.loggedIn,
    name: state.app.user.user.name,
    employerName: state.app.user.user.employerName,
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMenuIconClick: () => {
      dispatch(sidebarToggle());
    },
    getJobListings: () => {
      dispatch(fetchJobListings());
    }
    
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Sidebar);