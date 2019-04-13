namespace MyNursingFuture.DL.Models
{
    public class Link: IModel
    {
        public int LinkId { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
