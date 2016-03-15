using Augustus.CRM;
using Augustus.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class OpportunitiesQuery
    {
        
        public OrgQueryable Organization { get; set; }
        public DateTime ActiveDate { get; set; }

    }
}
