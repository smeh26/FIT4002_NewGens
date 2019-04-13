import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PageContent from '../Components/PageContent';
import { connect } from 'react-redux';
import { push } from 'react-router-redux';
import { fetchUserQuizzes, locationLabelUpdate, sidebarClose, loadQuiz, fetchFrameworkData } from '../Actions';

class UserQuizzes extends Component {
  
  componentWillMount() {
      this.props.onMount("My self-assessments")
  }
  
  translateType(type){
    switch(type){
      case "ASSESSMENT":
        return 'selfAssessment';
      case "PATHWAY":
        return 'careerPathways';
      default:
        return null;
    }
  }
  
  render() {
    let param;
    if (this.props && this.props.match && this.props.match.params && this.props.match.params.id){
      param = this.props.match.params.id
    }
    if (this.props.loading && this.props.quizzes && this.props.quizzes.length > 0){
      return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
    } else {
      let _type = (param === 'selfAssessment' ? 'ASSESSMENT' : 'PATHWAY' );
      let quizzes = this.props.quizzes.filter((q) => { return q.type == _type && q.results});
      let framework;
      if (_type === 'ASSESSMENT'){
        quizzes = quizzes.map((quiz) => {
          if (quiz.results){
            let quizData = JSON.parse(quiz.results);
            if (quizData.answers){
              for(let questionId in quizData.answers){
                if (this.props.selfAssessmentQuiz.length > 0){
                  let question = this.props.selfAssessmentQuiz.find((q) => { return q.questionId == questionId});
                  if (question && question.framework){
                    framework = question.framework;
                    break;
                  }
                }
              }
            }
          }
          return quiz;
        });
      }
      
      let newLink = '/';
      let quizText = (param == 'selfAssessment' ? 'Start new Self Assessment' : 'Start new Career Quiz');

      if (this.props.sections && this.props.sections.length > 0){
        let cqi = this.props.sections.find((s) => { return s.name == 'careerQuizIntro'});
        let sai = this.props.sections.find((s) => { return s.name == 'selfAssessmentIntro'});
        if (param == 'selfAssessment'){
          newLink = '/sections/' + sai.sectionId;
        } else {
          newLink = '/sections/' + cqi.sectionId;
        }
      }
      return (
      <div className="row">
      <div className="col-12">
        <div className="content-block">
        { quizzes.map((q,i) => {
                          return (
                              <Link onClick={() => { window.scroll(0, 0) }} to={'/results/' + this.translateType(q.type) + '/' + q.userQuizId}>
            <div className="row list-link-row" key={i}>
              <div className="col-12">
                { q.completed &&
                <div className="">{q.date}<span className="oi" data-glyph="chevron-right"></span></div>
                }
                { !q.completed &&
                <div className="" onClick={() => {window.scroll(0,0);this.props.loadInProgress(param, q.userQuizId, framework)}}>In progress - last saved: {q.date} <span className="oi" data-glyph="chevron-right"></span></div>
                }
              </div>
            </div>
                              </Link>
          );
        })}
        </div>
        <Link to={newLink}><button className="btn">{quizText}</button></Link>
      </div>

      </div>);

    } 
  }
}

const mapStateToProps = (state) => {
  return {
    quizzes: state.app.user.quizzes,
    loading: state.app.user.isLoading,
    error: state.app.user.error,
    sections: state.app.content.sections,
    selfAssessmentQuiz: state.app.quiz.selfAssessment
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: function(title) {
      dispatch(fetchUserQuizzes());
      dispatch(locationLabelUpdate(title));
      dispatch(sidebarClose());
      dispatch(fetchFrameworkData())
    },
    loadInProgress: function(type, id, framework){
      dispatch(loadQuiz(type,id));
      if (type === 'selfAssessment'){
        dispatch(push('/quiz/selfAssessment/' + framework));
      } else if (type === 'careerPathways'){
        dispatch(push('/quiz/careerPathways'));
      }
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserQuizzes);

