using Microsoft.Xrm.Sdk;
using System.Linq;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    public class OpportunityTools
    {
        public const string Name = "GeneratedProxyCode Opportunity Name";
        public const string Rename = "GeneratedProxyCode Opportunity Renamed";

        private CrmServiceContext context;

        public OpportunityTools(CrmServiceContext context)
        {
            this.context = context;
            
        }

        public Opportunity Get(string name)
        {
            return context.OpportunitySet.Single(o => o.Name == name);
        }


        public void Create(string name, Account account)
        {
            var entRef = new EntityReference("account", account.Id);

            var entity = new Opportunity() { Name = name, CustomerId = entRef };

            context.AddObject(entity);
            context.SaveChanges();
        }

        public void Update(string oldName, string newName)
        {
            var entity = Get(oldName);
            entity.Name = newName;
            context.UpdateObject(entity);
            context.SaveChanges();
        }

        public void Delete(string name)
        {
            var entity = Get(name);
            context.DeleteObject(entity);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            DeleteAll(Name);
            DeleteAll(Rename);
        }

        private void DeleteAll(string name)
        {
            var entities = context.OpportunitySet.Where(a => a.Name == name);
            foreach(var entity in entities)
            {
                context.DeleteObject(entity);
            }
            context.SaveChanges();
        }
    }
}
