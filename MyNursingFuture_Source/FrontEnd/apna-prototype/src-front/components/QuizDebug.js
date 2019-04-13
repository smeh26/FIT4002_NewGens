import React from 'react';

const QuizDebug = ({responses, complete}) => {
    return (
        <div className="debug">
            complete = {JSON.stringify(complete)} <br />
            <pre>
                {JSON.stringify(responses, false, 4)}
            </pre>
        </div>
    );
};

export default QuizDebug