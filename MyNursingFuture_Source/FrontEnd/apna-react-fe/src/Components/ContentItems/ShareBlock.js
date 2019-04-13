import React from 'react';
import { Link } from 'react-router-dom';
import { FacebookButton, TwitterButton, LinkedInButton } from "react-social";

const ShareBlock = (props) => (
  <div className="row">
    <div className="col-sm-6 offset-sm-3">
      <div className="content-block">
        <div className="row push-top">
          <div className="col-4">
            <a href="https://www.facebook.com/APNAnurses" target="_blank" >
              <img src="/img/fb.png" className="social-icon"/>
            </a>
          </div>
          <div className="col-4">
            <a href="https://twitter.com/apnanurses" target="_blank">
              <img src="/img/twitter.png"  className="social-icon" />
            </a>
          </div>
          <div className="col-4">
            <a href="https://www.linkedin.com/company/australian-primary-health-care-nurses-association-apna-" target="_blank">
              <img src="/img/linkedin.png"  className="social-icon" />
            </a>
          </div>
        </div>
      </div>
    </div>
</div>
);

export default ShareBlock;





