// import React, {Component} from 'react';
// import {connect} from 'react-redux';
// import {closeModal, openModal, sidebarToggle, startLogout, setUserLoggedOut, unsetUserData, endLogout, sidebarClose, logOutUser} from '../Actions';

// class AuthEmployer extends Component {
//    render(){
//        return(
//        <button onClick={()=>this.props.authModal()}>sign in</button>
//        );
//    }
//   }

// const mapDispatchToProps = (dispatch, props) => {
//     return {
//       closeModal: () => {
//         dispatch(closeModal());
//       },
//       authModal: () => {
//         dispatch(openModal('login',window.pageYOffset || document.documentElement.scrollTop));
//       },
//       sideBarClose: () => {
//         dispatch(sidebarToggle());
//       },
//       logOut: function(){
//         dispatch(startLogout());
//         dispatch(unsetUserData());
//         dispatch(logOutUser());
//         dispatch(endLogout());
//         dispatch(sidebarClose())
//         dispatch(push('/'))
//       }
//     }
//   }
//   export default connect(mapDispatchToProps)(AuthEmployer);