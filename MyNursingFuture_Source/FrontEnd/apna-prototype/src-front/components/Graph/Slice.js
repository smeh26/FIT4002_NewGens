import React from 'react';
import ReactDOM from 'react-dom';
import * as d3 from 'd3';

class Slice extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            isHovered: false,
        };
        this.svgElement = null;
        this.onMouseOver = this.onMouseOver.bind(this);
        this.onMouseOut = this.onMouseOut.bind(this);
    }

    componentDidMount() {
        var node = ReactDOM.findDOMNode(this.svgElement);
        d3.select(node)
            .on('mousemove', () => {
                this.updateMouse();
            });
    }

    updateMouse() {
        var show = true,
            {value} = this.props,
            text = value.data.label,
            [x, y] = d3.mouse(this.svgElement);

        this.props.toolTip({show, text, x, y});
    }

    onMouseOver() {
        this.setState({
            isHovered: true,
        });
    }

    onMouseOut() {
        var show = false;
        this.props.toolTip({show});
        this.setState({
            isHovered: false,
        });
    }

    render() {
        var {isHovered} = this.state,
            {value, innerRadius = 0, outerRadius, cornerRadius, padAngle, padRadius} = this.props,
            levelScore = value.data.level_score,
            opacity,
            fill;

        var colorScale = {
            dark: 'DarkGreen',
            light: 'LightGreen',
            hover: 'Pink',
        };

        if (isHovered) {
            fill = colorScale.hover;
            opacity = 1;
        }
        else if(levelScore == 1) {
            fill = colorScale.dark;
            opacity = 1;
        }
        else if(levelScore > 0) {
            fill = colorScale.dark;
            opacity = levelScore;
        }
        else
        {
            fill = colorScale.light;
            opacity = 0.35;
        }

        var arc = d3.arc()
            .innerRadius(innerRadius)
            .outerRadius(outerRadius)
            .cornerRadius(cornerRadius)
            .padAngle(padAngle)
            .padRadius(padRadius);
        return (
            <g onMouseOver={this.onMouseOver}
               onMouseOut={this.onMouseOut}
               ref={(input) => this.svgElement = input}
               >
                <path
                    d={arc(value)}
                    fill={fill}
                    fillOpacity={opacity}
                    />
            </g>
        );
    }

}

export default Slice