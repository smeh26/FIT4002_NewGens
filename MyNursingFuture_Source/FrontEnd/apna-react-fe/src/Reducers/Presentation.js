const initialState = {
  sidebarShowing: false,
  locationLabel: 'Home',
  modal: {
    showing: false,
    showingId: '',
    beforeShowScrollY: 0
  },
  sidebarMenu: [
    {
      items: [
        { href: "/whyprimaryhealthcare", text: 'Why primary health?' },
        { href: "#", text: 'Take our career quiz' },
        { href: "#", text: 'Start self assessment' },
        { href: "/articles", text: 'Career advice' },
        { href: "#", text: 'Sign in' }
      ]
    },
    {
      listClass: 'sub-menu',
      items: [
        { href: "#", text: 'Skills & experience' },
        { href: "/sectors", text: 'Roles and sectors' },
        { href: "/explore", text: 'About the framework' },
        { href: "/contactus", text: 'Contact us' },
        { href: "https://www.facebook.com/APNAnurses/", text: 'Join our Facebook group' }
      ]
    },
    {
      listClass: 'sub-menu',
      items: [
        { href: "#", text: 'Terms of Use' },
        { href: "#", text: 'Privacy Policy' },
        { href: "https://www.apna.asn.au/", text: 'Go to APNA\'s main website' }
      ]
    }                
  ]
};

const headerFooterMenus = (state = initialState, action) => {
  switch(action.type) {
    case 'SIDEBAR_TOGGLE':
      return Object.assign({}, state, {
        sidebarShowing: !state.sidebarShowing
      });
    case 'SIDEBAR_CLOSE':
      return Object.assign({}, state, {
        sidebarShowing: false
      });
    case 'LOCATION_LABEL_UPDATE':
      return Object.assign({}, state, {
        locationLabel: action.label
      });
    case 'MODAL_OPEN':
      return Object.assign({}, state, {
        modal: {
          showing: true,
          beforeShowScrollY: action.scrollY,
          showingId: action.id
        }
      });
    case 'MODAL_CLOSE':
      return Object.assign({}, state, {
        modal: {
          showing: false,
          showingId: ''
        }
      });
    case 'SET_SIDEBAR_MENU':
      return Object.assign({}, state, {
        sidebarMenu: action.data
      });
    default:
      return state;
  }
}


export default headerFooterMenus;