import React, { Component } from 'react';
import { connect } from 'react-redux';
import { fetchJobListings } from '../../Actions/index'
import '../../styles/bootstrap-table.css';

class JobListings extends React.Component {
    //get all Joblistings on Mount from actions
    componentDidMount() {
        this.props.getJobListings();
    }
    //format the date to display properly 
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
    //display the table with the information regarding the joblistings
    render() {
        if (this.props.isLoading) {
            return <div className="loading-wrapper"><img src="/img/loading.gif" /></div>
        }

        return (
            <table className="table table-hover table-condensed" style={{ width: '100%' }}>
                <thead>
                    <tr>
                        <th scope="col">Title</th>
                        <th scope="col">Nurse Type</th>
                        <th scope="col">Special Requirements</th>
                        <th scope="col">Salary Range</th>
                        <th scope="col">Application Deadline</th>
                        <th scope="col">State</th>

                    </tr>
                </thead>
                <tbody>
                    {
                        this.props.joblistings.map(job => {
                            let nurseType;
                            switch (job.nurseType) {
                                case 'RN':
                                    nurseType = "Registered Nurse";
                                    break;
                            }

                            let deadline = this.formatDate(job.applicationDeadline);
                            return <tr>
                                <td >{job.title}</td>
                                <td >{nurseType}</td>
                                <td >{job.specialRequirements ? job.specialRequirements : 'None'}</td>
                                <td >{job.minSalary} - {job.maxSalary} </td>
                                <td >{deadline}</td>
                                <td >{job.state}</td>
                            </tr>
                        })
                    }
                </tbody>
            </table>
        )

    }
}
//use props from mainstate in this component
const mapStateToProps = (state) => ({
    joblistings: state.app.joblistings.listing,
    isLoading: state.app.joblistings.isLoading,
});
//get joblistings action
const mapDispatchToProps = (dispatch) => ({
    getJobListings: () => {
        dispatch(fetchJobListings());
    }

});

export default connect(mapStateToProps, mapDispatchToProps)(JobListings);