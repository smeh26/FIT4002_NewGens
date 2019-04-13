import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Accordion from '../Components/ContentItems/Accordion';
import Heading from '../Components/ContentItems/Heading';
import AspectsLevelsAccordion from '../Components/ContentItems/AspectsLevelsAccordion';
import ActionsToGrow from '../Components/ContentItems/ActionsToGrow';
import MarkupBlock from '../Components/ContentItems/MarkupBlock';
import { connect } from 'react-redux';
import { fetchPageData, fetchFrameworkData, locationLabelUpdate, sidebarClose } from '../Actions';
import config from '../config';

class AspectPage extends Component {  
  componentWillMount() {
    this.props.onMount();
  }
  
  render() {
    var framework = this.props.match.params.framework;
    var domainId = this.props.match.params.domain;
    var aspectId = this.props.match.params.aspect;
    var aspect;
    var domain;
    var filterArray = [domainId];
    if (this.props.loading || !this.props.domains){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else if (this.props.error){
      return <div><h1>Error</h1></div>
    } else {
      if (this.props.domains && !this.props.loading && this.props.aspects){
        aspect = this.props.aspects.find((a) => { return +a.aspectId === +aspectId});
        domain = this.props.domains.find((d) => { return +d.domainId === +domainId});

        if (aspect){
          var examplesContent = {
            text: aspect.examples
          };
          var ex = <MarkupBlock content={examplesContent} />;
          var onlineContent = {
            text: aspect.onlineResources
          };
          var or = <MarkupBlock content={onlineContent} />;
          var education = {
            text: aspect.furtherEducation
          };
          var fe = <MarkupBlock content={education} />;
          var contact = {
            text: aspect.peopleContact
          };
          var cont = <MarkupBlock content={contact} />;
        }
      }
      if (!aspect || !domain){
        return (
        <div className="aspect-page-wrapper">
          <h1>This aspect does not exist.</h1>
        </div>
        );
      }
      return (
        <div className="aspect-page-wrapper">
          <div className="domain-image">
            <img src={config.siteUrl + config.imagesDirectory +domain.image} />
          </div>
          <h2 className="text-center">{domain.title}</h2>
          
          <div className="row">
            <div className="col-sm-12">
              <div className="content-block single-line aspect-heading">
                <span>Aspect of Practice</span>
                <h2>{aspect.title}</h2>
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-sm-12">
              <div className="content-block text-left">
                <em>Definition</em>
                <p dangerouslySetInnerHTML={{__html: aspect.text}}></p>
              </div>
            </div>
          </div>
          <Accordion child={ex} title="Examples of practice" open="true" />
          <AspectsLevelsAccordion title="Levels of practice" open="true" aspect={aspect}/>
          {aspect.actionsList && aspect.actionsList.length > 0 && <ActionsToGrow aspect={aspect} />}
          {aspect.onlineResources && <Accordion child={or} title="Online resources" open="true" />}
          {aspect.furtherEducation && <Accordion child={fe} title="Further Education" open="true" />}
          {aspect.peopleContact && <Accordion child={cont} title="People to contact for advice and further guidance" open="true" />}
          <div className="row">
            <div className="col-8 offset-2 push-top">
              <Link className="no-decoration" to={'/explore/' + framework + '/domain/' + domainId + '/'} onClick={() => {window.scroll(0,0)}}>
                <button className="btn inverse block">Back to domain </button>
              </Link>
            </div>
          </div>
        </div>
      )
    }
  }
  
}

const mapStateToProps = (state) => {
  return {
    domains: state.app.framework.domain,
    loading: state.app.framework.aspectsDataLoading,
    error: state.app.content.error,
    aspects: state.app.framework.aspects
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function(title) {
      dispatch(fetchFrameworkData('domain'));
      dispatch(fetchFrameworkData('aspects'));
      dispatch(locationLabelUpdate('Aspects of Practice'));
      dispatch(sidebarClose());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(AspectPage);

