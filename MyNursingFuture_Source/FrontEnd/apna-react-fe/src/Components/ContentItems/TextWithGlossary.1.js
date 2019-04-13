import React, { Component } from 'react';
import { connect } from 'react-redux';
import { fetchGlossaryData, setCurrentGlossaryItem, openModal } from '../../Actions';

class TextWithGlossary extends Component {
  
  componentWillMount() {
    this.props.onMount();
  }
  

  
  render() {
    let _content;
    if (this.props.glossary && this.props.glossary.length > 0){
      let _text, _items, _result;
      _result = [];
      
      let regexString = '(' + this.props.glossary.map((g)=>{return g.term}).join('\\b)|(') + '\\b)';
      _text = this.props.text.replace(new RegExp(regexString, 'gi'),'||$&||');
      _items = _text.split('||');
      for (var i of _items){
        _result.push({
          text: i, 
          glossary: this.props.glossary.includes(i)
        })
      }
      _content = _result;
    } else {
      _content = this.props.text;
    }

    if (typeof _content === "string"){
      return (
        <div className="inline-div" dangerouslySetInnerHTML={{__html: _content}}></div>
      );
    } else {
      if (_content && _content.length > 0){
        return (<div>
          {_content.map((item,index) => { return (
            <div className="inline-div" key={index}>
              {item.glossary &&
                <a href="#" onClick={() => {this.showGlossaryDefinition(item.text)}}>{item.text}</a>
              }
              {!item.glossary && 
                <div className="inline-div" dangerouslySetInnerHTML={{__html: item.text}}></div>
              }
            </div>);
          })}</div>
        )
      } else {
        return <div className="inline-div"></div>
      }
    }


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
      dispatch(fetchGlossaryData());
    },
    showGlossaryDefinition: function(term){
      dispatch(setCurrentGlossaryItem(term));
      dispatch(openModal('glossary'));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(TextWithGlossary);