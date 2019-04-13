import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchFrameworkData } from '../../Actions';

class ReasonsList extends Component {
  constructor(props){
    super(props);
    this.handleCarouselPrevious = this.handleCarouselPrevious.bind(this);
    this.handleCarouselNext = this.handleCarouselNext.bind(this);
  }

  componentWillMount() {
    this.props.onMount();
    this.state = {
      pointer: 0
    }
  }

  handleCarouselPrevious(){
    var pointer = this.state.pointer;
    var nextPointer = (pointer === 0 ? this.props.reasons.length : pointer-1 );
    this.setState({
      pointer: nextPointer
    })
  }
  
  handleCarouselNext(){
    var pointer = this.state.pointer;
    var nextPointer = (pointer === this.props.reasons.length ? 0 : pointer+1);
    this.setState({
      pointer: nextPointer
    })
  }

  render() {
      console.log('reasons length = ' + this.props.reasons.length)
      var carouselClassName = 'reasons-carousel row' + (this.props.variation ? ' variation' : '');
      if (this.state.pointer == 0){
        return (
          <div className={carouselClassName} key='0'>
            {this.props.reasons && !this.props.loading &&
              <div className="carousel-item">
                <p><span className="numbers">{this.props.reasons.length + " "}</span><span className="words">reasons to choose primary health care nursing</span></p>
                <Link to={'/reasons'} onClick={() => {window.scroll(0,0)}} className="readfull">
                  Read full article
                </Link>
                <span className="oi previous" data-glyph="chevron-left" onClick={this.handleCarouselPrevious}></span>
                <span className="oi next" data-glyph="chevron-right" onClick={this.handleCarouselNext}></span>
              </div>
            }
          </div>);
      }else{
        var currentReason;
        if (this.props.reasons && !this.props.loading){
          currentReason = this.props.reasons[this.state.pointer-1];
        }
        return (
          <div className={carouselClassName} key='1'>
            {this.props.reasons && !this.props.loading && currentReason &&
              <div className="carousel-item">
                <Link to={'/reasons'} onClick={() => {window.scroll(0,0)}}>
                  <h1>{currentReason.title}</h1>
                </Link>
                <span className="oi previous" data-glyph="chevron-left" onClick={this.handleCarouselPrevious}></span>
                <span className="oi next" data-glyph="chevron-right" onClick={this.handleCarouselNext}></span>
              </div>
            }
          </div>);
      }
  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.domainDataLoading,
    reasons: state.app.framework.reasons
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function() {
      dispatch(fetchFrameworkData());
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ReasonsList);