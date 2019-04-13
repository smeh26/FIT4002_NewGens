import React from 'react';

const HeroImage = (props) => (
  <div>
      <div className="hero-image">
        <div className="hero-text">
            <div className="apna-img"><img src="/img/APNAlogo_white.png"/></div>
            <div><img className="tree-img" src="/img/MNF_tree-White.png"/></div>
            <p>My Nursing Future</p>
            <p className="hero-description">Supporting current and future nurses<br/>
to grow their careers in primary health care.</p>            
        </div>
       </div>             
  </div>
);

export default HeroImage;



