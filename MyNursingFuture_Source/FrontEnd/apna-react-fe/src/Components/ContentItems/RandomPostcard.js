import React from 'react';
import { connect } from 'react-redux';
import { fetchFrameworkData } from '../../Actions';
import PostcardCarousel from './PostcardCarousel'

const RandomPostcard = (props) => {
  if (!props.loading){
    let min,max,index;
    min = 1;
    max = props.postcards.length;
    index = Math.floor(Math.random() * (max - min)) + min;
    return <PostcardCarousel initialPointer={index} />
  } else {
    return <span></span>;
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

export default connect(mapStateToProps, mapDispatchToProps)(RandomPostcard);