import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import ActionAccordion from './ActionAccordion';

class ActionsToGrow extends Component {
  constructor(){
    super();
    this.state = {
      level: ''
    };
    
    this.handleChangeLevel = this.handleChangeLevel.bind(this);
  }
  
  handleChangeLevel(event){
    this.setState({level: event.target.value});
  }

  render () {
    var level = this.state.level;
    var actions = [];
    if (!this.props.aspect){
      return <div></div>
    }
    if (this.state.level == 'all'){
      actions = this.props.aspect.actionsList;
    } else {
      actions = this.props.aspect.actionsList.filter((a) => { return ""+a.levelAction == ""+this.state.level});
    }
    

    return <div className="row">
            <div className="col-sm-12">
              <div className="content-block">
                <div className="row">
                  <div className="offset-sm-3 col-sm-6">
                  <h2>Actions to grow</h2>
                    <select className="select select-aqua" value={this.state.level} onChange={this.handleChangeLevel}>
                      <option value='' >Select your level</option>
                      <option value="0">Foundation level</option>
                      <option value="1">Intermediate level</option>
                      <option value="2">Advanced level</option>
                    </select>
                  </div>
                </div>
                <div className="row">
                  { actions.map((a,i) => { return (
                    <ActionAccordion action={a} index={i+1} key={i}/>
                  )})}
                </div>
              </div>
            </div>
          </div>
  }
}



export default ActionsToGrow;