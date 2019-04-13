import React, { Component } from 'react';
import FooterMenu from './FooterMenu';
import { connect } from 'react-redux';

class Footer extends Component {  
  render() {
    if (this.props.loading){
      return <div></div>;
    } else {
    return (
      <div>
          <div className="row footer">
            <div className="footerMainDivLg">
               <FooterMenu />
            </div>
          </div>
          
      </div>
    );
    }

  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.domainDataLoading
  }
};

export default connect(mapStateToProps)(Footer);