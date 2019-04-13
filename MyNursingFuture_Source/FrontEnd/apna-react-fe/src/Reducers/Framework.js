const initialState = {
  domain: [],
  sectors: [],
  roles: [],
  aspects: [],
  sectorScores: [],
  glossary: [{term: 'clam', definition: 'its a clam dummy'}],
  actions: [],
  menus: [],
  reasons: [],
  postcards: [],
  endorsedLogos: [],
  currentGlossaryItem: undefined,
  sectorDataLoading: false,
  rolesDataLoading: false,
  domainDataLoading: false,
  aspectsDataLoading: false
}

const framework = (state = initialState, action) => {
  switch(action.type) {
    case 'POPULATE_SECTOR_DATA':
      return Object.assign({}, state, {
        sectors: action.data
      });
    case 'SET_REASONS_DATA': 
      return Object.assign({}, state, {
        reasons: action.data
      });
    case 'SET_POSTCARDS_DATA':
        return Object.assign({}, state, {
            postcards: action.data
        });
    case 'SET_ENDORSEDLOGOS_DATA':
        return Object.assign({}, state, {
            endorsedLogos: action.data
        });
    case 'START_SECTORS_REQUEST':
      return Object.assign({}, state, {
        sectorDataLoading: true
      });
    case 'END_SECTORS_REQUEST':
      return Object.assign({}, state, {
        sectorDataLoading: false
      });
    case 'POPULATE_DOMAIN_DATA':
      return Object.assign({}, state, {
        domain: action.data
      });
    case 'START_DOMAINS_REQUEST':
      return Object.assign({}, state, {
        domainDataLoading: true
      });
    case 'END_DOMAINS_REQUEST':
      return Object.assign({}, state, {
        domainDataLoading: false
      });
    case 'POPULATE_ROLES_DATA':
      return Object.assign({}, state, {
        roles: action.data
      });
    case 'START_ROLES_REQUEST':
      return Object.assign({}, state, {
        rolesDataLoading: true
      });
    case 'END_ROLES_REQUEST':
      return Object.assign({}, state, {
        rolesDataLoading: false
      });
    case 'POPULATE_ASPECTS_DATA':
      return Object.assign({}, state, {
        aspects: action.data
      });
    case 'START_ASPECTS_REQUEST':
      return Object.assign({}, state, {
        aspectsDataLoading: true
      });
    case 'END_ASPECTS_REQUEST':
      return Object.assign({}, state, {
        aspectsDataLoading: false
      });
    case 'SET_GLOSSARY_DATA':
      return Object.assign({}, state, {
        glossary: action.data
      });
    case 'SET_CURRENT_GLOSSARY_ITEM':
      let gi = state.glossary.find((i) => {
          return (i.name.toLowerCase()) == (action.data.toLowerCase())
        });
      return Object.assign({}, state, {
        currentGlossaryItem: gi
      });
    case 'SET_ACTIONS_DATA':
      return Object.assign({}, state, {
        actions: action.data
      });
    case 'SET_SECTOR_SCORES':
      return Object.assign({}, state, {
        sectorScores: action.data
      });
    case 'SET_MENU_DATA':
      return Object.assign({}, state, {
        menus: action.data
      });
    default:
      return state;
  }
}

export default framework;



// /
// {
//   code: 200,
//   data: {
//     success: true,
//     entity: {
//       sectionId: 1,
//       name: 'sectorsAndRoles',
//       title: 'Sectors & Roles',
//       contentItems: [
//         {
//           contentItemId: 0,
//           type: 'default',
//           title: 'Explore the primary health care sectors',
//           text: '<p>bsbb</p><p>gawragrwg</p>'
//         },
//         {
//           contentItemId: 1,
//           type: 'sectorLinkList'
//         },
//         {
//           contentItemId: 2,
//           type: 'buttonLink',
//           text: 'View more sectors',
//           href: '/sectors'
//         },
//         {
//           contentItemId: 3,
//           type: 'default',
//           title: 'Teamwork in primary health care',
//           text: '<p>A guide to the different roles, responsibilities and accountabilities of nursing teams within primary health.</p>'
//         },
//         {
//           contentItemId: 1,
//           type: 'rolesLinkList'
//         }
//       ]
//     },
//     message: 'umm'
//   }
// }

// {
//   domains: [
//     { 
//       framework: '',
//       name: 'Direct Care',
//       description: ''
//     }
//   ],
//   sectors: [
//     {
//       name: 'Correctional Health',
//       contactText: 'bababbaba'
//     }
//   ]
// }