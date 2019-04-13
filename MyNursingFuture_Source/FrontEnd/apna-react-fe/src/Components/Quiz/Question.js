import React, { Component } from 'react';
import { connect } from 'react-redux';
import { 
  fetchFrameworkData, 
  setCareerPathwaysAnswer,
  setCareerPathwaysMultiAnswer,
  setSelfAssessmentAnswer,
  setSelfAssessmentMultiAnswer,
  setAboutYouQuizAnswer,
  setAboutYouMultiAnswer,
  setSurveyAnswer
} from '../../Actions'
import TextWithGlossary from '../ContentItems/TextWithGlossary';

class Question extends Component {
  constructor(props){
    super(props);
    
  }
  
  componentWillMount() {
    this.props.onMount();
  }  
  
  render(){

    if (!this.props.question){return <div></div>};
    var answers, setAnswer, setMultiAnswer;
    if (this.props.question.quizType == 'ASSESSMENT'){
      answers = this.props.selfAssessmentQuizAnswers;
      setAnswer = this.props.setSelfAssessmentAnswer;
      setMultiAnswer = this.props.setSelfAssessmentMultiAnswer;
    } else if (this.props.question.quizType == 'PATHWAY'){
      answers = this.props.careerPathwaysQuizAnswers;
      setAnswer = this.props.setCareerPathwaysAnswer;
      setMultiAnswer = this.props.setCareerPathwaysMultiAnswer;
    } else if (this.props.question.quizType == 'ABOUT'){
      answers = this.props.aboutYouAnswers;
      setAnswer = this.props.setAboutYouAnswer;
      setMultiAnswer = this.props.setAboutYouMultiAnswer;
    }
    
    if (this.props.quizType === 'SURVEY'){
      answers[this.props.question.questionId] = this.props.surveyAnswer;
      setAnswer = this.props.setSurveyAnswer; 
    }
    
    if (!this.props.question || this.props.dataLoading){return <div></div>};
    
    let answerValue = answers[this.props.question.questionId] || undefined;
    let patientTitleRegex = /(individual accessing care|individuals accessing care|an individual's|individual's)/gi;
    let choices = [];
    if (this.props.question.type === 'CHOICE' || this.props.question.type === 'MULTI'){
      for (var answer in this.props.question.answers){
        let curAnswer = Object.assign({},this.props.question.answers[answer]);
        if (this.props.question.quizType == 'ASSESSMENT'){
          if (curAnswer.text){
            let rOne = /(learning|learn)\b/gi;  
            let rTwo = /(confidently|confidence|confident)\b/gi;
            let rThree = /(leading|leader|lead|guiding|guide)\b/gi;
            
            curAnswer.text = curAnswer.text.replace(patientTitleRegex, this.props.patientTitle);
            curAnswer.text = (curAnswer.value === 0.33 ? curAnswer.text.replace(rOne, "<strong>$1</strong>") : curAnswer.text);
            curAnswer.text = (curAnswer.value === 0.66 ? curAnswer.text.replace(rTwo, "<strong>$1</strong>") : curAnswer.text);
            curAnswer.text = (curAnswer.value === 1 ? curAnswer.text.replace(rThree, "<strong>$1</strong>") : curAnswer.text);
          }
        }
        choices.push(curAnswer);
      }
    }
    
    return (
    <div className="row">
      <div className="col-sm-12">
        <div className="content-block">
          {this.props.question.quizType === 'ASSESSMENT' && 
            <TextWithGlossary 
            text={'<p className="text-left">'+this.props.question.text.replace(patientTitleRegex, this.props.patientTitle)+'</p>'} />
          }
          {(this.props.question.quizType === 'PATHWAY' || this.props.question.quizType === 'ABOUT') && <hr /> && 
            <TextWithGlossary text={"<h2>"+this.props.question.text+"</h2>"} />
          }
          
          {this.props.question.subText && 
            <TextWithGlossary text={'<p class="text-smaller">'+this.props.question.subText+"</p>"} />
          }
          {this.props.question.examples && this.props.question.examples.map && this.props.question.examples.length > 0 && 
            <div className="example-block grey-area">
            <strong>Such as:</strong>
            <ul>
              {this.props.question.examples.map((e,i) => {
                return (<li key={i}>
                  <TextWithGlossary text={e.Text.replace(patientTitleRegex, this.props.patientTitle)}/>
                  </li>);
              })}
            </ul>
            </div>
          }
          {this.props.question.quizType === 'ASSESSMENT' && <h2 className="text-left">Pick a statement that best describes the level you work at:</h2> }
          {this.props.question.type === 'INPUT' && 
          <div className="quiz-input-wrapper">
            {this.props.question.answers.map((answer, i) => {
              if (answer.type === 'ADDRESS'){
                let currentAnswerValue = answers[this.props.question.questionId] || {};
                return (
                  <div className="row" key={answer.answerId}>
                    <div className="col-12">
                      <h3 className="text-left">Please input your address</h3>
                      <div className="input-block" >
                        <input type="text" 
                          onChange={(e) => {setAnswer(this.props.question.questionId, Object.assign({},currentAnswerValue,{'country': e.target.value},))}} 
                          placeholder="Country" 
                          value={answers[this.props.question.questionId] ? answers[this.props.question.questionId].country : ''} />
                        <input type="text" 
                          onChange={(e) => {setAnswer(this.props.question.questionId, Object.assign({},currentAnswerValue,{'state': e.target.value}))}} 
                          placeholder="State" 
                          value={answers[this.props.question.questionId] ? answers[this.props.question.questionId].state : ''} />
                        <input type="text" 
                          onChange={(e) => {setAnswer(this.props.question.questionId, Object.assign({},currentAnswerValue,{'suburb': e.target.value}))}} 
                          placeholder="Suburb" 
                          value={answers[this.props.question.questionId] ? answers[this.props.question.questionId].suburb : ''} />
                        <input type="text" 
                          onChange={(e) => {setAnswer(this.props.question.questionId, Object.assign({},currentAnswerValue,{'postalCode': e.target.value}))}} 
                          placeholder="Postal code" 
                          value={answers[this.props.question.questionId] ? answers[this.props.question.questionId].postalCode : ''} />
                      </div>
                    </div>
                  </div>
                )
              } else if (this.props.question.fieldName === 'Age'){
                let min = new Date().getFullYear() - 120;
                let max = new Date().getFullYear() - 9;
                let years = [];
                for (let i = min; i < max; i++){
                  years.push(i);
                }
                return (
                  <div className="row" key={answer.answerId}>
                    <div className="col-12">
                      <h3 className="text-left">{answer.text}</h3>
                      <div className="input-block" >
                        <select className="select select-aqua"
                          onChange={(e) => {setAnswer(this.props.question.questionId, e.target.value)}} 
                          value={answers[this.props.question.questionId]}>
                          <option value="">Choose a year</option>
                          { years.map((y) => {
                            return <option value={y} key={y}>{y}</option>
                          })}
                        </select>
                      </div>
                    </div>
                  </div>
                )
              } else {
                return (
                  <div className="row" key={answer.answerId}>
                    <div className="col-12">
                      <h3 className="text-left">{answer.text}</h3>
                      <div className="input-block" >
                        <input type="text" 
                        onChange={(e) => {setAnswer(this.props.question.questionId, e.target.value)}}
                        value={answers[this.props.question.questionId]} />
                      </div>
                    </div>
                  </div>
                )
              }
            })}
          </div>
          }
          {this.props.question.type === 'RANGE' && 
          <div className={"quiz-range-wrapper " + ((typeof(answerValue) == "undefined") ? 'unset' : '') }>  
            <input type="range" min="0" max="1" step="0.01" onChange={(e) => {setAnswer(this.props.question.questionId, e.target.value)}} value={answerValue || 0}/>
            <div className="range-labels">
              <div className="label" key={0}>{this.props.question.answers[0].text}</div>
              <div className="label" key={2}>{this.props.question.answers[2].text}</div>
            </div>
          </div>
          
          }
          {this.props.question.type === 'CHOICE' && 
          <div className="quiz-choices-wrapper">
            {choices.map((answer, i) => {
              let cl = 'quiz-choice ' + (answers[this.props.question.questionId] == answer.value ? 'active' : '');
              return <div className={cl} onClick={() => setAnswer(this.props.question.questionId, answer.value)} key={i}  dangerouslySetInnerHTML={{__html: answer.text.replace(patientTitleRegex, this.props.patientTitle)}}></div>
            })}
          </div>
          }
          {this.props.question.type === 'HYBRID' && 
          <div className="quiz-choices-wrapper">
            {this.props.question.answers.map((answer, i) => { 
              if (answer.type == 'TEXTVALUE'){
                let cl = 'quiz-choice ' + (answers[this.props.question.questionId] == answer.text ? 'active' : '');
                return <div className={cl} onClick={() => setAnswer(this.props.question.questionId, answer.text)} key={i}  dangerouslySetInnerHTML={{__html: answer.text}}></div>
              } else {
                let cl = 'quiz-user-input ' + (answers[this.props.question.questionId] == answer.text ? 'active' : '');
                return (
                  <div className="row" key={answer.answerId}>
                    <div className="col-12">
                      <h3 className="text-left">{answer.text}</h3>
                      <div className={"input-block " + cl} >
                        <input type="text"  
                        onChange={(e) => {setAnswer(this.props.question.questionId, e.target.value);}}
                         />
                      </div>
                    </div>
                  </div>
                )
              } 
            })}
          </div>
          }
          {this.props.question.type === 'MULTI' && 
          <div className="quiz-multi-choices-wrapper">
            {choices.map((answer, i) => {
              let checked = (answers[this.props.question.questionId] && answers[this.props.question.questionId].includes(answer.value));
              let cl = 'quiz-multi-choice ' + (checked ? 'active' : '');
              return <div className={cl} key={i}>
                <label htmlFor={this.props.question.questionId + 'multichoice' + i} onClick={() => setMultiAnswer(this.props.question.questionId, answer.value)} >
                  <span className={'input-styled' + (checked ? ' checked' : '')}>
                  {checked && 
                    <span className="oi" data-glyph="check"></span>
                  }
                  </span>
                  <input type='checkbox'
                    checked={checked} 
                    name={this.props.question.questionId + 'multichoice' + i} />
                  <span>{answer.text}</span>
                </label>
              </div>
            })}
          </div>
          }
        </div>
      </div>
    </div>
    );
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
    dataLoading: state.app.framework.domainDataLoading,
    loggedIn: state.app.user.loggedIn,
    aboutYouQuiz: state.app.quiz.aboutYouQuiz,
    aboutYouAnswers: state.app.quiz.aboutYouAnswers,
    surveyQuestion: state.app.quiz.surveyQuestion,
    surveyAnswer: state.app.quiz.surveyAnswer
  }
};

const mapDispatchToProps = (dispatch) => {
  return {
    onMount: () => {
      dispatch(fetchFrameworkData()); 
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
    setAboutYouAnswer: (id,data) => {
      dispatch(setAboutYouQuizAnswer(id,data));
    },
    setAboutYouMultiAnswer: (id,data) => {
      dispatch(setAboutYouMultiAnswer(id,data));
    },
    setSurveyAnswer: (id,data) => {
      dispatch(setSurveyAnswer(data));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Question);
