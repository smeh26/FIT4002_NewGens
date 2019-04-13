import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { openModal } from '../../Actions';

class ContactBlock extends Component {
  constructor(){
    super();
    this.state = {
      sector: '',
      sectors: {},
      inputs: {
        email: ''
      }
    };

    this.handleChangeInput = this.handleChangeInput.bind(this);
  }
  
  handleChangeInput(event){
    const name = event.target.name;
    this.setState({inputs: {[name]: event.target.value}});
  }

  render () {
    return <div className="row gradient-bg-one">
          <div className="col-sm-12">
            <div className="content-block">
              <h1>Sign up for APNA Connect, our monthly newsletter</h1>
              <div className="row">
                <div className="offset-sm-3 col-sm-6">
                  <a className="btn" href="https://www.apna.asn.au/workflows/subscribe" target="_blank">Sign up</a>
                </div>
              </div>
            </div>
          </div>
        </div>
  }
}
const mapStateToProps = (state) => {
  return {
    sectors: state.app.contact.sectors || {}
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    newsLetterSignup: function(){
      dispatch(openModal('newsletterSignup', window.pageYOffset || document.documentElement.scrollTop));
    }
  }
}

ContactBlock = connect(mapStateToProps, mapDispatchToProps)(ContactBlock);
export default ContactBlock;

