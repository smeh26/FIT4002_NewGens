import React from 'react';

const EducationOpportunities = (props) => {
  let educationOpportunities = JSON.parse(props.content.educationOpportunities);
  if (!educationOpportunities) { return <div></div>}
  return (
    <div className="row"> 
        <div className="col-12">
          <div className="content-block">
            {educationOpportunities.map((eo) => {
              let levelText = (eo.level == 2 ? 'Advanced' : (eo.level == 1) ? 'Intermediate' : 'Foundation' )
              return (
                <div className="text-center">
                  <h4 className="inline bump-right">{levelText}</h4>
                  <ul className="progress-pills green inline">
                    <li className={(eo.level >= 0) ? 'filled' : ''}></li>
                    <li className={(eo.level >= 1) ? 'filled' : ''}></li>
                    <li className={(eo.level >= 2) ? 'filled' : ''}></li>
                  </ul>
                  <div dangerouslySetInnerHTML={{__html: eo.text}}></div>
                </div>
              );
            })} 
          </div>
        </div>
    </div>
  );
}

export default EducationOpportunities;





