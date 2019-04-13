import React, {Component} from 'react';
import MarkupBlock from './MarkupBlock';
import AnimateHeight from 'react-animate-height';

class Accordion extends Component {
  constructor(props){
    super(props);
    this.state = {
      open: props.open || false,
      height: 0
    };
    
    this.handleClick = this.handleClick.bind(this);
  }

  componentDidMount(){
    if(this.state.open != false){
      this.setState({height: 'auto'})
    }
  }
  
  handleClick(event){
    this.setState({
      open: !this.state.open,
      height: !this.state.height ? 'auto' : 0
    });
  }

  render () {
    var action = this.props.action;
    var classNameValue = 'action-accordion accordion-wrapper' + (this.state.open ? ' accordion-open' : '');
    return <div className={classNameValue}>
      <div className="row accordion">
        <div className="col-12" onClick={this.handleClick}>
          <div className="link-line">
            {action && action.domainLabel &&
              <div className="action-domain-and-title">
                <span className="text-smaller">{this.props.index}. {action.domainLabel}</span>
                <br />
                {action.title}
              </div>
            }
            {action && !action.domainLabel && 
              <div><div className="bold action-index">{this.props.index}. </div><span>{action.title}</span></div>
            }
            
            {this.state.open && 
            <span className="oi" data-glyph="chevron-top"></span>
            }
            {!this.state.open &&
            <span className="oi" data-glyph="chevron-bottom"></span>
            }
          </div>
        </div>
      </div>
      <AnimateHeight duration={500} height={this.state.height} contentClassName={"accordion-content"}>
        <MarkupBlock content={{text: action.text}}/>
      </AnimateHeight>      
    </div>
        
  }
}

export default Accordion;

