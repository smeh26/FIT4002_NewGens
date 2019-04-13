import React from 'react';

const QuizProgress = (props) => {
  return (
    <div className="row">
      <div className="col-sm-12">
        {props.mini && 
        <div className="">
          <div className="progress-indicator">
            <div className="progress" style={{width: ((props.complete / props.total)*100) + '%'}}></div>
          </div>
          <span>{props.complete >= props.total ? 'Complete!' : Math.round((+props.total - +props.complete) * 0.5) + ' min to complete'}</span>
        </div>
        }
        {!props.mini && 
        <div className="push-bottom">
          <p className="squishybottom"><span>{props.complete == props.total ? props.complete : props.currentQuestion}</span>/<span>{props.total}</span></p>
          <div className="progress-indicator">
            <div className="progress" style={{width: ((props.complete / props.total)*100) + '%'}}></div>
          </div>
          <hr/>
        </div>
        }
      </div>
    </div>
)};

export default QuizProgress;