import React from 'react';
import { Link } from 'react-router-dom';

const Postcard = (props) => (
    <div className="row postcard">
    <div className="col-sm-12">
      <div className="content-block ">
        {props.content.image && 
          <img src={props.content.image}/>
        }
        {props.content.title &&
          <h1>{props.content.title}</h1>
        }
        <div className="user-defined-markup" dangerouslySetInnerHTML={{__html: props.content.text}}></div>
      </div>
    </div>
  </div>
);
  
export default Postcard;