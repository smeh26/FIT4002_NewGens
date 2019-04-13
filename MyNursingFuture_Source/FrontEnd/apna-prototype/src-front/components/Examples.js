import React from 'react';

class Examples extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            expanded: false,
        };
        this.expandContract = this.expandContract.bind(this);
    }

    expandContract(event) {
        var expanded = this.state.expanded ? false : true;
        this.setState({
            expanded
        });
    }

    render() {
        var {expanded} = this.state;
        var {examples} = this.props;
        var exampleList = examples.map((example, idx) => {
            return (
                <p>
                    <strong>Example {idx+1}</strong><br />
                    {example}
                </p>
            );
        });
        exampleList = expanded
            ? <div className="example-list">{exampleList}</div>
            : null;

        var examplesClass = expanded
            ? 'examples examples-open'
            : 'examples';

        return (
            <div className={examplesClass} onClick={this.expandContract}>
                <div className="desc">
                    Show me some examples in <br />
                    different settings
                </div>
                {exampleList}
            </div>
        );
    }

}

export default Examples