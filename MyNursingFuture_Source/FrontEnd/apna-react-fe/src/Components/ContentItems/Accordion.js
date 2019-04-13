import React, {Component} from 'react';
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
    console.log(this.state.height)
    console.log(this.state.open)
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
      <AnimateHeight duration={500} height={this.state.height} contentClassName={"accordion-content"}>
        {this.props.child}
      </AnimateHeight>
    </div>
        
  }
}

export default Accordion;

