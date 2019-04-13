import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { push } from 'react-router-redux';

import { 
  fetchSelfAssessmentQuizData,
  fetchSelfAssessmentQuizResults,
  fetchFrameworkData,
  openModal,
  setSurveyAnswer
} from '../../Actions';

import QuizProgress from './QuizProgress';
import Question from './Question';
import QuestionNavigation from './QuestionNavigation';
import config from '../../config';

class SelfAssessmentHub extends Component {
  constructor(props){
    super(props);
    this.state = {
      currentQuestion: {},
      results: {},
      quizType: props.quizType || 'selfAssessment',
      questionPointer: 0,
      viewingResults: false,
      domainFilter: props.domainFilter || 0,
      surveyQuestionTime: false
    };
    this.isQuizFinished = this.isQuizFinished.bind(this);
    this.fetchResults = this.fetchResults.bind(this);
  }
  
  componentWillMount() {
    this.props.onMount();
  }
  
  isQuizFinished () {
    let _saq = this.props.selfAssessmentQuiz.filter((q)=>{return q.quizType == 'ASSESSMENT' && q.framework == this.props.match.params.framework});

    return Object.keys(this.props.selfAssessmentQuizAnswers).length >= _saq.length;
  }
  
  fetchResults () {
    if (this.isQuizFinished()){
      var quiz, answers;
      if (this.state.surveyQuestionTime){
        this.setState({
          viewingResults: true,
          surveyQuestionTime: false
        });
      } else {
        this.setState({
          viewingResults: false,
          surveyQuestionTime: true
        }); 
        return;
      }

      if (this.state.quizType === 'selfAssessment') {
        this.props.getSelfAssessmentQuizResults(this.props.match.params.framework);
      } else {
        this.props.getCareerPathwaysQuizResults();
      }
    }
  }
  
  render() {
    let param;
    let kickOut = false;
    if (this.props.user && this.props.user.nurseType){
      kickOut = ["1","5","6","7"].includes(""+this.props.user.nurseType);
    } else if (this.props.aboutYouAnswers && Object.keys(this.props.aboutYouAnswers).length > 0){
      let nt = this.props.aboutYouQuiz.find((q) => { return q.fieldName && q.fieldName.toLowerCase() == 'nursetype' });
      let nurseType = this.props.aboutYouAnswers[nt.questionId];
      kickOut = nurseType && ["1","5","6","7"].includes(""+nurseType);
    }
    
    if (this.props.match && this.props.match.params && this.props.match.params.framework){
      param = this.props.match.params.framework;
    } else {
      param = 'rn';
    }
    if (this.props.selfAssessmentQuiz && this.props.selfAssessmentQuiz.filter  && !this.props.selfAssessmentQuizLoading) {
      let domains = this.props.domains.filter((d) => { return d.framework == param});
      let groupedQuestions = domains.map((d) => {
        let qs = this.props.selfAssessmentQuiz.filter((q) => { return q.domainId == d.domainId});
        return {
          domain: d,
          questions: qs,
          completeQuestionsCount: qs.reduce((acc,val) => {
            if (this.props.selfAssessmentQuizAnswers[val.questionId] || this.props.selfAssessmentQuizAnswers[val.questionId] == 0){
              return acc+1;
            }
            return acc;
          },0)
        }
      });
      let aboutYouQuiz;
      if (this.props.aboutYouAnswers && this.props.aboutYouQuiz.length){
        let questionIdsRemove = [34];
        for (let qq of this.props.aboutYouQuiz){
          let meetsReq = true;
          if (qq.requirements){
            let req = JSON.parse(qq.requirements);
            for (let qr of req){
              if (qr){
                if (qr.questionId && qr.value){
                  let reqVal = JSON.parse(qr.value);
                  let reqQuestion = this.props.aboutYouQuiz.find((fqq) => { return fqq.questionId == qr.questionId});
                  let comparisonVal = this.props.aboutYouAnswers[qr.questionId];
                  if (reqVal.constructor == Array){
                    meetsReq = reqVal.includes(comparisonVal);
                  } else {
                    meetsReq = comparisonVal == reqVal;
                  }
                  if (!meetsReq){
                    questionIdsRemove.push(qq.questionId);
                  }
                }
              }
            }
          }
        }
        let aboutQuiz = this.props.aboutYouQuiz.filter((qq) => { return !questionIdsRemove.includes(qq.questionId)});
        
        aboutYouQuiz = {
        questions: aboutQuiz,
        completeQuestionsCount: this.props.aboutYouQuiz.reduce((acc,val) => {
            if (this.props.aboutYouAnswers[val.questionId] || this.props.aboutYouAnswers[val.questionId] == 0){
              return acc+1;
            }
            return acc;
          },0)
        }    
      } else {
        aboutYouQuiz = {
        questions: this.props.aboutYouQuiz,
        completeQuestionsCount: 0
        }  
      }

      if (this.state.surveyQuestionTime){
        return (
          <div className="self-assessment-hub-wrapper">
            <div className="quiz-wrapper">
              <div className="quiz">
                <div className="content-block">
                  <Question question={this.props.surveyQuestion} quizType="SURVEY"/>
                  {this.props.surveyAnswer && 
                  <div className="col-12">
                    <div className="content-block">
                      <button className="btn" onClick={this.fetchResults} >View my results</button>
                    </div>
                  </div>
                  }
                </div>
              </div>
            </div>
          </div>
        )
      }
      
      return (
        <div className="self-assessment-hub-wrapper">
        
        
        {!this.state.viewingResults && 
          <div className="row">
            <div className="col-6 col-sm-4">
              <div className="self-assessment-category">
                <Link to={"/quiz/selfAssessment/" + param + "/about"}>
                  <img src='/img/about-you.png' />
                  <p>About You</p>
                  <QuizProgress total={aboutYouQuiz.questions.length} complete={aboutYouQuiz.completeQuestionsCount} mini={true} />
                </Link>
              </div>
            </div>
            
          {!kickOut && groupedQuestions.map((gq,i) => {
            return (
            <div className="col-6 col-sm-4" key={i}>
              <div className="self-assessment-category">
                <Link onClick={() => {window.scrollTo(0,0)}} to={"/quiz/selfAssessment/" + param + "/" + gq.domain.domainId}>
                  {gq.completeQuestionsCount >= gq.questions.length && 
                    <span className="completeTick"><span className="oi" data-glyph="check"></span></span>
                  }
                  <img src={config.siteUrl + config.imagesDirectory + gq.domain.image} className={gq.completeQuestionsCount < gq.questions.length ? 'image-grey-filter' : ''}/>
                  <p>{gq.domain.title}</p>
                  <QuizProgress total={gq.questions.length} complete={gq.completeQuestionsCount} mini={true} />
                </Link>
              </div>
            </div>
            );
          })}
          {this.isQuizFinished() && 
          <div className="col-12">
            <button className="btn" onClick={this.fetchResults} >View my results</button>
          </div>
          }
          {!kickOut && !this.isQuizFinished() && this.props.loggedIn &&
          <div className="col-12">
            <button className="btn" onClick={this.props.exitAndSaveLoggedIn} >Exit and save</button>
          </div>
          }
          {!kickOut && !this.isQuizFinished() && !this.props.loggedIn &&
          <div className="col-12">
            <button className="btn" onClick={this.props.exitAndSaveNotLoggedIn} >Exit and save</button>
          </div>
          }
          {kickOut &&
          <div className="col-12">
            <p className="text-center">Only Registered Nurses, Registered Midwives and Nurse Practitioners can take the Self-Assessment Quiz.</p>
            <button className="btn" onClick={this.props.exitAndSaveLoggedIn} >Exit</button>
          </div>
          }
        </div>
        }
        {this.state.viewingResults && 
        <div className="row">
          <div className="col-12">
            <div className="quiz-results">
              {this.props.selfAssessmentResultsLoading && 
                <div className="content-block">
                  <h1>Well done!<br />You have completed all the questions.</h1>
                  <p>Calculating results...</p>
                </div>
              }
              {!this.props.selfAssessmentResultsLoading &&
                <div className="content-block">
                  <div className="quiz-complete-tick"><span className="oi" data-glyph="check"></span></div>
                  <h1 className="push-bottom">Your results are ready.</h1>
                  <Link to={'/results/selfAssessment'} className="btn">View my results</Link>
                </div>
              }
            </div>
          </div>
        </div>
        
        }
        </div>
      );
    } else {
      return (
        <div className="quiz-wrapper">
          <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
        </div>
      );
    }
  }
}

const mapStateToProps = (state) => {
  return {
    selfAssessmentQuiz: state.app.quiz.selfAssessment,
    selfAssessmentQuizAnswers: state.app.quiz.selfAssessmentCurrentAnswers,
    selfAssessmentQuizLoading: state.app.quiz.selfAssessmentLoading,
    selfAssessmentResultsLoading: state.app.quiz.selfAssessmentResultsLoading,
    domains: state.app.framework.domain,
    loggedIn: state.app.user.loggedIn,
    aboutYouQuiz: state.app.quiz.aboutYouQuiz,
    aboutYouAnswers: state.app.quiz.aboutYouAnswers,
    user: state.app.user.user,
    surveyQuestion: state.app.quiz.surveyQuestion,
    surveyAnswer: state.app.quiz.surveyAnswer
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: () => {
      dispatch(fetchFrameworkData('domain')); 
      dispatch(fetchFrameworkData('aspects'));
      dispatch(fetchFrameworkData('questions'));
      dispatch(fetchSelfAssessmentQuizData());
    },
    getSelfAssessmentQuizResults: (framework) => {
      dispatch(fetchSelfAssessmentQuizResults(framework));
    },
    exitAndSaveNotLoggedIn: () => {
      dispatch(openModal('doYouHaveAProfile'));
    },
    exitAndSaveLoggedIn: () => {
      dispatch(push('/'));
    },
    setSurveyAnswer: (data) => {
      dispatch(setSurveyAnswer(data));
    },
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SelfAssessmentHub);