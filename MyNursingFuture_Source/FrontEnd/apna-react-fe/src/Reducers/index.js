import { combineReducers } from 'redux';
import headerFooterMenus from './Presentation';
import contact from './Contact';
import content from './PageContent'
import framework from './Framework'
import articles from './Articles'
import quiz from './Quiz';
import user from './User';

const reducers = combineReducers({ headerFooterMenus, contact, content, framework, articles, quiz, user});

export default reducers;