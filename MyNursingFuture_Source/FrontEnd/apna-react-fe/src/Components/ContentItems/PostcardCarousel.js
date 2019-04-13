import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchFrameworkData } from '../../Actions';
import config from '../../config';

class PostcardCarousel extends Component {
  constructor(props){
    super(props);
    this.handleCarouselPrevious = this.handleCarouselPrevious.bind(this);
    this.handleCarouselNext = this.handleCarouselNext.bind(this);
  }

  componentWillMount() {
    this.props.onMount();
    this.state = {
      pointer: this.props.initialPointer || 0
    }
  }

  handleCarouselPrevious(){
    var pointer = this.state.pointer;
    var nextPointer = (pointer <= 1 ? this.props.postcards.length : pointer-1 );
    this.setState({
      pointer: nextPointer
    })
  }
  
  handleCarouselNext(){
    var pointer = this.state.pointer;
    var nextPointer = (pointer === this.props.postcards.length ? 1 : pointer+1);
    this.setState({
      pointer: nextPointer
    })
  }

  render() {
      var carouselClassName = 'postcards-carousel postcard row' + (this.props.variation ? ' variation' : '');
      var currentPostcard;

      if (this.state.pointer == 0)
          this.state.pointer = 1;

      if (this.props.postcards && !this.props.loading){
          currentPostcard = this.props.postcards[this.state.pointer - 1];
       }
      return (
        <div className={carouselClassName} key='1'>
          {this.props.postcards && !this.props.loading && currentPostcard &&
            <div className="col-12 carousel-item">
              <div className="content-block">
                      <h1>{currentPostcard.text}</h1>
                      <div className="contentImageDiv">
                          {currentPostcard.image &&
                              <img src={config.siteUrl + config.imagesDirectory + currentPostcard.image} />
                          }
                      </div>
                <span className="oi previous" data-glyph="chevron-left" onClick={this.handleCarouselPrevious}></span>
                <span className="oi next" data-glyph="chevron-right" onClick={this.handleCarouselNext}></span>
              </div>
            </div>
          }
        </div>);
  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.domainDataLoading,
    postcards: state.app.framework.postcards
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function() {
      dispatch(fetchFrameworkData());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(PostcardCarousel);