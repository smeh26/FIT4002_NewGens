import React from 'react';
import * as d3 from 'd3';
import translate from './translate';

const DomainLabels = ({dataByDomain, x, y, innerRadius, outerRadius, pieWidth}) => {

    var renderLabels = (domain, i) => {
        var labelArc = d3.arc()
            .innerRadius(innerRadius)
            .outerRadius(outerRadius);

        var fontSize = pieWidth >= 640 ? 18 : 12,
            yTextOffset = pieWidth > 640 ? 10 : 7;

        var levelScore = (domain.data.score + 1.0).toFixed(2);
        var levelName;

        if(levelScore >= 3) {
            levelName = 'Advanced Practice';
        }
        else if(levelScore >= 2) {
            levelName = 'Intermediate Practice'
        }
        else {
            levelName = 'Foundation Practice'
        }

        return (
            <g key={i}
               transform={translate(...labelArc.centroid(domain))}>
                <text
                    fontSize={fontSize}
                    y={yTextOffset}
                    fill="#555"
                    textAnchor="middle">
                    {domain.data.name.split(' ').map((subStr, idx) => {
                        return (
                            <tspan
                                key={idx}
                                x={0}
                                dy={(idx > 0 ? 17 : 0)}>
                                {subStr.replace('_', ' ')}
                            </tspan>
                        );
                    })}
                </text>
                <text
                    y={-yTextOffset}
                    fontSize={fontSize}
                    fontWeight="bold"
                    fill="#555"
                    textAnchor="middle">
                    {`Level ${levelName}`}
                </text>
            </g>
        );
    };

    var pie = d3.pie()
        .value((d) => d.value);

    return (
        <g transform={translate(x, y)}>
            {pie(dataByDomain).map(renderLabels)}
        </g>
    );
};

export default DomainLabels