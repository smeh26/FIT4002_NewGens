const fs = require('fs');
const config = require('./config.js');
var express = require('express');
var bodyParser = require('body-parser');
var wkhtmltopdf = require('wkhtmltopdf');
wkhtmltopdf.command = './wkhtmltopdf/bin/wkhtmltopdf.exe';

var app = express();
app.set('views', './views');
app.set('view engine', 'pug');
app.use(bodyParser.json());

app.post('/sareport', function (req, res) {
  
  var reportBody = req.body;
  reportBody.baseUrl = config.baseUrl;
  if (req.body.raw){
    res.render('report-rn', reportBody);
  } else {
    app.render('report-rn', reportBody, function(err,html){
      wkhtmltopdf(html)
        .pipe(res);
    });
  }
});


app.listen(8081, function () {
  console.log('SAREPORT service listening on port 8081');
})