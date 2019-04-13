import React from 'react';
import { Input } from 'semantic-ui-react'

class Answer extends React.Component {

    constructor(props) {
        super(props);

        var {response, type} = this.props;
        var answered = response !== null ? true : false;
        var value = response === null && type === 'range'
            ? 0
            : response;

        this.state = {
            answered,
            value,
        };
        this.handleChange = this.handleChange.bind(this);
    }

    componentWillMount() {
        var {response, type} = this.props;
        var answered = response !== null ? true : false;
        var value = response === null && type === 'range'
            ? 0
            : response;

        this.setState({
            answered,
            value,
        });
    }

    componentWillReceiveProps(nextProps) {
        var {response, type} = nextProps;
        var answered = response !== null ? true : false;
        var value = response === null && type === 'range'
            ? 0
            : response;

        this.setState({
            answered,
            value,
        });
    }

    handleChange(event) {
        var value = event.target.value;
        var answered = value !== null ? true : false;

        this.props.saveResponse(value);

        this.setState({
            answered,
            value,
        });
    }

    render() {
        var {choices, type, quizId} = this.props;
        var {answered, value} = this.state;
        var labelClassName;
        if (choices.length === 4) {
          labelClassName = 'label label-25';
        } else if (choices.length === 3) {
          labelClassName = 'label label-33';
        } else {
          labelClassName = 'label label-33';
        }
        var answeredClass = answered === true ? 'answered' : 'unanswered';
        var lowestV = 0;
        var highestV = 0;

        if(type === 'range') {
            
            var choiceList = choices.map((choice, idx) => {
              if (choice.score < lowestV){lowestV = choice.score;}
              if (choice.score > highestV){highestV = choice.score;}
                return (
                    <div className={labelClassName} key={idx}>
                        {choice.text}
                    </div>
                );
            });

            return (
                <div className="answer">
                    <Input
                        className={answeredClass}
                        style={{width:'100%', cursor:'pointer'}}
                        type="range"
                        min={lowestV}
                        max={highestV}
                        step="0.1"
                        onClick={this.handleChange}
                        onChange={this.handleChange}
                        value={value}
                    />
                    <div className="labels">
                        {quizId == 2 && 
                          <div className="label-zero">I do not</div>
                        }
                        {choiceList}
                    </div>
                </div>
            );
        }
        else if(type === 'choice') {
            var choicesList = choices.map((choice, idx) => {
                var selected = (value === choice.score) ? true : false;
                var answerClass = selected
                    ? 'choice choice-selected'
                    : 'choice';
                var icon = selected
                    ? <div style={{float:'left', fontSize:'24px', margin:'3px'}} >
                        <i className="check square icon"></i>
                      </div>
                    : null;
                return <div
                        className={answerClass}
                        key={idx}
                        onClick={() => {
                            var event = {
                                target: {
                                    value: choice.score,
                                }
                            };
                            this.handleChange(event);
                        }}>
                    {icon}
                    {choice.text}
                </div>;
            });

            return (
                <div className="answer">
                    <div className="choice-desc">
                        Pick a statement that best describes the level you work at:
                    </div>
                    {choicesList}
                </div>
            );
        }
        else {
            throw new Error('Unknown quiz type');
        }

    }
}

export default Answer