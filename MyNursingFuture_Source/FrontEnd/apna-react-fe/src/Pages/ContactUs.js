// import React, { Component } from 'react';
// import { Link } from 'react-router-dom';
// import PageContent from '../Components/PageContent';
// import { connect } from 'react-redux';
// import { fetchPageData } from '../Actions';

// class ContactUs extends Component {
//   constructor(props){
//     super(props);
//     this.state = {
//       apiEndpoint: '/contactus',
//       title: 'Contact Us'
//     };
//   }
    
//   componentWillMount() {
//     console.log('mounting page ContactUs', this.props);
//     this.props.onMount(this.state.apiEndpoint);
//   }
  
//   render() {
//       return <PageContent content={this.props.content} />
//   }
  
// }

// const mapStateToProps = (state) => {
//   return {
//     content: state.app.content.contentByPageEndpoint[state.app.content.displayPage],
//     loading: state.app.content.isLoading,
//     error: state.app.content.error,
//   }
// };

// const mapDispatchToProps = (dispatch) => {
//   return {
//     onMount: function(endpoint) {
//       dispatch(fetchPageData(endpoint));
//     }
//   }
// }

// export default connect(mapStateToProps, mapDispatchToProps)(ContactUs);

import React from 'react';
import AppPage from './AppPage';

// let hardContent = [
// {
//       type: 'HEADING',
//       title: 'General Enquiries'
//     },
//     // markup
//     {
//       type: 'MARKUP',
//       text: '<h1 class="xl">1300 303 184</h1><a href="mailto:framework@apna.asn.au">Or email us your question</a><p>Our office hours are Monday to Friday 9am  to 5pm (AEST).</p>'
//     },
//     // heading
//     { 
//       type: 'HEADING',
//       title: 'Career & Education Support'
//     },
//     // contactBlock
//     {
//       type: 'CONTACTBLOCK'
//     },
//     // heading
//     { 
//       type: 'HEADING',
//       title: 'Technical Support'
//     },
//     // markup
//     {
//       type: 'MARKUP',
//       text: '<p>For any issues using this website or accessing your profile, please call:</p><h1 class="xl">1300 303 184</h1><a href="mailto:framework@apna.asn.au">Or email us your question</a>'
//     },    
//     // heading
//     { 
//       type: 'HEADING',
//       title: 'Connect with APNA'
//     },    
//     {
//       type: "SHAREBLOCK"
//     },
//     // newsletterSignup
//     {
//       type: 'NEWSLETTERSIGNUP'
//     },
//     // heading
//     { 
//       type: 'HEADING',
//       title: 'Visit APNA'
//     },        
//     // default
//     {
//       type: 'MARKUP',
//       text: '<a href="https://www.apna.asn.au/" target="_blank">https://www.apna.asn.au/</a>'
//     }    
// ]


const ContactUs = () => (
  <AppPage endpoint="contact" title="Contact APNA" />
);

export default ContactUs;



