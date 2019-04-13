import React from 'react';

const VideoEmbed = (props) => (
  <div className="article-video-embed-wrapper">
    <div className="row">
      <div className="col-12">
        <div className="video-block">
          <div className="play-wrapper">
            <span className="oi" data-glyph="media-play"></span>
          </div>
        </div>
      </div>
    </div>
    <div className="row article-gradient-background">
      <div className="col-12">
        <p>{props.content.caption}</p>
      </div>
    </div>
  </div>

);

export default VideoEmbed;





