import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { push } from 'react-router-redux';
import {closeModal, openModal, sidebarToggle, startLogout, setUserLoggedOut, unsetUserData, endLogout, sidebarClose, logOutUser} from '../Actions';

class Menu extends Component {  

  render() {
    return (
      <div className="menu-wrap">
        <ul>
          {this.props.menuData.map((item, itemIndex) => {

            let action = ((item.action && this.props[item.action]) ? this.props[item.action] : false );
            return (
              <li 
                className={
                  item.className ? item.className: '' + ' ' +
                  ((item.submenu && !this.props.isFooterMenu) ? 'submenu' : '') + ' ' +
                  (item.separator ? 'separator' : '')
                } 
                key={itemIndex}>
              { item.href.substring(0,4) === 'http' && 
              <a href={item.href} target="_blank">{item.title}</a>
              }
              { item.href.substring(0,4) != 'http' && !action &&
              <Link onClick={() => {window.scrollTo(0,0), this.props.sideBarClose() }} to={item.href}>{item.title}</Link>
              }
              {action && 
                <a href="#" onClick={action}>{item.title}</a>
              }
              </li>
            );
          }
          )}
        </ul>
      </div>
    )}
}

const mapDispatchToProps = (dispatch, props) => {
  return {
    closeModal: () => {
      dispatch(closeModal());
    },
    authModal: () => {
      dispatch(openModal('login',window.pageYOffset || document.documentElement.scrollTop));
    },
    sideBarClose: () => {
      dispatch(sidebarToggle());
    },
    logOut: function(){
      dispatch(startLogout());
      dispatch(unsetUserData());
      dispatch(logOutUser());
      dispatch(endLogout());
      dispatch(sidebarClose())
      dispatch(push('/'))
    }
  }
}

export default connect(null,mapDispatchToProps)(Menu);