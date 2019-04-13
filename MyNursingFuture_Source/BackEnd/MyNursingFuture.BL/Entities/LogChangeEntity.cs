using MyNursingFuture.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Entities
{
    public class LogChangeEntity:LogChange
    {
        
    }
    public class LogChangeRows
    {
        public IEnumerable<LogChangeEntity> List { get; set; }
        public int Rows { get; set; }
    }
}
