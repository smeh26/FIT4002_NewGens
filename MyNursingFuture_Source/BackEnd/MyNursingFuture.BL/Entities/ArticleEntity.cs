using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.DL.Models;

namespace MyNursingFuture.BL.Entities
{
    public class ArticleEntity:Article, IEntity
    {
        public string CategoryName { get; set; }
        public string DateFormatted { get; set; }
        public List<ContentItemEntity> ContentItems { get; set; }
    }
}
