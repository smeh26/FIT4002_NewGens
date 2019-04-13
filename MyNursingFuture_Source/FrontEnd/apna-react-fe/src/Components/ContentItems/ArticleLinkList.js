import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchArticleData, fetchFrameworkData } from '../../Actions';  

class ArticleLinkList extends Component {
  
  componentWillMount() {
    this.props.onMount();
  }  
  render() {
    var filteredArticles = [];
    
    for (var a in this.props.articleContentById){
      if (this.props.articleContentById[a]){
        filteredArticles.push(this.props.articleContentById[a])
      }
    }
    
    var articleListFilter = this.props.filter;
    if (articleListFilter){
      filteredArticles = filteredArticles.filter((a) => {
        return !articleListFilter.includes(a.articleId);
      })
    }
    
    return (
      <div className="article-link-list-wrapper">
        <div className="row list-link-row" key="-1">
          <div className="col-12">
            <Link to="/reasons" onClick={() => {window.scroll(0,0)}}>
              <div className="list-link">{this.props.reasons.length + " reasons to choose primary health care"}<span className="oi" data-glyph="chevron-right"></span></div>
            </Link>
          </div>
        </div>
        {filteredArticles && filteredArticles.length > 0 && !this.props.loading && filteredArticles.map((ar,index) => 
          <div className="row list-link-row" key={index}>
            <div className="col-12">
              <Link to={'/articles/' + ar.articleId} onClick={() => {window.scroll(0,0)}}>
                <div className="list-link">{ar.title}<span className="oi" data-glyph="chevron-right"></span></div>
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
    articleContentById: state.app.articles.articleContentById,
    loading: state.app.articles.articleLoading,
    error: state.app.articles.error,
    reasons: state.app.framework.reasons
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function() {
      dispatch(fetchArticleData());
      dispatch(fetchFrameworkData());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ArticleLinkList);