import React from 'react';
import * as d3 from 'd3';
import Slice from './Slice';
import translate from './translate';

const Pie = ({data, x, y, innerRadius, outerRadius, cornerRadius, padAngle, padRadius, toolTip}) => {

    var pie = d3.pie()
        .value((d) => d.value);

    var renderSlice = (value, i) => {
        return (
            <Slice
                key={i}
                innerRadius={innerRadius}
                outerRadius={outerRadius}
                cornerRadius={cornerRadius}
                padAngle={padAngle}
                padRadius={padRadius}
                value={value}
                toolTip={toolTip} />
        );
    };

    return (
        <g transform={translate(x, y)}>
            {pie(data).map(renderSlice)}
        </g>
    );
};

export default Pie