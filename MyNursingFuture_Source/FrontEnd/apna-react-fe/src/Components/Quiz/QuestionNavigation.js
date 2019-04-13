import React from 'react';
import { Link } from 'react-router-dom';

const QuestionNavigation = (props) => {
  //let exitText = props.type == 'selfAssessment' ? 'Save and Progress to Next Domain' : 'Exit and save';
  let exitText = 'Exit and save';
  //console.log('answerValue from nav', props);
  return (
  <div className="question-nav">
    <div className="row">
      <div className="col-sm-4 offset-sm-2 col-6">
        <div className="content-block">
          <button className="btn inverse" onClick={props.clickBack}>Back</button>
        </div>
      </div>
      {props.canFinish && !props.canNext && 
      <div className="col-sm-4 col-6">
        <div className="content-block">
          <button className="btn" onClick={props.submit} >View my results</button>
        </div>
      </div>
      }
      {!props.canFinish && !props.canNext && props.canSave && props.atEnd &&
        <div className="col-sm-4 col-6">
          <div className="content-block">
            <button className="btn" onClick={props.exitAndSave}>Save and continue</button>
          </div>
        </div>
      }
      {!props.atEnd && 
        <div className="col-sm-4 col-6">
          <div className="content-block">
            <button className="btn" onClick={props.clickNext} disabled={!props.canNext}>Next</button>
          </div>
        </div>
      }

    </div>
    {props.canSave && !props.atEnd &&
    <div className="row">
      <div className="col-12">
        <button className="btn" onClick={props.exitAndSave}>{exitText}</button>
      </div>
    </div>
    }
    {props.canFinish && props.canNext && 
    <div className="row">
      <div className="col-12">
        <button className="btn" onClick={props.submit} >View my results</button>
      </div>
    </div>
    }

  </div>

  );
}



export default QuestionNavigation;