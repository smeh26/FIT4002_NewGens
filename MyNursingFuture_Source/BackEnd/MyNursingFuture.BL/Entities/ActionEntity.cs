using MyNursingFuture.DL.Models;

namespace MyNursingFuture.BL.Entities
{
    public class ActionEntity:ActionModel, IEntity
    {
        public int Position { get; set; }
        public int LevelAction { get; set; }
        public bool Created { get; set; }
        public bool Removed { get; set; }
        public bool Added { get; set; }
        public int AspectId { get; set; }
        public int DomainId { get; set; }
    }
}
