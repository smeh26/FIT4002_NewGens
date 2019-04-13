using System.Web;
using MyNursingFuture.DL.Models;

namespace MyNursingFuture.BL.Entities
{
    public class ContentItemEntity: ContentItem, IEntity
    {
        public HttpPostedFileBase ImagePosted { get; set; }
        public HttpPostedFileBase TitleImagePosted { get; set; }
        public string ImagePath { get; set; }
        public string TextShort { get; set; }
    }
}
