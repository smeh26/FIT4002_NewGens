using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Reason:IModel
    {
        public int ReasonId { get; set; }
        public string Title { get; set; }
        public int Ix { get; set; }
        public string Text { get; set; }
        public string TextPrev { get; set; }
    }
}
