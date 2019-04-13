import React from 'react';
import {Link} from 'react-router';
import {Button} from 'semantic-ui-react';

const QuizIntro_SomeOther = () => {
    return (
        <div className="quiz">
            <div className="quiz-title">
                <h2>Some Other Quiz</h2>
            </div>
            <div className="quiz-body">
                <p>Blaa blaa blaa blaa...</p>
                <Link to={'/quiz/3'}>
                    <Button className="btn-primary" content='Start Quiz' />
                </Link>
            </div>
        </div>
    );
};

export default QuizIntro_SomeOther