import React, { Component } from 'react';
import { createStore, combineReducers, applyMiddleware, compose } from 'redux'
import thunkMiddleware from 'redux-thunk'
import { Provider } from 'react-redux'
import createHistory from 'history/createBrowserHistory'
import { Route } from 'react-router'
import { ConnectedRouter, routerReducer, routerMiddleware } from 'react-router-redux'
import AppInner from './Components/AppInner';
import app from './Reducers'
import './styles/app.css';

const history = createHistory();
const reduxRouterMiddleware = routerMiddleware(history);
window.__redux_router_ga_last_location = '';

function logPathChange({ getState }) {
    return (next) =>
      (action) => {

        if (action.type == '@@router/LOCATION_CHANGE'){
           console.info(`Route Changed: ${action.payload.pathname}`);
          if (window.ga && window.__redux_router_ga_last_location != action.payload.pathname){
            window.ga('set','page', action.payload.pathname); // Correctly sets the GA tracker to the new active page/path/relative address
            window.ga('send','pageview'); // Updates GA as well as the tracker.
            // reference >  https://developers.google.com/analytics/devguides/collection/analyticsjs/single-page-applications
          }
          window.__redux_router_ga_last_location = action.payload.pathname;
        }

        return next(action);

      };
  }

// redux devtools
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(
  combineReducers({
    router: routerReducer,
    app
  }),
  composeEnhancers(
    applyMiddleware(reduxRouterMiddleware, thunkMiddleware, logPathChange)
  )
);


class App extends Component {  
  render() {
    return (
      <Provider store={store}>
        <AppInner passHistory={history} />
      </Provider>
    );
  }
}


export default App;
