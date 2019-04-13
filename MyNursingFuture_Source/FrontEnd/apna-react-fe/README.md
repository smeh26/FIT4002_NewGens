# APNA My Nursing Future Front-end React app

### Run locally

cd into apna-react-fe dir 

`cd apna-my-nursing-future-front-end/apna-react-fe`

install node modules 

`npm install`

start react-scripts

`npm run start`

open app in brower (default port 3000)

`http://localhost:3000/`

This project was bootstrapped with [Create React App](https://github.com/facebookincubator/create-react-app).

## React config

Using [react-redux](https://github.com/reactjs/react-redux) and [react-router-redux](https://github.com/ReactTraining/react-router/tree/master/packages/react-router-redux)

Yet to `npm eject` from the `create-react-app` project. Will be done so that all config is transparent.

## Styling

Custom LESS/CSS using `bootstrap-reboot.css` and `bootstrap-grid.css` 
both unceremoniously pulled from `bootstrap-4.0.0-alpha.6-dist`.

`open-iconic` is used for web app icons, also unceremoniously pulled from `open-iconic v1.1.1` using only `open-iconic.less` and the font files found in `/apna-react-fe/src/styles/fonts/open-iconic.*`

All imported into `/apna-react-fe/src/styles/app.less`. Oh my god it's ugly will be refactored. 
EDIT Will it? 
EDIT Maybe not...

## Tests

`create-react-app` comes with **Jest** configured so this will be used to write tests.
PS I didn't really write any tests.


## General Structure

Using react-router-redux

`/src/App.js` and `/src/Components/AppInner.js` provide the structure

Everything is wrapped in a `ConnectedRouter` for the react-redux-router to work, and in conditionals for the weird modal display approach.

Refer to `/src/Actions` and `/src/Reducers` for redux store stuff.

`/src/Pages` contains higher level wrapping of pages - mostly defining what 'name' the section is from the CMS but in some cases adding special page-specific content outside of the CMS flow. For example `/src/Pages/ArticlePage.js` contains sharing components and a contact component.


`<Header />` and `<Sidebar />` go outside of general content area to be app-level not page-level.

`<Route />` elements use page components such as `<Home />` found at `/src/Pages/Home.js`. This is a basic component that wraps `<AppPage />` providing the necessary props. Putting props through on a `<Route />` component was ugly and dumb so I did this instead, and it works better for flexibility also. 

`<AppPage />` uses `endpoint` prop to query the api for the content. Api is queried on component mount. It's ended up that most of the content is grabbed on one honking big call from the api `/api/Framework/nocache`. Then `endpoint` is used to find a section with the `name` specified by `endpoint`, otherwise it uses the id in the route to match a section.
`<AppPage />` displays loading element if the store has loading flag, content with `<PageContent />` component otherwise.

`<PageContent />` displays the content. Takes prop `content` which is expected to be a json array of objects. Found in `/src/Components/PageContent.js`. It checks the type of each content item object and then outputs one of the `/src/Components/ContentItems` components.

API was hardcoded using `/src/Actions/RoboRamiro.js` until the api was ready. The data reflected is out of date, but was useful for reference and may continue to be.

Look at `<PageContent />` to get an idea of how content is parsed and displayed.

# CMS data

refer to `fetchFrameworkData()` in `/src/Actions/index.js` for the big honker cms data call.

Just incase you need to fiddle with this guy without internet access or cms access or whatever I've put `/src/Actions/cmsData-07082017.json` in which is copied data from a cms request. it should work fine - use the dummyfetch stuff in RoboRamiro/Actions.js if necessary.

Here's a top level look:
```json
{
  "entity": {
    "sections": [],
    "domains": [],
    "sectors": [],
    "aspects": [],
    "actions": [],
    "roles": [],
    "postCards": [],
    "definitions": [],
    "questions": [],
    "menus": [],
    "scoring": [],
    "reasons": [],
  }
}
```
- **sections** are each 'page' type thing from the cms, identified by sectionId or name and contains an array of content objects.
- **domains** is an array of the deep big boy data element - important for self assessment and many of the pages.
- **aspects** are the items each self assessment question are against and make up the content of the domains.
- **sectors** is an array of the career sectors - important for the career pathways quiz.
- **actions** is an array of actions which are not entirely unique but have an aspect+level relationship.
- **roles** is an array of roles used for the roles pages.
- **postCards** is an array of postcards (text / image) little quotes or whatever that show up in the self assessment quiz and on loading quiz results.
- **definitions** is an array of the terms for the glossary. functionality defined in `/src/Components/ContentItems/TextWithGlossary.js`.
- **questions** all questions for all quizzes - processed in `fetchFrameworkData()` for use in the application.
- **menus** defines the footer and sidebar menu.
- **scoring** defines the scoring object for the career pathways quiz - set in CMS. It's a set of ideal answers for each sector to compare against.
- **reasons** is the special #(11) reasons why article which has a carousel that shows up in a few spots.

Have a squizzy at the database in the .NET application to kinda understand the relationships better.

It's kinda like this though:
Domains contain Aspects
Aspects have questions for self assessment
Aspects have actions for each level of practice
Three levels of practice (foundation, intermediate, advanced)

Sectors have (maybe) ideal answers per career pathway question.



# Quiz

`/src/Components/Quiz` contains the quiz-specific components. For the self assessment it starts at the `/src/Components/Quiz/SelfAssessmentHub.js`. This arranges the questions by domain so that they can be displayed and progress shown per domain + about.

The career quiz starts on `/src/Components/Quiz/CareerPathwaysQuiz`. The quiz takes the questions from the CMS.

The about you bit is another set of questions that map to user data, and when saved update the user.

## Questions

Questions have a quiz type (assessment, pathways, about). About is user data basically (defiend by the `fieldName` prop).  Questions can also have requirements (`requirements`) which is an array of objects `{ questionId: '', value: ''}`. For the progress each time the quiz is loaded it must be parsed and looped over to see which questions should be counted for the progress thing. There are two special questions: questionId 77 and questionId 34. 77 is the survey question which appears after the self assessment is done. 34 is the preferred patient type question which at the last minute was decided it should only show for the career pathways quiz.

There are a few types of questions you can just look at the file `/src/Components/Quiz/Question.js`

## Quiz Saving

The quiz saving logic is not ideal. I did it at 2am in a rush. But it pretty much works. Basically:
- If you're logged in & complete a quiz -> save it immediately, redirect user to the specific results page
- If you're not logged in & complete -> prompt to register/login -> logging in calls `fetchSaveInProgressQuizzes` (`/src/Actions/index.js`)
- It sucks and I hate it and It Makes Me Sad But I Ran Out Of Time.

The save data is thrown at the API as a big filthy dirty nasty bad bitch of a json object
```json
{
  "answers": {
    ... actual gosh darn answers state from app
  },
  "results": {
    ... actual gosh darn results object from app
  }
}
```
That's also what you get back from the API in the user `quizzes` var.

# User data

Auth is handled by storing the token in a cookie. Yeah it's not great but it kinda works.

These bits are defined by about you questions:
```json
        "nurseType": "1",
        "area": '',
        "state": "eg",
        "activeWorking": "2",
        "age": "1901",
        "country": "ge",
        "suburb": "eg",
        "postalCode": "eg",
        "patients": "",
        "patientsTitle": "Person",
        "qualification": "2",
        "setting": '',
```

`apnaUser` is true if the integration between the AMS api and the back-end exists, and thus the AMS api handles authentication (and we can't change user details)

`quizzes` is the saved results from quizzes. It can be long, I wrote code to load the most recent uncompleted quiz for a user but I don't know if that's what we want, so I commented it out. but there it is! line 778 of `/src/Actions/index.js`

User info can be edited on the `/src/Pages/UserProfile.js` page, user quizzes accessed on `/src/Pages/Userquizzes.js` page. These all show which parts of the store and which actions do the stuff you want.

# Report generator

Go look at the README.md file in the `self-assessment-report-generator` directory.
`selfAssessmentResultsToReportJSON` in `apna-react-fe/src/Actions/index.js` generates the json for the request to `/api/report`

# ContentItems

See `/src/Components/ContentItems/` for all the content item components. Look at `/src/Componets/PageContent.js` for how each is used.

`/src/Components/ContentItems/SectorLinkList.js` has a dumb hardcoded ordering that was meant to be a CMS feature but didnt make it in. So it's ordered by the object in the state.

# Deployment

1. confirm right values in config.js
2. npm run build  (by default this automatically builds a web.config configured for production.  See notes below to prevent any configuration problems when deploying to UAT and/or Prod environments)
3. **check that the web.config file is current, compare with the one on the server**
4. copy build dir to root dir (which is `C:\inetpub\wwwroot\WEB` as of writing on the prod server)
5. make sure you have appropriate url rewrites - public folder has a working web.config for iis (again, see notes below)

NOTE:  When deploying to UAT and/or Prod, first make a backup, then paste in the latest built code. 
    *  Copy the existing web root folder serving the front end code and you'll find on C:\ there are backups or a backup folder that contains sub folders named with today's date (DDMMYYYY). Create a new folder accordingly and paste in the current deployed and running version of the website.
    *  Next copy the contents of the \Build\ folder into the IIS web instance of UAT and/or Prod virtual machines.  Do not replace or overwrite the web.config's as they are pre-configured accordingly for Prod and likewise the web.config on UAT is also configured accordingly for that environment.

# Azure Environments

mnf-dev (13.73.105.110) -- Contains DB and CMS which can be accessed via http://13.73.105.110/cms/ (the CMS housed on this Dev virtual machine also serves as the DB and CMS system for your local dev working copy and also acts as the DB and CMS for the UAT front end)

mnf-uat (52.255.54.150) -- Front End for MyNursingFuture as Test/UAT environment.  Hooks in and utilizes the same DB and CMS system from the 'mnf-dev' virtual machine as stated by the comments attached to 'mnf-dev'.  This box simply serves front end code for testing.

mnf-prod (52.255.55.47) -- Contains DB, CMS and Front End for MyNursingFuture and is self contained within this single environment.  Accessing the front end is www.mynursingfuture.com.au.  Accessing the CMS is www.mynursingfuture.com.au/cms.


# Melbourne IT environment
180.235.131.104 -- Contains DB, CMS and Front End for MyNursingFuture and is self contained within this single environment.  Accessing the front end is www.mynursingfuture.com.au.  Accessing the CMS is www.mynursingfuture.com.au/cms.