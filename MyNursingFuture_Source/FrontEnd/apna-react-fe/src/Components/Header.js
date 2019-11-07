import React, { Component } from 'react';
import { connect } from 'react-redux';
import { sidebarToggle } from '../Actions'
import { Link } from 'react-router-dom';


class Header extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    let homeUrl = '/';
    if (this.props.loggedIn) {
      if (this.props.isEmployer) {
        homeUrl = '/sections/15';
      } else {
        // Nurse Portal
        homeUrl = '/sections/1';
      }
    }

    const backgroundClass = (this.props.locationLabel == 'Career Advice'
      ? 'article-gradient-background'
      : '');

    const showMenu = window.location.pathname !== "/";

    return (
      <div>
        <div className="header-bar">
          {/*start mobile menu*/}
          <div className="container-fluid">
            <div className={'row ' + backgroundClass}>
              <div className="header-col home-col">
                <Link to={homeUrl}>
                  <img src="/img/MNF_tree-White.png" />
                </Link>
              </div>
              <div className="header-col hidden-xs">
                <span className="page-title">{this.props.locationLabel}</span>
              </div>
              {showMenu && <div className="header-col menu-col">
                <div onClick={() => this.props.onMenuIconClick()}>
                  <span className="oi" data-glyph="menu"></span>
                </div>
              </div>}
            </div>
          </div>
          {/*end of mobile menu*/}
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    locationLabel: state.app.headerFooterMenus.locationLabel,
    loggedIn: state.app.user.loggedIn,
    isEmployer: state.app.user.employerName,
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMenuIconClick: () => {
      dispatch(sidebarToggle());
    }
  }
}

Header = connect(mapStateToProps, mapDispatchToProps)(Header);

export default Header;