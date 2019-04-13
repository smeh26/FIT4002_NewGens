using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Definition:IModel
    {
        public int DefinitionId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
