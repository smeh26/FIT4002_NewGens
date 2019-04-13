import React, {Component} from 'react';

class CareerPathwayMatchAccordion extends Component {
  constructor(props){
    super(props);
    this.state = {
      open: props.open || false
    };
    
    this.handleClick = this.handleClick.bind(this);
  }
  
  handleClick(event){
    this.setState({open: !this.state.open});
  }

  render () {
    var classNameValue = 'careerpathwaysaccordion accordion-wrapper' + (this.state.open ? ' accordion-open' : '');
    return <div className={classNameValue}>
      <div className="row accordion">
        <div className="col-12" onClick={this.handleClick}>
          <div className="link-line">
            <span>{this.props.title}</span>
            <span className="percentage-label">  ({this.props.percentMatch + '%'})</span>
            <div className="percentage-indicator">
              <div className="percentage-line">
                <div className="percentage-fill" style={{width: this.props.percentMatch + '%'}}></div>
              </div>
              
            </div>

            {this.state.open && 
            <span className="oi" data-glyph="chevron-top"></span>
            }
            {!this.state.open &&
            <span className="oi" data-glyph="chevron-bottom"></span>
            }
          </div>
        </div>
      </div>
      {this.state.open &&
      <div className="accordion-content">
        {this.props.children}
      </div>
      }
    </div>
        
  }
}

export default CareerPathwayMatchAccordion;

