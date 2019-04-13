import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';

class SectorLinkList extends Component {
  constructor(props){
    super(props);
    let _items = 5;
    let _showTop = false;
    if (props.routerLocation === '/sectors'){
      _showTop =  true
      _items = Infinity;
    }
    this.state = {
      showItems: _items,
      sectorOrder: {
        "6": 6,
        "4": 5,
        "5": 4,
        "2": 3,
        "1": 2,
        "3": 1
      }
    }
    this.showMoreItems = this.showMoreItems.bind(this);
  }
  
  showMoreItems(){
    this.setState({showItems: this.state.showItems + 5});
  }
  
  render() {
    let sectorsOrdered;
    if (this.props.sectors && this.props.sectors.length > 0){
      var self = this;
      sectorsOrdered = this.props.sectors.slice(0);
      sectorsOrdered.sort((a,b) => {
        let aVal, bVal;
        if (self.state.sectorOrder[a.sectorId]){ aVal = self.state.sectorOrder[a.sectorId];} else { aVal = -1;}
        if (self.state.sectorOrder[b.sectorId]){ bVal = self.state.sectorOrder[b.sectorId];} else { bVal = -1;}
        
        if (aVal > bVal){
          return -1;
        } else if (aVal < bVal){
          return 1;
        } else{
          return 0;
        }
      })
      
      return (
        <div className="sectorMainDiv">
        <div className="sector-link-list-wrapper">
          <div className="colCount">
          {sectorsOrdered && !this.props.loading && sectorsOrdered.map((se,index) => {
            if (index <= this.state.showItems){
            return (
              <div className="row list-link-row " key={index}>
                <div className="col-12">
                  <Link to={"/sectors/" + se.sectorId} onClick={() => {window.scroll(0,0)}}>
                    <div className="list-link">{se.title}<span className="oi" data-glyph="chevron-right"></span></div>
                  </Link>
                </div>
              </div>
            );
            }
          })}
          </div>
          {this.state.showItems != Infinity && 
          <div className="row">
            <div className="col-12">
              <div className="content-block">
                <Link to="/sectors">
                  <button className="btn">View all sectors</button>
                </Link>
              </div>
            </div>
          </div>
          }
        </div>
        </div>
      );
    }
  }
}

const mapStateToProps = (state) => {
  return {
    loading: state.app.framework.sectorDataLoading,
    sectors: state.app.framework.sectors,
    routerLocation: state.router.location.pathname
  }
};

export default connect(mapStateToProps)(SectorLinkList);