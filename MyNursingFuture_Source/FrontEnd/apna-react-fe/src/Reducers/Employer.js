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
        case 'EMP_START_LOGIN':
        case 'EMP_START_REGISTER':
        case 'START_EMPLOYER_DATA':
            return Object.assign({}, state, {
                isLoading: true
            });
        case 'EMP_END_LOGIN':
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
        case 'EMP_START_LOGOUT':
            return Object.assign({}, state, {
                isLoading: false
            });
        case 'EMP_END_LOGOUT':
        case 'EMP_END_REGISTER':
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