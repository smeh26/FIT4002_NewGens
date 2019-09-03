const initialState = {
    employer: {},
    quizzes: [],
    loggedIn: true,
    isLoading: false,
    error: undefined,
    message: undefined
};

const employer = (state = initialState, action) => {
    switch (action.type) {
        case 'START_LOGIN':
        case 'START_REGISTER':
        case 'START_EMPLOYER_DATA':
            return Object.assign({}, state, {
                isLoading: true
            });
        case 'END_LOGIN':
            return Object.assign({}, state, {
                isLoading: false,
                message: action.message
            });
        case 'SET_EMPLOYER_DATA':
            return Object.assign({}, state, {
                employer: action.data
            });
        case 'SET_EMPLOYER_QUIZZES':
            return Object.assign({}, state, {
                quizzes: action.data
            });
        case 'SET_EMPLOYER_LOGGED_IN':
            return Object.assign({}, state, {
                loggedIn: true
            });
        case 'SET_EMPLOYER_LOGGED_OUT':
            return Object.assign({}, state, {
                loggedIn: false
            });
        case 'START_LOGOUT':
            return Object.assign({}, state, {
                isLoading: false
            });
        case 'END_LOGOUT':
        case 'END_REGISTER':
        case 'END_EMPLOYER_DATA':
            return Object.assign({}, state, {
                isLoading: false,
                message: action.message
            });
        case 'UNSET_EMPLOYER_DATA':
            return Object.assign({}, state, {
                employer: {}
            });
        case 'SET_EMPLOYER_ERROR':
            return Object.assign({}, state, {
                error: action.data
            });
        case 'UNSET_EMPLOYER_ERROR':
            return Object.assign({}, state, {
                error: null
            });
        default:
            return state;
    }
}

export default employer;