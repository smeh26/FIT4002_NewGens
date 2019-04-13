import React, {Component} from 'react';
import TextWithGlossary from './TextWithGlossary';

class AspectsLevelsAccordion extends Component {
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
    var classNameValue = 'accordion-wrapper' + (this.state.open ? ' accordion-open' : '');
    return <div className={classNameValue}>
      <div className="row accordion">
        <div className="col-12" onClick={this.handleClick}>
          <div className="link-line">
            <span>{this.props.title}</span>
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
          <div className="row">
            <div className="col-sm-12">
              <div className="content-block">
                <TextWithGlossary text={this.props.aspect.levels}/>
              </div>
            </div>
          </div>
      </div>
      }
    </div>
        
  }
}

export default AspectsLevelsAccordion;

