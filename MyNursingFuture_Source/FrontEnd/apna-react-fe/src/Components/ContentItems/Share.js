import React from 'react';
import { FacebookButton } from 'react-social';
const Share = (props) => (
    <div className="col-12 share-text">

        <hr className="full-hr" />
            <div className="row share-row">
            
            <div className="col-8">
                Share this with your nursing friends
            </div>
            <div className="col-1">
                <a className="share-row" href={'https://facebook.com/sharer.php?u=' + (props.url || 'mynursingfuture.com.au')} target="_blank">
                    <img src="/img/fb.png" className="social-icon" />
                </a>
            </div>
            <div className="col-1">
                <a className="share-row" href={'https://www.linkedin.com/shareArticle?mini=true&url=' + (props.url || 'mynursingfuture.com.au')} target="_blank">
                    <img src="/img/linkedin.png" className="social-icon" />
                </a>
            </div>
            <div className="col-1">
                <a className="share-row" href={'http://www.twitter.com/share?url=' + (props.url || 'mynursingfuture.com.au')} target="_blank">
                    <img src="/img/twitter.png" className="social-icon" />
                </a>
            </div>
            
            </div>
            <hr className="full-hr" />
    </div>

);

export default Share;





