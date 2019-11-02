
namespace MyNursingFuture.DL.Models
{
    public class Answer:IModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string EmployerText { get; set; }
        public decimal Value { get; set; }
        public bool Active { get; set; }
        public string MatchText { get; set; }
        public string Type { get; set; }
        public string TextValue { get; set; }
    }
}
