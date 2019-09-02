import React from 'react';
import { connect } from 'react-redux'
import { fetchRegister } from '../Actions'
import { Redirect } from 'react-router'

class RegisterUser extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            email: '',
            password: '',
            loading: false,
        }
    }

    handleChangeInput = e => {
        if (!e.target.name) {
            return;
        }

        this.setState({
            [e.target.name]: e.target.value,
        })
    }

    handleRegister = () => {
        this.setState({ loading: true });
        const { name, email, password } = this.state;
        this.props.register(name, email, password);
    }

    render() {

        console.log('logged in ', this.props.loggedIn)
        if (this.props.loggedIn) {
            return <Redirect to='/' />;
        }

        if (this.state.loading) {
            return <p>Loadinig....</p>
        }

        const { name, email, password } = this.state;
        return (
            <div className="container m-5">
                <h1>Create a profile to save your report</h1>

                <div className="input-block">
                    <input type="text" value={name} name="name" placeholder="Name" onChange={this.handleChangeInput} />
                    <input type="text" value={email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                    <input type="password" value={password} name="password" placeholder="Password" onChange={this.handleChangeInput} />
                </div>
                {this.props.userError &&
                    <p>{this.props.userError}</p>
                }
                <button className="btn" onClick={this.handleRegister}>Create profile</button>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        userError: state.app.user.error,
        loggedIn: state.app.user.loggedIn,
        // isLoading: state.app.user.isLoading,
    }
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        register: function (n, e, p) {
            dispatch(fetchRegister(n, e, p));
        },
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterUser);