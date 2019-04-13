import React from 'react';
import QuizIntro_CareerPathways from './QuizIntro_CareerPathways';
import QuizIntro_SkillsAssessment from './QuizIntro_SkillsAssessment';
import QuizIntro_SkillsAssessment_Confirm from './QuizIntro_SkillsAssessment_Confirm';
import QuizIntro_SomeOther from './QuizIntro_SomeOther';

const QuizIntro = ({params}) => {

    var intro;
    if(parseInt(params.quiz_id) == 1) {
        intro = <QuizIntro_CareerPathways />;
    }
    else if(params.quiz_id == '2a') {
        intro = <QuizIntro_SkillsAssessment_Confirm />;
    }
    else if(parseInt(params.quiz_id) == 2) {
        intro = <QuizIntro_SkillsAssessment />;
    }
    else if(parseInt(params.quiz_id) == 3) {
        intro = <QuizIntro_SomeOther />;
    }

    return intro;
};

export default QuizIntro