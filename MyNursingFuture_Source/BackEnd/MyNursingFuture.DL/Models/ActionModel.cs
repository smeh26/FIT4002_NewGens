namespace MyNursingFuture.DL.Models
{
    public class ActionModel:IModel
    {
       public int ActionId { get; set; }
       public string Text { get; set; }
       public string Title { get; set; }
       public string Type { get; set; }
    }
}
