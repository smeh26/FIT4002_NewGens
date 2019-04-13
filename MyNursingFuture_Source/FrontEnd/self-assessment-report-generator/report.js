const fs = require('fs');
const config = require('./config.js');

var Promise = require("bluebird");
const sql = require('mssql/msnodesqlv8');
const json2csv = require('json2csv');
sql.Promise = Promise;
var express = require('express');
var bodyParser = require('body-parser');

var app = express();
app.set('views', './views');
app.set('view engine', 'pug');

// create application/x-www-form-urlencoded parser
var urlencodedParser = bodyParser.urlencoded({ extended: false })


app.get('/mnfreport', function (req, res) {
  res.sendfile('./views/reportlogin.html');
});

app.post('/mnfreport', urlencodedParser, function (req, res){
  let u = 'mnf_admin';
  let p = "gh35ywhe5T$e4w";
  
  if (req.body.u != u || req.body.p != p){
    res.status(401);
    res.send('unauthorized');
  }
  
  let type = req.body.type ? req.body.type : 'all'; // all / selfassessment
  
  const mssqlConfig = {
    user: 'sa',
    password: 'aB153759.',
    server: 'localhost\\SQLEXPRESS2016',
    database: 'MyNursingFuture',
    connectionTimeout: 1000,
    options: {
        encrypt: true
    }
  }
  
  
  var output = [];
  // can get questions, aspects, domains data in one
  // can get userquizzes and users in one
  // actions and answers depend on json data so separate requests to grab those tables and use them in the processing loop.
  
  // this libraries promises example was like this - using promises synchronously - need to come back and make it not dumb.
  sql.connect(mssqlConfig).then(pool => {
    var userQuizzes, answers, actions;
    var questions = [];
    return pool.request()
      .query('select Questions.Text, Questions.QuestionId, Aspects.Title as AspectTitle, Domains.Name as DomainTitle, Domains.DomainId as DomainId, Aspects.AspectId as AspectId, UserDataQuestions.FieldName as FieldName from Questions left outer join Aspects on Questions.AspectId = Aspects.AspectId left outer join Domains on Aspects.DomainId = Domains.DomainId left outer join UserDataQuestions on Questions.QuestionId = UserDataQuestions.QuestionId'
    ).then(function(q){
      questions = q.recordset;
      return pool.request()
      .query('select * from Answers where Active = 1')
    }).then(function(responseAnswers){
      answers = responseAnswers.recordset;
      return pool.request()
      .query('select * from Actions where Active = 1')
    }).then(function(responseActions){
      actions = responseActions.recordset;
      return pool.request()
      .query('select * from UsersQuizzes inner join Users on UsersQuizzes.UserId = Users.UserId where UsersQuizzes.Completed = 1')
    }).then(function(uq){
      userQuizzes = uq.recordset;
      pool.close();
      sql.close();
      
      for (var i in userQuizzes){
        let userQuiz = userQuizzes[i];
        try{
          var userData = {};
          for (let _q in questions){
            
            if (questions[_q].FieldName){
              if (userQuiz[questions[_q].FieldName]){
                switch(questions[_q].FieldName){
                  case 'Patients':
                    try{
                      var _paValue = JSON.parse(userQuiz[questions[_q].FieldName]);
                      for (var _pa in _paValue){
                        var _qa = answers.find(function(_a){ return _a.QuestionId == questions[_q].QuestionId && _a.Value == _paValue[_pa]});
                        if (userData[questions[_q].FieldName] == undefined){userData[questions[_q].FieldName] = ';';}
                        userData[questions[_q].FieldName] += _qa.Text + ";";
                      }
                    } catch(e){}
                    break;
                  case 'NurseType':
                  case 'ActiveWorking':
                  case 'Area':
                  case 'Qualification':
                  case 'Setting':
                    var _qa = null;
                    _qa = answers.find(function(_a){ return _a.QuestionId == questions[_q].QuestionId && _a.Value == userQuiz[questions[_q].FieldName]});
                    
                    if (_qa){
                      userData[questions[_q].FieldName] = _qa.Text;
                    }
                    
                    break;
                  default:
                    break;
                }
              }
            }
          }
          
          if (type == 'all'){
            var fields = ['name', 'email', 'completedTime','nurseType','activeWorking','nursingArea','preferredPatients','patientsTitle','country','suburb','postcode','state','yearOfBirth', 'quizType', 'questionAspect', 'questionDomain', 'questionText', 'answerValue', 'answerText'];            
            //console.log("gonna json parse this:",userQuiz.Results);
            let userResults = JSON.parse(userQuiz.Results);
            Object.keys(userResults.answers).forEach(function(key) {
              var val = userResults.answers[key];
              var tq = questions.find(function(_findq){
                return _findq.QuestionId == key
              });
              
              var ta = answers.find(function(_a){
                return _a.Value == val
              });
              
              let ir = {
                'name': userQuiz.Name,
                'email': userQuiz.Email,
                'completedTime': userQuiz.DateVal,
                'nurseType': userData.NurseType,
                'activeWorking': userData.ActiveWorking,
                'nursingArea': userData.Area,
                'preferredPatients': userData.Patients,
                'patientsTitle': userQuiz.PatientsTitle,
                'country': userQuiz.Country,
                'suburb': userQuiz.Suburb,
                'postcode': userQuiz.PostalCode,
                'state': userQuiz.State,
                'yearOfBirth': userQuiz.Age,  
                'quizType': userQuiz.Type,
                'questionAspect': tq ? tq.AspectTitle : '',
                'questionDomain': tq ? tq.DomainTitle : '',
                'questionText': tq ? tq.Text : '',
                'answerValue': val,
                'answerText': ta ? ta.Text : ''
              };
              output.push(ir);
            });
          } else {
            var fields = ['name', 'email', 'completedTime','nurseType','activeWorking','nursingArea','preferredPatients','patientsTitle','country','suburb','postcode','state','yearOfBirth', 'domainName', 'domainLevel', 'actionsToGrow'];
            //  'domainName', 'domainLevel', 'strongAspectName','actionsToGrow'
            
            let userResults = JSON.parse(userQuiz.Results);
            
            Object.keys(userResults.results.score).forEach(function(key) {
              var val = userResults.results.score[key];
              var tq = questions.find(function(_findq){
                return +_findq.DomainId == +key
              });
              
              var actionsToGrowAsStrings = ';';
              
              for( var actionId of userResults.results.actions[key]){
                let _action = actions.find(function(_atga){
                  return _atga.ActionId == actionId;
                });
                actionsToGrowAsStrings += _action.Title + "(" + _action.Type + ");";
              }
              
              let ir = {
                'name': userQuiz.Name,
                'email': userQuiz.Email,
                'completedTime': userResults.results.date,
                'nurseType': userData.NurseType,
                'activeWorking': userData.ActiveWorking,
                'nursingArea': userData.Area,
                'preferredPatients': userData.Patients,
                'patientsTitle': userQuiz.PatientsTitle,
                'country': userQuiz.Country,
                'suburb': userQuiz.Suburb,
                'postcode': userQuiz.PostalCode,
                'state': userQuiz.State,
                'yearOfBirth': userQuiz.Age,  
                'domainName': tq.DomainTitle,
                'domainLevel': val,
                'actionsToGrow': actionsToGrowAsStrings
              };
              output.push(ir);
              
            });
          }
        } catch(e){
          console.log(e);
          continue;
        }
      }
      res.attachment('report-' + type + '-' + new Date().toISOString() + '.csv');
      res.status(200).send(json2csv({ data: output, fields: fields}));
    });  
  })
})

app.listen(8081, function () {
  console.log('SAREPORT service listening on port 8081');
})