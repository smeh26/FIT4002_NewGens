import React from 'react';
import {Link} from 'react-router';
import {Button} from 'semantic-ui-react';
import Data from '../data/data';

class quizList extends React.Component {

    constructor(props) {
        super(props);
        this.quizList = Data.quizList;
    }

    render() {
        var quizList = this.quizList.map((quiz, idx) =>
            <div key={idx} className="quiz-item">
                <h2>{quiz.name}</h2>
                <p>{quiz.desc}</p>
                <Link to={'/quiz/intro/'+quiz.id}>
                    <Button className="btn-primary" content='Start Quiz' />
                </Link>
            </div>
        );
        return (
            <div>
                <header>
                    <h1>APNA Prototype</h1>
                </header>
                <div className="quiz-list">
                    {quizList}
                </div>
            </div>
        );
    }

}

export default quizList