import React from 'react';
import ReactDOM from 'react-dom';
import PieGraph from './PieGraph';
import ToolTip from './ToolTip';

class PieGraphContainer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            width: 0,
            toolTip: {
                show: false,
                text: null,
                x: 0,
                y: 0,
            },
        }
        this.setToolTip = this.setToolTip.bind(this);
        this.updateDimensions = this.updateDimensions.bind(this);
    }

    updateDimensions() {
        var width = ReactDOM.findDOMNode(this).offsetWidth;
        this.setState({
            width
        });
    }

    componentDidMount() {
        window.addEventListener("resize", this.updateDimensions);
        this.updateDimensions();
    }

    componentWillUnmount() {
        window.removeEventListener("resize", this.updateDimensions);
    }

    setToolTip({show, text, x, y}) {
        var {width} = this.state,
            offset = 10;
        x = (x + width/2 + offset) +'px';
        y = (y + width/2+ offset) +'px';
        this.setState({
            toolTip: {
                show: show,
                text: text,
                x: x,
                y: y,
            },
        });
    }

    render() {
        var {dataByDomain} = this.props,
            {width, toolTip} = this.state;
        return (
            <div className="star-graph">
                <PieGraph
                    dataByDomain={dataByDomain}
                    width={width}
                    toolTip={(tip) => this.setToolTip(tip)}
                    />
                <ToolTip {...toolTip}  />
            </div>
        );
    }

}

export default PieGraphContainer