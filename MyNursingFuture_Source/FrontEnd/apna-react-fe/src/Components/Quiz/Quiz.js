//type
//questions

//state
//progress
//results

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { push } from 'react-router-redux';

import { 
  fetchCareerPathwaysQuizData,
  fetchSelfAssessmentQuizData,
  setCareerPathwaysAnswer,
  setSelfAssessmentAnswer,
  setSelfAssessmentMultiAnswer,
  setCareerPathwaysMultiAnswer,
  fetchCareerPathwaysQuizResults,
  fetchSelfAssessmentQuizResults,
  fetchFrameworkData,
  fetchSaveInProgressQuizzes,
  openModal,
  setAboutYouQuizAnswer,
  setAboutYouMultiAnswer,
  setSurveyAnswer
} from '../../Actions';

import QuizProgress from './QuizProgress';
import Question from './Question';
import QuestionNavigation from './QuestionNavigation';
import RandomPostcard from '../ContentItems/RandomPostcard'
import MarkupBlock from '../ContentItems/MarkupBlock'
import config from '../../config';

class Quiz extends Component {
  constructor(props){
    super(props);
    this.state = {
      currentQuestion: {},
      results: {},
      quizType: props.quizType || 'selfAssessment',
      questionPointer: 0,
      viewingResults: false,
      domainFilter: props.domainFilter || 0,
      framework: props.framework,
      quiz: null,
      answers: null,
      setAnswer: null,
      setMultiAnswer: null,
      loading: true,
      resultsLoading: true,
      currentDomain: null,
      currentAspect: null,
      patientTitle: 'individual accessing care',
      answeredQuestionCount: 0,
      surveyQuestionTime: false
    };
    this.canSave = this.canSave.bind(this);
    this.canNext = this.canNext.bind(this);
    this.clickNext = this.clickNext.bind(this);
    this.clickBack = this.clickBack.bind(this);
    this.fetchResults = this.fetchResults.bind(this);
    this.isQuizFinished = this.isQuizFinished.bind(this);
    
    this.updateQuiz = this.updateQuiz.bind(this);
    this.setQuizPointerToLatest = this.setQuizPointerToLatest.bind(this);
    
  }
  
  componentWillMount() {
    this.props.onMount();
    this.updateQuiz({},this.props);
    this.setQuizPointerToLatest();
    this.setState({surveyQuestionTime: false});
  }  
  
  componentWillReceiveProps(nextProps){
    this.updateQuiz(this.props,nextProps);
  }
  
  setQuizPointerToLatest(){
    let qp = 0;
    if (this.state.quiz && this.state.quiz.constructor == Array){
      for (let qq of this.state.quiz){
        if (typeof this.state.answers[qq.questionId] != 'undefined'){
          qp++;
        }
      }
      console.log('setting question pointer',qp);
      this.setState({questionPointer: qp});
    }
  }
  
  updateQuiz(props,nextProps){
    let propsUpdated = Object.assign({},props,nextProps);
    
    let quiz, answers, setAnswer, setMultiAnswer, loading, resultsLoading, currentDomain, currentAspect;
    
    if (this.state.quizType === 'selfAssessment') {
      quiz = propsUpdated.selfAssessmentQuiz;
      if (this.state.domainFilter && quiz.filter && propsUpdated.aboutYouQuiz.filter){
        let _domains = propsUpdated.domains.filter((d) => {
          return d.framework == this.state.framework;
        });
        //quiz = quiz.filter((qq) => {return qq.domainId === domainFilter});
        if (this.state.domainFilter === 'about'){
          quiz = propsUpdated.aboutYouQuiz;
          quiz = quiz.filter((qq) => { return qq.fieldName != "Patients"});
        } else {
          quiz = quiz.filter((qq) => {
            return qq.domainId && +qq.domainId == +this.state.domainFilter
          });
          let questionDomainId = +quiz[this.state.questionPointer].domainId || +this.state.domainFilter;
          let questionAspectId = quiz[this.state.questionPointer].aspectId;
          currentDomain = _domains.find((domain) => {return +domain.domainId === +questionDomainId});
          currentAspect = propsUpdated.aspects.find((asp) => {return +asp.aspectId === +questionAspectId})
        }
        answers = {};
        for (let q of quiz){
          if (typeof propsUpdated.selfAssessmentQuizAnswers[q.questionId] != 'undefined'){
            answers[q.questionId] = propsUpdated.selfAssessmentQuizAnswers[q.questionId];
          }
        }
      } else {
        answers = propsUpdated.selfAssessmentQuizAnswers;
      }

      if (this.state.domainFilter === 'about'){
        setAnswer = propsUpdated.setAboutYouAnswer;
        setMultiAnswer = propsUpdated.setAboutYouQuizMultiAnswer;
        loading = propsUpdated.selfAssessmentLoading;
        resultsLoading = propsUpdated.selfAssessmentResultsLoading;
        answers = propsUpdated.aboutYouAnswers;
      } else {
        setAnswer = propsUpdated.setSelfAssessmentAnswer;
        setMultiAnswer = propsUpdated.setSelfAssessmentMultiAnswer;
        loading = propsUpdated.selfAssessmentLoading;
        resultsLoading = propsUpdated.selfAssessmentResultsLoading;
      }
    } else {
      quiz = propsUpdated.aboutYouQuiz.concat(propsUpdated.careerPathwaysQuiz);
      answers = Object.assign({},propsUpdated.aboutYouAnswers,propsUpdated.careerPathwaysQuizAnswers);
      setAnswer = propsUpdated.setCareerPathwaysAnswer;
      setMultiAnswer = propsUpdated.setCareerPathwaysMultiAnswer;
      loading = propsUpdated.careerPathwaysLoading;
      resultsLoading = propsUpdated.careerPathwaysResultsLoading;
    }

      //console.log('answers', answers);
      //console.log('answers[32]', answers[32]);

      // if (answers[32] == 1) { // We selected 'EN'
      //   //console.log('removing from answers and re-directing');
      //   delete answers[32]; // Delete this answer from our selection.
      //   this.props.redirectToAboutFramework(); // re-direct user to the about the framework page.
      //   //return;
      // }

    
    let patientTitle = propsUpdated.userPatientTitle;
    if (!patientTitle){
      if (propsUpdated.aboutYouQuiz){
        try{
          patientTitle = propsUpdated.aboutYouAnswers[propsUpdated.aboutYouQuiz.find((a) => { return a.fieldName == 'PatientsTitle'}).questionId];
        } catch (e){
          patientTitle = 'individual accessing care';
        }
      }
    }
    if (patientTitle) {
        patientTitle = patientTitle.toLowerCase();
    }
    else
        patientTitle = 'patient';
    let aq = 0;
    let questionIdsRemove = [];
    if (quiz.constructor == Array){
      for (let qq of quiz){
        // if (this.state.quizType == 'careerPathways'){
        //   if (qq.quizType == 'ABOUT'){
        //     if (typeof propsUpdated.aboutYouAnswers[qq.questionId] != 'undefined'){
        //        questionIdsRemove.push(qq.questionId);
        //     }
        //   }
        // }

        let meetsReq = true;
        if (qq.requirements){
          let req = JSON.parse(qq.requirements);
          for (let qr of req){
            if (qr){
              if (qr.questionId && qr.value){
                let reqVal = JSON.parse(qr.value);
                let reqQuestion = quiz.find((fqq) => { return fqq.questionId == qr.questionId});
                let comparisonVal;
                if (reqQuestion.quizType == 'ASSESSMENT'){
                  comparisonVal = propsUpdated.selfAssessmentCurrentAnswers[qr.questionId];
                } else if (reqQuestion.quizType == 'PATHWAY'){
                  comparisonVal = propsUpdated.careerPathwaysCurrentAnswers[qr.questionId];
                } else if (reqQuestion.quizType == 'ABOUT'){
                  comparisonVal = propsUpdated.aboutYouAnswers[qr.questionId];
                }
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
      quiz = quiz.filter((qq) => { return !questionIdsRemove.includes(qq.questionId)});

      for (let qq of quiz){
        if (answers && typeof answers[qq.questionId] != 'undefined'){
          aq++;
        }
      }
    }
    
    this.setState({
      quiz: quiz,
      answers: answers,
      setAnswer:setAnswer,
      setMultiAnswer: setMultiAnswer,
      loading: loading,
      resultsLoading: resultsLoading,
      patientTitle: patientTitle,
      answeredQuestionCount: aq
    })
    
    if ((props.quizType != nextProps.quizType) || (props.domainFilter != nextProps.domainFilter)){
      let qp = 0;
      if (quiz && quiz.constructor == Array){
        for (let qq of quiz){
          if (typeof answers[qq.questionId] != 'undefined'){
            qp++;
          }
        }
        if (qp == quiz.length){qp--;}
        console.log('setting question pointer',qp);
        this.setState({questionPointer: qp});
      }

    }
    
  }
  
  canSave(){
    //return this.state.quizType == 'selfAssessment';
    return true;
  }
  
  canNext(quiz, questionId){
    return quiz.length && 
      this.state.questionPointer < quiz.length-1 && questionId &&
      (
        (this.props.selfAssessmentQuizAnswers && typeof this.props.selfAssessmentQuizAnswers[questionId] != 'undefined') ||
        (this.props.careerPathwaysQuizAnswers && typeof this.props.careerPathwaysQuizAnswers[questionId] != 'undefined') || 
        (this.props.aboutYouAnswers && typeof this.props.aboutYouAnswers[questionId] != 'undefined')
      );
  }
  clickNext () {
    var quiz = this.state.quizType == 'selfAssessment' ? this.props.selfAssessmentQuiz : this.props.aboutYouQuiz.concat(this.props.careerPathwaysQuiz);
    if (quiz.length){
      var c = this.state.questionPointer + 1
      if (c >= quiz.length){ c = 0;}
      this.setState({
        questionPointer: c
      });
      window.scroll(0,0)
    }
  };
  
  
  clickBack () {
    var quiz = this.state.quizType == 'selfAssessment' ? this.props.selfAssessmentQuiz : this.props.aboutYouQuiz.concat(this.props.careerPathwaysQuiz);
    if (quiz.length){
      var c = this.state.questionPointer - 1;
      if (c < 0){ 
        c = 0;
        // go back to general quiz page?
      }
      this.setState({
        questionPointer: c
      });
      window.scroll(0,0)
    }
  };
  
  isQuizFinished () {
    if (this.props.selfAssessmentQuiz && this.props.selfAssessmentQuiz.length > 0){
      if (this.state.answers && this.state.answeredQuestionCount){
        return this.state.answeredQuestionCount >= this.state.quiz.length && !this.state.domainFilter;
      } else {
        return false;
      }
    } else {
      return false;
    }

  }
  
  fetchResults () {
    if (this.isQuizFinished()){
      var quiz, answers;
      this.setState({
        viewingResults: true
      })
      if (this.state.quizType === 'selfAssessment') {
        this.props.getSelfAssessmentQuizResults(this.state.framework);
      } else {
        this.props.getCareerPathwaysQuizResults();
      } 
    }
  }
  
  render() {
    var question, currentDomain, currentAspect;
    if (this.state.quiz && !this.state.loading){
      question = this.state.quiz[this.state.questionPointer];  
      
      if (this.state.quizType === 'selfAssessment') {
        
        if (this.state.domainFilter && this.state.quiz.filter && this.props.aboutYouQuiz.filter){
          let _domains = this.props.domains.filter((d) => {
            return d.framework == this.state.framework;
          });
          //quiz = quiz.filter((qq) => {return qq.domainId === domainFilter});
          if (this.state.domainFilter != 'about'){
            let questionDomainId = +this.state.quiz[this.state.questionPointer].domainId || +this.state.domainFilter;
            let questionAspectId = this.state.quiz[this.state.questionPointer].aspectId;
            currentDomain = _domains.find((domain) => {return +domain.domainId === +questionDomainId});
            currentAspect = this.props.aspects.find((asp) => {return +asp.aspectId === +questionAspectId})
          }
        }
      }
    }
    if (question){
      return (
        <div className="quiz-wrapper">
          {!this.state.viewingResults && 
            <div className="quiz">
              <div className="content-block">
                {this.state.quizType == 'selfAssessment' && currentDomain &&  
                <div className="quiz-header">
                  <img src={config.siteUrl + config.imagesDirectory + currentDomain.image} />
                  <h2 className="squishybottom">{currentDomain.title}</h2>
                  <QuizProgress currentQuestion={this.state.questionPointer+1} total={this.state.quiz.length} complete={this.state.answeredQuestionCount} />
                  {this.state.questionPointer == 0 && <MarkupBlock content={{text: currentDomain.text}} />}
                </div>
                }
                {this.state.quizType == 'selfAssessment' && currentAspect &&
                  <div className="quiz-aspect-title text-left"> 
                    <h3>{currentAspect.title}</h3>
                  </div>
                }
              </div>
            
            <Question question={question} quizType={this.state.quizType} patientTitle={this.state.patientTitle}/>
            <hr />
            <QuestionNavigation clickNext={this.clickNext} 
              clickBack={this.clickBack} 
              canNext={this.canNext(this.state.quiz, question.questionId)} 
              canFinish={ this.isQuizFinished() } 
              canSave={ this.canSave() }
              submit={this.fetchResults}
              type={this.state.quizType}
              exitAndSave={()=>{this.props.exitAndSaveQuiz(this.state.quizType,this.state.framework)}}
              atEnd={this.state.quiz.length <= this.state.answeredQuestionCount && this.state.questionPointer == this.state.quiz.length-1}
              />
          </div>
          }
          {this.state.viewingResults &&
            <div className="quiz-results">
              <div className="content-block squishybottom">
                <QuizProgress currentQuestion={this.state.questionPointer+1} total={this.state.quiz.length} complete={this.state.answeredQuestionCount} />
              </div>
              {this.state.resultsLoading && 
                <div className="content-block">
                  <h1>Well done!<br />You have completed all the questions.</h1>
                  <p>Calculating results...</p>
                </div>
              }
              {!this.state.resultsLoading &&
                <div className="content-block">
                  <div className="quiz-complete-tick"><span className="oi" data-glyph="check"></span></div>
                  <h1 className="push-bottom">Your results are ready.</h1>
                  <Link to={'/results/' + this.state.quizType} className="btn">View my results</Link>
                </div>
              }
              <RandomPostcard />
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
    careerPathwaysQuiz: state.app.quiz.careerPathways,
    selfAssessmentQuizAnswers: state.app.quiz.selfAssessmentCurrentAnswers,
    selfAssessmentQuizLoading: state.app.quiz.selfAssessmentLoading,
    careerPathwaysQuizAnswers: state.app.quiz.careerPathwaysCurrentAnswers,
    careerPathwaysQuizLoading: state.app.quiz.careerPathwaysLoading,
    selfAssessmentResultsLoading: state.app.quiz.selfAssessmentResultsLoading,
    careerPathwaysResultsLoading: state.app.quiz.careerPathwaysResultsLoading,
    domains: state.app.framework.domain,
    aspects: state.app.framework.aspects,
    loggedIn: state.app.user.loggedIn,
    aboutYouQuiz: state.app.quiz.aboutYouQuiz,
    aboutYouAnswers: state.app.quiz.aboutYouAnswers,
    userPatientTitle: state.app.user.user.patientsTitle,
    frameworkLoading: state.app.framework.domainDataLoading,
    surveyQuestion: state.app.quiz.surveyQuestion,
    surveyAnswer: state.app.quiz.surveyAnswer
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: () => {
      dispatch(fetchFrameworkData('domain')); 
      dispatch(fetchFrameworkData('aspects'));
      dispatch(fetchCareerPathwaysQuizData());
      dispatch(fetchSelfAssessmentQuizData());
    },
    setCareerPathwaysAnswer: (id,data) => {
      dispatch(setCareerPathwaysAnswer(id,data));
    },
    setCareerPathwaysMultiAnswer: (id,data) => {
      dispatch(setCareerPathwaysMultiAnswer(id,data));
    },
    setSelfAssessmentAnswer: (id,data) => {
      dispatch(setSelfAssessmentAnswer(id,data));
    },
    setSelfAssessmentMultiAnswer: (id,data) => {
      dispatch(setSelfAssessmentMultiAnswer(id,data));
    },
    getCareerPathwaysQuizResults: () => {
      dispatch(fetchCareerPathwaysQuizResults());
    },
    getSelfAssessmentQuizResults: (framework) => {
      dispatch(fetchSelfAssessmentQuizResults(framework));
    },
    setAboutYouAnswer: (id,data) => {
      dispatch(setAboutYouQuizAnswer(id,data));
    },
    setAboutYouQuizMultiAnswer: (id,data) => {
      dispatch(setAboutYouMultiAnswer(id,data));
    },
    setSurveyAnswer: (data) => {
      dispatch(setSurveyAnswer(data));
    },
    redirectToAboutFramework: () => {
      dispatch(push('/sections/14'))
    },
    exitAndSaveQuiz: (quiz,framework) => {
      try{
        dispatch(fetchSaveInProgressQuizzes(quiz));
        if (quiz === 'careerPathways'){
          dispatch(push('/'))
        }
      }catch(e){
        if (e != 'User is not logged in'){
          console.log(e);
        } else {
          if (quiz == 'careerPathways'){
            dispatch(openModal('doYouHaveAProfile'));
          }
          
        }
      }
      if (quiz === 'selfAssessment'){
        dispatch(push('/quiz/selfAssessment/'+framework));
      }
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Quiz);