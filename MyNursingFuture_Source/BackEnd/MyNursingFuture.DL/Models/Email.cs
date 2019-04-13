
using System.ComponentModel;

namespace MyNursingFuture.DL.Models
{
    public enum EmailType
    {
        Welcome,
        Report,
        Feedback,
        Contact,
        [Description("Recover Password")]
        RecoverPassword

    }
    public class Email: IModel
    {
        public int EmailId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
