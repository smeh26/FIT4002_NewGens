import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import Default from '../Components/ContentItems/Default'
import Accordion from '../Components/ContentItems/Accordion';
import MarkupBlock from '../Components/ContentItems/MarkupBlock';
import VideoEmbed from '../Components/ContentItems/VideoEmbed';
import Share from '../Components/ContentItems/Share';
import CareerPathways from '../Components/ContentItems/CareerPathways';
import EducationOpportunities from '../Components/ContentItems/EducationOpportunities';
import ContactBlock from '../Components/ContentItems/ContactBlock';
import { connect } from 'react-redux';
import { fetchPageData, fetchFrameworkData, locationLabelUpdate, sidebarClose } from '../Actions';

class SectorPage extends Component {
  constructor(props){
    super(props);
    this.handleFrameworkViewClick = this.handleFrameworkViewClick.bind(this);  
  }
  
  componentWillMount() {
    this.props.onMount();
    this.state = {
      framework: 'rn',
      careerPathwaysContentDefault: "[{\"level\":0,\"title\":\"\",\"text\":\"\"},{\"level\":1,\"title\":\"\",\"text\":\"\"},{\"level\":2,\"title\":\"\",\"text\":\"\"}]",
      educationOpportunitiesDefault: "[{\"level\":0,\"text\":\"\"},{\"level\":1,\"text\":\"\"},{\"level\":2,\"text\":\"\"}]"
    }
  }
  
  handleFrameworkViewClick = function(e){
    if (e.target.id == 'rn' || e.target.id == 'en'){
      this.setState({
        framework: e.target.id
      });
    }
  }
  
  render() {
    var sectorId = this.props.match.params.id;
    var sectorView = this.state.framework;
    var thisSector = this.props.sectors.find((s) => {return s.sectorId == sectorId });
    var thisSectorContent;
    let enAvailable = false;
    if (thisSector){
      if (this.state.framework == 'rn'){
        thisSectorContent = thisSector.sectorRn;
      } else {
        thisSectorContent = thisSector.sectorEn;
      }
      
    }
    if (this.props.loading || !thisSectorContent){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else {
      var careerPathwaysContent = <CareerPathways content={thisSectorContent} />;
      
      var introContent = {
        title: thisSector.title,
        text: thisSectorContent.intro
      };
      if (thisSectorContent.video){
        var videoEmbedContent = {
          video: thisSectorContent.video
        };
      }

      var workEnvironmentsContent = {
        text: thisSectorContent.workEnvironments
      }
      var we = <MarkupBlock content={workEnvironmentsContent} />
      var careerOpportunitiesContent = {
        text: thisSectorContent.careerOpportunities
      };
      var co = <MarkupBlock content={careerOpportunitiesContent}/>;
      
      var eo = <EducationOpportunities content={thisSectorContent} />;
      
      var onlineResourcesContent = {
        text: thisSectorContent.onlineResources
      };
      var or = <MarkupBlock content={onlineResourcesContent}/>

      var cc = <ContactBlock readOnlySector={sectorId} />
      
      var moreStoriesContent = {
        text: thisSectorContent.moreStories
      }
      var ms = <MarkupBlock content={moreStoriesContent} />
      
      var rnViewClass = 'col-6' + (this.state.framework === 'rn' ? ' active' : '');
      var enViewClass = 'col-6' + (this.state.framework === 'en' ? ' active' : '');
      
      return (
        <div className="sector-page-wrapper">
          <div className="row sector-framework-select">
            <div className={rnViewClass} onClick={this.handleFrameworkViewClick} id='rn'><h1 id='rn'>RN View</h1></div>
            {enAvailable && <div className={enViewClass} onClick={this.handleFrameworkViewClick} id='en'><h1 id='en'>EN View</h1></div>}
            {!enAvailable && <div className="col-6 " ><h1 id='en'></h1></div>}
          </div>
          <Default content={introContent}/>
          {videoEmbedContent && 
            <VideoEmbed content={videoEmbedContent} />
          }
          {thisSectorContent.careerPathways && thisSectorContent.careerPathways != this.state.careerPathwaysContentDefault && <Accordion child={careerPathwaysContent} title="Career pathways" open="true"/>}
          {thisSectorContent.workEnvironments && <Accordion child={we} title="Work Environments" />}
          {thisSectorContent.careerOpportunities && <Accordion child={co} title="Career Opportunities" />}
          {thisSectorContent.educationOpportunities && thisSectorContent.educationOpportunities != this.state.educationOpportunitiesDefault && <Accordion child={eo} title="Education pathways" />}
          {thisSectorContent.onlineResources && <Accordion child={or} title="Online resources" />}
          {thisSectorContent.moreStories && <Accordion child={ms} title="More stories" />}
          <Accordion child={cc} title="People to contact for advice and further guidance" />
        </div>
      )
    }
  }
  
}

const mapStateToProps = (state) => {
  return {
    sectors: state.app.framework.sectors,
    loading: state.app.framework.sectorDataLoading,
    error: state.app.content.error
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function(title) {
      dispatch(fetchFrameworkData('sectors'));
      dispatch(locationLabelUpdate('Sectors & Roles'));
      dispatch(sidebarClose());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SectorPage);

