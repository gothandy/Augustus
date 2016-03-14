using Augustus.CRM;
using Augustus.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.Domain
{
    public class Opportunities
    {
        
        public OrgQueryable Organization { get; set; }
        public DateTime ActiveDate { get; set; }

    }
}
