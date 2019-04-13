import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchFrameworkData, locationLabelUpdate, sidebarClose, fetchUserQuizzes, openModal, fetchSaveAnonymousCarerrQuiz } from '../../Actions';
import CareerPathwayMatchAccordion from './CareerPathwayMatchAccordion';
import MarkupBlock from '../ContentItems/MarkupBlock';
import { defaultPageContentByEndpoint } from '../../Actions/RoboRamiro';

class CareerPathwaysResults extends Component {
  constructor(props){
    super(props);
    this.scoreToPills = this.scoreToPills.bind(this);
    this.state = {
      quizId: this.props.match.params.id,
      showSectors: 5
    }
    this.showMoreSectors = this.showMoreSectors.bind(this);
    this.getFrameworkFromUserData = this.getFrameworkFromUserData.bind(this);
  }
  
  showMoreSectors(){
    this.setState({showSectors: this.state.showSectors + 5});
  }
  
  componentWillMount() {
    this.props.onMount();
  }
  
  getFrameworkFromUserData(){
    let fw;
    if (this.props.aboutYouQuiz && this.props.aboutYouQuiz.length > 0){
      let ntq = this.props.aboutYouQuiz.find((q) => { return q.fieldName === "NurseType"});
      if (this.props.user && this.props.user.nurseType){
        let ntqv = ntq.answers.find((a) => { return a.value == this.props.user.nurseType});
        if (ntqv.text == 'RN' || ntqv.text == 'RN/RM'){
          return 'rn';
        } else {
          return 'en'
        }
      } else if (this.props.aboutYouAnswers && ntq){
        let ntqv = ntq.answers.find((a) => { return a.value == this.props.aboutYouAnswers[ntq.questionId]});
        if (ntqv && ntqv.text){
          if (ntqv.text == 'RN' || ntqv.text == 'RN/RM'){
            return 'rn';
          } else {
            return 'en';
          }
        } else {
          return 'rn';
        }
      }
    } else {
      return 'rn';
    }
  }
  
  scoreToPills(score){
    var classOne = score > 0.1 ? 'filled' : '';
    var classTwo = (score > 0.3 ) ? 'filled' : '';
    var classThree = score > 0.6 ? 'filled' : '';
    return (
    <ul className="progress-pills">
      <li className={classOne}></li>
      <li className={classTwo}></li>
      <li className={classThree}></li>
    </ul>
    );
  }
  
  render() {
    let quiz;
    if (this.state.quizId){
      let targetQuiz = this.state.quizId;
      quiz = this.props.userQuizzes.find((q) => { return +q.userQuizId === +targetQuiz});
    }
    let results = quiz ? JSON.parse(quiz.results).results : this.props.results;
    if (results && !this.props.loading && !this.props.domainDataLoading && !this.props.sectorsDataLoading){
      //let framework = this.getFrameworkFromUserData();
      let framework = 'rn';
      var sectors = [];
      
      for (let s in results.score){
        if (results.score.hasOwnProperty(s)){
          let sectorsList = this.props.sectors;
          let matchedSector = sectorsList.find((a) => {return a.sectorId == s}) || {};
          
          let sect = {
            name: matchedSector.title || matchedSector.name || 'Sector not found',
            sectorId: matchedSector.sectorId,
            positives: results.scorePositives[s],
            negatives: results.scoreNegatives[s],
            percentMatch: results.scorePercentages[s]
          }
          if (matchedSector.sectorRn && matchedSector.sectorRn.intro && framework == 'rn'){
            sect.description = matchedSector.sectorRn.intro;
          }
          if (matchedSector.sectorEn && matchedSector.sectorEn.intro && framework == 'en'){
            sect.description = matchedSector.sectorEn.intro;
          }
          
          if (framework == 'en' && sect.sectorId == 5){
            sect.caveat = "Only in some States/Territories. Please check with your relevant State/Territory NMBA Boards."
          }
          
          sectors.push(sect);
          sectors.sort((a,b) => {
            if (a.percentMatch < b.percentMatch){ return 1;}
            if (a.percentMatch > b.percentMatch){ return -1;}
            return 0;
          });
        }
      }
      
      return (
      <div className="results-wrapper">
        <div className="row">
          <div className="col-12">
            <div className="content-block">
              <h1>Top matches to your profile</h1>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col-12">
            <div className="content-block">
            { sectors.map((sector, i) => { 
              if (i <= this.state.showSectors){
              return (
                <CareerPathwayMatchAccordion title={i+1 + '. ' + sector.name} percentMatch={sector.percentMatch} key={i}>
                  <div className="career-pathways-result-sector">
                    { sector.positives && sector.positives.map((positive, ii) => { return (
                      <p key={ii} className="positive"><span className="oi" data-glyph="check"></span> {positive}</p>
                    )})}                    
                    { 
                      /* Issue 38 - Only show matched attributes*/
                      /*
                      sector.negatives && sector.negatives.map((negative, ii) => { return (
                      <p key={ii} className="negative"><span className="oi" data-glyph="x"></span> {negative}</p>
                    )})*/
                    }
                    <h1>About {sector.name}</h1>
                    {sector.caveat && <p><i><strong>{sector.caveat}</strong></i></p> }
                    <MarkupBlock content={{text: sector.description}}/>
                    <br />
                    <Link to={'/sectors/' + sector.sectorId}>
                      <button className="btn">Learn more</button>
                    </Link>
                  </div>
                </CareerPathwayMatchAccordion>
              )
              }
            }
          )}
            </div>
            <hr />
          </div>
          <div className="col-12">
            {this.props.sectors && this.state.showSectors < this.props.sectors.length &&
            <button className="btn" onClick={this.showMoreSectors}>View more matches +</button>
            }
            {this.props.loggedIn && this.state.quizId &&
            <button className="btn inverse" onClick={this.props.startSaveLoggedIn}>Save to profile</button>
            }
            {!this.props.loggedIn && 
            <button className="btn inverse" onClick={this.props.startSave}>Save to profile</button>
            }
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
      dispatch(fetchFrameworkData()); 
      dispatch(locationLabelUpdate('Results'));
      dispatch(fetchUserQuizzes());
      dispatch(sidebarClose());
      dispatch(fetchSaveAnonymousCarerrQuiz());
    },
    startSave: () => {
      dispatch(openModal('doYouHaveAProfile'));
    },
    startSaveLoggedIn: () => {
      dispatch(openModal('signInToSave'));
    }
  }
}

const mapStateToProps = (state) => {
  return {
    results: state.app.quiz.careerPathwaysResults,
    loading: state.app.quiz.careerPathwaysResultsLoading,
    domains: state.app.framework.domain,
    domainsLoading: state.app.framework.domainDataLoading,
    sectors: state.app.framework.sectors,
    sectorsLoading: state.app.framework.sectorsDataLoading,
    userQuizzes: state.app.user.quizzes,
    loggedIn: state.app.user.loggedIn,
    user: state.app.user.user,
    aboutYouAnswers: state.app.quiz.aboutYouAnswers,
    aboutYouQuiz: state.app.quiz.aboutYouQuiz
  }
};


export default connect(mapStateToProps, mapDispatchToProps)(CareerPathwaysResults);