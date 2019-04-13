import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import MarkupBlock from './MarkupBlock';
import { fetchContactRequest, fetchFrameworkData, setUserError, unsetUserError } from '../../Actions/index';

class ContactBlock extends Component {
  constructor(){
    super();
    this.state = {
      sector: '',
      responseMessage: '',
      inputs: {
        name: '',
        phone: '',
        email: '',
        message: ''
      }
    };
    
    this.handleChangeSector = this.handleChangeSector.bind(this);
    this.handleChangeInput = this.handleChangeInput.bind(this);
    this.handleSubmitContactRequest = this.handleSubmitContactRequest.bind(this);
    this.handleConfirmSent = this.handleConfirmSent.bind(this);
  }
  
  handleChangeSector(event){
    this.setState({sector: event.target.value});
  }
  handleChangeInput(event){
    const name = event.target.name;
    this.setState({inputs: Object.assign({},this.state.inputs,{[name]: event.target.value})});
  }
  handleSubmitContactRequest(){
    this.setState({responseMessage: ''});
    let self = this;
    var sector;
    if (this.props.readOnlySector){
      sector = this.props.sectors.find((s) => { return s.sectorId == this.props.readOnlySector});
    } else {
      sector = this.props.sectors.find((s) => { return s.sectorId == this.state.sector});
    }
    this.props.sendContactRequest(this.state.inputs.name, this.state.inputs.email, this.state.inputs.phone, this.state.inputs.message, sector.name || 'undefined sector')
      .then(function(r){
        if (r){
          self.setState({inputs: {name: '',phone: '',email: '',message: ''}});
          self.setState({responseMessage: 'Thank you, your message has been sent.'});
        } else {
          self.props.setUserError('Your message failed to send.');
        }
      });
  }
  handleConfirmSent(){
    this.setState({responseMessage: ''});
    this.setState({inputs: {name: '',phone: '',email: '',message: ''}});
  }

  render () {
    var sector;
    if (this.props.readOnlySector){
      sector = this.props.sectors.find((s) => { return s.sectorId == this.props.readOnlySector});
    } else {
      sector = this.props.sectors.find((s) => { return s.sectorId == this.state.sector});
    }
    

    return <div className="row">
            <div className="col-sm-12">
              <div className="content-block">
                {!this.props.readOnlySector && <div className="row">
                  <div className="offset-sm-3 col-sm-6">
                    <select className="select" value={this.state.sector} onChange={this.handleChangeSector}>
                      <option value='' >Select your sector</option>
                      {this.props.sectors && this.props.sectors.map((s) => {
                        return (
                          <option value={s.sectorId} key={s.sectorId}>{s.title}</option>
                        )
                      })}
                    </select>
                  </div>
                </div>}
                {sector && 
                <div className="offset-sm-3 col-sm-6 push-top">
                  <MarkupBlock content={{text: sector.sectorRn.contactText}}/>
                  {!this.state.responseMessage && <div className="input-block">
                    <input type="text" value={this.state.inputs.name} name="name" placeholder="Your Name" onChange={this.handleChangeInput} />
                    <input type="text" value={this.state.inputs.phone} name="phone" placeholder="Phone" onChange={this.handleChangeInput} />
                    <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                    <textarea value={this.state.inputs.message} name="message" placeholder="Your question" onChange={this.handleChangeInput} />
                  </div>}
                  {this.state.responseMessage &&
                    <h4>{this.state.responseMessage}</h4>
                  }
                  {this.props.userError && 
                    <p>{this.props.userError}</p>
                  }
                  {!this.state.responseMessage && <button className="btn" onClick={this.handleSubmitContactRequest}>Submit</button> }
                  {this.state.responseMessage && <button className="btn" onClick={this.handleConfirmSent}>Done</button> }
                </div>
                }
              </div>
            </div>
          </div>
  }
}

const mapStateToProps = (state) => {
  return {
    sectors: state.app.framework.sectors,
    userError: state.app.user.error
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function(title) {
      dispatch(fetchFrameworkData());
    },
    sendContactRequest: function(name,email,phone,message,sectorName){
      dispatch(unsetUserError());
      if (!name || !email || !phone || !message){
        dispatch(setUserError('Please input all the required information.'));
      }
      return dispatch(fetchContactRequest(name,email,phone,message,sectorName));
    },
    setUserError: function(message){
      dispatch(setUserError(message));
    }
  }
}

ContactBlock = connect(mapStateToProps, mapDispatchToProps)(ContactBlock);
export default ContactBlock;

