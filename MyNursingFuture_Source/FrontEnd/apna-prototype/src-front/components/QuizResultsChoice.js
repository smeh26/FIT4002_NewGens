import React from 'react';
import QuizDebug from './QuizDebug';
import PieGraphContainer from './Graph/PieGraphContainer';
import ResultsExplanationModal from './ResultsExplanationModal';
import DomainActions from './DomainActions';
import Data from '../data/data';

class QuizResultsChoice extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            showDebug: false,
        };
    }

    render() {
        var {quiz, responses} = this.props;

        var segmentData = Data.segmentData;
        var allResponses = [];

        //Object.values(responses).forEach((responseArr) => {
        for(var key in responses) {
            var responseArr = responses[key];
            responseArr.forEach((response) => {
                allResponses.push(response);
            });
        }

        var mergeResponsesWithQuestions = (question) => {
            var response = allResponses.find((response) => {
                return response.attribute === question.attributes[0].attribute;
            });
            var value = (response && 'value' in response) ? response.value : null;
            return Object.assign(question, {value});
        };

        var reduceTotalsForResponses = (acc, question) => {
            if(!acc[question.group]) {
                acc[question.group] = {
                    total: 0,
                    len: 0,
                    avg: 0,
                };
            }
            ++acc[question.group].len;
            acc[question.group].total += question.value;
            acc[question.group].avg = acc[question.group].total / acc[question.group].len;
            return acc;
        };

        var mergeSegmentDataWithDomainResponses = (segment) => {
            var domainResponse = domainsWithResponses[segment.group];
            segment.score = domainResponse.avg;
            segment.label = `${domainResponse.total} / ${domainResponse.len}`;

            segment.levels = segment.levels.map((level, index) => {
                var segmentScore = segment.score + 1,
                    levelScore = segmentScore - index;
                if(levelScore >= 1 ) {
                    levelScore = 1;
                }
                else if(levelScore < 0) {
                    levelScore = 0;
                }
                return {
                    value: 1,
                    completed: segment.score >= index ? true : false,
                    level_score:  levelScore,
                    label: `Level ${index+1} = ${(levelScore).toFixed(2)}`,
                };
            });
            return segment;
        };

        var domainsWithResponses = quiz.questions
            .map(mergeResponsesWithQuestions)
            .reduce(reduceTotalsForResponses, {});

        var dataByDomain = segmentData.map(mergeSegmentDataWithDomainResponses);

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

                        <PieGraphContainer
                            dataByDomain={dataByDomain}
                            width={600}
                            />

                        <h1>Actions to skill up the the next level of practice</h1>
                        <DomainActions />

                    </div>
                    {quizDebug}
                </div>
            </div>
        );
    }
}

export default QuizResultsChoice