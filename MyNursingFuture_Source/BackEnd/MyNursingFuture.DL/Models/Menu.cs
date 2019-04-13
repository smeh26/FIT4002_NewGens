namespace MyNursingFuture.DL.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Position { get; set; }
        public bool Submenu { get; set; }
        public bool Separator { get; set; }
    }
}
