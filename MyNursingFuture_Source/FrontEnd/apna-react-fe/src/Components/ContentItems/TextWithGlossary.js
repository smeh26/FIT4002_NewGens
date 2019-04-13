import React, { Component } from 'react';
import { connect } from 'react-redux';
import { fetchGlossaryData, setCurrentGlossaryItem, openModal } from '../../Actions';

class TextWithGlossary extends Component {
  
  componentWillMount() {
    this.props.onMount();
    this.handleClickInside = this.handleClickInside.bind(this);
  }
  
  handleClickInside(e){
    let el = e.target;
    if (el.tagName == 'SPAN' && el.className == 'glossary-item'){
      let item = el.innerHTML;
      this.props.openGlossary(item);
    }
  }
  
  render() {
    let _content;
    if (this.props.glossary && this.props.glossary.length > 0 && this.props.text && this.props.text.length > 0){
      let _text, _items, _result;
      _result = [];
      let activeGlossary = this.props.glossary.filter((g) => {return g.name});
      let regexString = '(' + activeGlossary.map((g)=>{return g.name}).join('\\b)|(') + '\\b)';
      _text = this.props.text.replace(new RegExp(regexString, 'gi'),'<span class="glossary-item">$&</span>');
      _content = _text;
    } else {
      _content = this.props.text;
    }
    return (
      <div className="inline-div" dangerouslySetInnerHTML={{__html: _content}} onClick={this.handleClickInside}></div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    glossary: state.app.framework.glossary,
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function() {
      if (props.glossary && props.glossary.length){
        return;
      } else{
        dispatch(fetchGlossaryData());
      }
    },
    openGlossary: function(term) {
      dispatch(setCurrentGlossaryItem(term));
      dispatch(openModal('glossary'));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(TextWithGlossary);