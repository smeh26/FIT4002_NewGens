import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchFrameworkData, locationLabelUpdate, sidebarClose, fetchUserQuizzes, openModal, selfAssessmentResultsToReportJSON } from '../../Actions';
import ActionsToSkillUp from './ActionsToSkillUp';

import config from '../../config';

class SelfAssessmentResults extends Component {
  constructor(props){
    super(props);
    this.scoreToPills = this.scoreToPills.bind(this);
    this.handleSendEmailReport = this.handleSendEmailReport.bind(this);
    this.handleDownloadEmailReport = this.handleDownloadEmailReport.bind(this);
    this.state = {
      quizId: this.props.match.params.id
    }
  }
  
  componentWillMount() {
    this.props.onMount();
  }

  componentDidMount() {
    console.log(this.props)
    if (!this.props.loggedIn) {
      this.handleSaveAnonReportData()
    }
  }
  
  domainClass(domainTitle){
    switch(domainTitle){
      case 'Clinical Care':
        return 'domain-one';
      case 'Education':
        return 'domain-two';
      case 'Research':
        return 'domain-three';
      case 'Optimising health systems':
        return 'domain-four';
      case 'Leadership':
        return 'domain-five';
    }
  }
  
  scoreToPills(score){
    var classOne = score > 0.1 ? 'filled' : '';
    var classTwo = (score >= 0.5 ) ? 'filled' : '';
    var classThree = score >= 0.85 ? 'filled' : '';
    return (
    <ul className="progress-pills">
      <li className={classOne}></li>
      <li className={classTwo}></li>
      <li className={classThree}></li>
    </ul>
    );
  }

  // For anon data, saving for custom reporting

  handleSaveAnonReportData() {
    let targetQuiz = this.state.quizId;
    let quiz = this.props.userQuizzes.find((q) => { return +q.userQuizId === +targetQuiz});
    let results = quiz ? JSON.parse(quiz.results) : {
      answers: this.props.answers,
      results: this.props.results
    };
    this.props.anonSaveReportData(results,this.props.userData);
  }
  
  handleSendEmailReport(){
    let targetQuiz = this.state.quizId;
    let quiz = this.props.userQuizzes.find((q) => { return +q.userQuizId === +targetQuiz});
    let results = quiz ? JSON.parse(quiz.results) : {
      answers: this.props.answers,
      results: this.props.results
    };
    this.props.sendEmailReport(results,this.props.userData);
  }
  handleDownloadEmailReport(){
    let targetQuiz = this.state.quizId;
    let quiz = this.props.userQuizzes.find((q) => { return +q.userQuizId === +targetQuiz});
    let results = quiz ? JSON.parse(quiz.results) : {
      answers: this.props.answers,
      results: this.props.results
    };
    this.props.downloadEmailReport(results,this.props.userData);
  }
  
  render() {

    let quiz;
    if (this.state.quizId){
      let targetQuiz = this.state.quizId;
      quiz = this.props.userQuizzes.find((q) => { return +q.userQuizId === +targetQuiz});
    }
    let results = quiz ? JSON.parse(quiz.results).results : this.props.results;
    
    if (results && results.framework && !this.props.loading && !this.props.domainsLoading && !this.props.userDataLoading){
      let domains = this.props.domains.filter((d) => { return d.framework == results.framework});
      return (
      <div className="results-wrapper">
        <div className="row">
          <div className="col-12">
            <div className="content-block squishybottom">
              <div className="title-and-image">
                <img src="/img/completed.png" className="title-image"/>
                <h2>Completed:<br/><span className="result-date">{results.date}</span></h2>
              </div>
              <h1></h1>
            </div>
            <hr />
          </div>
        </div>
        <div className="row">
          <div className="col-12">
            <div className="content-block">
              <p className="text-center">You are working at the following level of practice for each domain.</p>
              {domains.map((domain,i) => {
                console.log(domain)
                return (
                  <div className={"results-domain " + this.domainClass(domain.title)} key={i}>
                    <div className="icon">
                      <img src={config.siteUrl + config.imagesDirectory + domain.image} />
                    </div>
                    <div className="name-and-score">
                      <span>{domain.title}</span>
                      {this.scoreToPills(results.score[domain.domainId])}
                    </div>
                  </div>
                )
              })}
            </div>
            <hr />
          </div>
          <div className="col-12">
            <div className="content-block">
              <p>Download your personalised report to find out what these results mean, see a detailed breakdown of your answers, results and your actions list!</p>

                <button className="btn" onClick={this.props.startEmailReport}>Email me the full report</button>
                {this.props.loggedIn && 
                <button className="btn inverse" onClick={this.props.startSaveLoggedIn}>Save to profile</button>
                }
                {!this.props.loggedIn && 
                <button className="btn inverse" onClick={this.props.startSave}>Save to profile</button>
                }
            </div>
          </div>
          <div className="col-12">
            <div className="content-block">
              <ActionsToSkillUp actions={results.actions}/>
            </div>
          </div>
        </div>
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

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: () => {
      dispatch(fetchFrameworkData('domain')); 
      dispatch(fetchFrameworkData('aspects'));
      dispatch(locationLabelUpdate('Results'));
      dispatch(fetchUserQuizzes());
      dispatch(sidebarClose());
    },
    startEmailReport: () => {
      dispatch(openModal('emailreport'));
    },
    startSave: () => {
      dispatch(openModal('doYouHaveAProfile'));
    },
    startSaveLoggedIn: () => {
      dispatch(openModal('signInToSave'));
    },
    anonSaveReportData: (quiz, user) => {
      dispatch(selfAssessmentResultsToReportJSON(quiz, user, null, null, null, false, true))
    },
    sendEmailReport: (quiz,user) => {
      dispatch(selfAssessmentResultsToReportJSON(quiz,user))
    },
    downloadEmailReport: (quiz,user) => {
      dispatch(selfAssessmentResultsToReportJSON(quiz, user, null, null, null, true, null))
    }
  }
}

const mapStateToProps = (state) => {
  return {
    results: state.app.quiz.selfAssessmentResults,
    userQuizzes: state.app.user.quizzes,
    loading: state.app.quiz.selfAssessmentResultsLoading,
    domains: state.app.framework.domain,
    domainsLoading: state.app.framework.domainDataLoading,
    userDataLoading: state.app.user.isLoading,
    userData: state.app.user.user,
    loggedIn: state.app.user.loggedIn,
    answers: state.app.quiz.selfAssessmentCurrentAnswers
  }
};


export default connect(mapStateToProps, mapDispatchToProps)(SelfAssessmentResults);