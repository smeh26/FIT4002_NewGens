var conf;

conf = {
  apiUrl: 'http://localhost:50565/',
  apiBaseUrl: 'api/',
  siteUrl: 'http://localhost:50565/',
  shareBaseUrl: 'http://localhost:50565/',
  imagesDirectory: 'Content/img/'
};


// if (process.env.NODE_ENV == 'production'){
    // apiUrl: 'http://180.235.131.104/',
    // apiBaseUrl: 'app/api/',
    // siteUrl: 'http://180.235.131.104/',
    // shareBaseUrl: 'http://180.235.131.104/',
    // imagesDirectory: 'app/Content/img/'
// } else {
//   conf = {
//     apiUrl: 'http://180.235.131.104/',
//     apiBaseUrl: 'app/api/',
//     siteUrl: 'http://180.235.131.104/',
//     shareBaseUrl: 'http://180.235.131.104/',
//     imagesDirectory: 'app/Content/img/'
//   };
// } else {
//   // conf = {
//   //     apiUrl: 'http://13.73.105.110',
//   //   apiBaseUrl: '/api/',
//   //   siteUrl: 'http://13.73.105.110',
//   //   shareBaseUrl: 'http://13.73.105.110',
//   //   imagesDirectory: '/Content/img/'
//   // };
//   conf = {
//     apiUrl: 'http://180.235.131.104/',
//     apiBaseUrl: 'app/api/',
//     siteUrl: 'http://180.235.131.104/',
//     shareBaseUrl: 'http://180.235.131.104/',
//     imagesDirectory: 'app/Content/img/'
//   };


export default conf;