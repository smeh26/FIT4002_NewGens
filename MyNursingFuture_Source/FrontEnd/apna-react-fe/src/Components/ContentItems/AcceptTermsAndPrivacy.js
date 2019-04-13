import React, {Component} from 'react';
import { Link } from 'react-router-dom';
class AcceptTermsAndPrivacy extends Component {
  constructor(){
    super();
    this.state = {
      inputs: {
        accepted: false
      }
    };
    this.handleChangeInput = this.handleChangeInput.bind(this);
  }
  
  handleChangeInput(event){
    let inputs = this.state.inputs;
    if (event.target && event.target.tagName != 'A'){
      inputs.accepted = !this.state.inputs.accepted;
      this.setState({inputs: inputs});
    }

  }

  render () {
    var checked = this.state.inputs.accepted;
    
    return <div className="row">
            <div className="col-sm-12">
              <div className="content-block styled-checkbox centered">
                <label htmlFor="accepted" onClick={this.handleChangeInput}>
                  <span className={'input-styled' + (checked ? ' checked' : '')}>
                  {checked && 
                  <span className="oi" data-glyph="check"></span>
                  }
                  </span>
                  <input type='checkbox' 
                    checked={checked} 
                    name="accepted" />
                  <b>I agree to the <Link to="/sections/10" target="_blank">terms of use</Link> & <Link to="/sections/11" target="_blank">privacy policy</Link></b>
                </label>
              </div>
            </div>
            <div className="col-sm-12">
              <div className="content-block">
              {this.state.inputs.accepted && <Link to="/quiz/selfAssessment/rn" className="btn" onClick={() => {window.scroll(0,0)}}>Start</Link>}
              {!this.state.inputs.accepted&& <a className="btn" >Start</a>}
              </div>
            </div>
          </div>
  }
}

export default AcceptTermsAndPrivacy;

