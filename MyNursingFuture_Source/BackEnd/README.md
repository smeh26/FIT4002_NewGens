##MyNursingFuture Backend Architecture

###Setup

Requirements

SQL EXPRESS 2016

.NET FRAMEWORK 4.5.2

VISUAL STUDIO 2017

1- After installing the db run the script on MyNursingFuture\MyNursingFuture.DL\SqlScripts. This will create a new database called MyNursingFuture

2- Change the connection string on the web.config of each project to the new database created
```
<connectionStrings>
    <add name="MyNursingFutureConnection" connectionString="Data Source=YOURINSTANCE;Initial Catalog=MyNursingFutureDev;User ID=sa;Password=*******.;Integrated Security=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
```
3- The application on MyNursingFuture\MyNursingFuture.AdminUser could be use create a new administration user for the CMS. It just need to have the connection string changed.

4- Each web.config/app.config has it configurations that needs to be modified as required.


###Architecture
We have 7 projects in the solutions.

###MyNursingFuture.Api:
A Web Api 2 project used by the react application

###MyNursingFuture.Cms
A MVC5 project that is the CMS of the application

###MyNursingFuture.BL
A Bussiness Layer with all the entities and managers used across the application

###MyNursingFuture.DL
A Data Layer project that manage the db queries using Dapper.

###MyNursingFuture.Util
Library for common types used all over the solution. 

###MyNursingFuture.Test
A Test project used mainly as for Integration tests

###MyNursingFuture.AdminUser
An Console Application (Adminuser) used for generate new administrator users


#Result class
This class in the Util project and it is used all over the applications. It just a simple class with 3 properties. 

Entity that can be any object

Success that indicates the success or failure of the operation

Message that has the result message of the operation.


###Authentication
MyNursingFuture.BL.Manangers.CredentialsManager

We are not using owin authentication, we have a custom authentication for users and administrators.

We are using a credentials manager to generate hashed passwords and JSON Tokens.

Whenever a new user or administrator is created, also a unique salt for hashing the password is created.

In the database we are storing a Hashed Password with the hash for each user and administrator and that is what we use for validate the login.

In the credentials manager we also generate json tokens for each type of login and password recovery. The password recovery is not available to adminsitrators for security reasons.

In the CMS and the API we have filters called JWTAuthorize for validate the tokens.

The token on the CMS is stored on a cookie or Session.

The token on the Web Api is sent with each request that requires authorization.

Administrators created with the tool AdminUser Console App will be sealed and can not be modified in the cms.

###Dependency Injection

Both projects are using SimpleInjector for dependency injectiopn, on the cms readme the is examples of implementation of the DI and how to use it with automapper.

###EnumManager

The Enumeration Manager has public enums defined that are used all over both applications.


###Caching

We have a CacheManager that can be injected in the Web Api controllers, the types of caches are defined on the enumeration manager.

###Image Uploads

The image manager has the methods for uploading images, the are only used on the cms, and whenever an new image is uploaded, it is also copied into the folder defined in the web.config. This folder is on the api application and are the images that are loaded from the React Application.
```
<add key="UploadsWebAPI" value="C:\ImagesPath\" />
```

Make sure to change the security so it can modified by the CMS application

#Using the Dapper Connection Manager
The dapper connection manager has a variety of methods for each situation.

There is a class defined in the same file called QueryEntity, all these methods receive one or many QueryEntites as parameters.

Each QueryEntity has two properties, Entity and Query.

Whenever you are creating a QueryEntity it needs them both, if you don't need to map a object on the query just send an empty object.

```
var query = new QueryEntity();
query.Entity = new {};
query.Query = @"SELECT * FROM TABLE"
```

We are using Transaction Scope for every query, when the name says Unscoped it means that it has to be scoped from outside the method.

We are using the scope in case of any sql error occurred in will not affect the data base.

If the method is not generic it will not use dapper for mapping an object.
```
public Result ExecuteQueries(List<QueryEntity> queries)
```

```
public Result ExecuteQuery(QueryEntity query)
```

```
public Result ExecuteQuery<T>(QueryEntity query)
```

Every Insert method returns the ID of the last element inserted on the result 
```
public Result InsertQuery(QueryEntity query)
```

```
public Result InsertQueryUnScoped(QueryEntity query)
```

```
public Result ExecuteQueryUnScoped(QueryEntity query)
```
```
public Result ExecuteQueryUnScoped<T>(QueryEntity query)
```

###Logging

We have an static logger on the Util Project class, the logs path should be defined on the web.config or app.config

```
<add key="ErrorLogPath" value="C:\Logs\" />
```

###Email Manager

The email manager takes the configuration from the web.config
```
<add key="configuration.email.user" value="****@****.com" />
<add key="configuration.email.password" value="*****." />
<add key="configuration.email.smtpServer" value="smtp.****.com" />
<add key="configuration.email.from" value="*****@****.com" />
<add key="configuration.email.port" value="587" />
<add key="configuration.email.ssl" value="true" />
<add key="configuration.email.feedback" value="*****@****.com" />
<add key="configuration.email.contact" value="*****@****.com" />
```
It can also accept multiples email on the parameter if they are separated by ";".

###Report Generation
Whenever a report is generated on the site, we get the JSON on the Web Api and we send it to our nodejs application that is in charge of generate the report.

The config por the node server is on the web.config
```
<add key="report.url" value="http://localhost:8081/sareport" />
```


