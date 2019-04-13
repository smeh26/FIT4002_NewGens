import React from 'react';
import {Link} from 'react-router';
import {Button} from 'semantic-ui-react';

const QuizIntro_CareerPathways = () => {
    return (
        <div className="quiz">
            <div className="quiz-title">
                <h2>Career Pathways</h2>
            </div>
            <div className="quiz-body">
                <h1>Is primary health care nursing right for you?</h1>
                <h5>Why take this quiz?</h5>
                <p>We have 15 questions to find out more about you, your interests and the type of work you enjoy doing. At the end, we will present you with a number of career pathways in the primary health sector that are best suited to you!</p>
                <p>The quiz will take a maximum of 10 minutes of your time.</p>
                <h5>Please read carefully!</h5>
                <p>Before we start, please read our terms of use and privacy policy. You can access this information at any time using the links at the bottom of every page.</p>
                <p>
                    <label>
                        <input type="checkbox" className="agree-checkbox" />
                        I agree to the terms of use & privacy policy
                    </label>
                </p>

                <Link to={'/quiz/1'}>
                    <Button className="btn-primary" content='Start Quiz' />
                </Link>
            </div>
        </div>
    );
};

export default QuizIntro_CareerPathways