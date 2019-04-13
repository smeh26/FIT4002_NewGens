import React from 'react';
import { Button } from 'semantic-ui-react';
import QuizProgress from './QuizProgress';
import QuizDebug from './QuizDebug';

const QuizComplete = ({quiz, questionIdx, showResults}) => {
    return (
        <div className="quiz">
            <div className="quiz-title">
                <h2>{quiz.name}</h2>
            </div>
            <div className="quiz-body">
                <QuizProgress
                    percent="100"
                    length={quiz.questions.length}
                    current={questionIdx}
                    />
                <div className="quest">
                    <h3>Good work! You have completed all the questions.</h3>
                </div>
                <Button
                    className="btn-primary"
                    size="huge"
                    color="black"
                    onClick={showResults}
                    content="View my results"
                    />
            </div>
        </div>
    );
};

export default QuizComplete