import React from 'react';
import { connect } from 'react-redux'
import { fetchRegister } from '../Actions'
import { Redirect } from 'react-router'
//register user component
class RegisterUser extends React.Component {
    constructor(props) {
        super(props);
        //state of component to register nurse
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
//action to register the nurse
    handleRegister = () => {
        this.setState({ loading: true });
        const { name, email, password } = this.state;
        this.props.register(name, email, password);
    }
// render modal to display register nurse form
    render() {

        console.log('logged in ', this.props.loggedIn)
        if (this.props.loggedIn) {
            return <Redirect to='/' />;
        }

        if (this.state.loading) {
            return <p>Loading....</p>
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
//props to be used on component from global state
const mapStateToProps = (state) => {
    return {
        userError: state.app.user.error,
        loggedIn: state.app.user.loggedIn,
        // isLoading: state.app.user.isLoading,
    }
};
//register action to be used as a prop in this component from actions
const mapDispatchToProps = (dispatch, props) => {
    return {
        register: function (n, e, p) {
            dispatch(fetchRegister(n, e, p));
        },
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterUser);