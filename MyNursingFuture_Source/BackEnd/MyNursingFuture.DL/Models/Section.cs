namespace MyNursingFuture.DL.Models
{
    public class Section:IModel
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Published { get; set; }
        public bool Sealed { get; set; }
        public int LinkId { get; set; }
    }
}
