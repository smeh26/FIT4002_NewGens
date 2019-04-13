import React from 'react';
import { Link } from 'react-router-dom';

const Heading = (props) => (
  <div className="row">
    <div className="col-sm-12">
      <div className="content-block single-line">
        <h1>{props.content.title}</h1>
      </div>
    </div>
  </div>
);

export default Heading;



