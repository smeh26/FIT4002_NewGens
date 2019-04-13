import React from 'react';
import {Link} from 'react-router';
import {Button} from 'semantic-ui-react';

const QuizIntro_SkillsAssessment_Confirm = () => {
    return (
        <div className="quiz">
            <div className="quiz-title">
                <h2>Skills Self Assessment</h2>
            </div>
            <div className="quiz-body">
                <h1>Please read carefully!</h1>
                <p>
                    Before we start, please read our terms of use and privacy policy.
                    You can access this at any time using the links at bottom of every page.
                </p>
                <div>
                    <label>
                        <input type="checkbox" className="agree-checkbox" />
                        I agree to the terms of use & privacy policy
                    </label>
                </div>
                <div className="ssa-start-quiz">
                    <Link to={'/quiz/2'}>
                        <Button className="btn-primary" content='Next' />
                    </Link>
                </div>
            </div>
        </div>
    );
};

export default QuizIntro_SkillsAssessment_Confirm