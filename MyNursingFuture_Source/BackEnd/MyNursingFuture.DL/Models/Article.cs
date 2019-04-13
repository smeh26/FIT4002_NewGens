using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Article:IModel
    {
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public int LinkId { get; set; }
        public string Title { get; set; }
        public bool Published { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
