import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import TextWithGlossary from './TextWithGlossary';
import { fetchFrameworkData } from '../../Actions'

class AspectLinkList extends Component {
  componentWillMount() {
    this.props.onMount();
  }
  
  render() {
    var fd = [];
    var faspects = [];
    var domainId = this.props.content.domainId;
    if (this.props.domains && this.props.aspects){
      fd = this.props.domains.find((fr) => {return +fr.domainId === +domainId});
      faspects = this.props.aspects.filter((a) => { return +fd.domainId === +a.domainId });
    }
    
    var filter = this.props.filter;
    if (filter && faspects){
      faspects = faspects.filter((r) => {
        return !filter.includes(r.domainId);
      })
    }
    
    return (
      <div className="aspects-link-list-wrapper">
        {this.props.aspects && !this.props.loading && faspects.map((aspect,index) => 
          <div className="row list-link-row" key={index}>
            <div className="col-12">
              <Link to={'/explore/' + aspect.framework + '/domain/' + aspect.domainId + '/' + aspect.aspectId} onClick={() => {window.scroll(0,0)}}>
                <div className="list-link"><h3 className="aspects-link-heading">{aspect.title}<span className="oi" data-glyph="chevron-right"></span></h3></div>
              </Link>
              <div className="user-defined-markup" >
                <TextWithGlossary text={aspect.text}/>
              </div>
            </div>
          </div>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.aspectsDataLoading,
    domains: state.app.framework.domain,
    aspects: state.app.framework.aspects
  }
};
const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function() {
      dispatch(fetchFrameworkData('aspects'));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(AspectLinkList);