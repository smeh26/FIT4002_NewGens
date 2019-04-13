import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchFrameworkData } from '../../Actions';
import TextWithGlossary from './TextWithGlossary';

class ReasonsList extends Component {
  constructor(props){
    super(props);
  }

  componentWillMount() {
    this.props.onMount();
  }


  render() {
    return (
      <div className="reasons-list-wrapper">
        <div className="row">
          <div className="col-12">
            <ol>
            {this.props.reasons && !this.props.loading && this.props.reasons.map(reason => 
                <li id={reason.ix} key={reason.ix}>
                  <h3>{reason.title}</h3>
                  <TextWithGlossary text={"<p>"+reason.text+"</p>"}/>
                </li>
              )}
            </ol>
          </div>
        </div>
      </div>
    );
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