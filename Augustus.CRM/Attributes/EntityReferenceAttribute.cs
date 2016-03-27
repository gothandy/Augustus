using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.CRM.Attributes
{
    public class EntityReferenceAttribute : Attribute
    {
        private string logicalName;
        public EntityReferenceAttribute(string logicalName)
        {
            this.logicalName = logicalName;
        }

        public string LogicalName
        {
            get
            {
                return logicalName;
            }
        }
    }
}
