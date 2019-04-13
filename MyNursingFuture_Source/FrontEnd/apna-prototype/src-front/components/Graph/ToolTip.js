import React from 'react';

const ToolTip = ({show, text, x, y}) => {

    var display = show === true
        ? 'inline-block'
        : 'none';

    return (
        <div className="tool-tip"
             style={{display:display, left:x, top:y}}>
            {text}
        </div>
    );
};

export default ToolTip