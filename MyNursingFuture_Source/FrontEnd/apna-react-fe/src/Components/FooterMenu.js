import React, { Component } from 'react';
import Menu from './Menu';
import { connect } from 'react-redux';

class FooterMenu extends Component {
  constructor(props){
    super(props);
    this.state = {
      menu: [
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
            { href: "/sectors", text: 'Skills & experience' },
            { href: "/sectors", text: 'Roles and sectors' },
            { href: "/explore", text: 'About the framework' },
            { spacer: true },
            { href: "/contactus", text: 'Contact us' },
            { href: "https://www.facebook.com/APNAnurses/", text: 'Join our Facebook group' }
          ]
        },
        {
          listClass: 'sub-menu',
          items: [
            { href: "#", text: 'Terms of Use' },
            { href: "#", text: 'Privacy Policy' },
            { href: "https://www.apna.asn.au/", text: 'Go to APNA\'s main website' },
            { spacer: true }
          ]
        }                
      ]
    };
  }
  
  render() {
    let menuList = [];
    let isFooter = true;
    if (this.props.menus && this.props.menus.length > 0){
      menuList = this.props.menus.filter((i) => { return i.type == 'BOT'});
    }
    return (    
    <div className="col FooterColCount">
      <Menu menuData={menuList} isFooterMenu={isFooter}/>
    </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    menus: state.app.framework.menus,
    loggedIn: state.app.user.loggedIn,
    name: state.app.user.user.name
  }
};

export default connect(mapStateToProps)(FooterMenu);