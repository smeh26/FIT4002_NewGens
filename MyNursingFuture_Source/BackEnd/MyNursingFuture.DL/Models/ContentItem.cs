
namespace MyNursingFuture.DL.Models
{
    public class ContentItem : IModel
    {
        public int ContentItemId { get; set; }
        public int? SectionId { get; set; }
        public int? ArticleId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public string Image { get; set; }
        public string Carousel { get; set; }
        public string Link { get; set; }
        public string ButtonLink { get; set; }
        public string TitleImage { get; set; }
        public string Video { get; set; }
    }
}
