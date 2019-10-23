import React from 'react';
import Default  from './ContentItems/Default';
import SingleLineLink  from './ContentItems/SingleLineLink';
import ContactBlock from './ContentItems/ContactBlock';
import NewsletterSignup from './ContentItems/NewsletterSignup';
import Heading from './ContentItems/Heading';
import ShareBlock from './ContentItems/ShareBlock';
import MarkupBlock from './ContentItems/MarkupBlock';
import SectorLinkList from './ContentItems/SectorLinkList';
import RolesLinkList from './ContentItems/RolesLinkList';
import ArticleLinkList from './ContentItems/ArticleLinkList';
import ArticleVideoEmbed from './ContentItems/ArticleVideoEmbed';
import VideoEmbed from './ContentItems/VideoEmbed';
import ReasonsList from './ContentItems/ReasonsList';
import PostcardCarousel from './ContentItems/PostcardCarousel';
import Share from './ContentItems/Share';
import FrameworkSelect from './ContentItems/FrameworkSelect';
import DomainLinkList from './ContentItems/DomainLinkList';
import DomainListTitle from './ContentItems/DomainListTitle';
import AcceptTermsAndPrivacy from './ContentItems/AcceptTermsAndPrivacy';
import HeroImage from './ContentItems/HeroImage';
import Endorsedlogo from './ContentItems/Endorsedlogo';
import Accordion from './ContentItems/Accordion';
import AuthNurse from './ContentItems/AuthNurse';
import AuthEmployer from './ContentItems/AuthEmployer';
// import AuthEmployer from './ContentItems/AuthEmployer';
// contentItem types:
// default
// sectorLinkList (semi-static)
// buttonLink
// rolesLinkList (semi-static)
// heading
// contactSectors (static (dynamic list))

var PageContent = (props) => {
  var empty = {title: 'No content.'};
  if (!props.content || props.content.length == 0){
    return <Default content={empty} />
  }

  var cm = props.content.map((contentItem, index) => {
    if (contentItem.link && contentItem.link.constructor == String){contentItem.link = JSON.parse(contentItem.link);}
    if (contentItem.buttonLink && contentItem.buttonLink.constructor == String){contentItem.buttonLink = JSON.parse(contentItem.buttonLink);}
    switch(contentItem.type){
      case 'DEFAULT':
        return <Default content={contentItem} key={index} />
      case 'HEADING':
        return <Heading content={contentItem} key={index} />
      case 'SINGLELINELINK':
        return <SingleLineLink content={contentItem} key={index} />
      case 'SECTORSLIST':
        return <SectorLinkList key={index}/>
      case 'HEROIMAGE':
        return <HeroImage key={index}/>      
      case 'AUTHNURSE':
            return <AuthNurse key={index}/>  
      case 'AUTHEMPLOYER':
            return <AuthEmployer key={index}/>   
      case 'ENDORSEDLOGO':
        return <Endorsedlogo key={index}/>      
      case 'ROLESLIST':
        return <RolesLinkList key={index} />
      case 'ARTICLELINKLIST':
        return <ArticleLinkList key={index} />
      case 'CONTACTBLOCK':
        return <ContactBlock key={index} />
      case 'NEWSLETTERSIGNUP':
        return <NewsletterSignup key={index} />
      case 'SHAREBLOCK':
        return <ShareBlock key={index} />
      case 'MARKUP':
        return <MarkupBlock content={contentItem} key={index}/>
      case 'ARTICLEVIDEOEMBED':
        return <ArticleVideoEmbed key={index}/>
      case 'VIDEOEMBED':
        return <VideoEmbed key={index} content={contentItem} />
      case 'REASONSLIST':
          return <ReasonsList key={index} carousel={contentItem.carousel} variation={contentItem.variation} />
      case 'POSTCARDCAROUSEL':
          return <PostcardCarousel key={index} carousel={contentItem.carousel} variation={contentItem.variation} />
      case 'SHARE':
        return <Share key={index} />
      case 'FRAMEWORKSELECT':
        return <FrameworkSelect key={index} />
      case 'DOMAINLINKLIST':
        return <DomainLinkList key={index} content={contentItem} />
      case 'DOMAINLISTTITLE':
        return <DomainListTitle key={index} content={contentItem} />
      case 'POLICY':
        return <AcceptTermsAndPrivacy key={index} content={contentItem} />
      case 'ACCORDION':
        let child = <MarkupBlock content={contentItem} />
        return (
          <Accordion key={index} title={contentItem.title} child={child}  />
        )
      default:
        return <p key={index}>Undefined contentItem type</p>
    }
  });

  return (
    <div className="page-content">
      {cm.map(ri => 
        ri
      )}
    </div> 
  );
}

export default PageContent;