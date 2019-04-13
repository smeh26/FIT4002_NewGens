import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import Accordion from '../Components/ContentItems/Accordion';
import MarkupBlock from '../Components/ContentItems/MarkupBlock';
import Share from '../Components/ContentItems/Share';
import SupportContact from '../Components/ContentItems/SupportContact';
import Heading from '../Components/ContentItems/Heading';
import ReasonsListContent from '../Components/ContentItems/ReasonsListContent';
import { connect } from 'react-redux';
import { fetchArticleData, locationLabelUpdate, openModal, closeModal, sidebarClose, fetchSubmitArticleFeedback, fetchFrameworkData } from '../Actions';
import config from '../config';

class ReasonsArticlePage extends Component {
  constructor(props){
    super(props);
    this.handleFeedbackClick = this.handleFeedbackClick.bind(this);
    this.handleChangeInput = this.handleChangeInput.bind(this);
  }
  
  componentWillMount() {
    this.props.onMount();
    this.state = {
      inputs: {
        message: ''
      },
      feedbackMode: '',
      articleFooterSectionName: 'articlesFooter'
    }
  }

  componentDidMount() {
    this.hashLinkScroll();
  }

  hashLinkScroll() {
    const { hash } = window.location;

    if (hash !== '') {
      setTimeout(() => {
        const id = hash.replace('#', '');
        const element = document.getElementById(id);
        if (element) element.scrollIntoView();
      }, 0);
    }
  }
  
  handleFeedbackClick = function(e){
    var mode = e.target.name;
    this.setState({
      feedbackMode: mode
    });
  }

  handleChangeInput = function(e){
    var name = e.target.name;
    this.setState({inputs: {[name]: e.target.value}});
  }
  
  render() {
    var shareUrl = config.shareBaseUrl+'/reasons'; 
    if (this.props.loading){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else {
     let footerContent = this.props.sections.find((s) => { return s.name == this.state.articleFooterSectionName});
    //  var frContent = {
    //    text: article.furtherReading
    //  }
    //  var fr = <MarkupBlock content={frContent}/>
     
    //  var tdContent = {
    //    text: article.toolsAndDownloads
    //  }
    //  var td = <MarkupBlock content={tdContent} />;
     
    //  var rcContent = {
    //    text: article.relatedCourses
    //  }
    //  var rc = <MarkupBlock content={rcContent} />
    //  var contactHeadingContent = {
    //    text: 'Do you have questions? Call us'
    //  }
      let content = [
      {
        type: 'HEADING',
        title: this.props.reasons.length + ' reasons to choose primary health care nursing'
      }
    ]
      return (
        <div className="article-page-wrapper">
          <PageContent content={content}/>
          <ReasonsListContent />
          <div className="row">
            <div className="col-12">
              <p className="text-center">Was this article helpful to you?</p>
              <div className="row">
                <div className="col-6">
                  <button className="btn" name="yes" onClick={this.props.feedbackModal}>Yes</button>
                </div>
                <div className="col-6">
                  <button className="btn" name="no" onClick={this.props.feedbackModal}>No</button>
                </div>
              </div>
              <div className="row reasons-share">
                <div className="col-12 share-row share-row-small">
                  <span className='share-text'>Share this article</span>
                  <a
                    href={'https://facebook.com/sharer.php?u=' + shareUrl}
                    target="_blank">
                    <img src="/img/fb.png" className="social-icon"/>
                  </a>
                  <a
                    href={'https://twitter.com/home?status=' + shareUrl}
                    target="_blank">
                    <img src="/img/twitter.png" className="social-icon"/>
                  </a>
                  <a
                    href={'https://www.linkedin.com/shareArticle?mini=true&url=' + shareUrl}
                    target="_blank">
                    <img src="/img/linkedin.png" className="social-icon"/>
                  </a>
                </div>
              </div>
              
              {footerContent && <PageContent content={footerContent.contentItems} />}
            </div>
          </div>

        </div>
      )
    }
  }
  
}

const mapStateToProps = (state) => {
  return {
    reasons: state.app.framework.reasons,
    loading: state.app.framework.domainDataLoading,
    error: state.app.articles.error,
    sections: state.app.content.sections
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function(title) {
      dispatch(fetchFrameworkData());
      dispatch(locationLabelUpdate('Reasons to Choose Primary Health Care Nursing'));
      dispatch(sidebarClose());
    },
    feedbackModal: function(positive){
       dispatch(openModal('articleFeedback' + (positive ? '-Yes' : '-No'),window.pageYOffset || document.documentElement.scrollTop));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ReasonsArticlePage);

