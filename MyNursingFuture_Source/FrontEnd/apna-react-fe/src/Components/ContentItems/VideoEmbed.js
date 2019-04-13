import React, {Component} from 'react';
import Youtube from 'react-youtube';

class VideoEmbed extends Component {
  constructor(props) {
    super(props);

    this.state = {
      opts: {
        playerVars: {
          modestbranding: true,
          controls: 1,
          playsinline: 0,
          showinfo: 0
        }
      },
      optsXs: {
        width: '320',
        height: '180',
        playerVars: {
          modestbranding: true,
          controls: 0,
          playsinline: 0,
          showinfo: 0
        }
      },
      playing: false
    }

    this.handlePlay = this
      .handlePlay
      .bind(this);
    this.handlePause = this
      .handlePause
      .bind(this);
  }

  handlePlay() {
    this.setState({playing: true});
  }
  handlePause() {
    this.setState({playing: false});
  }

  render() {
    let blockClass = 'video-block' + (this.state.playing
      ? ' playing'
      : '');
    return (
      <div>
        <div className={"row hidden-xs row" + this.props.content.contentItemId}>
          <div className="col-12">
            <div className={blockClass}>
              <p>{this.props.content.caption}</p>
              <div className="play-wrapper">
                <Youtube
                  videoId={this.props.content.video}
                  className='yt-player'
                  opts={this.state.opts}
                  onPlay={this.handlePlay}/>
              </div>
            </div>
          </div>
        </div>
        <div className="row hidden-lg hidden-sm hidden-md">
          <div className="col-12">
            <div className={blockClass}>
              <p>{this.props.content.caption}</p>
              <div className="play-wrapper">
                <Youtube
                  videoId={this.props.content.video}
                  className='yt-player'
                  opts={this.state.optsXs}
                  onPlay={this.handlePlay}/>
              </div>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col-12 share-row share-row-small">
            <span className='share-text'>Share this video</span>
            <a
              href={'https://facebook.com/sharer.php?u=' + ('https://www.youtube.com/watch?v=' + this.props.content.video)}
              target="_blank">
              <img src="/img/fb.png" className="social-icon"/>
            </a>
            <a
              href={'https://twitter.com/home?status=' + ('https://www.youtube.com/watch?v=' + this.props.content.video)}
              target="_blank">
              <img src="/img/twitter.png" className="social-icon"/>
            </a>
            <a
              href={'https://www.linkedin.com/shareArticle?mini=true&url=' + ('https://www.youtube.com/watch?v=' + this.props.content.video)}
              target="_blank">
              <img src="/img/linkedin.png" className="social-icon"/>
            </a>
          </div>
        </div>
      </div>
    );
  }
}

export default VideoEmbed;
