import React, { Component } from 'react';
import { connect } from 'react-redux';
import '../../styles/bootstrap-table.css';
import { declineMatch, applyMatch } from '../../Actions/MatchesActions';

class PendingMatches extends Component {

    //deletes match, used for clicking on decline button
    declineClick = (matchId) => {
        console.log(matchId);
        this.props.declineMatch(matchId);
    }

    //accepts match, used for clicking on apply button
    applyClick = (matchId) => {
        console.log(matchId);
        this.props.applyMatch(matchId)
    }

    //format date in YYYY-MM-DD format
    formatDate = (date) => {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }

    render() {
        return (
            //Pending Matches Table
            <div>
            <h2 className="text-center">Pending Matches</h2>
                <p></p>
                <table className="table table-hover table-condensed" style={{ width: '100%' }}>
                    <thead>
                        <tr>
                            <th scope="col">Employer</th>
                            <th scope="col">Title</th>
                            <th scope="col">Location</th>
                            <th scope="col">Application Deadline</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>

                    <tbody>
                        {
                            //map over matches and add available matches to the table
                            this.props.matches.map(match => {
                                if (match.isApplied === false) {

                                    let deadline = this.formatDate(match.closeDate);
                                    return (
                                        <tr key={match.matchId}>
                                            <td >{match.employer}</td>
                                            <td >{match.title}</td>
                                            <td >{match.location} </td>
                                            <td >{deadline}</td>
                                            <td>
                                                <button className="btnSmall" onClick={() => this.applyClick(match.matchId)}>Apply</button>
                                                <p></p>
                                                <button className="btnSmall" onClick={() => this.declineClick(match.matchId)}>Decline</button>
                                            </td>
                                        </tr>)
                                }
                            })
                        }

                    </tbody>
                </table>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        matches: state.app.matches.matches
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        declineMatch: (matchId) => { dispatch(declineMatch(matchId))},
        applyMatch: (matchId) => { dispatch(applyMatch(matchId))}
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(PendingMatches)
