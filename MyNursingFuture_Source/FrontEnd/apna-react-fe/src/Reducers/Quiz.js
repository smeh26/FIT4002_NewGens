const initialState = {
  selfAssessment: {},
  selfAssessmentCurrentAnswers: {},
  selfAssessmentLoading: false,
  selfAssessmentResults: {},
  selfAssessmentResultsLoading: false,
  selfAssessmentCurrentUserQuizId: null,
  careerPathways: {},
  careerPathwaysCurrentAnswers: {},
  careerPathwaysLoading: false,
  careerPathwaysResults: {},
  careerPathwaysResultsLoading: false,
  careerPathwaysCurrentUserQuizId: null,
  error: undefined,
  aboutYouQuiz: [],
  aboutYouAnswers: {},
  surveyQuestion: null,
  surveyAnswer: null
};

const toggleArrayElement = (o, d) => {
  let newValue;
  if (!o){o = []; }
  if (o.includes(d)){
    newValue = o.filter((e) => {return e != d});
  } else {
    o.push(d);
    newValue = o;
  }
  return newValue;
}

const quiz = (state = initialState, action) => {
  switch(action.type) {
    case 'START_SELF_ASSESSMENT_REQUEST':
      // enforce single request in pipeline at once?
      return Object.assign({}, state, {
        selfAssessmentLoading: true
      });
    case 'END_SELF_ASSESSMENT_REQUEST':
      return Object.assign({}, state, {
        selfAssessmentLoading: false
      });
    case 'SET_SELF_ASSESSMENT_DATA':
      return Object.assign({}, state, {
        selfAssessment: action.data
      });
    case 'START_CAREER_PATHWAYS_REQUEST':
      return Object.assign({}, state, {
        careerPathwaysLoading: true
      });
    case 'SET_SURVEY_DATA':
      return Object.assign({}, state, {
        surveyQuestion: action.data
      });
    case 'SET_SURVEY_ANSWER':
      return Object.assign({}, state, {
        surveyAnswer: action.data
      });
    case 'END_CAREER_PATHWAYS_REQUEST':
      return Object.assign({}, state, {
        careerPathwaysLoading: false
      });
    case 'SET_CAREER_PATHWAYS_DATA':
      return Object.assign({}, state, {
        careerPathways: action.data
      });
    case 'SET_SELF_ASSESSMENT_ANSWER': 
      return Object.assign({}, state, {
        selfAssessmentCurrentAnswers: Object.assign({},state.selfAssessmentCurrentAnswers,{
          [action.id]: action.data
        })
      });
    case 'SET_SELF_ASSESSMENT_MULTI_ANSWER':
      return Object.assign({}, state, {
        selfAssessmentCurrentAnswers: Object.assign({},state.selfAssessmentCurrentAnswers,{
          [action.id]: toggleArrayElement(state.selfAssessmentCurrentAnswers[action.id],action.data)
        })
      });
    case 'SET_CAREER_PATHWAYS_ANSWER': 
      return Object.assign({}, state, {
        careerPathwaysCurrentAnswers: Object.assign({},state.careerPathwaysCurrentAnswers,{
          [action.id]: action.data
        })
      });
    case 'SET_CAREER_PATHWAYS_MULTI_ANSWER':
      return Object.assign({}, state, {
        careerPathwaysCurrentAnswers: Object.assign({},state.careerPathwaysCurrentAnswers,{
          [action.id]: toggleArrayElement(state.careerPathwaysCurrentAnswers[action.id],action.data)
        })
      });
    case 'START_SCORE_SELF_ASSESSMENT_QUIZ':
      return Object.assign({}, state, {
        selfAssessmentResultsLoading: true
      });
    case 'END_SCORE_SELF_ASSESSMENT_QUIZ':
      return Object.assign({}, state, {
        selfAssessmentResultsLoading: false
      });
    case 'SET_SELF_ASSESSMENT_QUIZ_RESULTS':
      return Object.assign({}, state, {
        selfAssessmentResults: action.data
      });
    case 'START_SCORE_CAREER_PATHWAYS_QUIZ':
      return Object.assign({}, state, {
        careerPathwaysResultsLoading: true
      });
    case 'END_SCORE_CAREER_PATHWAYS_QUIZ':
      return Object.assign({}, state, {
        careerPathwaysResultsLoading: false
      });
    case 'SET_CAREER_PATHWAYS_QUIZ_RESULTS':
      return Object.assign({}, state, {
        careerPathwaysResults: action.data
      });
    case 'SET_CAREER_PATHWAYS_QUIZ_CURRENT_ANSWERS':
      return Object.assign({}, state, {
        careerPathwaysCurrentAnswers: action.data
        }
      );
    case 'SET_SELF_ASSESSMENT_QUIZ_CURRENT_ANSWERS':
      return Object.assign({}, state, {
        selfAssessmentCurrentAnswers: action.data
        }
      );
    case 'SET_SELF_ASSESSMENT_QUIZ_USER_QUIZ_ID':
      return Object.assign({}, state, {
        selfAssessmentCurrentUserQuizId: action.data
        }
      );
    case 'SET_CAREER_PATHWAYS_QUIZ_USER_QUIZ_ID':
      return Object.assign({}, state, {
        careerPathwaysCurrentUserQuizId: action.data
        }
      );
    case 'SET_ABOUT_YOU_QUIZ_ANSWER':
      return Object.assign({}, state, {
        aboutYouAnswers: Object.assign({},state.aboutYouAnswers,{
          [action.id]: action.data
        })
      });
    case 'SET_ABOUT_YOU_MULTI_ANSWER':
      return Object.assign({}, state, {
        aboutYouAnswers: Object.assign({},state.aboutYouAnswers,{
          [action.id]: toggleArrayElement(state.aboutYouAnswers[action.id],action.data)
        })
      });
    case 'SET_ABOUT_YOU_QUIZ_ANSWERS':
      return Object.assign({}, state, {
        aboutYouAnswers: action.data
        }
      );
    case 'SET_ABOUT_YOU_DATA':
      return Object.assign({}, state, {
        aboutYouQuiz: action.data
      });
    case 'SET_ERROR':
      return Object.assign({}, state, {
        error: action.error
      })
    default:
      return state;
  }
}

export default quiz;