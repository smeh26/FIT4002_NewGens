import React from 'react';
import { Button } from 'semantic-ui-react'

const QuestionNav = ({back, next}) => {
    return (
        <nav className="question-nav">
            <Button
                className='btn-secondary'
                size="huge"
                onClick={back}
                content="Back" />
            <Button
                className='btn-primary'
                size="huge"
                onClick={next}
                content="Next" />
        </nav>
    );
};

export default QuestionNav