import React from 'react';
import { Link } from 'react-router-dom';
import TextWithGlossary from './TextWithGlossary';
import config from '../../config';


const Default = (props) => (
  <div className={"row default-content-block " + props.content.style}>
    <div className="col-sm-12">
      <div className="content-block">
        <div className={"row" + props.content.contentItemId}>
          <div className="contentImageDiv">
              {props.content.image && 
                <img src={config.siteUrl + config.imagesDirectory + props.content.image}/>
              }
            </div>
            
            <div className="contentTitleImage">
              {props.content.titleImage && props.content.title &&
                <div className="title-and-image">
                  <img src={config.siteUrl + config.imagesDirectory + props.content.titleImage} className="title-image"/>
                  <h2>{props.content.title}</h2>
                </div>
              }
              </div>
       
        <div className="contentTitleTextAndButtonDiv">
            {props.content.title && !props.content.titleImage &&
              <h1 className="text-center">{props.content.title}</h1>
            }
          
            {props.content.text && 
              <TextWithGlossary text={props.content.text} />
            }
      
            {props.content.buttonLink && 
              <Link to={props.content.buttonLink.href} className="btn" onClick={() => {window.scroll(0,0)}}>{props.content.buttonLink.text}</Link>
            }
       
            {props.content.link &&
              <Link to={props.content.link.href} onClick={() => {window.scroll(0,0)}}>{props.content.link.text}</Link>
            }
            
            {props.content.horizontalRule && 
            <hr />
            }
        </div>
        <div style={{"clear":"both"}}></div>
        </div>
      </div>
    </div>
  </div>
);
// <div className="inline-div" dangerouslySetInnerHTML={{__html: props.content.text}}></div>
export default Default;