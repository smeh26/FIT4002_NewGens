namespace MyNursingFuture.DL.Models
{
    public class Sector:IModel
    {
        public int SectorId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int LinkId { get; set; }
        public bool Published { get; set; }
    }
}
