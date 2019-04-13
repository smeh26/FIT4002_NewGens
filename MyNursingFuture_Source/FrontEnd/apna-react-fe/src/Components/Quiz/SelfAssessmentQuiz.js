import React from 'react';
import Quiz from './Quiz';

const SelfAssessmentQuiz = (props) => {
  return (
    <Quiz quizType='selfAssessment' domainFilter={props.match.params.domainFilter} framework={props.match.params.framework} />
  ); 
}
export default SelfAssessmentQuiz;