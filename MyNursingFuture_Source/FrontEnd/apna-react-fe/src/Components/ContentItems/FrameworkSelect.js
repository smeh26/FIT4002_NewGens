import React from 'react';
import { Link } from 'react-router-dom';

const FrameworkSelect = (props) => (
  <div className="row">
    <div className="col-sm-6 offset-sm-3">
      <div className="content-block">
        <div className="row">
          <div className="col-12">
            <p><a href="/explore/rn" className="btn">RN Framework</a></p>
          </div>
          <div className="col-12">
            <p><a href="/explore/en" className="btn">EN Framework</a></p>
          </div>
          <hr />
        </div>
      </div>
    </div>
</div>
);

export default FrameworkSelect;





