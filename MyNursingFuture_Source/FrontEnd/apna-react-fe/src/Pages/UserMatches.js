import React, { Component } from 'react';
import { connect } from 'react-redux';
import { locationLabelUpdate } from '../Actions';
import PendingMatches from '../Components/Matches/PendingMatches';
import AppliedMatches from '../Components/Matches/AppliedMatches';

class UserMatches extends Component {

    componentWillMount(){
        this.props.onMount('Matches')
    }

    render() {
        return (
            <div>
                <PendingMatches></PendingMatches>
                <p></p>
                <AppliedMatches></AppliedMatches>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    
}

const mapDispatchToProps = (dispatch) => {
    return {
        onMount: (title) => { dispatch(locationLabelUpdate(title)) }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserMatches);