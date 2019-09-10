import fetch from 'isomorphic-fetch';
import {defaultPageContentByEndpoint, dummyFetch } from './RoboRamiro';
import cookies from '../Misc/cookies';
import config from '../config';
import { push } from 'react-router-redux'

var fetchingFrameworkData = false;

function shuffle(array) { // thanks https://stackoverflow.com/questions/2450954/how-to-randomize-shuffle-a-javascript-array
  var currentIndex = array.length, temporaryValue, randomIndex;
  while (0 !== currentIndex) {
    randomIndex = Math.floor(Math.random() * currentIndex);
    currentIndex -= 1;
    temporaryValue = array[currentIndex];
    array[currentIndex] = array[randomIndex];
    array[randomIndex] = temporaryValue;
  }
  return array;
}

export const sidebarToggle = () => {
  return {
    type: 'SIDEBAR_TOGGLE'
  }
};
export const sidebarClose = () => {
  return {
    type: 'SIDEBAR_CLOSE'
  }
}

export const locationLabelUpdate = (label) => {
  return {
    type: 'LOCATION_LABEL_UPDATE',
    label
  }
};

export const closeModal = () => {
  return {
    type: 'MODAL_CLOSE'
  }
}
export const openModal = (id, scrollY) => {
  return {
    type: 'MODAL_OPEN',
    id: id,
    scrollY: scrollY
  }
}

export const requestPageContent = (endpoint) => {
  return {
    type: 'START_PAGE_CONTENT_REQUEST',
    endpoint
  }
};
export const completeRequestPageContent = () => {
  return {
    type: 'END_PAGE_CONTENT_REQUEST'
  }
}
export const setPageData = (data) => {
  return {
    type: 'SET_PAGE_DATA',
    data: data
  }
}
export const setPageError = (error) => {
  return {
    type: 'SET_PAGE_ERROR',
    error
  }
}

export const setSectorData = (data) => {
  return {
    type: 'POPULATE_SECTOR_DATA',
    data: data
  }
}
export const requestSectorData = () => {
  return {
    type: 'START_SECTOR_REQUEST'
  }
}
export const completeRequestSectorData = () => {
  return {
    type: 'END_SECTOR_REQUEST'
  }
}
export const setRolesData = (data) => {
  return {
    type: 'POPULATE_ROLES_DATA',
    data: data
  }
}
export const requestRolesData = () => {
  return {
    type: 'START_ROLES_REQUEST',
  }
}
export const completeRequestRolesData = () => {
  return {
    type: 'END_ROLES_REQUEST',
  }
}
export const setDomainData = (data) => {
  return {
    type: 'POPULATE_DOMAIN_DATA',
    data: data
  }
}
export const requestDomainData = () => {
  return {
    type: 'START_DOMAINS_REQUEST'
  }
}
export const completeRequestDomainData = () => {
  return {
    type: 'END_DOMAINS_REQUEST'
  }
}

export const setAspectsData = (data) => {
  return {
    type: 'POPULATE_ASPECTS_DATA',
    data: data
  }
}
export const requestAspectsData = () => {
  return {
    type: 'START_ASPECTS_REQUEST',
  }
}
export const completeRequestAspectsData = () => {
  return {
    type: 'END_ASPECTS_REQUEST',
  }
}

export const requestArticleContent = () => {
  return {
    type: 'START_ARTICLE_CONTENT_REQUEST'
  }
}
export const setArticleData = (articleId, data) => {
  return {
    type: 'SET_ARTICLE_DATA',
    articleId: articleId,
    data: data
  }
}
export const completeRequestArticleContent = () => {
  return {
    type: 'END_ARTICLE_CONTENT_REQUEST'
  }
}
export const requestArticleList = () => {
  return {
    type: 'START_ARTICLE_LIST_REQUEST'
  }
}
export const setArticleListData = (data) => {
  return {
    type: 'SET_ARTICLE_LIST',
    data: data
  }  
}
export const completeRequestArticleList = () => {
  return {
    type: 'END_ARTICLE_LIST_REQUEST'
  }
}

export const requestSelfAssessmentQuizContent = () => {
  return {
    type: 'START_SELF_ASSESSMENT_REQUEST'
  }
}
export const setSelfAssessmentQuizContent = (data) => {
  return {
    type: 'SET_SELF_ASSESSMENT_DATA',
    data: data
  }
}
export const completeRequestSelfAssessmentQuizContent = () => {
  return {
    type: 'END_SELF_ASSESSMENT_REQUEST'
  }
}

export const setMenuData = (data) => {
  return {
    type: 'SET_MENU_DATA',
    data: data
  }
}

export const completeRequestCareerPathwaysQuizContent = () => {
  return {
    type: 'END_CAREER_PATHWAYS_REQUEST'
  }
}
export const requestCareerPathwaysQuizContent = () => {
  return {
    type: 'START_CAREER_PATHWAYS_REQUEST'
  }
}
export const setCareerPathwaysQuizContent = (data) => {
  return {
    type: 'SET_CAREER_PATHWAYS_DATA',
    data: data
  }
}

export const completeRequestCareerPathwaysQuizResults = () => {
  return {
    type: 'END_SCORE_CAREER_PATHWAYS_QUIZ'
  }
}
export const requestCareerPathwaysQuizResults = () => {
  return {
    type: 'START_SCORE_CAREER_PATHWAYS_QUIZ'
  }
}
export const setCareerPathwaysQuizResults = (data) => {
  return {
    type: 'SET_CAREER_PATHWAYS_QUIZ_RESULTS',
    data: data
  }
}
export const completeRequestSelfAssessmentQuizResults = () => {
  return {
    type: 'END_SCORE_SELF_ASSESSMENT_QUIZ'
  }
}
export const requestSelfAssessmentQuizResults = () => {
  return {
    type: 'START_SCORE_SELF_ASSESSMENT_QUIZ'
  }
}
export const setSelfAssessmentQuizResults = (data) => {
  return {
    type: 'SET_SELF_ASSESSMENT_QUIZ_RESULTS',
    data: data
  }
}

export const setSelfAssessmentAnswer = (id, data) => {
  return {
    type: 'SET_SELF_ASSESSMENT_ANSWER',
    id: id,
    data: data
  }
}
export const setSelfAssessmentMultiAnswer = (id, data) => {
  return {
    type: 'SET_SELF_ASSESSMENT_MULTI_ANSWER',
    id: id,
    data: data
  }
}
export const setCareerPathwaysAnswer = (id, data) => {
  return {
    type: 'SET_CAREER_PATHWAYS_ANSWER',
    id: id,
    data: data
  }
}
export const setCareerPathwaysMultiAnswer = (id, data) => {
  return {
    type: 'SET_CAREER_PATHWAYS_MULTI_ANSWER',
    id: id,
    data: data
  }
}

export const setSelfAssessmentQuizCurrentAnswers = (data) => {
  return {
    type: 'SET_SELF_ASSESSMENT_QUIZ_CURRENT_ANSWERS',
    data: data
  }
}

export const setCareerPathwaysQuizCurrentAnswers = (data) => {
  return {
    type: 'SET_CAREER_PATHWAYS_QUIZ_CURRENT_ANSWERS',
    data: data
  }
}

export const setCareerPathwaysQuizCurrentUserQuizId = (data) => {
  return {
    type: 'SET_CAREER_PATHWAYS_QUIZ_USER_QUIZ_ID',
    data: data
  }
}

export const setSelfAssessmentQuizCurrentUserQuizId = (data) => {
  return {
    type: 'SET_SELF_ASSESSMENT_QUIZ_USER_QUIZ_ID',
    data: data
  }
}

export const setUserQuizzes = (data) => {
  return {
    type: 'SET_USER_QUIZZES',
    data: data
  }
}

export const requestLogin = () => {
  return {
    type: 'START_LOGIN'
  } 
}
export const requestUserData = () => {
  return {
    type: 'START_USER_DATA'
  }
}
export const endUserData = () => {
  return {
    type: 'END_USER_DATA'
  }
}
export const endLogin = (message) => {
  return {
    type: 'END_LOGIN',
    message: message
  }
}
export const setUserLoggedIn = () => {
  return {
    type: 'SET_USER_LOGGED_IN'
  }
}
export const setUserLoggedOut = () => {
  return {
    type: 'SET_USER_LOGGED_OUT'
  }
}

export const requestRegister = () => {
  return {
    type: 'START_REGISTER'
  }
}
export const endRegister = (message) => {
  return {
    type: 'END_REGISTER',
    message: message
  }
}

export const setUserData = (user) => {
  return {
    type: 'SET_USER_DATA',
    data: user
  }
}

export const startLogout = () => {
  return {
    type: 'START_LOGOUT'
  }
}
export const endLogout = (message) => {
  cookies.removeItem('token');
  return {
    type: 'END_LOGOUT',
    message: message
  }
}

export const unsetUserData = () => { // TODO this might not work right cuz merge not set
  return {
    type: 'UNSET_USER_DATA',
    data: {}
  }
}

export const setUserError = (error) => {
  return {
    type: 'SET_USER_ERROR',
    data: error
  }
}
export const unsetUserError = () => { // TODO this might not work right cuz merge not set
  return {
    type: 'UNSET_USER_ERROR',
    data: {}
  }
}

export const setGlossaryData = (data) => {
  return {
    type: 'SET_GLOSSARY_DATA',
    data: data
  }
}

export const setAboutYouQuizAnswer = (id, data) => {
  return {
    type: 'SET_ABOUT_YOU_QUIZ_ANSWER',
    id: id,
    data: data
  }
}

export const setReasonsData = (data) => {
  return {
    type: 'SET_REASONS_DATA',
    data: data
  }
}

export const setPostcardsData = (data) => {
  return {
    type: 'SET_POSTCARDS_DATA',
    data: data
  }
}

export const setEndorsedLogosData = (data) => {
    return {
        type: 'SET_ENDORSEDLOGOS_DATA',
        data: data
    }
}

export const setAboutYouQuizData = (data) => {
  return {
    type: 'SET_ABOUT_YOU_DATA',
    data: data
  }
}

export const setAboutYouAnswers = (data) => {
  return {
    type: 'SET_ABOUT_YOU_QUIZ_ANSWERS',
    data: data
  }
}

export const setAboutYouMultiAnswer = (id, data) => {
  return {
    type: 'SET_ABOUT_YOU_MULTI_ANSWER',
    id: id,
    data: data
  }
}

export const setCurrentGlossaryItem = (data) => {
  return {
    type: 'SET_CURRENT_GLOSSARY_ITEM',
    data: data
  }
}

export const setActionsData = (data) => {
  return {
    type: 'SET_ACTIONS_DATA',
    data: data
  }
}

export const setSectorScores = (data) => {
  return {
    type: 'SET_SECTOR_SCORES',
    data: data
  }
}

export const setSurveyData = (data) => {
  return {
    type: 'SET_SURVEY_DATA',
    data: data
  }
}

export const setSurveyAnswer = (data) => {
  return {
    type: 'SET_SURVEY_ANSWER',
    data: data
  }
}


export function fetchGlossaryData(){
    return function (dispatch, getState){
      if (getState().app.framework.glosssary && getState().app.framework.glosssary.length){
        return;
      } else {
        dispatch(fetchFrameworkData());
      }
    }
} 

export function fetchCheckUserAuth(token){
  return function (dispatch, getState){
    dispatch(requestLogin());
    dispatch(unsetUserError());
    let data = JSON.stringify({'token': token});
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + token
      },
      body: data
    }
    return fetch(config.apiUrl+config.apiBaseUrl+'login', options).then(function(response){
      return response.json();
    }).then(function(response){
      if (!response.success){
        throw response.message;
      }
      dispatch(setUserData(response.entity));
      dispatch(mapUserDataToAboutYouAnswers());
      if (!cookies.hasItem('token')){
        cookies.setItem('token', token);
        dispatch(fetchSaveInProgressQuizzes());
      }
      dispatch(setUserLoggedIn());
      dispatch(unsetUserError());
    }).catch(function(error){
      console.log(error);
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
      
    }).then(function(){
      dispatch(endLogin());
    });
  }
}

export function logOutUser(){
  return function (dispatch, getState){
    dispatch(setUserLoggedOut());
    cookies.removeItem('token');
  }
}

export function fetchFrameworkData(type){
  return function (dispatch, getState){
    if (fetchingFrameworkData){return;}
    fetchingFrameworkData = true;
    const cached = getState().app.framework.domain; // since all in one call this works.
    dispatch(requestDomainData());
    switch(type){
      case 'roles':
        dispatch(requestRolesData());
        break;
      case 'sectors':
        dispatch(requestSectorData());
        break;
      case 'aspects':
        dispatch(requestAspectsData());
        break;
      default:
        break;
    }
    
    if (cached && cached.length > 0){
      switch(type){
        case 'roles':
          dispatch(completeRequestRolesData());
          break;
        case 'sectors':
          dispatch(completeRequestSectorData());
          break;
        case 'aspects':
          dispatch(completeRequestAspectsData());
          break;
        default:
          break;
      }
      dispatch(completeRequestDomainData());  
      fetchingFrameworkData = false;
      return;
    }
    
    let options = {
      method: 'GET',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      } 
    }
    return fetch(config.apiUrl+config.apiBaseUrl+'Framework/nocache', options).then(function(response){
      return response.json();
    }).then(json => {
      // because all data is in one call we do it all no matter what we requested.
      if (json && json.success){
        if (json.entity.domains){
          dispatch(setDomainData(json.entity.domains));
        }
        if (json.entity.sectors){
          dispatch(setSectorData(json.entity.sectors));
        }
        if (json.entity.aspects){
          dispatch(setAspectsData(json.entity.aspects));
        }
        if (json.entity.roles){
          dispatch(setRolesData(json.entity.roles));
        }
        if (json.entity.actions){
          dispatch(setActionsData(json.entity.actions));
        }
        if (json.entity.questions){
          let _saq = json.entity.questions.filter((q) => {
            return q.quizType == "ASSESSMENT";
          });
          let _cpq = json.entity.questions.filter((q) => {
            return q.quizType == "PATHWAY";
          });
          let _ayq = json.entity.questions.filter((q) => {
            return q.quizType == "ABOUT" && ""+q.questionId != "77";
          });
          
          let _survey = json.entity.questions.find((q) => { return q.questionId == 77 });
          
          let _saqm = json.entity.questions.map((q) => {
            let aspect = json.entity.aspects.find((a) => {return a.aspectId == q.aspectId});
            if (!aspect){
              return Object.assign({},q,{examples: JSON.parse(q.examples)});
            }
            let domainFramework = json.entity.domains.find((d) => { return d.domainId == aspect.domainId}).framework;
            return Object.assign({},q,{domainId: aspect.domainId, framework: domainFramework},{examples: JSON.parse(q.examples)});
          })
          dispatch(setSelfAssessmentQuizContent(_saqm));
          dispatch(setCareerPathwaysQuizContent(_cpq));
          dispatch(setAboutYouQuizData(_ayq));
          dispatch(mapUserDataToAboutYouAnswers());
          dispatch(setSurveyData(_survey));
        }
        if (json.entity.scoring){
          dispatch(setSectorScores(json.entity.scoring));
        }
        if (json.entity.definitions){
          dispatch(setGlossaryData(json.entity.definitions));
        }
        if (json.entity.sections){
          dispatch(setPageData(json.entity.sections));
        }
        if (json.entity.menus){
          dispatch(setMenuData(json.entity.menus));
        }
        if (json.entity.reasons){
            dispatch(setReasonsData(json.entity.reasons));
        }
        if (json.entity.postCards) {
            dispatch(setPostcardsData(json.entity.postCards));
        }
        if (json.entity.endorsedLogos) {
            dispatch(setEndorsedLogosData(json.entity.endorsedLogos));
        }
        
      } else {
        throw "null response";
      }
    }).then((r) => {
      fetchingFrameworkData = false;
      switch(type){
        case 'roles':
          dispatch(completeRequestRolesData());
          break;
        case 'sectors':
          dispatch(completeRequestSectorData());
          break;
        case 'aspects':
          dispatch(completeRequestAspectsData());
          break;
        default:
          break;
      }
      dispatch(completeRequestDomainData());
    })
  }
}

export function fetchArticleData(articleId){
  return function (dispatch, getState){
    const cached = getState().app.articles.articleContentById[articleId];
    dispatch(requestArticleContent(articleId));
    
    if (cached){
      dispatch(completeRequestArticleContent());
      return;
    }

    let options = {
      method: 'GET',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      } 
    }
    return fetch(config.apiUrl+config.apiBaseUrl+'articles', options).then(function(response){
      return response.json();
    }).then(json => {
      if (json && json.success){
        if (json.entity){
          for (var a in json.entity){
            dispatch(setArticleData(json.entity[a].articleId, json.entity[a]));
          }
        }
      }
      dispatch(completeRequestArticleContent());
    });
    
    // return dummyFetch('/articles/' + articleId).then(response => response.json())
    // .then(json => {
    //   dispatch(setArticleData(articleId, json));
    //   dispatch(completeRequestArticleContent());
    // })
  }
}

export function fetchSubmitArticleFeedback(articleId,title,feedback,positive){
    return function (dispatch, getState){
      let data = JSON.stringify({
        articleId: articleId,
        title: title,
        feedback: feedback,
        positive: positive
      })

      let options = {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + cookies.getItem('token')
        },
        body: data
      }
      return fetch(config.apiUrl+config.apiBaseUrl+'Articles/feedback', options).then(function(response){
        return response.json();
      }).then(json => {
        if (json && json.success){
          dispatch(openModal('thanks'));
        }
      })
    }
}

export function fetchUserQuizzes(forceUpdate){
  return function (dispatch, getState){
    if (!cookies.hasItem('token')){
      // console.log( "User is not logged in");
      return;
    }
    const cached = getState().app.user.quizzes;
    dispatch(requestUserData());
    if (!forceUpdate){
      if (cached && cached.length > 0){ 
        dispatch(endUserData());
        return;
      }
    }
    
    let options = {
      method: 'GET',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      } 
    }
    return fetch(config.apiUrl+config.apiBaseUrl+'users/quizzes', options).then(function(response){
      return response.json();
    }).then(function(response){
      if (!response.success){
        throw response.message;
      }
      if (response.success){
        dispatch(setUserQuizzes(response.entity));
        
        // load most recent unfinished quizzes as current quizzes.
        // if (response.entity && response.entity.length && response.entity.constructor == Array){
        //   let quizzes = response.entity.sort((a,b) => {
        //     let aDate = new Date(a.dateVal);
        //     let bDate = new Date(b.dateVal);
        //     if (aDate > bDate){ return -1;}
        //     if (aDate < bDate){ return 1;}
        //     return 0;
        //   });

        //   let latestAssessment = quizzes.find((q) => { return q.type === "ASSESSMENT" && !q.completed && q.results});
        //   let latestCareer = quizzes.find((q) => { return q.type === "PATHWAY" && !q.completed && q.results});
        //   if (latestAssessment){
        //     dispatch(loadQuiz("selfAssessment", latestAssessment.userQuizId));
        //   }
        //   if (latestCareer){
        //     dispatch(loadQuiz("careerPathways", latestCareer.userQuizId));
        //   }
        // }
      }
    }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
    }).then(function(){
      dispatch(endUserData());
    });
  }
}

export function loadQuiz(type, id){
  return function (dispatch, getState){
    let _quiz, _answers;
    _quiz = getState().app.user.quizzes.find((q) => { return q.userQuizId == id});
    if (_quiz) { 
      _answers = JSON.parse(_quiz.results).answers; 
      if (type === 'selfAssessment'){
        dispatch(setSelfAssessmentQuizCurrentAnswers(_answers));
        dispatch(setSelfAssessmentQuizCurrentUserQuizId(_quiz.userQuizId));
      } else if (type === 'careerPathways'){
        dispatch(setCareerPathwaysQuizCurrentAnswers(_answers));
        dispatch(setCareerPathwaysQuizCurrentUserQuizId(_quiz.userQuizId));
      }
    } else {
      throw "Invalid quiz";
    }
  }
}

export function fetchLogIn(e,p){
  return function (dispatch, getState){
    dispatch(requestLogin());
    dispatch(unsetUserError());
    let data = JSON.stringify({'email': e, 'password': p});
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
      },
      body: data
    }
    return fetch(config.apiUrl+config.apiBaseUrl+'login', options).then(function(response){
      return response.json();
    }).then(function(response){
      if (!response.success){
        throw response;
      }

      dispatch(setUserData(response.entity));
      if (response.entity.token){
        cookies.setItem('token', response.entity.token, 2592000);
      }
      
      dispatch(setUserLoggedIn());
      dispatch(mapUserDataToAboutYouAnswers());
      dispatch(fetchSaveInProgressQuizzes());
    }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
    }).then(function(){
      dispatch(endLogin());
    });
  }
}

// 'Authentication': 'bearer ' + cookies.getItem('token')


export function fetchRegister(n,e,p){
  return function (dispatch, getState){
    dispatch(requestRegister());
    dispatch(unsetUserError());
    let data = JSON.stringify({'name': n, 'email': e, 'password': p});
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
      },
      body: data
    };
    return fetch(config.apiUrl+config.apiBaseUrl+'users', options)
      .then(function(response){
        return response.json();
      }).then(function(response){
        if (!response.success){
          throw response.message;
        }
        dispatch(setUserData(response.entity));
        if (response.entity.token){
          dispatch(fetchCheckUserAuth(response.entity.token));
        }
      }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
      }).then(function(){
        dispatch(endRegister());
      });
  }
}

export function fetchUpdateUserDetails(name,email,salary){
  return function (dispatch, getState){
    dispatch(unsetUserError());
    if ((!name || !email || !salary)){
      dispatch(setUserError('Please ensure you have entered the required data.'));
      return;
    }
    if (!getState().app.user.loggedIn){
      dispatch(setUserError('Please ensure you are logged in.'));
      return;
    }
    let data = {
      name: name,
      email: email,
      salary: salary,
    };
    
    data = JSON.stringify(data);
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      },
      body: data
    };

    console.log(data);
    return fetch(config.apiUrl+config.apiBaseUrl+'users/edit', options).then(function(response){
        return response.json();
      }).then(function(response){
        if (!response.success){
          dispatch(setUserError(response.message));
        } else {
          dispatch(setUserData(Object.assign({},getState().app.user.user,{name: name, email: email, salary:salary})));
        }
      }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
      })
  }
}

export function fetchUpdateUserPassword(pass,newPass){
  return function (dispatch, getState){
    dispatch(unsetUserError());
    if (pass === newPass){
      return;
    }
    
    let data = {
      password: pass,
      newpassword: newPass
    };
    
    data = JSON.stringify(data);
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      },
      body: data
    };
    return fetch(config.apiUrl+config.apiBaseUrl+'users/changepassword', options).then(function(response){
        return response.json();
      }).then(function(response){
        if (!response.success){
          dispatch(setUserError(response.message));
        }
      }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
      })
  }
}

export function mapUserDataToAboutYouAnswers(){
  return function (dispatch, getState){
    let user = getState().app.user.user;
    let loggedIn = getState().app.user.loggedIn;
    let aboutYouQuestions = getState().app.quiz.aboutYouQuiz;
    let addressObj = {};
    if (user && loggedIn && aboutYouQuestions && aboutYouQuestions.length > 0){
      for (var ayq in aboutYouQuestions){
        if (aboutYouQuestions[ayq].fieldName){
          let camelCaseFieldName = aboutYouQuestions[ayq].fieldName.substring(0,1).toLowerCase() + aboutYouQuestions[ayq].fieldName.substring(1);
          if (camelCaseFieldName && camelCaseFieldName != 'undefined' && (camelCaseFieldName == 'address' || typeof user[camelCaseFieldName] != 'undefined')){
            let val;
            if (camelCaseFieldName == 'address'){
              addressObj = {
                country: user.country,
                suburb: user.suburb,
                postalCode: user.postalCode,
                state: user.state
              };
              dispatch(setAboutYouQuizAnswer(aboutYouQuestions[ayq].questionId, addressObj));
            } else {
              try{
                val = JSON.parse(user[camelCaseFieldName]);
              } catch(e){
                val = user[camelCaseFieldName];
              }
              if (val != {} && val != null){
                dispatch(setAboutYouQuizAnswer(aboutYouQuestions[ayq].questionId, val));
              }
              
            }
          }
        }
      }
    }
  }
}

export function fetchResetPassword(token,password){
  return function (dispatch, getState){
    let data = JSON.stringify({
      token: token,
      password: password
    });
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      },
      body: data
    };
    return fetch(config.apiUrl + config.apiBaseUrl + 'users/recover/reset', options).then(function(response){
      return response.json();
    }).then(function(response){
        if (!response.success){
          dispatch(setUserError(response.message || 'Invalid token'))
        } else {
          dispatch(closeModal());
          dispatch(openModal('resetComplete'));
        }
    })
  }
}

export function fetchRequestResetPassword(email){
  return function (dispatch, getState){
    if (!email){
      if (getState().app.user.user){
        email = getState().app.user.user.email
      }
      if (!email){
        dispatch(setUserError("No email provided."));
        return;
      }
    }
    let data = JSON.stringify({
      email: email
    });
    
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      },
      body: data
    };
    
    return fetch(config.apiUrl + config.apiBaseUrl + 'users/recover', options).then(function(response){
      return response.json();
    }).then(function(response){
        if (!response.success){
          dispatch(setUserError(response.message || 'Error requesting password reset token.'))
        } else {
          dispatch(openModal('resetSent'));
        }
    })
  }
}

export function fetchSaveInProgressQuizzes(quiz){
  return function (dispatch, getState){
    if (!cookies.hasItem('token')){
      throw "User is not logged in";
    }
    
    var selfAssessmentCurrentAnswers = getState().app.quiz.selfAssessmentCurrentAnswers;
    var careerPathwaysCurrentAnswers = getState().app.quiz.careerPathwaysCurrentAnswers;
    var selfAssessmentCurrentUserQuizId = getState().app.quiz.selfAssessmentCurrentUserQuizId;
    var careerPathwaysCurrentUserQuizId = getState().app.quiz.careerPathwaysCurrentUserQuizId;
    
    var selfAssessmentResults = getState().app.quiz.selfAssessmentResults;
    var careerPathwaysResults = getState().app.quiz.careerPathwaysResults;
    
    var aboutYouAnswers = getState().app.quiz.aboutYouAnswers;
    
    if (!quiz || quiz === 'selfAssessment'){
      if (Object.keys(selfAssessmentCurrentAnswers).length > 0){
        let results = {
            "answers": selfAssessmentCurrentAnswers
          };
        let completed = false;
        if (Object.keys(selfAssessmentResults).length > 0){
          results.results = selfAssessmentResults;
          completed = true;
        }
        results = JSON.stringify(results);
        let data = {
          "quizId": 0,
          "completed": completed,
          "results": results
        };
        if (selfAssessmentCurrentUserQuizId){data.userQuizId = selfAssessmentCurrentUserQuizId;}
        dispatch(fetchSaveQuiz('selfAssessment', JSON.stringify(data)));
      }
    }
    
    if (!quiz || quiz === 'careerPathways'){
      if (Object.keys(careerPathwaysCurrentAnswers).length > 0 ){
        let results = {
            "answers": careerPathwaysCurrentAnswers
        };
        let completed = false;
        if (Object.keys(careerPathwaysResults).length > 0){
          completed = true;
          results.results = careerPathwaysResults;
        }
        results = JSON.stringify(results);
        
        let data = {
          "quizId": 1,
          "completed": completed,
          "results": results
        };  
        if (careerPathwaysCurrentUserQuizId){data.userQuizId = careerPathwaysCurrentUserQuizId;}
        dispatch(fetchSaveQuiz('careerPathways', JSON.stringify(data)));
      }
    }
    
    if (aboutYouAnswers && Object.keys(aboutYouAnswers).length > 0){
      for (let answer in aboutYouAnswers){
        if (aboutYouAnswers[answer] && aboutYouAnswers[answer] != undefined && aboutYouAnswers[answer].constructor == Array){
          aboutYouAnswers[answer] = JSON.stringify(aboutYouAnswers[answer]);
        }
      }
      let answers = JSON.stringify(aboutYouAnswers);
      let data = JSON.stringify({
        "quizId": 2,
        "answers": answers,
        complete: true
      })
      dispatch(fetchSaveQuiz('aboutYou', data));
    }
  }
}

// self assessment is hardcoded quiz id 0, career pathways 1
export function fetchSaveQuiz(type, data){ 
  return function (dispatch, getState){
    if (!cookies.hasItem('token')){
      throw "User is not logged in";
    }
    
    if (!data || data == {}){
      return;
    }
    
    let _type, uqid;
    
    if (type === 'careerPathways'){
      _type = 'career';
      uqid = getState().app.quiz.careerPathwaysCurrentUserQuizId;
    }
    if (type === 'selfAssessment'){
      _type = 'selfassessment';
      uqid = getState().app.quiz.selfAssessmentCurrentUserQuizId;
    }
    if (type === 'aboutYou'){
      _type = 'aboutyou';
    }
    
    dispatch(unsetUserError());
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      },
      body: data
    };
    return fetch(config.apiUrl + config.apiBaseUrl + 'users/quiz/' + _type + '/save', options)
      .then(function(response){
        return response.json();
      }).then(function(response){
        if (!response.success){
          throw response.message;
        }
        if (!uqid){
          if (_type === 'career'){
            dispatch(setCareerPathwaysQuizCurrentUserQuizId(response.entity));
          } else if (_type === 'selfassessment'){
            dispatch(setSelfAssessmentQuizCurrentUserQuizId(response.entity));
          }
        }
      }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
      })
  }
}

export function fetchSaveAnonymousCarerrQuiz(data) {
  return function (dispatch, getState) {
    if (!cookies.hasItem('token')){

      let data = {
        careerPathwaysCurrentAnswers: getState().app.quiz.careerPathwaysCurrentAnswers,
        careerPathwaysResults: getState().app.quiz.careerPathwaysResults,
        aboutYouAnswers: getState().app.quiz.aboutYouAnswers
      };
      let options = {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
        },
        body: JSON.stringify(data)
      };
      return fetch(config.apiUrl+config.apiBaseUrl+'report/saveanoncareerreport', options).then(
        () => console.log("career info saved")
      ).catch((error) => console.log(error))
    }
  }
}

export function fetchContactRequest(name,phone,email,message,sectorName){
  return function (dispatch, getState){
    dispatch(unsetUserError());
    if (!name || !phone || !email || !message){
      dispatch(setUserError("Please fill in all required information."));
      return;
    }
    
    let data = JSON.stringify({
      name: name,
      email: email,
      phone: phone,
      message: message,
      sectorName: sectorName
    });
    
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
      },
      body: data
    };
    return fetch(config.apiUrl + config.apiBaseUrl + 'Framework/contact', options)
    .then(function(response){
      return response.json();
    }).then(function(response){
      if (!response.success){
        dispatch(setUserError(response.message));
        return false;
      }
      return true;
    })
  }
}

export function fetchSaveCompletedQuiz(type, redirect){
  return function (dispatch, getState){
    if (!cookies.hasItem('token')){
      dispatch(completeRequestCareerPathwaysQuizResults());
      dispatch(completeRequestSelfAssessmentQuizResults());
      return;
    }
    
    let _type, uqid, answers, results, data, quizId, survey;
    
    if (type === 'careerPathways'){
      _type = 'career';
      uqid = getState().app.quiz.careerPathwaysCurrentUserQuizId;
      answers = getState().app.quiz.careerPathwaysCurrentAnswers;
      results = getState().app.quiz.careerPathwaysResults;
      quizId = 1;
    }
    if (type === 'selfAssessment'){
      _type = 'selfassessment';
      uqid = getState().app.quiz.selfAssessmentCurrentUserQuizId;
      answers = getState().app.quiz.selfAssessmentCurrentAnswers;
      results = getState().app.quiz.selfAssessmentResults;
      quizId = 0;
      survey = getState().app.quiz.surveyAnswer;
    }
    
    
    data = {
      quizId: quizId,
      completed: true,
      results: JSON.stringify({
        answers: answers,
        results: results,
      })
    };
    
    if (uqid){
      data.userQuizId = uqid;
    } 
    if (survey){
      data.survey = survey;
    }
    
    let aboutYouAnswers = getState().app.quiz.aboutYouAnswers;
    if (aboutYouAnswers && Object.keys(aboutYouAnswers).length > 0){
      let sa = JSON.stringify(aboutYouAnswers);
      let aboutYouData = JSON.stringify({
        "answers": sa
      })
      dispatch(fetchSaveQuiz('aboutYou', aboutYouData));
    }
    
    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookies.getItem('token')
      },
      body: JSON.stringify(data)
    };
    return fetch(config.apiUrl + config.apiBaseUrl + 'users/quiz/' + _type + '/save', options)
      .then(function(response){
        return response.json();
      }).then(function(response){
        if (!response.success){
          throw response.message;
        }
        if (_type === 'career'){
          dispatch(setCareerPathwaysQuizCurrentAnswers({}));
          dispatch(setCareerPathwaysQuizCurrentUserQuizId(null));
          if (redirect){
            dispatch(setCareerPathwaysQuizResults({}));
            dispatch(completeRequestCareerPathwaysQuizResults());
            if (uqid){
              dispatch(push('/results/careerPathways/'+uqid))
            }else{
              dispatch(push('/results/careerPathways/'+response.entity))
            }
            
          }
        };
        if (_type === 'selfassessment'){
          dispatch(setSelfAssessmentQuizCurrentAnswers({}));
          dispatch(setSelfAssessmentQuizCurrentUserQuizId(null));
          if (redirect){
            dispatch(setSelfAssessmentQuizResults({}));
            dispatch(completeRequestSelfAssessmentQuizResults());
            if (uqid){
              dispatch(push('/results/selfAssessment/'+uqid))
            }else{
              dispatch(push('/results/selfAssessment/'+response.entity))
            }
          }
        };
        dispatch(fetchUserQuizzes(true));
      }).catch(function(error){
        if (error && error.message){
          dispatch(setUserError(error.message));
        } else {
          dispatch(setUserError('An unknown error occurred.'));
        }
      })
  }
}

export function fetchArticleList(){
  return function (dispatch, getState){
    const cached = getState().app.articles.articleList;
    dispatch(requestArticleList());
    
    if (cached && cached.length > 0){
      dispatch(setArticleListData(cached));
      dispatch(completeRequestArticleList());
      return;
    }
    
    return dummyFetch('/articles/list').then(response => response.json())
    .then(json => {
      dispatch(setArticleListData(json));
      dispatch(completeRequestArticleList());
    })
  }
}

export function fetchSelfAssessmentQuizData(){
  return function(dispatch, getState){
    const cached = getState().app.quiz.selfAssessment;
    dispatch(requestSelfAssessmentQuizContent());
    
    if (cached && cached.length > 0){
      dispatch(completeRequestSelfAssessmentQuizContent());
      return;
    }
    
    // return dummyFetch('/quiz/selfAssessment').then(response => response.json())
    // .then(json => {
    //   dispatch(setSelfAssessmentQuizContent(json));
    //   dispatch(completeRequestSelfAssessmentQuizContent());
    // })
    dispatch(fetchFrameworkData('questions'));
    dispatch(completeRequestSelfAssessmentQuizContent());
  }
}

export function fetchCareerPathwaysQuizData(){
  return function(dispatch, getState){
    const cached = getState().app.quiz.careerPathways;
    dispatch(requestCareerPathwaysQuizContent());
    
    if (cached && cached.length > 0){
      dispatch(completeRequestCareerPathwaysQuizContent());
      return;
    }
    
    dispatch(fetchFrameworkData('questions'));
    dispatch(completeRequestCareerPathwaysQuizContent());
  }
}

export function fetchCareerPathwaysQuizResults(){
  return function(dispatch, getState){
    var quiz = getState().app.quiz.careerPathways
    var userAnswers = getState().app.quiz.careerPathwaysCurrentAnswers;
    var mappedAnswers = [];
    for(var a in userAnswers) {
      if(userAnswers.hasOwnProperty(a)) {
          mappedAnswers.push({
            questionId: a,
            value: userAnswers[a]
          })
      }
    }
    let patients = [];
    let patientsLookup = {};
    
    let pq = getState().app.quiz.aboutYouQuiz.find((ayq) => { return ayq.fieldName == 'Patients'});
    if (pq){
      for (let plq of pq.answers){
        patientsLookup[plq.value] = plq.text;
      }
    }
    
    if (getState().app.user.user && getState().app.user.user.patients){
      patients = JSON.parse(getState().app.user.user.patients);
    } else {
      if (pq && getState().app.quiz.aboutYouAnswers && getState().app.quiz.aboutYouAnswers[pq.questionId]){
        patients = getState().app.quiz.aboutYouAnswers[pq.questionId];
      }
    }
    
    dispatch(requestCareerPathwaysQuizResults());
    
    return scoreCareerQuiz(mappedAnswers,getState().app.framework.sectors,getState().app.framework.sectorScores, quiz,patients,patientsLookup).then(json => {
      dispatch(setCareerPathwaysQuizResults(json));
      dispatch(completeRequestCareerPathwaysQuizResults());
    }).then(() => {
      dispatch(fetchSaveCompletedQuiz('careerPathways', true));
    });
  }
}
export function fetchSelfAssessmentQuizResults(framework){
  return function(dispatch, getState){
    var quiz = getState().app.quiz.selfAssessment;
    var userAnswers = getState().app.quiz.selfAssessmentCurrentAnswers;
    var mappedAnswers = [];
    for(var a in userAnswers) {
      if(userAnswers.hasOwnProperty(a)) {
        var qq = quiz.find((q) => {return q.questionId == +a});
        // until i get real data from server gonna use my dumb aspect id thing.
        if (qq.aspectId){
          var qqDomainId = qq.domainId;
          var qqAspectId = qq.aspectId;
          var mappedAnswer = {
            questionId: a,
            value: userAnswers[a],
            aspectId: qqAspectId,
            domainId: qqDomainId
          };
          mappedAnswers.push(mappedAnswer);
        }
      }
    }
    
    dispatch(requestSelfAssessmentQuizResults());
    
    return scoreSelfAssessmentQuiz(mappedAnswers, getState().app.framework.aspects, framework).then(json => {
      dispatch(setSelfAssessmentQuizResults(json));
      dispatch(fetchSaveCompletedQuiz('selfAssessment', true));
    })
  }
}

export function fetchPageData(endpoint){
  return function (dispatch, getState){
    const cached = getState().app.content.sections;
    dispatch(requestPageContent(endpoint));
    
    if (cached && cached.length > 0){
      dispatch(completeRequestPageContent());
      return;
    }
    
    dispatch(fetchFrameworkData());
    dispatch(completeRequestPageContent());
  }
}

export function scoreSelfAssessmentQuiz(answers, aspects, framework){
  var scores = {};
  var averages = {};
  var actions = {};
  if (!framework){framework = 'rn'}
  for (let a of answers){
    var domainId = a.domainId
    if (!scores[domainId]){ scores[domainId] = 0; }
    scores[domainId] += +a.value;
    
    // actions
    let answerAspect = aspects.find((asp) => { return asp.aspectId == a.aspectId});
    if (answerAspect){
      let actionsToAdd = answerAspect.actionsList.filter((act) => { return act.levelAction > a.answerValue || act.levelAction == 2});
      if (!actions[""+a.domainId]){
        actions[""+a.domainId] = [];
      }
      for (let act in actionsToAdd){
        let currentDomainActions = actions[""+a.domainId].find((action) => { return actionsToAdd[act].actionId == action.actionId});
        let duplicate = currentDomainActions && currentDomainActions.length > 0;
        if (!duplicate){
          actions[""+a.domainId].push(actionsToAdd[act].actionId);
        }
      }
    }
    // /actions
    
  }
  for(let s in scores){
    var numberInDomain = answers.filter((ss,ii) => { return +ss.domainId == +s }).length;
    averages[s] = scores[s] / numberInDomain;
  }
  
  var dateString =  new Date().toDateString().split(' ');
  dateString = dateString[2] + ' ' + dateString[1] + ' ' + dateString[3];
  
  return new Promise((res,rej) => {
    setTimeout(() => {
      res({
        score: averages,
        date: dateString,
        id: 0,
        framework: framework,
        actions: actions
      });
    }, 1500);
  });
}

export function selfAssessmentResultsToReportJSON(quiz, user, name, email, quizId, download, saveOnly){
  return function (dispatch, getState){
    let domainIdNameLookup = {};
    let aspectsByDomain = {};
    let aspectStrengthsByDomain = {};
    let topActionsByDomain = {};
    let anon

    if (!getState().app.user.loggedIn) {
      anon = true
    } else {
      anon = false
    }

    if (quiz == null){
      if (quizId == null || isNaN(+quizId)){
        quiz = {
          answers: getState().app.quiz.selfAssessmentCurrentAnswers,
          results: getState().app.quiz.selfAssessmentResults
        }
        anon = true;
      }else{
        quiz = JSON.parse(getState().app.user.quizzes.find((q) => { return q.userQuizId == quizId}).results);
      }
      
    }
    let frameworkDomains = getState().app.framework.domain.filter((d) => {
      return d.framework == quiz.results.framework;
    });

    for( var d in frameworkDomains){
      let currentDomain = frameworkDomains[""+d];
      let _ass = getState().app.framework.aspects.filter((a) => { return a.domainId == currentDomain.domainId});
      let _questionAnswers = getState().app.quiz.selfAssessment.filter((e) => { 
        return typeof quiz.answers[e.questionId] != "undefined"
      });

      let mappedQuestionAnswers = _questionAnswers.map((q) => {
        return {
          aspectId: q.aspectId,
          answerValue: quiz.answers[q.questionId],
          answerText: q.answers.find((a) => { return a.value == quiz.answers[q.questionId]}).text
        }
      });
      
      aspectStrengthsByDomain["" + currentDomain.domainId] = mappedQuestionAnswers.filter((qa) => {
          return _ass.find((a) => { return qa.aspectId == a.aspectId && qa.answerValue >= 0.33 });
      }).sort((a,b) => {
        if (a.answerValue < b.answerValue){return 1;}
        if (a.answerValue > b.answerValue){return -1;}
        return 0;
      }).map((e) => {
        return e.aspectId;
      }).slice(0,3);
      
      aspectsByDomain[""+currentDomain.domainId] = _ass.map((ass) => {
        let _qa = mappedQuestionAnswers.find((a) => { return a.aspectId == ass.aspectId });
        return {
          aspectId: ass.aspectId,
          answer: _qa.answerValue,
          answerText: _qa.answerText.replace(/’/g,"'"),
          name: ass.title,
          definition: ass.text.replace(/<\/?[^>]+(>|$)/g, ""),
          actionsToGrow: ass.actionsList.filter((a) => { return (a.levelAction == 2 && _qa.answerValue >= 0.66) || (a.levelAction == 1 && _qa.answerValue >= 0.33 && _qa.answerValue < 0.66) || (a.levelAction == 0 && _qa.answerValue < 0.33) }).map((a) => { return { text: a.title } }).slice(0, 4)
        }
      });


      // -- Start get actions and push them into our action list.
      /*let actionsToAdd = aspectsByDomain[""+currentDomain.domainId];
      var hasDuplicateAction = false;

      for (var e in actionsToAdd){
        let actionsToGrow = actionsToAdd["" + e].actionsToGrow;

        for (var f in actionsToGrow) {
          if (currentDomain.actionsList.length > 9) { // getting only a max limit of 10 items
            break;
          }

          // Check for duplicates before adding to actions.
          hasDuplicateAction = false;
          for (var g in currentDomain.actionsList) {
            if (currentDomain.actionsList[g].text == actionsToGrow["" + f].text) {
              hasDuplicateAction = true;
            }
          }

          if (!hasDuplicateAction) { // If false, push to array otherwise we found a duplicate so skip..
            currentDomain.actionsList.push(actionsToGrow["" + f]);
          }
        }
      }*/
      // -- End get actions


      topActionsByDomain[""+currentDomain.domainId] = currentDomain.actionsList;
      domainIdNameLookup[""+currentDomain.domainId] = currentDomain.title;

      //console.log('currentDomain.actionsList', currentDomain.actionsList);
      //console.log('topActionsByDomain[""+currentDomain.domainId]', currentDomain.domainId, topActionsByDomain[""+currentDomain.domainId]);
    }
    //return;
    
    let postCards = shuffle(getState().app.framework.postcards);
    
    
    
    let resUser = {
      name: user.name || name,
      date: quiz.results.date,
      domainNames: domainIdNameLookup,
      postcards: postCards,
      aspects: aspectsByDomain,
      domainScores: quiz.results.score,
      domainStrengths: aspectStrengthsByDomain,
      topActions: topActionsByDomain
    }

    let resAnon = {
      name: user.name || name,
      date: quiz.results.date,
      domainNames: domainIdNameLookup,
      postcards: postCards,
      aspects: aspectsByDomain,
      domainScores: quiz.results.score,
      domainStrengths: aspectStrengthsByDomain,
      topActions: topActionsByDomain,
      aboutYouAnswers: getState().app.quiz.aboutYouAnswers,
      selfAssessmentResults: getState().app.quiz.selfAssessmentResults,
      email: email,
      saveOnly: saveOnly
    }
    
    let data

    if (anon){
      data = JSON.stringify(resAnon)
    } else {
      data = JSON.stringify(resUser)
    }

    let options = {
      method: 'POST',
      headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
      },
      body: data
    }
    let endpoint;
    if (user.name){
      options.headers.Authorization = 'Bearer ' + cookies.getItem('token');
      endpoint = 'report'
    }else{
      endpoint = 'report/anonymous'
    }
    
    if (download){
    return fetch(config.apiUrl+config.apiBaseUrl+'report/download', options).then(function(response){
      return response.blob();
    }).then(function(response){
      let filename = "Report-" + new Date().getTime() + ".pdf";
      if (typeof window.chrome !== 'undefined') {
          // Chrome version
          var link = document.createElement('a');
          link.href = URL.createObjectURL(response);
          link.download = filename;
          link.click();
      } else if (typeof window.navigator.msSaveBlob !== 'undefined') {
          // IE version
          var blob = new Blob([response], { type: 'application/pdf' });
          window.navigator.msSaveBlob(blob, filename);
      } else {
          // Firefox version
          var file = new File([response], filename, { type: 'application/force-download' });
          window.open(URL.createObjectURL(file));
      }
      
    });
    }else{
      return fetch(config.apiUrl+config.apiBaseUrl+endpoint, options).then(function(response){
        return response.json();
      }).then(function(response){
      
      });
    }
    


  }
  
  //name
  //date
  //domainName lookup {id: name}
  //postcard
  //aspects
  //  domainId
  //    [ aspects ]
  //    answer, answerText, name, definition, actionsToGrow
  //domainScores
  //domainStrengths (aspectIds highest)
  //topactions
  //  domainId
  //    action {text}
}

export function scoreCareerQuiz(answers,sectors,scoring,quizData,patients,patientsLookup){
  var sectorScores = {};
  var sectorPositives = {};
  var sectorNegatives = {};
  var sectorPercentages = {};
  var sectorDifferenceCeilings = {};
  
  // hard coding this because it was not scoped and is being put in too late
  var invalidPatientTypesBySector = {
    "1": [], //general
    "2": [1,6,3,4,2], //residential
    "3": [], // refugee
    "4": [], // community
    "5": [1], // correctional
    "6": [], // aboriginal
    "7": [], // public health
    "8": [1], // drug and alcohol
    "9": [1], // sexual health
    "10": [], // hospital setting
  }
  
  for (let sec of scoring){
    sectorDifferenceCeilings[sec.sectorId] = sec.idealAnswers.reduce((acc,val) => {
      let aVal = +(val.value);
      if (aVal == 0){aVal = 1}
      return acc + aVal;
    },1);
    
    if (!sectorNegatives[sec.sectorId]){ sectorNegatives[sec.sectorId] = []; }
    if (!sectorPositives[sec.sectorId]){ sectorPositives[sec.sectorId] = []; }
    
    var patientsDiff = 0;
    let skipInvalidPatientsCheck = false;
    if (invalidPatientTypesBySector[""+sec.sectorId]){
      if (patients.constructor == Array){
        if (patients.includes(7)){
          skipInvalidPatientsCheck = true;
        }
      } else if (patients.constructor == String || patients.constructor == Number){
        if (patients == "7"){
          skipInvalidPatientsCheck = true;
        }
      }
      if (!skipInvalidPatientsCheck){
        let foundInvalid = false;
        for (let pt of patients){
          if (invalidPatientTypesBySector[""+sec.sectorId].includes(pt)){
            foundInvalid = true;
            sectorNegatives[sec.sectorId].push("Do not work with " + patientsLookup[pt]);
          } else {
            sectorPositives[sec.sectorId].push("Work with " + patientsLookup[pt]);
          }
        }
        if (foundInvalid){patientsDiff = 0.1;}
      }

    }
  
    
    for (let sectorAnswer of sec.idealAnswers){ 

      var userAnswer = answers.find((a) => {return a.questionId == sectorAnswer.questionId});
      if (!userAnswer){continue;} // this shouldn't happen but uhh lets ignore it anyway
      
      if (!sectorScores[sec.sectorId]) { sectorScores[sec.sectorId] = 0; }
      
      var diff = Math.abs(userAnswer.value - sectorAnswer.value);
      
      let matchText;
      let matchedAnswer = quizData.find((q) => {
        return q.questionId === sectorAnswer.questionId;
      }).answers.find((a) => {
        return a.value === sectorAnswer.value;
      });
      if (matchedAnswer){ matchText = matchedAnswer.matchText }
      if (matchText){
        if (diff <= 0.5){
          if (!sectorPositives[sec.sectorId]){ sectorPositives[sec.sectorId] = []; }
          sectorPositives[sec.sectorId].push(matchText);
        }else if (diff >= 0.6){
          if (!sectorNegatives[sec.sectorId]){ sectorNegatives[sec.sectorId] = []; }
          sectorNegatives[sec.sectorId].push(matchText);
        }
      }
      sectorScores[sec.sectorId] += Math.abs(userAnswer.value - sectorAnswer.value);
      sectorPercentages[sec.sectorId] = Math.round(100 - ((+sectorScores[sec.sectorId] / +sectorDifferenceCeilings[sec.sectorId]) * 100) - patientsDiff * 100);
    }
  }

  var dateString =  new Date().toDateString().split(' ');
  dateString = dateString[2] + ' ' + dateString[1] + ' ' + dateString[3];
  
  //return sectorScores;
  return new Promise((res,rej) => {
    setTimeout(() => {
      res({
        score: sectorScores,
        scorePositives: sectorPositives,
        scoreNegatives: sectorNegatives,
        scorePercentages: sectorPercentages,
        date: dateString,
        id: 0
         });
    }, 1500);
  });
}