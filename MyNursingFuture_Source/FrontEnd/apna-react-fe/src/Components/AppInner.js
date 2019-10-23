import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Route } from 'react-router'
import { ConnectedRouter } from 'react-router-redux'
import cookies from '../Misc/cookies';
import RegisterUser from './RegisterUser'
import RegisterEmployer from './RegisterEmployer'

import { fetchFrameworkData, 
  closeModal, 
  openModal, 
  fetchLogIn, 
  fetchRegister, 
  fetchCheckUserAuth, 
  selfAssessmentResultsToReportJSON, 
  fetchResetPassword,
  fetchRequestResetPassword,
  fetchSubmitArticleFeedback,
  fetchUserQuizzes,
  fetchCheckEmpAuth } from '../Actions'

// Top-level components
import Header from './Header';
import Footer from './Footer';
import Sidebar from './Sidebar';

// Pages
import Home from '../Pages/Home';
import KitchenSink from '../Pages/KitchenSink';
import ContactUs from '../Pages/ContactUs';
import Sectors from '../Pages/Sectors';
import Section from '../Pages/Section';
import SectorPage from '../Pages/SectorPage';
import RolePage from '../Pages/RolePage';
import Articles from '../Pages/Articles';
import ArticlePage from '../Pages/ArticlePage';
import Why from '../Pages/Why';
import Explore from '../Pages/Explore';
import DomainsEN from '../Pages/DomainsEN';
import DomainsRN from '../Pages/DomainsRN';
import DomainPage from '../Pages/DomainPage';
import AspectPage from '../Pages/AspectPage';
import SelfAssessmentIntro from '../Pages/SelfAssessmentIntro';
import SelfAssessmentQuiz from '../Components/Quiz/SelfAssessmentQuiz';
import SelfAssessmentResults from '../Components/Quiz/SelfAssessmentResults';
import CareerQuizIntro from '../Pages/CareerQuizIntro';
import CareerPathwaysQuiz from '../Components/Quiz/CareerPathwaysQuiz';
import CareerPathwaysResults from '../Components/Quiz/CareerPathwaysResults';
import SelfAssessmentHub from '../Components/Quiz/SelfAssessmentHub';
import UserProfile from '../Pages/UserProfile'
import UserQuizzes from '../Pages/UserQuizzes'
import UserHub from '../Pages/UserHub'
import ReasonsArticlePage from '../Pages/ReasonsArticlePage';


class AppInner extends Component {  
  constructor(props){
    super(props);
    this.handleModalCloseClick = this.handleModalCloseClick.bind(this);
    this.handleChangeInput = this.handleChangeInput.bind(this);
    this.handleLogIn = this.handleLogIn.bind(this);
    this.handleRegister = this.handleRegister.bind(this);
    this.anonEmailInput = this.anonEmailInput.bind(this);
    this.handleSendEmailReport = this.handleSendEmailReport.bind(this);
    this.handleResetPassword = this.handleResetPassword.bind(this);
    this.handleRequestResetPassword = this.handleRequestResetPassword.bind(this);
    this.handleSubmitArticleFeedback = this.handleSubmitArticleFeedback.bind(this);
    this.state = {
      inputs: {
        articleFeedback: '',
        email: '',
        name: '',
        password: '',
        address: '',
        phone: '',
        dob: '',
        pp: false,
        newPassword: ''
      },
      emailModalSent: false,
      resetToken: '',
      recoverPasswordUpdated: false,
      anonEmail: null
    };
  }
  
  handleModalCloseClick(){
    this.props.closeModal();
    this.setState({emailModalSent: false});
  }
  
  handleChangeInput(event){
    const name = event.target.name;
    let inputs = this.state.inputs;
    inputs[name] = event.target.value
    this.setState({inputs: inputs});
  }

  anonEmailInput(event){
    this.setState({anonEmail: event.target.value})
  }
  
  handleSubmitArticleFeedback = function(){
    let pathSegments = this.props.passHistory.location.pathname.split('/');
    if (pathSegments.includes('articles')){
      let articleId = pathSegments[pathSegments.length-1];
      let articleTitle = this.props.articleContentById[articleId].title
      if (articleId && articleTitle){
        this.props.submitArticleFeedback(articleId,articleTitle,this.state.inputs.articleFeedback,this.props.modal.showingId.indexOf('Yes') >= 0 ? true : false);
      }
    } else if (pathSegments.includes('reasons')){
      this.props.submitArticleFeedback(null,"Reasons",this.state.inputs.articleFeedback,this.props.modal.showingId.indexOf('Yes') >= 0 ? true : false);
    }

  }
  
  handleEmpLogIn = () => {
    this.props.empLogIn(this.state.inputs.email,this.state.inputs.password);
    
  }

  handleLogIn() {
    this.props.logIn(this.state.inputs.email,this.state.inputs.password);
  }
  
  handleRegister(){
    this.props.register(this.state.inputs.name, this.state.inputs.email, this.state.inputs.password);
  }
  
  handleSendEmailReport(){
    let p = window.location.href.split('/');
    //console.log('handleSendEmailReport()', this.props, this.state);
    if (this.state.anonEmail) {
      this.props.emailReportAnon(null,this.props.user,"Anonymous User",this.state.anonEmail,p[p.length-1])
      this.setState({emailModalSent: true});
    } else {
      this.props.emailReport(null,this.props.user,this.state.inputs.name,this.state.inputs.email,p[p.length-1])
      this.setState({emailModalSent: true});
    }
  }
  
  handleResetPassword(){
    this.props.submitResetPassword(this.state.resetToken, this.state.inputs.newPassword);
  }
  
  handleRequestResetPassword(e){
    e.preventDefault();
    this.props.submitRequestResetPassword(this.state.inputs.email);
  }
  
  componentWillMount() {
    let resetToken = this.props.onMount();
    if (resetToken){
      this.setState({resetToken: resetToken});
      this.props.resetPassword();
    }
    document.title = "My Nursing Future";
  }
  
  render() {
    const history = this.props.passHistory;
    const modalContainerClass = 'container-fluid modal-container' + (this.props.modal.showing ? '' : ' normal-bg' );

    //console.log('rendering of modals', this.props.user.name, this.props.user.email);
    console.log(this.props.userMessage);
    return (
      <ConnectedRouter history={history}>
        <div className="App">
          { !this.props.modal.showing &&
          <div>
            <Header />
            <Sidebar />
          </div>
          }
          { this.props.modal.showing && this.props.modal.showingId == 'login' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                    {this.props.userMessage &&
                    <p>{this.props.userMessage}</p>
                    }
                    {this.props.loggedIn && 
                    <div className="row">
                      <div className="col-8 offset-2">
                        <h2>Hello {this.props.user.name}</h2>
                        <p>You are now logged in. Click the button below to continue.</p>
                        <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                      </div>
                    </div>
                    }
                    {!this.props.loggedIn && !this.props.userLoading &&
                    <div className="row">
                        <div className="col-8 offset-2">
                        
                        <h1>Sign in</h1>
                        <p>Sign in to access your self-assessments and manage your account settings.  If you're an existing APNA member, please use your existing login details.</p>
                        <div className="input-block">
                          <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                          <input type="password" value={this.state.inputs.password} name="password" placeholder="Password" onChange={this.handleChangeInput} />
                        </div>
                        
                        {this.props.userError &&
                        <p>{this.props.userError}</p>
                        }
                        <button className="btn" onClick={this.handleLogIn} disabled={!this.state.inputs.email || !this.state.inputs.password}>Log in</button>
                        <div><a href="/user/register">Register</a></div>
                        <a href="" onClick={this.handleRequestResetPassword}>Reset password</a>
                        <p>
                          If you are using your APNA member login details and need to reset your password click <a href="https://www.apna.asn.au/account/getanewpassword" target="_blank">here</a>.
                        </p>
                      </div>
                    </div>
                    }

                    {this.props.userLoading && <div className="loading-wrapper"><img src="/img/loading.gif" /></div>}

                </div>
              </div>
            </div>
            }

  { this.props.modal.showing && this.props.modal.showingId == 'employerlogin' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                    {this.props.userMessage &&
                    <p>{this.props.userMessage}</p>
                    }
                    {this.props.loggedIn && 
                    <div className="row">
                      <div className="col-8 offset-2">
                        <h2> {this.props.user.employerName}</h2>
                        <p>You are now logged in. Click the button below to continue.</p>
                        <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                      </div>
                    </div>
                    }
                    {!this.props.loggedIn && !this.props.userLoading &&
                    <div className="row">
                        <div className="col-8 offset-2">
                        
                        <h1>Sign in</h1>
                        <p>Sign in to create job listings and match with the perfect nurse for your needs!</p>
                        <div className="input-block">
                          <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                          <input type="password" value={this.state.inputs.password} name="password" placeholder="Password" onChange={this.handleChangeInput} />
                        </div>
                        
                        {this.props.userError &&
                        <p>{this.props.userError}</p>
                        }
                        <button className="btn" onClick={this.handleEmpLogIn} disabled={!this.state.inputs.email || !this.state.inputs.password}>Log in</button>
                        <div><a href="/employer/register">Register</a></div>
                        {/* <a href="" onClick={this.handleRequestResetPassword}>Reset password</a>
                        <p>
                          If you are using your APNA member login details and need to reset your password click <a href="https://www.apna.asn.au/account/getanewpassword" target="_blank">here</a>.
                        </p> */}
                      </div>
                    </div>
                    }

                    {this.props.userLoading && <div className="loading-wrapper"><img src="/img/loading.gif" /></div>}

                </div>
              </div>
            </div>
            } 
          
            { this.props.modal.showing && this.props.modal.showingId == 'emailreport' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                  {!this.state.emailModalSent && 
                    <div className="col-8 offset-2">
                      <h1>Email a copy of your report</h1>
                      {this.props.loggedIn && 
                      <div className="input-block">
                        <dl className="list-unstyled">
                          <dt>Your name:</dt>
                          <dd><span className="form-control">{this.props.user.name}</span></dd>
                          <dt>Email address:</dt>
                          <dd><span className="form-control">{this.props.user.email}</span></dd>
                        </dl>
                      </div>
                      }
                      {!this.props.loggedIn &&
                        <div className="input-block">
                          <form>
                            <label>
                              Please enter an email address below so we can email you a copy of your report
                              <input type="email" name="name" onChange={this.anonEmailInput} placeholder="Email Address"/>
                            </label>
                          </form>
                        </div>
                      }
                      <button className="btn" onClick={this.handleSendEmailReport}>Email report</button>
                    </div>
                  }
                  {this.state.emailModalSent &&
                    <div className="col-8 offset-2">
                      <h1>Your report has been sent!</h1>
                      <p>Would you like to save a copy of your report to access again at a later date?</p>
                      <p>By saving your report, you will be able to keep track of your actions and monitor your career progress over time.</p>
                      {this.props.loggedIn &&
                        <button className="btn" onClick={()=>{this.props.modalToModal('signInToSave')}}>Yes</button>
                      }
                      {!this.props.loggedIn && 
                        <button className="btn" onClick={()=>{this.props.modalToModal('doYouHaveAProfile')}}>Yes</button>
                      }
                      <button className="btn" onClick={this.handleModalCloseClick}>No</button>
                    </div>
                  }
                  </div>
                </div>
              </div>
            </div>
            }
          
            { this.props.modal.showing && this.props.modal.showingId == 'doYouHaveAProfile' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Do you already have a profile with us? If you're an existing APNA member, please use your existing login details.</h1>
                      <button className="btn" onClick={()=>{this.props.modalToModal('signInToSave')}}>Yes</button>
                      <button className="btn" onClick={()=>{this.props.modalToModal('registerToSave')}}>No</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }

            { this.props.modal.showing && this.props.modal.showingId == 'signInToSave' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    {this.props.loggedIn && 
                    <div className="col-8 offset-2">
                      <h1>Your quiz has been saved!</h1>
                      <p>You can access your results at any time by signing in to your profile.  If you're an existing APNA member, please use your existing login details</p>
                      <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                    </div>
                    }
                    {!this.props.loggedIn && 
                    <div className="col-8 offset-2">
                      <h1>Sign in to save your progress</h1>
                      <div className="input-block">
                        <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                        <input type="password" value={this.state.inputs.password} name="password" placeholder="Password" onChange={this.handleChangeInput} />
                      </div>
                      {this.props.userError &&
                      <p>{this.props.userError}</p>
                      }
                      <button className="btn" onClick={this.handleLogIn}>Log in</button>
                      <a href="" onClick={this.handleRequestResetPassword}>Reset password</a>
                      <p>
                        If you are using your APNA member login details and need to reset your password click <a href="https://www.apna.asn.au/account/getanewpassword" target="_blank">here</a>.
                      </p>
                    </div>
                    }
                  </div>
                </div>
              </div>
            </div>
            }
            { this.props.modal.showing && this.props.modal.showingId == 'registerToSave' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    {this.props.loggedIn && 
                    <div className="col-8 offset-2">
                      <h1>Your quiz has been saved!</h1>
                      <p>You will receive an email shortly with your account details.</p>
                      <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                    </div>
                    }
                    {!this.props.loggedIn && 
                    <div className="col-8 offset-2">
                      <h1>Create a profile to save your report</h1>

                      <div className="input-block">
                        <input type="text" value={this.state.inputs.name} name="name" placeholder="Name" onChange={this.handleChangeInput} />
                        <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                        <input type="password" value={this.state.inputs.password} name="password" placeholder="Password" onChange={this.handleChangeInput} />
                      </div>
                      {this.props.userError &&
                      <p>{this.props.userError}</p>
                      }
                      <button className="btn" onClick={this.handleRegister}>Create profile</button>
                    </div>
                    }
                  </div>
                </div>
              </div>
            </div>
            }
            
            { this.props.modal.showing && this.props.modal.showingId == 'newsletterSignup' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Newsletter signup</h1>
                      <p>Enter your details here to sign up for the APNA newsletter.</p>
                      <div className="input-block">
                        <input type="text" value={this.state.inputs.name} name="name" placeholder="Your Name" onChange={this.handleChangeInput} />
                        <input type="text" value={this.state.inputs.phone} name="phone" placeholder="Phone" onChange={this.handleChangeInput} />
                        <input type="text" value={this.state.inputs.email} name="email" placeholder="Email address" onChange={this.handleChangeInput} />
                        <input type="text" value={this.state.inputs.address} name="address" placeholder="Address" onChange={this.handleChangeInput} />
                        <label for="pp" className="check-input">Agree to the APNA Privacy Policy <input type="checkbox" checked={this.state.inputs.pp} name="pp" onChange={this.handleChangeInput} /></label>
                        <button className="btn" onClick={this.props.thanksModal}>Submit</button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }
            
            { this.props.modal.showing && this.props.modal.showingId == 'thanks' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Thank you!</h1>
                      <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }
          
            { this.props.modal.showing && this.props.modal.showingId == 'resetSent' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Password reset requested.</h1>
                      <p>Please check your email inbox for instructions.</p>
                      <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }
          
            { this.props.modal.showing && this.props.modal.showingId == 'resetComplete' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Password reset complete</h1>
                      <p>You may now log in with your new password.</p>
                      <button className="btn" onClick={this.handleModalCloseClick}>Close</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }
            
            { this.props.modal.showing && this.props.modal.showingId == 'glossary' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h2 className="text-left">{this.props.glossary && this.props.currentGlossaryItem && 
                       <i>{this.props.currentGlossaryItem.name}</i> 
                      }</h2>
                      <hr className="full-hr" />
                      {this.props.glossary && this.props.currentGlossaryItem && 
                        <div className="text-left" dangerouslySetInnerHTML={{__html: this.props.currentGlossaryItem.text}}></div>
                      }

                      <button className="btn squishybottom" onClick={this.handleModalCloseClick}>Close</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }
            
            { this.props.modal.showing && this.props.modal.showingId.indexOf('articleFeedback') >= 0 &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Please tell us how we can improve this advice.</h1>
                      <div className="input-block">
                        <textarea value={this.state.inputs.articleFeedback} name="articleFeedback" placeholder="Your feedback" onChange={this.handleChangeInput} />
                      </div>
                      <button className="btn" onClick={this.handleSubmitArticleFeedback}>Submit</button>
                      <p>or call us to give your feedback:</p>
                      <h2>1300 303 184</h2>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            }
          
          { this.props.modal.showing && this.props.modal.showingId == 'reset' &&
            <div className={modalContainerClass}>
              <div className="modal-inner">
                <span className="oi modal-close" data-glyph="x" onClick={this.handleModalCloseClick}></span>
                <div className="modal">
                  <div className="row">
                    <div className="col-8 offset-2">
                      <h1>Reset password</h1>
                      <div className="input-block">
                        <input type="text" value={this.state.inputs.newPassword} name="newPassword" placeholder="New Password" onChange={this.handleChangeInput} />
                      </div>
                      {this.props.userError &&
                      <p>{this.props.userError}</p>
                      }
                      <button className="btn" onClick={this.handleResetPassword}>Reset</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          }
            
          { !this.props.modal.showing &&
          <div className="container-fluid main-content-area">
            <Route exact path="/" component={Home} />
            
            <Route exact path="/sections/:id" component={Section} />
            
            <Route path="/kitchensink" component={KitchenSink} />
            <Route path="/contact" component={ContactUs} />
            <Route exact path="/sectors" component={Sectors} />
            <Route path="/sectors/:id" component={SectorPage} />
            <Route path="/roles/:id" component={RolePage} />
            <Route exact path="/articles" component={Articles} />
            <Route exact path="/reasons" component={ReasonsArticlePage} />
            <Route path="/articles/:id" component={ArticlePage} />
            <Route path="/whyprimaryhealthcare" component={Why} />
            <Route exact path="/explore" component={Explore} />
            <Route exact path="/explore/rn" component={DomainsRN} />
            <Route exact path="/explore/en" component={DomainsEN} />
            <Route exact path="/explore/:framework/domain/:domain/:aspect" component={AspectPage} />
            <Route exact path="/explore/:framework/domain/:id" component={DomainPage} />
            
            <Route exact path="/careerquizintro" component={CareerQuizIntro} />
            <Route exact path="/selfassessmentintro" component={SelfAssessmentIntro} />
            <Route exact path="/quiz/careerPathways" component={CareerPathwaysQuiz} />
            <Route exact path="/quiz/selfAssessment/:framework" component={SelfAssessmentHub} />
            <Route exact path="/quiz/selfAssessment/:framework/:domainFilter" component={SelfAssessmentQuiz} />

            <Route exact path="/results/selfAssessment/:id" component={SelfAssessmentResults} />
            <Route exact path="/results/careerPathways/:id" component={CareerPathwaysResults} />
            <Route exact path="/results/selfAssessment" component={SelfAssessmentResults} />
            <Route exact path="/results/careerPathways" component={CareerPathwaysResults} />
            
            <Route exact path="/user/profile" component={UserProfile} />
            <Route exact path="/user/quizzes/:id" component={UserQuizzes} />
            
            <Route exact path="/user" component={UserHub} />
            <Route exact path="/user/register" component={RegisterUser} />
            <Route exact path="/employer/register" component={RegisterEmployer} />

            <Footer />
            </div>
        }
        </div>
      </ConnectedRouter>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    modal: state.app.headerFooterMenus.modal,
    user: state.app.user.user,
    loggedIn: state.app.user.loggedIn,
    userLoading: state.app.user.isLoading,
    userError: state.app.user.error,
    userMessage: state.app.user.message,
    glossary: state.app.framework.glossary,
    currentGlossaryItem: state.app.framework.currentGlossaryItem,
    user: state.app.user.user,
    articleContentById: state.app.articles.articleContentById,
  }
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    onMount: function() {
      dispatch(fetchFrameworkData());
      dispatch(fetchUserQuizzes());

      if (cookies.getItem('token')){
        dispatch(fetchCheckUserAuth(cookies.getItem('token')));
      }
      if (props.passHistory.location.search){
        if (props.passHistory.location.search.indexOf('resetToken') >= 0){
          let resetToken = props.passHistory.location.search.split('&').find((a) => { return a.indexOf('resetToken') >= 0});
          resetToken = resetToken.split('=')[1];
          return resetToken;
        }
      }
    },
    closeModal: function(){
      dispatch(closeModal());
    },
    thanksModal: function(){
      dispatch(openModal('thanks',window.pageYOffset || document.documentElement.scrollTop));
    },
    modalToModal: function(modal){
      dispatch(closeModal());
      dispatch(openModal(modal,window.pageYOffset || document.documentElement.scrollTop));
    },
    resetPassword: function(){
      dispatch(openModal('reset'));
    },
    submitResetPassword: function(token,password){
      dispatch(fetchResetPassword(token,password));
    },
    submitRequestResetPassword: function(email){
      dispatch(fetchRequestResetPassword(email));
    },
    submitArticleFeedback: function(articleId,title,feedback,positive){
      dispatch(fetchSubmitArticleFeedback(articleId,title,feedback,positive));
    },
    empLogIn: function(e,p){
      dispatch(fetchCheckEmpAuth(e,p));
    },



    logIn: function(e,p){
      dispatch(fetchLogIn(e,p));
    },
    register: function(n,e,p){
      dispatch(fetchRegister(n,e,p));
    },
    emailReport: function(quiz,user,name,email,quizId){
      dispatch(selfAssessmentResultsToReportJSON(quiz,user,name,email,quizId));
    },
    emailReportAnon: function(quiz,user,name,email,quizId){
      dispatch(selfAssessmentResultsToReportJSON(quiz,user,name,email,quizId));
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(AppInner);
