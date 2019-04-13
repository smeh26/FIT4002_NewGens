var path = require('path');
     
 module.exports = {
     entry: './src-front/index.js',
     output: {
         path: path.resolve(__dirname, 'web'),
         filename: 'bundle.js'
     },
     module: {
         loaders: [
             {
                 test: /\.js$/,
                 exclude: /node_modules/,
                 loader: 'babel-loader',
                 query: {
                     presets: ['es2015', 'react']
                 }
             },
             {
                 test: /\.css$/,
                 loaders: ['style-loader', 'css-loader']
             }
         ]
     }
 };