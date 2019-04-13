namespace MyNursingFuture.DL.Models
{
    public class SectorView:IModel
    {
        public int SectorViewId { get; set; }
        public string Type { get; set; }
        public string Framework { get; set; }
        public int SectorId { get; set; }
        public string Intro { get; set; }
        public string Video { get; set; }
        public string MoreStories { get; set; }
        public string CareerPathways { get; set; }
        public string WorkEnvironments { get; set; }
        public string CareerOpportunities { get; set; }
        public string EducationOpportunities { get; set; }
        public string ContactText { get; set; }
        public string OnlineResources { get; set; }
        public bool Active { get; set; }
    }
}
