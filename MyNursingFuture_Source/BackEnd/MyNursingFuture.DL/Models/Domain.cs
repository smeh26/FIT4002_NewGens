
namespace MyNursingFuture.DL.Models
{
    public class Domain: IModel
    {
        public int DomainId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Framework { get; set; }
        public string Text { get; set; }
        public int LinkId { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public bool Published { get; set; }
        public int Position { get; set; }
    }
}
