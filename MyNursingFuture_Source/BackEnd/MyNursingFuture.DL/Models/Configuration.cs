using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime DateModified { get; set; }
    }
}
