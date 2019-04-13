import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import {scoreCareerQuiz, scoreSelfAssessmentQuiz} from './Actions/RoboRamiro';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
});

test('score self assessment works', () => {
  var saqAnswers = [
    { questionId: 0, aspectId: 0, domainId: 1, value: 1},
    { questionId: 1, aspectId: 1, domainId: 1, value: 1},
    { questionId: 2, aspectId: 2, domainId: 1, value: 0.5},
    { questionId: 3, aspectId: 3, domainId: 1, value: 0},
    { questionId: 4, aspectId: 3, domainId: 1, value: 0},
    { questionId: 5, aspectId: 4, domainId: 1, value: 0},
    { questionId: 6, aspectId: 5, domainId: 1, value: 0},
    { questionId: 7, aspectId: 6, domainId: 1, value: 1},
    { questionId: 8, aspectId: 7, domainId: 1, value: 1},
    { questionId: 9, aspectId: 8, domainId: 1, value: 1},
    { questionId: 10, aspectId: 10, domainId: 1, value: 1},
    { questionId: 11, aspectId: 11, domainId: 1, value: 1},
    { questionId: 12, aspectId: 12, domainId: 2, value: 1},
    { questionId: 13, aspectId: 13, domainId: 2, value: 1},
    { questionId: 14, aspectId: 14, domainId: 2, value: 1},
    { questionId: 15, aspectId: 15, domainId: 2, value: 1},
    { questionId: 16, aspectId: 16, domainId: 3, value: 1},
    { questionId: 17, aspectId: 17, domainId: 3, value: 0.5},
    { questionId: 18, aspectId: 18, domainId: 3, value: 0.5},
    { questionId: 19, aspectId: 19, domainId: 3, value: 0.5},
    { questionId: 20, aspectId: 20, domainId: 4, value: 0.5},
    { questionId: 21, aspectId: 21, domainId: 4, value: 0.5},
    { questionId: 22, aspectId: 22, domainId: 4, value: 0.5},
    { questionId: 23, aspectId: 23, domainId: 4, value: 0.5},
    { questionId: 24, aspectId: 24, domainId: 4, value: 0.5},
    { questionId: 25, aspectId: 25, domainId: 5, value: 0.5},
    { questionId: 26, aspectId: 26, domainId: 5, value: 0.5},
    { questionId: 27, aspectId: 27, domainId: 5, value: 0.5},
    { questionId: 28, aspectId: 28, domainId: 5, value: 0.5},
    { questionId: 29, aspectId: 29, domainId: 5, value: 0.5}
  ];
  console.log(scoreSelfAssessmentQuiz(saqAnswers));
  
});

test('score career pathways works', () => {
  var cpqAnswers = [
    { questionId: 0,  value: 4},
    { questionId: 1, value: 2},
    { questionId: 2, value: 6},
    { questionId: 3, value: 1},
    { questionId: 4, value: 1},
    { questionId: 5, value: 1},
    { questionId: 6, value: 0},
    { questionId: 7, value: 1},
    { questionId: 8, value: 1},
    { questionId: 9, value: 1},
    { questionId: 10, value: 1},
    { questionId: 11, value: 0},
    { questionId: 12, value: 1},
    { questionId: 13, value: 1},
    { questionId: 14, value: 1},
    { questionId: 15, value: 1},
    { questionId: 16, value: 1},
    { questionId: 17, value: 0.5}
  ];
  console.log(scoreCareerQuiz(cpqAnswers));
});


