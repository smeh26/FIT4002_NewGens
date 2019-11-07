const initialState = {
    listing: [],
    isLoading: false,
}
//reducer to add data received from APi to global state.
export default (state = initialState, action) => {
    switch (action.type) {
        case 'SET_JOBLISTING_DATA':
            return Object.assign({}, state, {
                listing: action.data
            });
        case 'START_JOBLISTING_REQUEST':
            return Object.assign({}, state, { isLoading: true });
        case 'END_JOBLISTING_REQUEST':
            return Object.assign({}, state, { isLoading: false })
        default:
            return state;
    }
}
