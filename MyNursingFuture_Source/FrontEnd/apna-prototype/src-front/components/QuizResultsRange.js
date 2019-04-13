import React from 'react';
import { Button } from 'semantic-ui-react';
import QuizResultsAccordion from './QuizResultsAccordion';
import QuizDebug from './QuizDebug';


class QuizResultsRange extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            numResultsShown: 5,
            showDebug: false,
        };
        this.showNumResults.bind(this);
    }

    showNumResults(numResultsShown) {
        this.setState({
            numResultsShown
        });
    }

    render() {
        var {quiz, responses} = this.props;
        var {numResultsShown} = this.state;

        var allResponses = [];

        //Object.values(responses).forEach((responseArr) => {
        for(var key in responses) {
            var responseArr = responses[key];
            responseArr.forEach((response) => {
                allResponses.push(response);
            });
        }

        var mergeAttributeAndResponse = (attribute) => {
            var response = allResponses.find((response) => {
                return response.attribute === attribute.attribute;
            });
            var value = (response && 'value' in response) ? response.value : null;
            return Object.assign(attribute, {value});
        };

        var mergeResultAndAttributes = (result) => {
            var attributes = result.attributes.map(mergeAttributeAndResponse);
            return Object.assign(result, attributes);
        };

        var calculateGroupAvg = (acc, cur) => {
            return ('value' in cur)
                ? acc + cur.value
                : acc;
        };

        var mergeGroupScore = (result) => {
            var groupAvg = result.attributes.reduce(calculateGroupAvg, 0);
            var groupScore = ((groupAvg / result.attributes.length).toFixed(2) * 100);
            return Object.assign(result, {groupScore});
        };

        var sortByGroupScore = (a, b) => {
            if (a.groupScore < b.groupScore) {
                return 1;
            }
            if (a.groupScore > b.groupScore) {
                return -1;
            }
            return 0;
        };

        var filterResultsShown = (result, idx) => {
            return idx < numResultsShown;
        };

        var results = quiz.results
            .map(mergeResultAndAttributes)
            .map(mergeGroupScore)
            .sort(sortByGroupScore)
            .filter(filterResultsShown);

        var showResultsButton = null;
        if(quiz.results.length > 5)
        {
            showResultsButton = (
                <div className="show-results">
                    <Button
                        className="btn-primary"
                        size="small"
                        onClick={() => this.showNumResults(numResultsShown === 5 ? quiz.results.length : 5)}
                        content={numResultsShown === 5 ? 'Show All' : 'Show 5'}
                    />
                </div>
            );
        }

        var quizDebug = this.state.showDebug
            ? <QuizDebug responses={responses} complete={false} />
            : null;

        return (
            <div className="quiz">
                <div className="quiz-title">
                    <h2>{quiz.name}</h2>
                </div>
                <div className="quiz-body">
                    <div className="results">
                        <QuizResultsAccordion results={results} />
                        {showResultsButton}
                    </div>
                    {quizDebug}
                </div>
            </div>
        );
    }
}

export default QuizResultsRange