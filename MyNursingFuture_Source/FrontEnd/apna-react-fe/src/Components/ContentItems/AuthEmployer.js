import React, { Component } from 'react';
import { connect } from 'react-redux';
import { closeModal, openModal } from '../../Actions';
class AuthEmployer extends Component {
    constructor(props) {
        super(props)
    }

componentDidMount(){
    if(!this.props.loggedIn){
        this.props.authModal();

    }
}

    render() {
        return (
            null
        );
    }

}

const mapStateToProps = (state) => {
    return {
        loggedIn: state.app.user.loggedIn,
    }
};
const mapDispatchToProps = (dispatch) => {
    return {
        closeModal: () => {
            dispatch(closeModal());
        },
        authModal: () => {
            dispatch(openModal('employerlogin', window.pageYOffset || document.documentElement.scrollTop));
        },
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(AuthEmployer);