using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class LogChange:IModel
    {
        public int LogChangeId { get; set; }
        public string Username { get; set; }
        public string TableName { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Operation { get; set; }
        public int? Identifier { get; set; }
    }
}
