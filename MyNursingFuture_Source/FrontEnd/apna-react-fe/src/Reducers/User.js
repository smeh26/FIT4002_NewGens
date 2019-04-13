const initialState = {
  user: {},
  quizzes: [],
  loggedIn: false,
  isLoading: false,
  error: undefined,
  message: undefined
};

const user = (state = initialState, action) => {
  switch(action.type) {
    case 'START_LOGIN':
    case 'START_REGISTER':
    case 'START_USER_DATA':
      return Object.assign({}, state, {
        isLoading: true
      });
    case 'END_LOGIN':
      return Object.assign({}, state, {
        isLoading: false,
        message: action.message
      });
    case 'SET_USER_DATA':
      return Object.assign({}, state, {
        user: action.data
      });
    case 'SET_USER_QUIZZES':
      return Object.assign({}, state, {
        quizzes: action.data
      });
    case 'SET_USER_LOGGED_IN':
      return Object.assign({}, state, {
        loggedIn: true
      });
    case 'SET_USER_LOGGED_OUT':
      return Object.assign({}, state, {
        loggedIn: false
      });
    case 'START_LOGOUT':
      return Object.assign({}, state, {
        isLoading: false
      });
    case 'END_LOGOUT':
    case 'END_REGISTER':
    case 'END_USER_DATA':
      return Object.assign({}, state, {
        isLoading: false,
        message: action.message
      });
    case 'UNSET_USER_DATA':
      return Object.assign({}, state, {
        user: {}
      });
    case 'SET_USER_ERROR':
      return Object.assign({}, state, {
        error: action.data
      });
    case 'UNSET_USER_ERROR':
      return Object.assign({}, state, {
        error: null
      });
    default:
      return state;
  }
}

export default user;