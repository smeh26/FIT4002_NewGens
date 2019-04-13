import React from 'react';
import Data from '../data/data';

class DomainActions extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            indexOpen: null,
        };
        this.openIndex = this.openIndex.bind(this);
    }

    openIndex(indexOpen) {
        this.setState({
            indexOpen
        });
    }

    render() {
        var domainActions = Data.actions;
        var {indexOpen} = this.state;

        var actions = domainActions.map((action, index) => {
            var openClose = index === indexOpen
                ? null
                : index;
            var actionList = action.action_list.map((actionItem, idx) => {
                return (
                    <li key={idx}>{actionItem}</li>
                );
            });
            var content = index === indexOpen
                ? <ul>{actionList}</ul>
                : null;

            var actionClass = (index === indexOpen)
                ? 'action action-open'
                : 'action';

            return (
                <div
                    key={index}
                    className={actionClass}
                    onClick={() => this.openIndex(openClose)}
                    >
                    <h3>{index+1}. {action.group}</h3>
                    <p>{action.group_desc}</p>
                    <ul>
                        {content}
                    </ul>
                </div>
            );
        });

        return (
            <div className="actions">
                {actions}
            </div>
        );
    }
}

export default DomainActions