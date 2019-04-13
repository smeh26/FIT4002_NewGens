using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class ApnaTokenResponse
    {
        public string access_token { get; set; }
    }

    public class ApnaUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberId { get; set; }
        public string Email { get; set; }
        public ApnaUserAddress HomeAddress { get; set; }
        public bool IsMember { get; set; }
    }

    public class ApnaUserAddress
    {
        public string Suburb   { get; set; }
        public string State    { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}