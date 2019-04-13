import React from 'react';
import TextWithGlossary from './TextWithGlossary';

const CareerPathways = (props) => {
  let careerPathways = JSON.parse(props.content.careerPathways);
  if (!careerPathways || !careerPathways[0] || !careerPathways[0].title) { return <div></div>}
  return (
    <div className="row"> 
        <div className="col-12">
          <div className="career-pathways-block">
            <div className="pathway-level">
              <div className="pathway-level-descriptor">
                <h4>Foundation</h4>
                <ul className="progress-pills"><li className="filled"></li><li></li><li></li></ul>
              </div>
              <div className="pathway-info-block entry">
                <span className="circle"></span>
                <p><strong>{careerPathways[0].title}</strong></p>
                <div className="user-defined-markup">
                  <TextWithGlossary text={careerPathways[0].text}/>
                </div>
              </div>
            </div>
            <div className="pathway-level">
              <div className="pathway-level-descriptor mid">
                <h4>Intermediate</h4>
                <ul className="progress-pills"><li className="filled"></li><li className="filled"></li><li></li></ul>
              </div>
              <div className="pathway-info-block mid">
                <span className="circle"></span>
                <p><strong>{careerPathways[1].title}</strong></p>
                <div className="user-defined-markup">
                  <TextWithGlossary text={careerPathways[1].text}/>
                </div>
              </div>
            </div>
            <div className="pathway-level">
              <div className="pathway-level-descriptor advanced">
                <h4>Advanced</h4>
                <ul className="progress-pills"><li className="filled"></li><li className="filled"></li><li className="filled"></li></ul>
              </div>
              <div className="pathway-info-block advanced">
                <span className="circle"></span>
                <p><strong>{careerPathways[2].title}</strong></p>
                <div className="user-defined-markup">
                  <TextWithGlossary text={careerPathways[2].text}/>
                </div>
              </div>
            </div>
          </div>
        </div>
    </div>
  );
}

export default CareerPathways;





