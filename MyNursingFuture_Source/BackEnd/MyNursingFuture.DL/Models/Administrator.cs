using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Administrator
    {
        public int AdministratorId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public bool Sealed { get; set; }
        public string Name { get; set; }
    }
}
