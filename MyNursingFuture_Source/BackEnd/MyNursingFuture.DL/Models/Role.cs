namespace MyNursingFuture.DL.Models
{
    public class Role:IModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int LinkId { get; set; }
        public string LinkName { get; set; }
        public string WhatIs { get; set; }
        public string WhatIsTheirRole { get; set; }
        public string Accountabilities { get; set; }
        public string Examples { get; set; }
        public string FurtherInformation { get; set; }
        public string Pathways { get; set; }
        public bool Published { get; set; }
    }
}
