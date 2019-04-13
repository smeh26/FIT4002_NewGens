import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';

const DomainListTitle = (props) => (
    <div className="row">
    <div className="col-sm-12">
      <div className="content-block">
        
        {props.domains && props.content && 
          <div>
            <h1>The {props.domains.filter((d) => { return d.framework == props.content.framework}).length} domains of primary health care for {props.content.framework == 'en' ? 'ENs' : 'RNs' }</h1>
            
          </div>
        }
        
      </div>
    </div>
  </div>
);

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.domainDataLoading,
    domains: state.app.framework.domain
  }
};

export default connect(mapStateToProps)(DomainListTitle);