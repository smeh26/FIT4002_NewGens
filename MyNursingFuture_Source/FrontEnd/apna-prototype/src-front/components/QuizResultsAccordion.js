import React from 'react';
import ReactDOM from 'react-dom';
import { List, Progress, Button } from 'semantic-ui-react';

class QuizResultsAccordion extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            indexOpen: null,
        };
        this.openIndex.bind(this);
    }

    componentDidMount () {
        var width = ReactDOM.findDOMNode(this).offsetWidth;
        this.setState({
           width
        });
    }

    openIndex(indexOpen) {
        this.setState({
            indexOpen
        });
    }

    render() {
        var {results} = this.props;
        var {indexOpen, width} = this.state;

        var mapAttributeToList = (attribute, idx) => {
            var iconColor = attribute.value === null ? '#AAA' : '#52CBCB';
            var contentColor = attribute.value === null ? '#AAA' : '#666';

            // var listContent = attribute.value === null
            //     ? attribute.text
            //     : `${attribute.text} - ${attribute.value*100}%`;
            var listContent = attribute.text;

            return (
                <List.Item key={idx}>
                    <List.Icon name="checkmark" style={{color:iconColor}} />
                    <List.Content style={{color:contentColor}}>{listContent}</List.Content>
                </List.Item>
            );
        };

        var accordion = results.map((result, index) => {
            var contentList = index === indexOpen
                ? <List>{result.attributes.map(mapAttributeToList)}</List>
                : null;
            var openClose = index === indexOpen
                ? null
                : index;

            var accordionItemClass = (index === indexOpen)
                ? 'accordion-item accordion-item-open'
                : 'accordion-item';

            return (
                <div
                    key={index}
                    onClick={() => this.openIndex(openClose)}
                    className={accordionItemClass}
                    >
                    <div className="accordion-title">
                        {index +1}. {result.group} - {result.groupScore.toFixed(0)}%
                    </div>
                    <Progress
                        percent={result.groupScore.toFixed(0)}
                        color="teal"
                        size="tiny"
                        />
                    {contentList !== null
                        ? <div className="accordion-content">
                            {contentList}
                            <a href="https://projects.invisionapp.com/d/main#/console/10340491/220189687/preview" className="ui button tiny btn-secondary">Learn More</a>
                          </div>
                        : null
                    }
                </div>
            );
        });

        return (
            <div className="accordion">
                {accordion}
            </div>
        );
    }
}

export default QuizResultsAccordion