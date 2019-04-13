import React from 'react';
import {Link} from 'react-router';
import {Button} from 'semantic-ui-react';

const QuizIntro_SkillsAssessment = () => {
    return (
        <div className="quiz">
            <div className="quiz-title">
                <h2>Skills Self Assessment</h2>
            </div>
            <div className="quiz-body">
                <h1>Asses your current skill level</h1>
                <p>
                    <strong>Are you a registered or enrolled nurse? </strong>
                    Please select a framework that matches to your current or
                    anticipated role if you are a student?
                </p>

                <div className="ssa-start-quiz">
                    <img className="ssa-wheel-sample" src="/img/ssa_rn_wheel_sample.png" />
                    <Link to={'/quiz/intro/2a'}>
                        <Button className="btn-primary" content='Self asses using the RN framework' />
                    </Link>
                </div>

                <div className="ssa-start-quiz">
                    <img className="ssa-wheel-sample" src="/img/ssa_en_wheel_sample.png" />
                    <Link to={'/quiz/intro/2a'}>
                        <Button className="btn-primary" content='Self asses using the EN framework' />
                    </Link>
                </div>
            </div>
        </div>
    );
};

export default QuizIntro_SkillsAssessment