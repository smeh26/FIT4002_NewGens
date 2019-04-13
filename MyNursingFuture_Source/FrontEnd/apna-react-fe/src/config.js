var conf;

if (process.env.NODE_ENV == 'production'){
  conf = {
    apiUrl: 'http://180.235.131.104/',
    apiBaseUrl: 'app/api/',
    siteUrl: 'http://180.235.131.104/',
    shareBaseUrl: 'http://180.235.131.104/',
    imagesDirectory: 'app/Content/img/'
  };
} else {
  // conf = {
  //     apiUrl: 'http://13.73.105.110',
  //   apiBaseUrl: '/api/',
  //   siteUrl: 'http://13.73.105.110',
  //   shareBaseUrl: 'http://13.73.105.110',
  //   imagesDirectory: '/Content/img/'
  // };
  conf = {
    apiUrl: 'http://180.235.131.104/',
    apiBaseUrl: 'app/api/',
    siteUrl: 'http://180.235.131.104/',
    shareBaseUrl: 'http://180.235.131.104/',
    imagesDirectory: 'app/Content/img/'
  };
}

export default conf;