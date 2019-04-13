using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Entities
{
    public class AddressEntity: IEntity
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Suburb { get; set; }

    }
}
