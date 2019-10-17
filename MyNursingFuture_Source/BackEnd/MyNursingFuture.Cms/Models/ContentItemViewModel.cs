using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{

    public enum QuestionTypes
    {
        [Description("Multiple Choice")]
        MULTI,
        [Description("Single Choice")]
        CHOICE,
        [Description("Range Question")]
        RANGE,
        [Description("User Input")]
        INPUT,
        [Description("User Input and Choices")]
        HYBRID
    }

    public enum ContentItemTypes
    {
        //standard items        
        [Category("Default")]
        [Description("Default Item")]
        DEFAULT,
        [Category("Heading")]
        [Description("Heading")]
        HEADING,
        [Category("SingleLineLink")]
        [Description("Single Line Link")]
        SINGLELINELINK,
        [Category("SingleButtonLink")]
        [Description("Single Button Link")]
        SINGLEBUTTONLINK,
        [Category("Video")]
        [Description("Video")]
        VIDEOEMBED,
        [Category("Markup")]
        [Description("Markup")]
        MARKUP,
        [Category("Accordion")]
        [Description("Accordion")]
        ACCORDION,

        //generic items

        [Category("GenericItem")]
        [Description("Share buttons")]
        SHARE,
        [Category("GenericItem")]
        [Description("Sectors List")]
        SECTORSLIST,
        [Category("GenericItem")]
        [Description("Roles List")]
        ROLESLIST,
        [Category("GenericItem")]
        [Description("Reasons Carousel")]
        REASONSLIST,
        [Category("GenericItem")]
        [Description("Postcard Carousel")]
        POSTCARDCAROUSEL,
        [Category("GenericItem")]
        [Description("Policy Button")]
        POLICY,
        [Category("GenericItem")]
        [Description("Domains List")]
        DOMAINSLIST,
        [Category("GenericItem")]
        [Description("Articles List")]
        ARTICLELINKLIST,
        [Category("GenericItem")]
        [Description("Newsletter Signup")]
        NEWSLETTERSIGNUP,
        [Category("GenericItem")]
        [Description("Contact block")]
        CONTACTBLOCK,
        [Category("GenericItem")]
        [Description("Share block")]
        SHAREBLOCK,
        [Category("GenericItem")]
        [Description("Hero Image")]
        HEROIMAGE,
        [Category("GenericItem")]
        [Description("Endorsed Logo")]
        ENDORSEDLOGO,
        [Category("GenericItem")]
        [Description("Auth Nurse")]
        AUTHNURSE,
        [Category("GenericItem")]
        [Description("Auth Employer")]
        AUTHEMPLOYER,
    }

    public class ContentItemViewModel
    {
        public int ContentItemId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public int? SectionId { get; set; }
        public int? ArticleId { get; set; }
        public int Position { get; set; }
        public string Image { get; set; }
        public string Carousel { get; set; }
        public string Video { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string ButtonLink { get; set; }
        public string TitleImage { get; set; }
        [Required]
        public string Operation { get; set; }

        public List<LinkEntity> Links { get; set; }

        public HttpPostedFileBase ImagePosted { get; set; }
        public HttpPostedFileBase TitleImagePosted { get; set; }

    }

    /// <summary>
    /// Profile used for automapper
    /// </summary>
    public class ContentItemsProfile : Profile
    {
        public ContentItemsProfile()
        {
            CreateMap<ContentItemEntity, ContentItemViewModel>().ReverseMap();
        }
    }
}