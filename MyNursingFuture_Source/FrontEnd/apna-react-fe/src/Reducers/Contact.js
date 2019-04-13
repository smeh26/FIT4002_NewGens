const initialState = {
  sectors: {
    'Correctional Health': {
      text: 'We will send our questions as an email to our nurse ambassador for Correctional Health. They will get back to you to arrange a time to talk within 2 business days.'
    }
  }
}

const contact = (state = initialState, action) => {
  switch(action.type) {
    case 'POPULATE_SECTOR_CONTACT_DATA':
      return Object.assign({}, state, {
        sectors: action.sectors
      });
    default:
      return state;
  }
}

export default contact;