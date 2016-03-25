using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Objects
{
    public class Organization
    {
        public IEnumerable<Account> ActiveAccounts { get; set; }
        public IEnumerable<Account> NewAccounts { get; set; }
    }
}
