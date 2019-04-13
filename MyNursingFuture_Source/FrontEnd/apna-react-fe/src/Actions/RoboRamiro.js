export const defaultPageContentByEndpoint = {
  '/': [
      {
        type: 'link',
        href: '/whyprimaryhealthcare',
        text: 'What is Primary Health?'
      },
      {
        image: '/img/about-you.png',
        type: 'default',
        title: 'Is primary health care nursing right for you?',
        text: '<p>Our 10-minute quiz will help you identify how your interests, strengths and goals might set you up for a great career in primary health care</p>',
        link: {
          text: 'Explore the different sectors',
          href: '/sectors'
        },
        buttonLink: {
          text: 'Take career quiz',
          href: '/careerquizintro'
        },
      },
      {
        type: 'default',
        image: '/img/self-assessment.png',
        title: 'What skills and experience do you have?',
        text: '<p>Assess your current skill level and learn what steps you can take to progress your career in primary health.</p>',
        link: {
          text: 'Explore the skills framework',
          href: '/explore'
        },
        buttonLink: {
          text: 'Self Assessment',
          href: '/selfassessmentintro'
        }
      },
      {
        type: 'reasonsList',
        carousel: true
      },
      {
        type: 'default',
        title: 'Why primary health?',
        titleImage: '/img/person.png',
        text: '<p>Listen to nurses tell of their own experiences working in Primary Health.</p>'
      },
      {
        type: 'videoEmbed',
        caption: 'Watch Sarah\'s story',
        videoId: 'BuhDLlAraJs'
      },
      {
        type: 'default',
        titleImage: '/img/owl.png',
        title: 'Words of Wisdom and career advice',
        text: '<p>Listen to nurses tell of their own experiences working in Primary Health.</p>',
        buttonLink: {
          text: 'Read Career Advice',
          href: '/articles'
        }
      },
      {
        type: 'share'
      },
      {
        type: 'default',
        titleImage: '/img/explore.png',
        title: 'Explore the primary health care sectors',
        text: '<p>Listen to nurses tell of their own experiences working in primary health.</p><p>Learn about the career opportunities in each sector and the various education pathways.</p><p>Find people to contact for advice and support and where to access online resources to continue your research.</p>',
      },
      {
        type: 'sectorLinkList'
      },
      {
        type: 'default',
        titleImage: '/img/logo-old.png',
        title: 'What is the APNA Career and Education Framework?',
        text: '<p>Learn why this framework is important and how you and your employer can use it to make primary health care in our communities stronger</p>',
        buttonLink: {
          text: 'Learn about the framework',
          href: '/explore'
        }
      },
    ],
  '/careerquizintro': [
    {
      type: 'heading',
      text: 'Is primary health care nursing right for you?'
    },
    {
      type: 'default',
      image: '/img/Quiz Intro Icon 1.png',
      title: 'Why take this quiz?',
      text: '<p>We have crafted 17 questions to find out more about you, your interests and the type of work you enjoy doing.</p><p>At the end, we will present you with a number of career pathways in the primary health sector that are best suited to you!</p>',
    },
    {
      type: 'default',
      image: '/img/Quiz Intro Icon 2.png',
      text: '<p>The quiz will take a maximum of 10 minutes of your time.</p>',
    },
    {
      type: 'default',
      image: '/img/Quiz Intro Icon 3.png',
      title: 'Please read carefully!',
      text: '<p>Before we start, please read our <a>terms of use</a> and <a>privacy policy</a>. You can access this information at any time using the links at the bottom of every page.</p>',
    },
    {
      type: 'ACCEPTTERMSANDPRIVACYPOLICY'
    },
    {
      type: 'default',
      buttonLink: {
        text: 'Start Quiz',
        href: '/quiz/careerPathways'
      }
    }
  ],
  '/selfassessmentintro': [
    {
      type: 'default',
      title: 'Assess your current skill level',
      text: '<p>For nurses working in Primary Health</p>'
    },
    {
      type: 'default',
      image: '/img/Quiz Intro Icon 1.png',
      text: '<p>Answer a set of questions on your current skill level with reference to all past & current experiences</p>',
    },
    {
      type: 'default',
      image: '/img/Quiz Intro Icon 2.png',
      text: '<p>Take 30 minutes and you can claim as CPD points.</p>',
    },
    {
      type: 'default',
      image: '/img/save.png',
      text: '<p>You can save and come back anytime.</p>',
    },
    {
      type: 'default',
      image: '/img/report.png',
      text: '<p>Receive a personalised report, including a set of actions to grow your career in primary health.</p><p><a href="#">See example report</a></p>',
    },
    {
      type: 'default',
      image: '/img/Quiz Intro Icon 3.png',
      title: 'Please read carefully!',
      text: '<p>Before we start, please read our <a>terms of use</a> and <a>privacy policy</a>. You can access this information at any time using the links at the bottom of every page.</p>',
    },
    {
      type: 'ACCEPTTERMSANDPRIVACYPOLICY'
    },
    {
      type: 'default',
      title: 'Start your assessment',
      text: '<p>Are you a RN or EN?</p>',
    },
    {
      type: 'default',
      buttonLink: {
        text: 'RN framework',
        href: '/quiz/selfAssessment/rn'
      }
    },
    {
      type: 'default',
      buttonLink: {
        text: 'EN framework',
        href: '/quiz/selfAssessment/en'
      }
    }
  ],
  '/contactus': [
    // heading
    {
      type: 'heading',
      text: 'General Enquiries'
    },
    // markup
    {
      type: 'markup',
      text: '<h1 class="xl">1300 303 184</h1><a href="#">Or email us your question</a><p>Our office hours are Monday to Friday 9am  to 5pm (AEST).</p>'
    },
    // heading
    { 
      type: 'heading',
      text: 'Career & Education Support'
    },
    // contactBlock
    {
      type: 'contactBlock'
    },
    // heading
    { 
      type: 'heading',
      text: 'Technical Support'
    },
    // markup
    {
      type: 'markup',
      text: '<p>For any issues using this website or accessing your profile, please call:</p><h1 class="xl">1300 303 184</h1><a href="#">Or email us your question</a>'
    },    
    // heading
    { 
      type: 'heading',
      text: 'Connect with us'
    },    
    // images
    {
      type: 'SHAREBLOCK'
    },
    // newsletterSignup
    {
      type: 'newsletterSignup'
    },
    // heading
    { 
      type: 'heading',
      text: 'Visit Us'
    },        
    // default
    {
      type: 'markup',
      text: '<p class="underline">Level 2, 159 Dorcas Street<br/>South Melbourne VIC 3205</p>'
    }    
  ],
  '/sectors': [
    // default
      {
        type: 'default',
        title: 'Explore the primary health care sectors',
        text: '<p>Listen to nurses tell of their own experiences working in Primary Health.</p><p>Learn about the career opportunities in each sector and the various education pathways.</p><p>Find people to contact for advice and support and where to access online resources to continue your research.</p>',
      },
      {
        type: 'sectorLinkList'
      },
      {
        type: 'default',
        title: 'Teamwork in primary health care',
        text: '<p>A guide to the different roles, responsibilities and accountabilities of nursing teams within primary health.</p>'
      },
      {
        type: 'roleLinkList'
      }
  ],
  '/explore': [
      {
        type: 'markup',
        text: '<p>The career and education framework has been designed to give direction and support for nurses that want to build a career in primary health care.</p><p>We have two versions for you to explore depending on if you are working as an EN or an RN.</p><p>Choose which framework you\'d like to look at:</p>'
      },
      {
        type: 'frameworkSelect'
      },
      {
        type: 'default',
        titleImage: '/img/self-assessment.png',
        title: 'What is the APNA Career and Education Framework?',
        text: '<p>Learn why this framework is important and how you and your employer can use it to make primary health care in our communities stronger</p>',
        buttonLink: {
          text: 'Learn about the framework',
          href: '/'
        }
      }
  ],
  '/domains/en': [
    {
      type: 'domainListTitle',
      framework: 'en'
    },
    {
      type: 'domainLinkList',
      framework: 'en'
    }
  ],
  '/domains/rn': [
    {
      type: 'domainListTitle', // fuck this off
      framework: 'rn'
    },
    {
      type: 'domainLinkList', 
      framework: 'rn'
    }
  ],  
  '/framework/glossary': [
    { 
      term: 'nursing',
      definition: 'its nursing'
    },
    {
      term: 'interests',
      definition: 'your butt!'
    }
  ],
  // /framework is the more structured & cross-app data
  '/framework/sectors': [
    {
      name: 'General Practice',
      link: '/sectors/generalpractice',
      sector: 'GeneralPractice',
      sectorId: 0,
      content: [
        {
          framework: 'en',
          intro: '<p>A practice nurse is a registered or enrolled nurse who works in or works for a general practice</p>' +
          '<p>Practice nurses play a major role in the delivery of primary care, health promotion and the management of acute and chronic conditions.</p>' +
          '<p>Nursing in general practice is one of the fastest growing areas of nursing in Australia</p>',
          video: '#somewhere',
          moreStories: '#somewhere',
          careerPathways: [
            {
              level: 0,
              title: 'Practice Nurse',
              text: '<p>Patient care, health prevention and promotion, triage, care coordination.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p>',
            },
            {
              level: 1,
              title: 'Senior Practice Nurse',
              text: '<p>Team leader, care coordination and case management, work health and safety, clinical governance.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p><p><strong>Highly recommended:</strong><br />Post graduate certificate/diploma (or the equivalent in self directed reading, worplace experience, CPD and online education)</p>',
            },
            {
              level: 2,
              title: 'Advanced Practice Nurse',
              text: '<p>Working at an Advanced Practice Nursing level, leading nurse clinics, building practice capacity, liasing and networking with external organisations.</p><p><strong>Education required:</strong><br />Bachelor of Nursing and a Masters degree (or the equivalent in self directed reading, workplace experience, CPD and online education)</p>'
            }
          ],
          workEnvironments: '<p>Practice nurses might work in any of the following environments:</p><ul><li>Private General Practice</li><li>Locum work</li><li>24 hour out of hours practice</li></ul><p>The skills gained as a practice nurse can be easily transferred into the following settings:</p><ul><li>Correctional Health</li><li>Community Health</li><li>School nursing</li></ul>',
          careerOpportunities: '<p><strong>Contract Practice Nurse - Queanbeyan, ACT</strong><br/>Looking for nurses with an interest in aged care to perform in-home health checks in the Queanbeyan area, starting January 2017.</p>', // this is going to be populated from feeds at some point?
          educationOpportunities: '<ul class="progress-pills blue"><li class="filled"></li><li></li><li></li></ul><p><a href="#">Post graduate courses in primary care, primary health care, community health, community nursing, general practice or chronic disease management</a></p><ul class="progress-pills blue"><li class="filled"></li><li class="filled"></li><li></li></ul><p><a href="#">APNA e-learning portal: An Orientation for Nurses New to General Practice</a><strong>(12 hours, $250)</strong></p><ul class="progress-pills blue"><li class="filled"></li><li class="filled"></li><li class="filled"></li></ul><p><a href="#">APNA: Foundations of General Practice Nursing workshops</a><strong>(2 days, $420)</strong></p><p><a href="#">APNA: Chronic Disease and Healthy Ageing workshops</a><strong>(2 days, $220)</strong></p>',
          onlineResources: '<p><a href="#">Practice Nurse Salaries in Australia (Health Times)</a></p><p><a href="#">APNA: Healthy Practices - NUrsing Roles in General Practice</a></p><p><a href="#">APNA e-learning portal: Opportunities for Practice Nurses</a></p>',
          contactText: '<h1>APNA Nurse Enquiry Line</h1><h1 class="xl">1300 303 184</h1><a href="#">Or email us your question</a><p>Our office hours are Monday to Friday 9am  to 5pm (AEST).</p>'      
        },
        {
          framework: 'rn',
          intro: '<p>(RN) A practice nurse is a registered or enrolled nurse who works in or works for a general practice</p>' +
          '<p>Practice nurses play a major role in the delivery of primary care, health promotion and the management of acute and chronic conditions.</p>' +
          '<p>Nursing in general practice is one of the fastest growing areas of nursing in Australia</p>',
          video: '#somewhere',
          moreStories: '#somewhere',
          careerPathways: [
            {
              level: 0,
              title: 'Practice Nurse',
              text: '<p>Patient care, health prevention and promotion, triage, care coordination.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p>',
            },
            {
              level: 1,
              title: 'Senior Practice Nurse',
              text: '<p>Team leader, care coordination and case management, work health and safety, clinical governance.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p><p><strong>Highly recommended:</strong><br />Post graduate certificate/diploma (or the equivalent in self directed reading, worplace experience, CPD and online education)</p>',
            },
            {
              level: 2,
              title: 'Advanced Practice Nurse',
              text: '<p>Working at an Advanced Practice Nursing level, leading nurse clinics, building practice capacity, liasing and networking with external organisations.</p><p><strong>Education required:</strong><br />Bachelor of Nursing and a Masters degree (or the equivalent in self directed reading, workplace experience, CPD and online education)</p>'
            }
          ],
          workEnvironments: '<p>Practice nurses might work in any of the following environments:</p><ul><li>Private General Practice</li><li>Locum work</li><li>24 hour out of hours practice</li></ul><p>The skills gained as a practice nurse can be easily transferred into the following settings:</p><ul><li>Correctional Health</li><li>Community Health</li><li>School nursing</li></ul>',
          careerOpportunities: '<p><strong>Contract Practice Nurse - Queanbeyan, ACT</strong><br/>Looking for nurses with an interest in aged care to perform in-home health checks in the Queanbeyan area, starting January 2017.</p>', // this is going to be populated from feeds at some point?
          educationOpportunities: '<p><a href="#">Post graduate courses in primary care, primary health care, community health, community nursing, general practice or chronic disease management</a></p><p><a href="#">APNA e-learning portal: An Orientation for Nurses New to General Practice</a><strong>(12 hours, $250)</strong></p><p><a href="#">APNA: Foundations of General Practice Nursing workshops</a><strong>(2 days, $420)</strong></p><p><a href="#">APNA: Chronic Disease and Healthy Ageing workshops</a><strong>(2 days, $220)</strong></p>',
          onlineResources: '<p><a href="#">Practice Nurse Salaries in Australia (Health Times)</a></p><p><a href="#">APNA: Healthy Practices - NUrsing Roles in General Practice</a></p><p><a href="#">APNA e-learning portal: Opportunities for Practice Nurses</a></p>',
          contactText: '<h1>APNA Nurse Enquiry Line</h1><h1 class="xl">1300 303 184</h1><a href="#">Or email us your question</a><p>Our office hours are Monday to Friday 9am  to 5pm (AEST).</p>'      
        }        
      ]
    },
    {
      sectorId: 1,
      name: 'Refugee / Detention'
    },
    {
      sectorId: 2,
      name: 'Aged care',
    },
    {
      name: 'Community Health',
      link: '/sectors/communityhealth',
      sector: 'CommunityHealth',
      sectorId: 3,
      content: [
        {
          framework: 'en',
          intro: '<p>A practice nurse is a registered or enrolled nurse who works in or works for a general practice</p>' +
            '<p>Practice nurses play a major role in the delivery of primary care, health promotion and the management of acute and chronic conditions.</p>' +
            '<p>Nursing in general practice is one of the fastest growing areas of nursing in Australia</p>',
          video: '#somewhere',
          moreStories: '#somewhere',
          careerPathways: [
            {
              level: 0,
              title: 'Practice Nurse',
              text: '<p>Patient care, health prevention and promotion, triage, care coordination.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p>',
            },
            {
              level: 1,
              title: 'Senior Practice Nurse',
              text: '<p>Team leader, care coordination and case management, work health and safety, clinical governance.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p><p><strong>Highly recommended:</strong><br />Post graduate certificate/diploma (or the equivalent in self directed reading, worplace experience, CPD and online education)</p>',
            },
            {
              level: 2,
              title: 'Advanced Practice Nurse',
              text: '<p>Working at an Advanced Practice Nursing level, leading nurse clinics, building practice capacity, liasing and networking with external organisations.</p><p><strong>Education required:</strong><br />Bachelor of Nursing and a Masters degree (or the equivalent in self directed reading, workplace experience, CPD and online education)</p>'
            }
          ],
          workEnvironments: '<p>Practice nurses might work in any of the following environments:</p><ul><li>Private General Practice</li><li>Locum work</li><li>24 hour out of hours practice</li></ul><p>The skills gained as a practice nurse can be easily transferred into the following settings:</p><ul><li>Correctional Health</li><li>Community Health</li><li>School nursing</li></ul>',
          careerOpportunities: '<p><strong>Contract Practice Nurse - Queanbeyan, ACT</strong><br/>Looking for nurses with an interest in aged care to perform in-home health checks in the Queanbeyan area, starting January 2017.</p>', // this is going to be populated from feeds at some point?
          educationOpportunities: '<p><a href="#">Post graduate courses in primary care, primary health care, community health, community nursing, general practice or chronic disease management</a></p><p><a href="#">APNA e-learning portal: An Orientation for Nurses New to General Practice</a><strong>(12 hours, $250)</strong></p><p><a href="#">APNA: Foundations of General Practice Nursing workshops</a><strong>(2 days, $420)</strong></p><p><a href="#">APNA: Chronic Disease and Healthy Ageing workshops</a><strong>(2 days, $220)</strong></p>',
          onlineResources: '<p><a href="#">Practice Nurse Salaries in Australia (Health Times)</a></p><p><a href="#">APNA: Healthy Practices - NUrsing Roles in General Practice</a></p><p><a href="#">APNA e-learning portal: Opportunities for Practice Nurses</a></p>',
          contactText: '<h1>APNA Nurse Enquiry Line</h1><h1 class="xl">1300 303 184</h1><a href="#">Or email us your question</a><p>Our office hours are Monday to Friday 9am  to 5pm (AEST).</p>'
        },
        {
          framework: 'rn',
          intro: '<p>(RN) A practice nurse is a registered or enrolled nurse who works in or works for a general practice</p>' +
            '<p>Practice nurses play a major role in the delivery of primary care, health promotion and the management of acute and chronic conditions.</p>' +
            '<p>Nursing in general practice is one of the fastest growing areas of nursing in Australia</p>',
          video: '#somewhere',
          moreStories: '#somewhere',
          careerPathways: [
            {
              level: 0,
              title: 'Practice Nurse',
              text: '<p>Patient care, health prevention and promotion, triage, care coordination.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p>',
            },
            {
              level: 1,
              title: 'Senior Practice Nurse',
              text: '<p>Team leader, care coordination and case management, work health and safety, clinical governance.</p><p><strong>Education required:</strong><br/>Bachelor of Nursing</p><p><strong>Highly recommended:</strong><br />Post graduate certificate/diploma (or the equivalent in self directed reading, worplace experience, CPD and online education)</p>',
            },
            {
              level: 2,
              title: 'Advanced Practice Nurse',
              text: '<p>Working at an Advanced Practice Nursing level, leading nurse clinics, building practice capacity, liasing and networking with external organisations.</p><p><strong>Education required:</strong><br />Bachelor of Nursing and a Masters degree (or the equivalent in self directed reading, workplace experience, CPD and online education)</p>'
            }
          ],
          workEnvironments: '<p>Practice nurses might work in any of the following environments:</p><ul><li>Private General Practice</li><li>Locum work</li><li>24 hour out of hours practice</li></ul><p>The skills gained as a practice nurse can be easily transferred into the following settings:</p><ul><li>Correctional Health</li><li>Community Health</li><li>School nursing</li></ul>',
          careerOpportunities: '<p><strong>Contract Practice Nurse - Queanbeyan, ACT</strong><br/>Looking for nurses with an interest in aged care to perform in-home health checks in the Queanbeyan area, starting January 2017.</p>', // this is going to be populated from feeds at some point?
          educationOpportunities: '<p><a href="#">Post graduate courses in primary care, primary health care, community health, community nursing, general practice or chronic disease management</a></p><p><a href="#">APNA e-learning portal: An Orientation for Nurses New to General Practice</a><strong>(12 hours, $250)</strong></p><p><a href="#">APNA: Foundations of General Practice Nursing workshops</a><strong>(2 days, $420)</strong></p><p><a href="#">APNA: Chronic Disease and Healthy Ageing workshops</a><strong>(2 days, $220)</strong></p>',
          onlineResources: '<p><a href="#">Practice Nurse Salaries in Australia (Health Times)</a></p><p><a href="#">APNA: Healthy Practices - NUrsing Roles in General Practice</a></p><p><a href="#">APNA e-learning portal: Opportunities for Practice Nurses</a></p>',
          contactText: '<h1>APNA Nurse Enquiry Line</h1><h1 class="xl">1300 303 184</h1><a href="#">Or email us your question</a><p>Our office hours are Monday to Friday 9am  to 5pm (AEST).</p>'
        }        
      ]
    },
    {
      sectorId: 4,
      name: 'Aboriginal Community Controlled Health Services'
    },
    {
      sectorId: 5,
      name: 'Correctional / Justice / Prison health'
    },
    {
      sectorId: 6,
      name: 'Drugs and Alcohol'
    },
    {
      sectorId: 8,
      name: 'Maternal / child health'
    },
    {
      sectorId: 9,
      name: 'Men\s Health'
    },
    {
      sectorId: 10,
      name: 'Mental Health'
    },
    {
      sectorId: 11,
      name: 'Work health & safety / Occupational health & safety'
    },
    {
      sectorId: 12,
      name: 'Public health'
    },
    {
      sectorId: 13,
      name: 'Primary or secondary school'
    },
    {
      sectorId: 14,
      name: 'Remote area nursing'
    },
    {
      sectorId: 15,
      name: 'Sexual and reproductive health'
    },
    {
      sectorId: 16,
      name: 'Women\s health'
    },
    {
      sectorId: 17,
      name: 'Hospital based nursing'
    }
  ],
  '/framework/roles': [
    {
      name: 'The Registered Nurse',
      role: 'registerednurse',
      linkLabel: 'Registered Nurse (RN)',
      link: '/roles/registerednurse',
      whatIs: '<p>A registered nurse (RN) must have successfully completed a ...</p>',
      whatIsTheirRole: '',
      accountabilities: '',
      examples: '',
      furtherInformation: ''
    },
    {
      name: 'Specialised Registered Nurse',
      role: 'specialisedrn',
      linkLabel: 'Specialised RN',
      link: '/roles/specialisedrn',
      whatIs: '<p>A specialised registered nurse is ...</p>',
      whatIsTheirRole: '',
      accountabilities: '',
      examples: '',
      furtherInformation: ''
    },
    {
      name: 'Advanced Practice Nurse',
      role: 'advancedpracticenurse',
      linkLabel: 'Advanced Practice Nurse (APN)',
      link: '/roles/advancedpracticenurse',
      whatIs: '<p>An advanced practice nurse is ...</p>',
      whatIsTheirRole: '',
      accountabilities: '',
      examples: '',
      furtherInformation: ''
    },
    {
      name: 'Nurse Practitioner',
      role: 'nursepractitioner',
      linkLabel: 'Nurse Practitioner (NP)',
      link: '/roles/nursepractitioner',
      whatIs: '<p>A nurse practitioner is ...</p>',
      whatIsTheirRole: '',
      accountabilities: '',
      examples: '',
      furtherInformation: ''
    },
    {
      name: 'Enrolled Nurse',
      role: 'enrollednurse',
      linkLabel: 'Enrolled Nurse (EN)',
      link: '/roles/enrollednurse',
      whatIs: '<p>An enrolled nurse is ...</p>',
      whatIsTheirRole: '',
      accountabilities: '',
      examples: '',
      furtherInformation: ''
    },
    {
      name: 'Advanced Enrolled Nurse',
      role: 'advanceden',
      linkLabel: 'Advanced EN',
      link: '/roles/advanceden',
      whatIs: '<p>An advanced enrolled nurse is ...</p>',
      whatIsTheirRole: '',
      accountabilities: '',
      examples: '',
      furtherInformation: ''
    }
  ],
  '/framework/domain': {
    'en': [],
    'rn': [
      {
        title: 'Clinical Care',
        name: 'clinicalcare',
        id: 0,
        introText: '<p>Introduction text</p>',
        image: '/img/direct-care.png',
        icon: '/img/direct-care-icon.png'
      },
      {
        title: 'Education',
        name: 'education',
        id: 1,
        introText: '<p>Introduction text</p>',
        image: '/img/education.png',
        icon: '/img/education-icon.png'
      },
      {
        title: 'Research',
        name: 'research',
        id: 2,
        introText: '<p>Introduction text</p>',
        image: '/img/research.png',
        icon: '/img/research-icon.png'
      },
      {
        title: 'Support',
        name: 'support',
        id: 3,
        introText: '<p>Introduction text</p>',
        image: '/img/systems.png',
        icon: '/img/systems-icon.png'
      },
      {
        title: 'Professional Leadership',
        name: 'professionalleadership',
        id: 4,
        introText: '<p>Introduction text</p>',
        image: '/img/leadership.png',
        icon: '/img/leadership-icon.png'
      }
    ]
  },
  '/framework/aspects': [
    {
      id: 1,
      framework: 'rn',
      domainId: 0,
      title: 'Assessment and Management',
      text: '<p>Applies evidence-based clinical skills in nursing assessment and management to meet the needs of individuals accessing care</p>',
      examples: '<h3>Example 1</h3><p>Conduct triage training in own area of practice</p><h3>Example 2</h3><p>Use validated risk screening tools as part of the assessment process</p><h3>Example 3</h3><p>Conducts mental health screening with individuals identified as “at risk” of harm to self or others</p>',
      levels: [
        {
          id: 0,
          text: '<p>Conducts a nursing assessment, using core clinical assessment skills, taking into account their physical, mental and social states alongside the impact of their environment and social support available to them.</p>',
          actions:[
            {
              title: 'Access education targeting areas specific to primary health care',
              text: '<a href="http://www.apna.asn.au/education/cdmha" target="_blank">APNA Chronic Disease and Healthy Ageing workshops</a><br/ ><br/ ><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=What%27s%20New%20in%20Asthma%20Management" target="_blank">APNA elearning portal: What\'s New in Asthma Management</a><br/ ><br/ ><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=Asthma%20Fundamentals%20for%20Primary%20Health%20Care%20Nurses" target="_blank">APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses</a>',
              type: 'Education'
            },
            {
              title: 'Incorporate evidence -based tools into your care delivery',
              text: '<p>text ... </p>',
              type: 'Environment'
            }  
          ]
        },
        {
          id: 1,
          text: '<p>Undertakes complex assessments appropriate to the situation and application of physical and clinical examination skills to inform the objective assessment encompassing all aspects of the individual\'s needs.</p>',
          actions:[
            {
              title: 'Intermediate action 1',
              text: '<a href="http://www.apna.asn.au/education/cdmha" target="_blank">APNA Chronic Disease and Healthy Ageing workshops</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=What%27s%20New%20in%20Asthma%20Management" target="_blank">APNA elearning portal: What\'s New in Asthma Management</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=Asthma%20Fundamentals%20for%20Primary%20Health%20Care%20Nurses" target="_blank">APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses</a>',
              type: 'Education'
            },
            {
              title: 'Intermediate action 2',
              text: '<p>text ... </p>',
              type: 'Environment'
            }  
          ]
        },
        {
          id: 2,
          text: '<p>Consolidate own assessment skills and support others in making assessment judgements.</p>',
          actions:[
            {
              title: 'Advanced action 1',
              text: '<a href="http://www.apna.asn.au/education/cdmha" target="_blank">APNA Chronic Disease and Healthy Ageing workshops</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=What%27s%20New%20in%20Asthma%20Management" target="_blank">APNA elearning portal: What\'s New in Asthma Management</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=Asthma%20Fundamentals%20for%20Primary%20Health%20Care%20Nurses" target="_blank">APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses</a>',
              type: 'Education'
            },
            {
              title: 'Advanced action 2',
              text: '<p>text ... </p>',
              type: 'Environment'
            }
          ]
        }
      ]
    },
    {
      id:2,
      framework: 'rn',
      domainId: 0,
      title: 'Planning Care',
      text: '<p>Applies evidence-based clinical skills in planning nursing care to meet the needs of individuals accessing care.</p>',
      examples: '<h3>Example 1</h3><p>Use a clinical audit tool to identify individuals with cardiovascular risk factors</p><h3>Example 2</h3><p>Apply COPD-X guidelines when planning care for individuals with a diagnosis of COPD</p><h3>Example 3</h3><p>Able to prioritise clinical workload in order of need</p>',
      levels: [
        {
          id: 0,
          text: '<p>Conducts a nursing assessment, using core clinical assessment skills, taking into account their physical, mental and social states alongside the impact of their environment and social support available to them.</p>',
          actions:[
            {
              title: 'Access education targeting areas specific to primary health care',
              text: '<a href="http://www.apna.asn.au/education/cdmha" target="_blank">APNA Chronic Disease and Healthy Ageing workshops</a><br/ ><br/ ><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=What%27s%20New%20in%20Asthma%20Management" target="_blank">APNA elearning portal: What\'s New in Asthma Management</a><br/ ><br/ ><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=Asthma%20Fundamentals%20for%20Primary%20Health%20Care%20Nurses" target="_blank">APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses</a>',
              type: 'Education'
            },
            {
              title: 'Incorporate evidence -based tools into your care delivery',
              text: '<p>text ... </p>',
              type: 'Environment'
            }  
          ]
        },
        {
          id: 1,
          text: '<p>Undertakes complex assessments appropriate to the situation and application of physical and clinical examination skills to inform the objective assessment encompassing all aspects of the individual\'s needs.</p>',
          actions:[
            {
              title: 'Intermediate action 1',
              text: '<a href="http://www.apna.asn.au/education/cdmha" target="_blank">APNA Chronic Disease and Healthy Ageing workshops</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=What%27s%20New%20in%20Asthma%20Management" target="_blank">APNA elearning portal: What\'s New in Asthma Management</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=Asthma%20Fundamentals%20for%20Primary%20Health%20Care%20Nurses" target="_blank">APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses</a>',
              type: 'Education'
            },
            {
              title: 'Intermediate action 2',
              text: '<p>text ... </p>',
              type: 'Environment'
            }  
          ]
        },
        {
          id: 2,
          text: '<p>Consolidate own assessment skills and support others in making assessment judgements.</p>',
          actions:[
            {
              title: 'Advanced action 1',
              text: '<a href="http://www.apna.asn.au/education/cdmha" target="_blank">APNA Chronic Disease and Healthy Ageing workshops</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=What%27s%20New%20in%20Asthma%20Management" target="_blank">APNA elearning portal: What\'s New in Asthma Management</a><a href="http://apna.e3learning.com.au/content/store/store.jsp?courseFilter=Asthma%20Fundamentals%20for%20Primary%20Health%20Care%20Nurses" target="_blank">APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses</a>',
              type: 'Education'
            },
            {
              title: 'Advanced action 2',
              text: '<p>text ... </p>',
              type: 'Environment'
            }  
          ]
        }
      ]
    }
  ],
  '/whyprimaryhealthcare': [
    {
      type: 'reasonsList',
      carousel: true,
      variation: true
    },
    {
      type: 'heading',
      text: 'Listen to nurses tell of their own experiences working in Primary Health.'
    },
    {
      type: 'videoEmbed',
      caption: 'Watch Sarah\'s story',
      videoId: 'BuhDLlAraJs'
    },
    {
      type: 'default',
      title: 'Is primary health care nursing right for you?',
      text: '<p>  </p>',
      buttonLink: {
        text: 'Take career quiz',
        href: ''
      }
    }
  ],
  '/articles': [
    {
      type: 'markup',
      text: '<p>During the consultation to develop this framework, we listened to nurses (over 700 in total!) talk about the difficulties they\'ve faced in establishing a long-term and satisfying career in primary health care nursing.</p><p>But along with these stories of frustration, we also heard many positive stories from veteran nurses with enviable primary health care nursing careers. With passion and resilience they faced their challenges head on and found ways to overcome them.</p><p>Their stories were inspirational to us. This section hopes to gather that insight and hard won wisdom into a single resource to provide guidance and support for nurses trying to find their way in primary health care nursing.</p>'
    },
    {
      type: 'articleLinkList',
    }
  ],
  '/articles/list': [
    {
      id: 'reasons',
      title: '11 reasons to choose primary health care nursing'
    },
    {
      id: 0,
      title: 'An article'
    }
  ],
  '/articles/reasons': {
    reasons: [
      {
        p: 'one',
        title: 'Flexible hours',
        text: 'Sick of the grind of shift work? One of the great things about working in primary health care is flexible working hours. Working in a primary health care setting offers more opportunities for working within business hours and nurses can often fit work around family needs. For example, school nurses work school hours. And nurses in general practice often work 9am-5pm. Sounds attractive!?'
      },
      {
        p: 'two',
        title: 'Autonomy in decision making',
        text: 'Nurses in primary health care can have wonderful autonomy. It comes with broad responsibility, while working collaboratively with a team. It’s a positive, challenging and satisfying career option.'
      },
      {
        p: 'three',
        title: 'Wide scope of practice',
        text: 'Nursing is a continuously-evolving practice discipline. Scientific and technological advancement and the demands of the community require nurses to constantly gain new knowledge and skills. Nursing activities can no longer easily be listed and ticked off! The margins of nursing practice are expanding, particularly in primary health care.'
      },
      {
        p: 'four',
        title: 'Nurse-led clinics',
        text: 'Nurse-led clinics are where you run the show! Nurse-led clinics benefit patients and can also provide professional satisfaction to the nurse. This is due to increased autonomy and positive experiences with patients. You still work as a multidisciplinary team, but you are the primary provider of care and have a patient caseload.'
      },
      {
        p: 'five',
        title: 'Patient-centred care',
        text: 'In primary health care, you get to put the patient at the centre of care. That is, you are able to provide healthcare that is respectful of, and responsive to, the preferences, needs and values of patients and consumers.'
      },
      {
        p: 'six',
        title: 'No day is ever the same!',
        text: 'The day to day role of a nurse in primary health care is always different. For example, you may be responding to the needs of patients presenting on any one day (e.g. triage, wound care, immunisation, facilitating referrals) whereas on another day you may operate a dedicated clinic with your own appointment schedule (e.g. women’s health clinic, chronic illness care plan clinic, aged care health assessment clinic).'
      },
      {
        p: 'seven',
        title: 'Be part of the community',
        text: 'Nurses can make a big impact on community health. Nurses provide a wide range of health promotion programs, as well as services to children, young people, adults and their families. For example some nurses work within schools and provide health related programs for students and staff.'
      },
      {
        p: 'eight',
        title: 'Varied environments outside of a hospital setting',
        text: 'Primary health care can be provided in a wide variety of settings i.e. the home, community-based settings, women’s health clinics, general practice.  Within these settings, there are sub-specialties. For example, a community health service may have specialist programs for refugees and asylum seekers; and a general practice may specialise in areas such as maternal and child health, Aboriginal and Torres Strait Islander health or mental health.'
      },
      {
        p: 'nine',
        title: 'Flat management hierarchy',
        text: 'Primary health care is often less hierarchical than a hospital. There are many advantages of flat management hierarchy. It can elevate the nurses’ level of responsibility in the organisation, improve the coordination and speed of communication between the team, and encourage an easier decision-making process within the organisation.'
      },
      {
        p: 'ten',
        title: 'You can make a difference!',
        text: 'Work at the coal face of health care in Australia. The future of nursing lies in primary health care. Complex and chronic problems can by managed in the primary health care setting. Nurses in primary health care are at the coalface of diagnosis and management. The onus falls on nurses in primary health care to go beyond a medical model of health, and address the complex issues that affect a communities’ health.'
      },
    ],
    title: '11 reasons to choose primary health care nursing',
    content: [
      {
        type: 'heading',
        text: '11 reasons to choose primary health care nursing'
      },
      {
        type: 'reasonsList'
      }
    ]
  },
  '/articles/0': {
    id: 0,
    title: 'An article',
    content: [
      { 
        type: 'heading',
        text: 'Article content.'
      },
      {
        type: 'markup',
        text: '<p>Article article article Article article article Article article article </p>'
      }
    ],
    furtherReading: '<p>Further reading ...</p>',
    toolsAndDownloads: '<p>Tools and downloads ...</p>',
    relatedCourses: '<p>Related courses ...</p>',
  },
  '/quiz/career/scoring': [
    {
      sectorId: 0,
      name: 'General Practice',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 0 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: 1 },
        { questionId: 1011, value: 0 }
      ]
    },
    {
      sectorId: 1,
      name: 'Refugee / Detention',
      idealAnswers: [
        { questionId: 1003, value: 0.5 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 0 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: 1 },
        { questionId: 1011, value: 0 }
      ]
    },
    {
      sectorId: 2,
      name: 'Aged care',
      idealAnswers: [
        { questionId: 1003, value: 0.5 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 0 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: 1 },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 3,
      name: 'Community health / District nursing',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: 1 },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 4,
      name: 'Aboriginal Community Controlled Health Services',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: 1 },
        { questionId: 1011, value: 0 }
      ]
    },
    {
      sectorId: 5,
      name: 'Correctional / Justice / Prison health',
      idealAnswers: [
        { questionId: 1003, value: 0 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 0 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: 0 },
        { questionId: 1011, value: 0 }
      ]
    },
    {
      sectorId: 6,
      name: 'Drugs and Alcohol',
      idealAnswers: [
        { questionId: 1003, value: 0.5 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 0.5 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null }, // null indicates dont care
        { questionId: 1011, value: 0 }
      ]
    },
    {
      sectorId: 8,
      name: 'Maternal / child health',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 0 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 9,
      name: 'Men\s Health',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 0.5 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 10,
      name: 'Mental Health',
      idealAnswers: [
        { questionId: 1003, value: 0.5 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 0.5 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 11,
      name: 'Work health & safety / Occupational health & safety',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 0 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: null }
      ]
    },
    {
      sectorId: 12,
      name: 'Public health',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 0 }
      ]
    },
    {
      sectorId: 13,
      name: 'Primary or secondary school',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: null }
      ]
    },
    {
      sectorId: 14,
      name: 'Remote area nursing',
      idealAnswers: [
        { questionId: 1003, value: 0 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 1 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 15,
      name: 'Sexual and reproductive health',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 0.5 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 1 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 16,
      name: 'Women\s health',
      idealAnswers: [
        { questionId: 1003, value: 1 },
        { questionId: 1004, value: 1 },
        { questionId: 1005, value: 0.5 },
        { questionId: 1006, value: 1 },
        { questionId: 1007, value: 0 },
        { questionId: 1008, value: 1 },
        { questionId: 1009, value: 1 },
        { questionId: 1010, value: null },
        { questionId: 1011, value: 1 }
      ]
    },
    {
      sectorId: 17,
      name: 'Hospital based nursing',
      idealAnswers: [
        { questionId: 1003, value: 0 },
        { questionId: 1004, value: 0 },
        { questionId: 1005, value: 0 },
        { questionId: 1006, value: 0 },
        { questionId: 1007, value: 0 },
        { questionId: 1008, value: 0 },
        { questionId: 1009, value: 0 },
        { questionId: 1010, value: 0 },
        { questionId: 1011, value: 1 }
      ]
    }
  ],
  '/quiz/career': [
    {
      questionId: 1000,
      text: 'Tell us about yourself. Are you a',
      type: 'CHOICE',
      quizType: 'PATHWAY',
      
      answers: [
        {
          answerId: 1000,
          questionId: 1000,
          text: 'EN',
          value: 1
        },
        {
          answerId: 1001,
          text: 'RN',
          value: 2,
          questionId: 1000,
        },
        {
          answerId: 1002,
          text: 'RN/RM',
          value: 3,
          questionId: 1000
        },
        {
          answerId: 1003,
          text: 'NP',
          value: 4,
          questionId: 1000
        },
        {
          answerId: 1004,
          text: 'Studying to become an EN',
          value: 5,
          questionId: 1000
        },
        {
          answerId: 1005,
          text: 'Studying to become an RN',
          value: 6,
          questionId: 1000
        },
        {
          answerId: 1006,
          text: 'Non-nursing professional',
          value: 7,
          questionId: 1000
        }
      ]
    },
    {
      questionId: 1001,
      type: 'CHOICE',
      quizType: 'PATHWAY',
      text: 'What is your highest level of nursing education completed?',
      answers: [
        {
          answerId: 1007,
          questionId: 1001,
          text: 'Diploma or Cert IV',
          value: 1
        },
        {
          answerId: 1008,
          questionId: 1001,
          text: 'Bachelor of Nursing Degree',
          value: 2
        },
        {
          answerId: 1009,
          questionId: 1001,
          text: 'Post Grad Certificate',
          value: 3
        },
        {
          answerId: 1010,
          questionId: 1001,
          text: 'Post Grad Diploma',
          value: 4
        },
        {
          answerId: 1011,
          questionId: 1001,
          text: 'Masters',
          value: 5
        },
        {
          answerId: 1012,
          questionId: 1001,
          text: 'PhD',
          value: 6
        },
        {
          answerId: 1013,
          questionId: 1001,
          text: 'Hospital trained',
          value: 7
        },
        {
          answerId: 1014,
          questionId: 1001,
          text: 'None of the above',
          value: 8
        }
      ]
    },
    {
      questionId: 1002,
      text: 'What kinds of people do you enjoy working with?',
      type: 'MULTI',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1002,
          answerId: 1015,
          text: 'Babies and Children',
          value: 1
        },
        {
          questionId: 1002,
          answerId: 1016,
          text: 'Youth',
          value: 2
        },
        {
          questionId: 1002,
          answerId: 1017,
          text: 'Women',
          value: 3
        },
        {
          questionId: 1002,
          answerId: 1018,
          text: 'Men',
          value: 4
        },
        {
          questionId: 1002,
          answerId: 1019,
          text: 'Elderly',
          value: 5
        },
        {
          questionId: 1002,
          answerId: 1020,
          text: 'Marginalised and disavantaged communities',
          value: 6
        },
        {
          questionId: 1002,
          answerId: 1021,
          text: 'all of the above',
          value: 7
        }
      ]
    },
    {
      questionId: 1003,
      text: 'How important is it that you have a job with family friendly hours? ie within buisness hours',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1003,
          answerId: 1022,
          text: 'not important',
          value: 0,
          matchText: 'Shift work hours including some night shifts'
        },
        {
          questionId: 1003,
          answerId: 1023,
          text: 'moderately important',
          value: 0.5,
          matchText: 'A mix of work within buisness hours and some shift work'
        },
        {
          questionId: 1003,
          answerId: 1024,
          text: 'very important',
          value: 1,
          matchText: 'Shifts within business hours'
        }
      ]
    },
        {
      questionId: 1004,
      text: 'How important is it for you to have the flexibility to empower people to take care of their own health?',
      type: 'RANGE',
      quizType: 'PATHWAY',  
      answers: [
        {
          questionId: 1004,
          answerId: 1025,
          text: 'not important',
          value: 0
        },
        {
          questionId: 1004,
          answerId: 1026,
          text: 'moderately important',
          value: 0.5
        },
        {
          questionId: 1004,
          answerId: 1027,
          text: 'very important',
          value: 1,
          matchText: 'Empower patients to take care of own health'
        }
      ]
    },
    {
      questionId: 1005,
      text: 'Would you prefer to work within or outside a hospital environment?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1005,
          answerId: 1028,
          text: 'within hospital',
          value: 0,
          matchText: 'Work inside of hospital environment'
        },
        {
          questionId: 1005,
          answerId: 1029,
          text: 'a mix of both',
          value: 0.5,
          matchText: 'A mix of inside and outside hospital environments'
        },
        {
          questionId: 1005,
          answerId: 1030,
          text: 'outside hospital',
          value: 1,
          matchText: 'Work outside of hospital environment'
        }
      ]
    },
        {
      questionId: 1006,
      text: 'How willing are you to travel and work in a range of environments in the community eg schools, people\'s homes etc)',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1006,
          answerId: 1031,
          text: 'not willing',
          value: 0,
          matchText: 'A single workplace environment everyday'
        },
        {
          questionId: 1006,
          answerId: 1032,
          text: 'moderately willing',
          value: 0.5
        },
        {
          questionId: 1006,
          answerId: 1033,
          text: 'very willing',
          value: 1,
          matchText: 'A range of environments in the community'
        }
      ]
    },
        {
      questionId: 1007,
      text: 'How much does the perception of your nursing colleagues influence your decision about which area/speciality you want to practice in?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1007,
          answerId: 1034,
          text: 'very influential',
          value: 0
        },
        {
          questionId: 1007,
          answerId: 1034,
          text: 'moderate influence',
          value: 0.5
        },
        {
          questionId: 1007,
          answerId: 1034,
          text: 'no influence',
          value: 1
        }
      ]
    },
        {
      questionId: 1008,
      text: 'How willing are you to navigate and drive your own career pathway?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1008,
          answerId: 1035,
          text: 'not willing',
          value: 0,
          matchText: 'Career pathway well estabilished'
        },
        {
          questionId: 1008,
          answerId: 1036,
          text: 'moderately willing',
          value: 0.5
        },
        {
          questionId: 1008,
          answerId: 1037,
          text: 'very willing',
          value: 1,
          matchText: 'Ability to navigate your own career pathway'
        }
      ]
    },
        {
      questionId: 1009,
      text: 'Would you like to directly influence the care of an individual and actively advocate for them?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1009,
          answerId: 1038,
          text: 'No',
          value: 0
        },
        {
          questionId: 1009,
          answerId: 1039,
          text: 'moderate value',
          value: 0.5
        },
        {
          questionId: 1009,
          answerId: 1040,
          text: 'Yes',
          value: 1,
          matchText: 'Ability to directly influence the care of an individual and actively advocate for them'
        }
      ]
    },
    {
      questionId: 1010,
      text: 'How willing are you to accept lower pay as a trade off for better work/life balance?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1010,
          answerId: 1041,
          text: 'not at all willing',
          value: 0,
          matchText: 'Pay comparible with hospital colleagues'
        },
        {
          questionId: 1010,
          answerId: 1042,
          text: 'moderately willing',
          value: 0.5
        },
        {
          questionId: 1010,
          answerId: 1043,
          text: 'Very willing',
          value: 1
        }
      ]
    },
    {
      questionId: 1011,
      text: 'How important is it to you to work in a setting where the nursing role is well established and has  national practice standards in place?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1011,
          answerId: 1044,
          text: 'not important',
          value: 0
        },
        {
          questionId: 1011,
          answerId: 1045,
          text: 'moderately important',
          value: 0.5
        },
        {
          questionId: 1011,
          answerId: 1046,
          text: 'very important',
          value: 1,
          matchText: 'Nursing role is well established and has been defined by the profession.'
        }
      ]
    },
    {
      questionId: 1012,
      text: 'How important is it to work in a setting that limits the amount of physical stress to your body?',
      type: 'RANGE',
      quizType: 'PATHWAY',
      answers: [
        {
          questionId: 1012,
          answerId: 1047,
          text: 'not important',
          value: 0
        },
        {
          questionId: 1012,
          answerId: 1048,
          text: 'moderately important',
          value: 0.5
        },
        {
          questionId: 1012,
          answerId: 1049,
          text: 'very important',
          value: 1,
          matchText: 'Limited exposure to physical stress'
        }
      ]
    }
  ],
  '/quiz/selfAssessment': [
    {
      id: 100,
      text: 'Tell us about yourself. Are you a',
      type: 'choice',
      answers: [
        {
          text: 'EN',
          value: 1
        },
        {
          text: 'RN',
          value: 2
        },
        {
          text: 'RN/RM',
          value: 3
        },
        {
          text: 'NP',
          value: 4
        },
        {
          text: 'Studying to become an EN',
          value: 5
        },
        {
          text: 'Studying to become an RN',
          value: 6
        },
        {
          text: 'Non-nursing professional',
          value: 7
        }
      ]
    },
    {
      id: 101,
      text: 'What is your highest level of nursing education completed?',
      type: 'choice',
      answers: [
        {
          text: 'Diploma or Cert IV',
          value: 1
        },
        {
          text: 'Bachelor of Nursing Degree',
          value: 2
        },
        {
          text: 'Post Grad Certificate',
          value: 3
        },
        {
          text: 'Post Grad Diploma',
          value: 4
        },
        {
          text: 'Masters',
          value: 5
        },
        {
          text: 'PhD',
          value: 6
        },
        {
          text: 'Hospital trained',
          value: 7
        },
        {
          text: 'None of the above',
          value: 8
        }
      ]
    },
    {
      id: 102,
      text: 'What kinds of people do you enjoy working with?',
      type: 'multi',
      answers: [
        {
          text: 'Babies and Children',
          value: 1
        },
        {
          text: 'Youth',
          value: 2
        },
        {
          text: 'Women',
          value: 3
        },
        {
          text: 'Men',
          value: 4
        },
        {
          text: 'Elderly',
          value: 5
        },
        {
          text: 'Marginalised and disavantaged communities',
          value: 6
        },
        {
          text: 'all of the above',
          value: 7
        }
      ]
    },
    {
      id: 0,
      text: 'How do you apply evidence-based clinical skills to your everyday practice?',
      type: 'choice',
      examples: [
        'Conduct triage training in own area of practice',
        'Use validated risk screening tools as part of the assessment process',
        'Conduct mental health screening with individuals identified as "at risk" of harm to self or others'
      ],
      answers: [
        {
          text: 'I am learning how to apply and carry out clinical care that is holistic, person-centred and culturally safe.',
          value: 0
        },
        {
          text: 'I am confidently practicing clinical care that is holistic, person-centred and culturally safe.',
          value: 0.5
        },
        {
          text: 'I lead and guide others to confidently practice clinical care that is holistic, person-centred and culturally safe.',
          value: 1
        }
      ],
      aspectId: "1.1"
    },
    {
      id: 1,
      text: 'How do you use evidence-based clinical skills when planning care?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to use data to identify individuals with chronic disease risks',
          value: 0
        },
        {
          text: 'I can confidently identify, use and monitor local data that identifies individuals with chronic disease risks ',
          value: 0.5
        },
        {
          text: 'I work with individuals and local communities to guide a health care approach that supports and maximizes the population’s health and wellbeing whilst leading the primary health care team to build community capacity ',
          value: 1
        }
      ],
      aspectId: "1.2"
    },
    {
      id: 2,
      text: 'How do you use evidence-based clinical skills to measure the impact of your care on clients?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to utilise and apply critical thinking to explore and interpret evidence in clinical practice',
          value: 0
        },
        {
          text: 'I confidently utilise enhanced critical thinking to explore and analyse evidence in clinical practice',
          value: 0.5
        },
        {
          text: 'I lead by using advanced critical thinking skills to evaluate and apply evidence in clinical practice and guide the primary health care team to ensure high level assessment and decision making',
          value: 1
        }
      ],
      aspectId: "1.3"
    },
    {
      id: 3,
      text: 'How do you align your skills with the health needs* of the population?',
      subText: 'These needs may include delivering targeted public health, health promotion and prevention.',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to participate in campaigns aimed at addressing relevant public health issues within the local primary health care setting',
          value: 0
        },
        {
          text: 'I am confident in participating in public health strategies aligned to the local setting and community and work collaboratively with others to improve health and avoid hospitalisation',
          value: 0.5
        },
        {
          text: 'I lead by actively contributing to the development and implementation of public health campaigns and strategies relevant to local, regional or national needs',
          value: 1
        }
      ],
      aspectId: "1.4"
    },
    {
      id: 4,
      text: 'How do you accept responsibility and professional accountability within your scope of practice?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to utilise and apply critical thinking to explore and analyse evidence in clinical practice',
          value: 0
        },
        {
          text: 'I confidently embed critical thinking in clinical decision making in practice',
          value: 0.5
        },
        {
          text: 'I lead by utilising critical thinking to inform advanced  clinical decision making in practice ',
          value: 1
        }
      ],
      aspectId: "1.5"
    },
    {
      id: 5,
      text: 'How do you consider the health literacy of the client when talking to them and their healthcare team?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to recognise the impact of low health literacy ',
          value: 0
        },
        {
          text: 'I am confident in my understanding of the impact of health literacy as an essential component of culturally safe care',
          value: 0.5
        },
        {
          text: 'I lead the delivery of culturally safe care by ensuring the primary health care team understand the impact of poor health literacy',
          value: 1
        }
      ],
      aspectId: "1.6"
    },
    {
      id: 6,
      text: 'How do you practice non-discriminatory and non-judgemental care?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to deliver care and support that respects the dignity, wishes and beliefs of all individuals',
          value: 0
        },
        {
          text: 'I’m confident to act as a role model to provide non-judgemental, value-based care and expect and promote these values to other team members',
          value: 0.5
        },
        {
          text: 'I act as a guide for values-based care. I lead the professional development and quality improvement strategies to ensure they reflect a values-based approach to care ',
          value: 1
        }
      ],
      aspectId: "1.7"
    },
    {
      id: 7,
      text: 'Do you practice culturally safe care for all Aboriginal and Torres Strait Islander people accessing care?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning the importance of demonstrating respect and providing culturally safe care for Aboriginal and Torres Strait Islander people in the community',
          value: 0
        },
        {
          text: 'I am confident to take an active role in ensuring the primary health care service is culturally safe for Aboriginal and Torres Strait Islander people',
          value: 0.5
        },
        {
          text: 'I lead and guide the primary health care team to deliver culturally safe care for Aboriginal and Torres Strait Islander people',
          value: 1
        }
      ],
      aspectId: "1.8"
    },
    {
      id: 8,
      text: 'When taking care of a patient, how do you consider cultural and language barriers?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning the importance of demonstrating respect and providing culturally safe care for culturally and linguistically diverse people in the community ',
          value: 0
        },
        {
          text: 'I confidently I take an active role in ensuring the primary health care service is culturally safe for culturally and linguistically diverse people ',
          value: 0.5
        },
        {
          text: 'I lead and guide the primary health care team to deliver culturally safe care for culturally and linguistically diverse people ',
          value: 1
        }
      ],
      aspectId: "1.9"
    },
    {
      id: 9,
      text: ' In clinical practice, how do you apply personal and professional reflection?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to participate in a clinical support network within the primary health care team ',
          value: 0
        },
        {
          text: 'I confidently evaluate outcomes from my own clinical reflection and contribution to a clinical support network ',
          value: 0.5
        },
        {
          text: 'I guide and monitor the involvement and outcomes of members of the primary health care team in clinical support networks to support professional reflection',
          value: 1
        }
      ],
      aspectId: "1.10"
    },
    {
      id: 10,
      text: 'As part of a primary healthcare team, how do you use your knowledge to assess and manage risk?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to identify and document possible risks to individuals accessing care and colleagues',
          value: 0
        },
        {
          text: 'I can confidently manage risk to individuals accessing care and colleagues, whilst remaining accountable for the care provided to these individuals',
          value: 0.5
        },
        {
          text: 'I lead the design and implementation of strategies for risk management to protect individuals accessing care and colleagues ',
          value: 1
        }
      ],
      aspectId: "1.11"
    },
    {
      id: 11,
      text: 'How do you use technology* in your everyday practice?',
      subText: '*information technology, clinical software and decision support tools.',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to use and apply available software or data management systems to improve the ability to identify individuals with, or at risk of, chronic diseases',
          value: 0
        },
        {
          text: 'I’m confidently using my skills to identify, implement and evaluate the use of new technologies in health care delivery',
          value: 0.5
        },
        {
          text: 'I lead by ensuring data capture systems are fit for purpose',
          value: 1
        }
      ],
      aspectId: "1.12"
    },
    {
      id: 12,
      text: 'How do you tailor your education to increase competence and confidence in your scope of practice?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to be responsible and accountable for keeping my knowledge and skills up to date through continuing professional development and participating in clinical support strategies e.g. mentoring, coaching, clinical supervision',
          value: 0
        },
        {
          text: 'I confidently expand my scope of practice to meet the needs of those accessing care by undertaking appropriate education and skill development',
          value: 0.5
        },
        {
          text: 'I act as a leader by proactively seeking to expand and maintain advanced scope of practice by undertaking appropriate education and skill development',
          value: 1
        }
      ],
      aspectId: "2.1"
    },
    {
      id: 13,
      text: 'How do you incorporate learning, teaching and appraisal* into your nursing role?',
      subText: '*These may include: peer learning, knowledge about evidence-based initiatives and appraisals.',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning the importance of sharing information and external learning with other members of the primary health care team',
          value: 0
        },
        {
          text: 'I’m confident when teaching colleagues and students through informal and/or formal education',
          value: 0.5
        },
        {
          text: 'I lead and guide members of the primary health care team by providing formal education relating to evidence-based initiatives and processes',
          value: 1
        }
      ],
      aspectId: "2.2"
    },
    {
      id: 14,
      text: ' How do you contribute to an environment that cultivates learning?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to apply relevant professional standards and guidelines into teaching and learning experiences ',
          value: 0
        },
        {
          text: 'I can confidently identify relevant professional standards and clinical guidelines to support teaching and learning experiences',
          value: 0.5
        },
        {
          text: 'I play a lead role in ensuring the application of standards and guidelines that underpin a quality teaching and learning experience',
          value: 1
        }
      ],
      aspectId: "2.3"
    },
    {
      id: 15,
      text: 'How do you work with the community to identify and share information relevant to their healthcare needs?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning to work with the community to identify health and support needs',
          value: 0
        },
        {
          text: 'I’m confident to consult and collaborate with the community to identify health and support needs',
          value: 0.5
        },
        {
          text: 'I lead the development of collaborations with the community to ensure local and national public health needs',
          value: 1
        }
      ],
      aspectId: "2.4"
    },
    {
      id: 16,
      text: 'How do you support a culture of continuous quality improvement in all aspects of service delivery?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to participate in quality improvement activities to evaluate the quality and effectiveness of nursing care ',
          value: 0
        },
        {
          text: 'I’m confident to conduct quality improvement activities to identify quality issues within the clinical setting',
          value: 0.5
        },
        {
          text: 'I lead activities within the primary health care team around quality improvement related to nursing care ',
          value: 1
        }
      ],
      aspectId: "3.1"
    },
    {
      id: 17,
      text: 'How do you participate in research and its evaluation?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to recognise the ethical implications of an audit, research project or clinical trials',
          value: 0
        },
        {
          text: 'I’m confident in my understanding of the principles of research governance ',
          value: 0.5
        },
        {
          text: 'I lead by ensuring frameworks for research governance are applied appropriately',
          value: 1
        }
      ],
      aspectId: "3.2"
    },
    {
      id: 18,
      text: 'How do you identify the opportunities and resources needed for quality improvement, research and evaluation?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to seek information about opportunities for funding research activities',
          value: 0
        },
        {
          text: 'I can confidently identify opportunities for funding or additional resources to support evaluation activities or research ',
          value: 0.5
        },
        {
          text: 'I act as a leader in clinical policy and research communities by identifying deficits in evidence and potential funding sources for practice or research development ',
          value: 1
        }
      ],
      aspectId: "3.3"
    },
    {
      id: 19,
      text: 'How does evidence-based research underpin your nursing practise?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to demonstrate knowledge of current policies and procedures and an understanding of their implications for nursing practice ',
          value: 0
        },
        {
          text: 'I confidently demonstrate sound understanding of all current policies and procedures and an understanding of their implications for nursing practice ',
          value: 0.5
        },
        {
          text: 'I lead and guide the primary health care team in their understanding of current policies and procedures and their evidence-base',
          value: 1
        }
      ],
      aspectId: "3.4"
    },
    {
      id: 20,
      text: 'How do you develop and implement models of care to improve care delivery?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to use local care pathways to apply timely access to appropriate care',
          value: 0
        },
        {
          text: 'I confidently contribute to the development or improvement of local care pathways, encouraging team members and, where possible, Individuals to contribute',
          value: 0.5
        },
        {
          text: 'I lead and work collaboratively to develop and evaluate the care pathways utilised by the primary health care team ',
          value: 1
        }
      ],
      aspectId: "4.1"
    },
    {
      id: 21,
      text: 'How do you promote effective communication to improve the way primary healthcare teams work together?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to recognise effective verbal and non-verbal communication techniques and how to apply these across a variety of situations',
          value: 0
        },
        {
          text: 'I have confidence in my ability to promote and implement the use of effective communication techniques',
          value: 0.5
        },
        {
          text: 'I lead by mentoring and supporting the use of effective communication techniques in the primary health care team and with service users',
          value: 1
        }
      ],
      aspectId: "4.2"
    },
    {
      id: 22,
      text: 'How do you support a culture of effective teamwork?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to adopt an innovative approach to identifying new ways of working',
          value: 0
        },
        {
          text: 'I’m confident in my ability to mentor and support colleagues to embrace an innovative approach to the development of the service',
          value: 0.5
        },
        {
          text: 'I lead by influencing service development by supporting and developing innovative and lateral thinking in self and others ',
          value: 1
        }
      ],
      aspectId: "4.3"
    },
    {
      id: 23,
      text: 'To what extent do you develop, implement or review policies and procedures?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning the importance of developing clinical assessment policies tailored to my own area of practice',
          value: 0
        },
        {
          text: 'I’m confident to contribute to the development of guidelines and policy locally, regionally and nationally, where appropriate ',
          value: 0.5
        },
        {
          text: 'I lead on local, regional or national primary health care nursing policies and strategies to deliver quality care',
          value: 1
        }
      ],
      aspectId: "4.4"
    },
    {
      id: 24,
      text: 'How do you contribute to change within the nursing profession?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to seek opportunities to improve the service, by generating ideas for innovation',
          value: 0
        },
        {
          text: 'I’m confident in my role as a change agent',
          value: 0.5
        },
        {
          text: 'I lead the application of theoretical perspectives of change management to create an environment for successful change and practice development',
          value: 1
        }
      ],
      aspectId: "4.5"
    },
    {
      id: 25,
      text: 'How do you engage in professional leadership within and beyond your own setting?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to build professional networks promoting exchange of knowledge, skills and resources ',
          value: 0
        },
        {
          text: 'I’m confident to support peers to develop networks and share information',
          value: 0.5
        },
        {
          text: 'I lead and guide the establishment of professional networks with peers from the interdisciplinary team and promote exchange of knowledge, skills and resources',
          value: 1
        }
      ],
      aspectId: "5.1"
    },
    {
      id: 26,
      text: 'How do you build and maintain relationships to contribute to the improvement of the nursing profession?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to provide collegial support to ENs and RNs in other settings',
          value: 0
        },
        {
          text: 'I’m confident to identify and disseminate information relevant to ENs and RNs through existing networks, such as the APNA Nurse Network',
          value: 0.5
        },
        {
          text: 'I lead the development of communities of practice and networks, to disseminate resources and practice initiatives ',
          value: 1
        }
      ],
      aspectId: "5.2"
    },
    {
      id: 27,
      text: 'How do you share nursing expertise with other health professionals?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to share research findings with colleagues ',
          value: 0
        },
        {
          text: 'I’m confident to share research findings through local bulletins, team meetings, forums and/or professional journals ',
          value: 0.5
        },
        {
          text: 'I lead and guide the dissemination of scholarly activity and new developments to support the integration of evidence based practice and influence the development of the learning environment',
          value: 1
        }
      ],
      aspectId: "5.3"
    },
    {
      id: 28,
      text: 'How do you promote the role of the nurse in primary health care?',
      type: 'choice',
      answers: [
        {
          text: 'I’m learning how to promote nursing in primary health care to other nurses and health professionals, individuals and other relevant groups',
          value: 0
        },
        {
          text: 'I’m confident to engage and recruit individuals and organisations to advocate for the role of the primary health care nurse in the broader health care system',
          value: 0.5
        },
        {
          text: 'I lead through collaborating proactively with public health agencies and local authorities to ensure primary health care nursing is actively engaged in the health improvement strategies for the local community',
          value: 1
        }
      ],
      aspectId: "5.4"
    },
    {
      id: 29,
      text: 'To what extent do you act as a knowledge resource for others*?',
      subText: '*these may include the community, committees and other health professionals beyond your own setting',
      type: 'choice',
      answers: [
        {
          text: 'I am learning how to identify an area of particular interest and provide insight into the contemporary evidence that supports practice',
          value: 0
        },
        {
          text: 'I’m confident to work towards an area of clinical expertise including undertaking continuing professional development (CPD) activities',
          value: 0.5
        },
        {
          text: 'I am a leader in at least one area of practice and am seen as a local expert to articulate the most contemporary evidence, and approaches to practice and management',
          value: 1
        }
      ],
      aspectId: "5.5"
    },
  ],
  '/user/profile': {
    name: '',
    email: '',
    amsId: '', // reference to their ams user ref
  },
  '/user/quiz/career/inprogress': [],
  '/user/quiz/career/complete': [],
  '/user/quiz/selfassessment/complete': [],
  '/user/quizzes': [
    {
      id: 0,
      type: 'careerPathways',
      date:"11 Jul 2017",
      completed: true,
      results: {
        "score":{
          "0":5.26,
          "1":4.76,
          "2":3.84,
          "3":3.9399999999999995,
          "4":4.859999999999999,
          "5":4.579999999999999,
          "6":3.28,
          "8":3.3999999999999995,
          "9":2.8600000000000003,
          "10":2.36,
          "11":4.319999999999999,
          "12":4.279999999999999,
          "13":4.279999999999999,
          "14":3.26,
          "15":2.8600000000000003,
          "16":2.9000000000000004,
          "17":4.14
        },
        "scorePositives":{
          "0":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "1":[
              "A mix of work within buisness hours and some shift work",
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "2":[
              "A mix of work within buisness hours and some shift work",
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "3":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "4":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "5":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "6":[
              "A mix of work within buisness hours and some shift work",
              "Empower patients to take care of own health",
              "A mix of inside and outside hospital environments",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "8":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "9":[
              "Empower patients to take care of own health",
              "A mix of inside and outside hospital environments",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "10":[
              "A mix of work within buisness hours and some shift work",
              "Empower patients to take care of own health",
              "A mix of inside and outside hospital environments",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "11":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "12":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "13":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them"
          ],
          "14":[
              "Empower patients to take care of own health",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "15":[
              "Empower patients to take care of own health",
              "A mix of inside and outside hospital environments",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "16":[
              "Empower patients to take care of own health",
              "A mix of inside and outside hospital environments",
              "Ability to directly influence the care of an individual and actively advocate for them",
              "Nursing role is well established and has been defined by the profession."
          ],
          "17":[
              "Nursing role is well established and has been defined by the profession."
          ]
        },
        "scoreNegatives":{
          "5":[
              "Pay comparible with hospital colleagues"
          ],
          "17":[
              "Work inside of hospital environment",
              "Career pathway well estabilished",
              "Pay comparible with hospital colleagues"
          ]
        },
        "scorePercentages":{
          "0":42,
          "1":44,
          "2":55,
          "3":56,
          "4":46,
          "5":49,
          "6":59,
          "8":62,
          "9":66,
          "10":71,
          "11":52,
          "12":52,
          "13":52,
          "14":64,
          "15":66,
          "16":66,
          "17":54
        },
        "date":"11 Jul 2017",
        "id":0,
        "framework":"rn"
      }
    },
    {
      id: 1,
      type: 'selfAssessment',
      completed: true,
      date: "10 Jul 2017",
      results: {
        "score":{
            "1":0.75,
            "2":0,
            "3":1,
            "4":1,
            "5":1
        },
        "date":"10 Jul 2017",
        "id":1,
        "framework":"rn",
        "actions":{
            "0":[
              {
                  "title":"Access education targeting areas specific to primary health care",
                  "text":"<p>text ... </p>",
                  "domain":0,
                  "domainLabel":"Clinical Care"
              }
            ],
            "1":[
              {
                  "title":"Incormporate evidence-based tools into your care delivery",
                  "text":"<p>text ... </p>",
                  "domain":1,
                  "domainLabel":"Education"
              }
            ],
            "2":[
              {
                  "title":"Become familiar with quality improvement tools and examples of when to use them.",
                  "text":"<p>text ... </p>",
                  "domain":2,
                  "domainLabel":"Research"
              }
            ],
            "3":[
              {
                  "title":"Complete the online module Leadership in action",
                  "text":"<p>text ... </p>",
                  "domain":3,
                  "domainLabel":"Support"
              }
            ],
            "4":[
              {
                  "title":"Consider joining your local APNA Nurse Network",
                  "text":"<p>text ... </p>",
                  "domain":4,
                  "domainLabel":"Professional Leadership"
              }
            ]
        }
      }
    }
  ]
};


export function scoreSelfAssessmentQuiz(answers){
  /*
    answers are direct aspect of practice score
    domain score is average of all aspect in that domain scores.
    
  */
  // using logic of aspectId being defined as DOMAINID.ASPECTID to
  // group answers.
  /*
  example answer: 
  {
    questionId: 0,
    aspectId: '1.1',
    value: 0.3
  }
  */
  
  // here is me enjoying how weird and permissive javascript is
  // Deal With It
  // !<:0)
  var scores = {};
  var averages = {};
  for (let a of answers){
    var domainId = a.domainId
    if (!scores[domainId]){ scores[domainId] = 0; }
    scores[domainId] += a.value;
  }
  for(let s in scores){
    var numberInDomain = answers.filter((ss,ii) => { return ss.domainId == s }).length;
    averages[s] = scores[s] / numberInDomain;
  }
  

  
  return {
   averages: averages
  };
}

export function scoreCareerQuiz(answers){
  /*
  take questions 4 to 13
  for each sector get diff between answer and ideal, add up
  sort sectors ascending by diff value
  */
  var sectors = defaultPageContentByEndpoint['/quiz/career/scoring'];
  var sectorScores = {};
  
  for (let sec of sectors){
    for (let sectorAnswer of sec.idealAnswers){
      var userAnswer = answers.find((a) => {return a.questionId == sectorAnswer.questionId});
      if (!userAnswer){continue;} // this shouldn't happen but uhh lets ignore it anyway
      if (!sectorScores[sec.sectorId]) { sectorScores[sec.sectorId] = 0; }
      sectorScores[sec.sectorId] += Math.abs(userAnswer.value - sectorAnswer.value);
    }
  }
  return sectorScores;
}

export function dummyFetch(endpoint){
  var self = this;
  return new Promise((res,rej) => {
    setTimeout(() => {
      res({ json: function(){return defaultPageContentByEndpoint[endpoint]} });
    }, 250);
  });
}

