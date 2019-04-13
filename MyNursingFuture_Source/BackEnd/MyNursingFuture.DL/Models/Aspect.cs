namespace MyNursingFuture.DL.Models
{
    public class Aspect:IModel
    {
        public int AspectId { get; set; }
        public int DomainId { get; set; }
        public int LinkId { get; set; }
        public string Framework { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Examples { get; set; }
        public string OnlineResources { get; set; }
        public string FurtherEducation { get; set; }
        public string PeopleContact { get; set; }
        public string Levels { get; set; }
        public bool Active { get; set; }
        public bool Published { get; set; }
        public int Position { get; set; }
    }
}
