const initialState = {
  sections: [],
  currentSection: null,
  currentPageEndpoint: '/',
  displayPage: '/',
  isLoading: false,
  error: undefined
};

const pageContent = (state = initialState, action) => {
  switch(action.type) {
    case 'START_PAGE_CONTENT_REQUEST':
      // enforce single request in pipeline at once?
      return Object.assign({}, state, {
        isLoading: true,
        currentPageEndpoint: action.endpoint,
        displayPage: action.endpoint
      });
    case 'END_PAGE_CONTENT_REQUEST':
      return Object.assign({}, state, {
        isLoading: false
      });
    case 'SET_PAGE_DATA':
      return Object.assign({}, state, {
        sections: action.data
      });
    case 'SET_PAGE_ERROR':
      return Object.assign({}, state, {
        error: action.error
      })
    default:
      return state;
  }
}

export default pageContent;