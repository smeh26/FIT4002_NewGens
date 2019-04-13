import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import Default from '../Components/ContentItems/Default'
import Accordion from '../Components/ContentItems/Accordion';
import MarkupBlock from '../Components/ContentItems/MarkupBlock';
import VideoEmbed from '../Components/ContentItems/VideoEmbed';
import Share from '../Components/ContentItems/Share';
import CareerPathways from '../Components/ContentItems/CareerPathways';
import DomainLinkList from '../Components/ContentItems/DomainLinkList';
import AspectLinkList from '../Components/ContentItems/AspectLinkList';
import Heading from '../Components/ContentItems/Heading';
import { connect } from 'react-redux';
import { fetchPageData, fetchFrameworkData, locationLabelUpdate, sidebarClose } from '../Actions';
import config from '../config';

class DomainPage extends Component {  
  componentWillMount() {
    this.props.onMount();
  }
  
  render() {
    var framework = this.props.match.params.framework;
    var domainId = this.props.match.params.id;
    var domain;
    var filterArray = [domainId];
    if (this.props.loading || !this.props.domains){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else if (this.props.error){
      return <div><h1>Error</h1></div>
    } else {
      if (this.props.domains && !this.props.loading){
        domain = this.props.domains.find((d) => { return +d.domainId === +domainId});
      }

      if (!domain){
        return (
        <div className="domain-page-wrapper">
          <h1>This domain does not exist.</h1>
        </div>
        );
      }
      return (
        <div className="domain-page-wrapper">
          <div className="domain-image">
            <img src={config.siteUrl + config.imagesDirectory + domain.image}/>
          </div>
          <h2 className="text-center">{domain.title}</h2>
          <MarkupBlock content={{text: domain.text}} />
          <Heading content={{title: 'Aspects of Practice'}}/>
          <AspectLinkList content={{domainId: domain.domainId, framework: framework}} />
          <DomainLinkList filter={filterArray} content={{framework: framework}}/>
        </div>
      )
    }
  }
  
}

const mapStateToProps = (state) => {
  return {
    domains: state.app.framework.domain,
    loading: state.app.framework.domainDataLoading,
    error: state.app.content.error
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function(title) {
      dispatch(fetchFrameworkData('domain'));
      dispatch(fetchFrameworkData('aspects'));
      dispatch(locationLabelUpdate('Domains'));
      dispatch(sidebarClose());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(DomainPage);

