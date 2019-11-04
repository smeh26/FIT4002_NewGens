import React, { Component } from 'react';
import { connect } from 'react-redux';
import '../../styles/bootstrap-table.css';


class AppliedMatches extends Component {

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
            //Applied Matches Table
            <div>
            <h2 className="text-center">Applied Matches</h2>
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
                            //map over matches and only add matches to the table where isApplied is true
                            this.props.matches.map(match => {
                                if (match.isApplied === true) {

                                    let deadline = this.formatDate(match.closeDate);
                                    
                                    return (
                                        <tr key={match.matchId}>
                                            <td >{match.employer}</td>
                                            <td >{match.title}</td>
                                            <td >{match.location} </td>
                                            <td >{deadline}</td>
                                            <td>Applied</td>
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

export default connect(mapStateToProps)(AppliedMatches)
