import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import ActionAccordion from '../ContentItems/ActionAccordion';

class ActionsToSkillUp extends Component {
  constructor(props){
    super(props);
    this.state = {
      domainFilter: 'all',
      actions: props.actions || [],
      framework: props.framework || 'rn',
      showing: 5
    };
    this.handleChangeDomainFilter = this.handleChangeDomainFilter.bind(this);
    this.handleShowMore = this.handleShowMore.bind(this);
  }
  
  handleChangeDomainFilter(event){
    this.setState({domainFilter: event.target.value});
    this.setState({showing: 5});
  }
  handleShowMore(){
    this.setState({showing: this.state.showing+5});
  }

  render () {
    let actionList = [];
    for (let a in this.props.actions){
      if (this.state.domainFilter === 'all' || a == this.state.domainFilter){
        let ia = this.props.actions[a];
        if (this.props.actionsReference && Object.keys(this.props.actionsReference).length > 0){
          let iam = ia.map((actionId) => {
            return this.props.actionsReference.find((aa) => {return aa.actionId == actionId});
          })    
          actionList = actionList.concat(iam);
        }
      }
    }
    return <div className="row">
            <div className="col-sm-12">
            {this.props.domains && 
              <div className="content-block">
                <div className="row">
                  <div className="offset-sm-3 col-sm-6">
                  <h2>Actions to skill up to the next level of practice</h2>
                    <select className="select select-aqua" value={this.state.domainFilter} onChange={this.handleChangeDomainFilter}>
                      <option value="all">All domains</option>
                      {this.props.domains.map((d,i) => { if (d.framework == this.state.framework){
                        return(
                        <option value={d.domainId} key={i}>{d.title}</option>
                      )
                      }})}
                    </select>
                  </div>
                </div>
                <div className="row">
                  <div className="col-12">
                  { this.props.actionsReference && actionList.map((a,i) => { 
                    if (i < this.state.showing && a){
                    return (
                      <ActionAccordion action={a} index={i+1} key={i}/>
                    )
                    }
                  })}
                  </div>
                </div>
              </div>
            }
            </div>
            <div className="row">
              <div className="col-12">
              { this.state.showing < actionList.length && 
                <button onClick={this.handleShowMore} className="btn">Show more</button>
              }
              </div>
            </div>
          </div>
  }
}

const mapStateToProps = (state) => {
  return {
    actionsReference: state.app.framework.actions,
    domains: state.app.framework.domain
  }
};

export default connect(mapStateToProps)(ActionsToSkillUp);

