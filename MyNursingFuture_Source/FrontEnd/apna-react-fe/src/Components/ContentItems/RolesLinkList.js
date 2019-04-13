import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';

class RolesLinkList extends Component {
  render() {
    var filteredRoles = this.props.roles;
    var rolesFilter = this.props.filter;
    if (rolesFilter){
      filteredRoles = filteredRoles.filter((r) => {
        return !rolesFilter.includes(""+r.roleId);
      })
    }
    return (
      <div className="roles-link-list-wrapper">
        {filteredRoles && !this.props.loading && filteredRoles.map((ro,index) => 
          <div className="row list-link-row" key={index}>
            <div className="col-12">
              <Link to={'/roles/' + ro.roleId} onClick={() => {window.scroll(0,0)}}>
                <div className="list-link">{ro.linkName}<span className="oi" data-glyph="chevron-right"></span></div>
              </Link>
            </div>
          </div>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.rolesDataLoading,
    roles: state.app.framework.roles
  }
};

export default connect(mapStateToProps)(RolesLinkList);