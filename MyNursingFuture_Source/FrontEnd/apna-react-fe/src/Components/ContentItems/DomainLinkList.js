import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import config from '../../config';

class DomainLinkList extends Component {
  render() {
    var fd = [];
    if (this.props.domains && this.props.content){
      fd = this.props.domains;
    }
    var filter = this.props.filter;
    let framework = this.props.content.framework;
    if (fd){
      fd = fd.filter((r) => {
        return r.framework == framework;
      })
    }
    if (filter){
      fd = fd.filter((r) => {
        return !filter.includes(""+r.domainId);
      })
      
    }
    
    return (
      <div className="domain-link-list-wrapper">
        {this.props.domains && !this.props.loading && fd.map((domain,index) => 
          <div className="row list-link-row" key={index}>
            <div className="col-12">
              <Link to={'/explore/' + this.props.content.framework + '/domain/' + domain.domainId} onClick={() => {window.scroll(0,0)}}>
                <div className="list-link"><img src={config.siteUrl + config.imagesDirectory + domain.icon} className="domain-icon" />{domain.title}<span className="oi" data-glyph="chevron-right"></span></div>
              </Link>
            </div>
          </div>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.domainDataLoading,
    domains: state.app.framework.domain
  }
};

export default connect(mapStateToProps)(DomainLinkList);