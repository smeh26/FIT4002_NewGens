import React, {Component} from 'react';
import { connect } from 'react-redux';
import { sidebarToggle } from '../Actions'
import { Link } from 'react-router-dom';


class Header extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isHome: false,
    }
  }

// componentWillMount() {
//   // if (window.location.pathname === "/"){
//   //   this.setState({isHome: true})
//   // }
//   console.log(window.location);
// }


  render() {
    const backgroundClass = (this.props.locationLabel == 'Career Advice'
      ? 'article-gradient-background'
      : '');

    const  {onMenuIconClick} = this.props;

    return (
      <div>
        <div className="header-bar">
          {/*start mobile menu*/}
          <div className="container-fluid">
            <div className={'row ' + backgroundClass}>
              <div className="header-col home-col">
                <Link to="/">
                  <img src="/img/MNF_tree-White.png" />
                </Link>
              </div>
              <div className="header-col hidden-xs">
                <span className="page-title">{this.props.locationLabel}</span>
              </div>
              {window.location.pathname !== "/" && <div className="header-col menu-col">
                <div onClick={() => onMenuIconClick()}>
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
  return { locationLabel: state.app.headerFooterMenus.locationLabel, loggedIn: state.app.user.loggedIn }
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