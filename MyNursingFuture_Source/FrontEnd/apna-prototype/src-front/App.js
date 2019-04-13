import React from 'react';
import { Router, Route, hashHistory } from 'react-router';
import QuizList from './components/QuizList';
import Quiz from './components/Quiz';
import QuizIntro from './components/QuizIntro';

class App extends React.Component {

    render() {
        return (
            <div className="ui container">
                <Router history={hashHistory}>
                    <Route path="/" component={QuizList} />
                    <Route path="/quiz/:quiz_id" component={Quiz}/>
                    <Route path="/quiz/intro/:quiz_id" component={QuizIntro}/>
                </Router>
            </div>
        );
    }

}

export default App;