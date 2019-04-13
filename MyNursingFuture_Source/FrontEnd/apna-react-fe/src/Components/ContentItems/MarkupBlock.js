import React from 'react';
import { Link } from 'react-router-dom';
import TextWithGlossary from './TextWithGlossary';

const MarkupBlock = (props) => (
    <div className="row">
    <div className="col-sm-12">
      <div className="content-block">
        <div className="user-defined-markup">
          <TextWithGlossary text={props.content.text}/>
        </div>
      </div>
    </div>
  </div>
);

export default MarkupBlock;