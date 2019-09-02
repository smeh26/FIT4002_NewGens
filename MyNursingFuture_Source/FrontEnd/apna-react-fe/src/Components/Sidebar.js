import React, { Component }  from 'react';
import Menu from './Menu';
import { connect } from 'react-redux';
import { sidebarToggle } from '../Actions';

class Sidebar extends Component{
  constructor(props){
    super(props);
  }
  render(){
    var cssClass = 'sidebar';
    if (this.props.sidebarShowing){
      cssClass += ' showing'
    }
    var loginMenu = [];
    if (this.props.loggedIn){
      loginMenu = [
        { href: "#", title: 'Hi, ' + this.props.name, className: 'no-decoration'},
        { href: "/user/quizzes/selfAssessment", title: 'My self-assessments', className: 'sign-in'},
        { href: "/user/quizzes/careerPathways", title: 'Career quiz results', className: 'sign-in'},
        { href: "/user/profile", title: 'Account settings', className: 'sign-in'},
        { href: "#", title: 'Sign out', className: 'sign-in', action: 'logOut'},
        { href: "", spacer: true }
        ]
    } else {
      loginMenu = [
        { href: "#", title: 'Sign in', className: 'sign-in', action: 'authModal'},
        { href: "/user/register", title: 'Register', className:'sign-in'},
        { href: "", spacer: true }
      ];
    }
    
    let sidebarMenuItems = [];
    if (this.props.menus && this.props.menus.length > 0){
      sidebarMenuItems = this.props.menus.filter((i) => { return i.type == 'TOP'});
    }
    
    return ( <div className={cssClass}>
      <div className="close-container" onClick={() => this.props.onMenuIconClick()}>
        <span className="oi" data-glyph="x"></span>
      </div>
      <div className="content-container">
        <Menu menuData={loginMenu} />
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
    name: state.app.user.user.name
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMenuIconClick: () => {
      dispatch(sidebarToggle());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Sidebar);