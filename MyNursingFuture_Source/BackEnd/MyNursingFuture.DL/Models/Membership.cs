using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Membership : IModel
    {
        public int MembershipID { get; set; }
        public int EmployerID { get; set; }
        public string MembershipType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
