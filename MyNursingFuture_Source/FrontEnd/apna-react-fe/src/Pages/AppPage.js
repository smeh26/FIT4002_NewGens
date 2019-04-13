import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import { connect } from 'react-redux';
import { fetchPageData, locationLabelUpdate, sidebarClose } from '../Actions';

class AppPage extends Component {
  constructor(props){
    super(props);
    this.state = {
      apiEndpoint: props.endpoint,
      title: props.title
    };
  }
  
  componentWillMount() {
    this.props.onMount(this.state.apiEndpoint, this.state.title);
  }
  
  render() {
    let content;
    if (this.props.loading || !this.props.sections || this.props.sections.length == 0){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else if (this.props.error){
      return <div><h1>Error</h1></div>
    } else {
      
      if (this.props.hardContent){
        return <PageContent content={this.props.hardContent} />
      } else {
        if (this.state.apiEndpoint){
          content = this.props.sections.find((s) => { return s.name == this.state.apiEndpoint});
        } else {
          content = this.props.sections.find((s) => { return s.sectionId == this.props.match.params.id});
        }
        
        if (!this.props.title && content && content.title && this.props.appTitle != content.title){
          this.props.setTitle(content.title);
        }
        
        return <PageContent content={content.contentItems} />
      }
    }
  }
  
}

const mapStateToProps = (state) => {
  return {
    sections: state.app.content.sections,
    loading: state.app.content.isLoading,
    error: state.app.content.error,
    appTitle: state.app.headerFooterMenus.locationLabel
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: function(endpoint, title) {
      dispatch(fetchPageData(endpoint));
      dispatch(locationLabelUpdate(title));
      dispatch(sidebarClose());
    },
    setTitle: function(title){
      dispatch(locationLabelUpdate(title));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(AppPage);

