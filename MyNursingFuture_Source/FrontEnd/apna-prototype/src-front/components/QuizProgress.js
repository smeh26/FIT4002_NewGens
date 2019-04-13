import React from 'react';
import { Progress } from 'semantic-ui-react';

const QuizProgress = ({percent, length, current}) => {
    var count = current !== length
        ? `${current + 1} / ${length}`
        : 'Done!';
    return (
        <div className="progress-block">
            <Progress
                percent={percent}
                color="teal"
                size="tiny"
                />
            <div className="count">
                {count}
            </div>
        </div>
    );
};

export default QuizProgress