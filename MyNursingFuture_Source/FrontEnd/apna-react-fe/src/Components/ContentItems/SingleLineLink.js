import React from 'react';
import { Link } from 'react-router-dom';

const SingleLineLink = (props) => (
  <div className="row">
    <div className="col-sm-12">
      <div className="content-block single-line">
        <h2>{props.content.link.text}</h2>
        {props.content.link.href.substring(0,4) === 'http' && 
        <a href={props.content.link.href} target="_blank">find out more here</a>
        }
        {props.content.link.href.substring(0,4) != 'http' && 
        <Link to={props.content.link.href}>find out more here</Link>
        }
      </div>
    </div>
  </div>
);

export default SingleLineLink;



