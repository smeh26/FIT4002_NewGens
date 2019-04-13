import React from 'react';
import Pie from './Pie';
import DomainLabels from './DomainLabels';

const PieGraph = ({dataByDomain, width, toolTip}) => {

    // re-organise 5 domains into 3 pie graphs
    var reduceByPieGraph = (acc, domain, domainIndex) => {
        domain.levels.forEach((level, levelIndex) => {
            if(!Array.isArray(acc[levelIndex])) {
                acc[levelIndex] = [];
            }
            acc[levelIndex][domainIndex] = level;
        });
        return acc;
    };

    var dataByPieGraph = dataByDomain.reduce(reduceByPieGraph, []);

    // console.log('dataByPieGraph');
    // console.log(dataByPieGraph);

    var x = (width / 2),
        y = (width / 2);

    var gap       = width >= 640 ? 10 : 5,
        offset    = width >= 640 ? 0.6 : 0.85,
        padRadius = width >= 640 ? 30 : 15,
        padAngle  = width >= 640 ? 0.5 : 0.5;

    var segmentsSection = width / dataByPieGraph.length,
        segmentRadius = (segmentsSection / dataByPieGraph.length) - gap,
        labelsInnerRadius = segmentRadius * (dataByPieGraph.length + offset),
        labelsOuterRadius = segmentRadius * (dataByPieGraph.length + offset * 2);

    var pieGraphs = dataByPieGraph.map((pieGraphData, idx) => {
        var outerRadius = segmentRadius * (idx + 1) + gap,
            innerRadius = (segmentRadius * idx) + gap + gap;
        return (
            <Pie key={idx}
                 data={pieGraphData}
                 x={x}
                 y={y}
                 innerRadius={innerRadius}
                 outerRadius={outerRadius}
                 padAngle={padAngle}
                 padRadius={padRadius}
                 toolTip={toolTip} />
        );
    });

    return (
        <div style={{width:'100%'}}>
            <svg width={width+'px'} height={width+'px'}>
                {pieGraphs}
                <DomainLabels
                    x={x}
                    y={y}
                    dataByDomain={dataByDomain}
                    innerRadius={labelsInnerRadius}
                    outerRadius={labelsOuterRadius}
                    pieWidth={width}
                />
            </svg>
        </div>
    );

}

export default PieGraph