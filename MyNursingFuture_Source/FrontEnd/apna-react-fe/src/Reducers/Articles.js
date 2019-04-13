const initialState = {
  articleList: [],
  articleContentById: {},
  displayArticle: '',
  articleListLoading: false,
  articleLoading: false,
  error: undefined
};

const articles = (state = initialState, action) => {
  switch(action.type) {
    case 'START_ARTICLE_CONTENT_REQUEST':
      // enforce single request in pipeline at once?
      return Object.assign({}, state, {
        articleLoading: true
      });
    case 'END_ARTICLE_CONTENT_REQUEST':
      return Object.assign({}, state, {
        articleLoading: false
      });
    case 'SET_ARTICLE_DATA':
      return Object.assign({}, state, {
        articleContentById: Object.assign({},state.articleContentById,{
          [action.articleId]: action.data
        })
      });
    case 'START_ARTICLE_LIST_REQUEST':
      return Object.assign({}, state, {
        articleListLoading: true
      });
    case 'END_ARTICLE_LIST_REQUEST':
      return Object.assign({}, state, {
        articleListLoading: false
      });
    case 'SET_ARTICLE_LIST':
      return Object.assign({}, state, {
        articleList: action.data
      });
    case 'SET_ERROR':
      return Object.assign({}, state, {
        error: action.error
      })
    default:
      return state;
  }
}

export default articles;