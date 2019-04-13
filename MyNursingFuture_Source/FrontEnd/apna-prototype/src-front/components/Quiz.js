import React from 'react';
import Examples from './Examples';
import Answer from './Answer';
import QuestionNav from './QuestionNav';
import QuizComplete from './QuizComplete';
import QuizResultsRange from './QuizResultsRange';
import QuizResultsChoice from './QuizResultsChoice';
import QuizProgress from './QuizProgress';
import QuizDebug from './QuizDebug';
import Data from '../data/data';

class Quiz extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            quiz: {},
            questionIdx: 0,
            responses: {},
            isComplete: false,
            percentComplete: 0,
            showResults: false,
            showDebug: false,
        };
        this.setQuestionIdx = this.setQuestionIdx.bind(this);
        this.saveResponse = this.saveResponse.bind(this);
        this.showResults = this.showResults.bind(this);
    }

    componentWillMount() {
        var quizList = Data.quizList;
        var quiz = quizList.find((quiz) => {
            return quiz.id === parseInt(this.props.params.quiz_id);
        });
        this.setState({
            quiz
        });
    }

    setQuestionIdx(questionIdx) {
        var percentComplete, isComplete;
        var {quiz} = this.state;

        if((questionIdx) === this.state.quiz.questions.length && this.state.isComplete === false) {
            // DISABLE MUST_ANSWER_ALL
            //(questionIdx) = 0;
        }
        else if((questionIdx) < 0) {
            (questionIdx) = this.state.quiz.questions.length - 1;
        }
        percentComplete = parseInt((questionIdx / quiz.questions.length) * 100);
        isComplete = percentComplete === 100 ? true : false;

        this.setState({
            questionIdx,
            isComplete,
            percentComplete,
        });
    }

    saveResponse(response) {
        var {questionIdx, responses} = this.state;

        responses = Object.assign(responses, {[questionIdx]: response});

        this.setState({
            responses,
        });
    }

    showResults() {
        this.setState({
            showResults: true,
        });
    }

    render() {
        var displayValue = null;
        var {quiz, questionIdx, responses,
             isComplete, percentComplete, showResults} = this.state;
        var currentResponse = responses[questionIdx];

        var question = quiz.questions.find((question, idx) => {
            return idx === questionIdx;
        });

        var questionType = (typeof(question) !== 'undefined' && 'type' in question) ? question.type : quiz.type;

        if(Array.isArray(currentResponse) && currentResponse[0]) {
            var {positive_score, value} = currentResponse[0];
            displayValue = positive_score
                ? value
                : parseFloat(1 - value).toFixed(1);
        }

        if(showResults) {
            return quiz.id === '1'
                ? <QuizResultsRange quiz={quiz} responses={responses} />
                : <QuizResultsChoice quiz={quiz} responses={responses} />;
        }

        if(questionIdx === quiz.questions.length) {
        // DISABLE MUST_ANSWER_ALL
        //if(isComplete && typeof question === 'undefined') {
            return (
                <QuizComplete quiz={quiz} questionIdx={questionIdx} showResults={this.showResults} />
            );
        }

        var examples = (typeof(question) !== 'undefined' && 'examples' in question)
            ? <Examples examples={question.examples} />
            : null;

        var quizDebug = this.state.showDebug
            ? <QuizDebug responses={responses} complete={isComplete} />
            : null;

        var saveResponse = (value) => {
            var attributes = question.attributes.map((attr) => {
                var attribute = attr.attribute;
                var positive_score = attr.positive_score;
                value = attr.positive_score
                    ? parseFloat(value)
                    : parseFloat(1 - value);
                return {
                    attribute,
                    positive_score,
                    value,
                };
            });
            return this.saveResponse(attributes);
        };

        return (
            <div className="quiz">
                <div className="quiz-title">
                    <h2>
                        {quiz.name}
                        {question.group_name ? ' > '+ question.group_name : ''}
                    </h2>
                </div>
                <div className="quiz-body">
                    <QuizProgress percent={percentComplete} length={quiz.questions.length} current={questionIdx} />
                    <div className="quest">
                        <h3>{question.text}</h3>
                    </div>
                    {examples}
                    <Answer
                        type={questionType}
                        choices={question.choices}
                        saveResponse={saveResponse}
                        questionIdx={questionIdx}
                        response={displayValue}
                        quizId={quiz.id}
                        />
                    <QuestionNav
                        back={() => this.setQuestionIdx(questionIdx-1)}
                        next={() => this.setQuestionIdx(questionIdx+1)}
                        />
                    {quizDebug}
                </div>
            </div>
        );
    }

}

export default Quiz