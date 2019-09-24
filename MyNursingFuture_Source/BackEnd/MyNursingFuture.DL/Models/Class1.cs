using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    class JobListing: IModel
    {
        public int listingID { get; set; }
        public string title { get; set; }

    }
}
