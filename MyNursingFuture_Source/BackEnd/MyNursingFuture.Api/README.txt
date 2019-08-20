My Nursing Future API Documentation
------ 

#Summary :  

This package cover the API controller in the MVC setup. That serves the My Nursing Future Website.
The API calls are handled by the ASP.NET framework, 
Generally, it received the requests from the front end and t


The API in this package inherits directly from the native ASP .NET ApiController. 


#Structure: 

##App_Start
This folder contains the configuration of the ApiController. Modify with caution.



##Content
This is the plain text version of the HTML file intended to use for the front end. They are not ebing used at the moment.


##Controllers

Controllers are the heart of the APIs. They fetch, modify data from the back end and serve the front end. List of the current API are as follow.

api/Articles
api/Articles/{id}
api/Framework
api/Login
api/Login/{id}
api/Report
api/Report/anonymous
api/Report/saveanoncareerreport
api/report/download
api/Users
api/users/quiz/career/{complete}
api/users/quiz/career/save
api/users/quiz/selfassessment/{complete}
api/users/quiz/selfassessment/save
api/users/quiz/aboutyou/save
api/users/quizzes
api/users/quizzes/{id}
api/users/recover
api/users/recover/reset
api/users/edit
api/users/changepassword
api/Users/{id}



##Filters

This package provide the HTTP response when the backend is not working properly,
or when the user is not authorised.

ExceptionFilter  : Provide the HTTP response when the backend is not working properly. 
JwtAuthorized:  Authentication control, filter out requests that are not authenticated. 


##Global.asax
This is the application file, it set ups the instance of the managers and the controllers during execution time.


##Infrastructure
HTTP Client Manager

##Models
Template for the object received/send to the API


##MyNursingFuture.Api.csproj -> project file


##Properties -> Extra information such as copyright info


##README.txt

##Web.config

##packages.config
