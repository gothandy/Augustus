using System.Linq;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    public class AccountTools
    {
        public const string Name = "GeneratedProxyCode Account Name";
        public const string Rename = "GeneratedProxyCode Account Renamed";

        private CrmServiceContext context;

        public AccountTools(CrmServiceContext context)
        {
            this.context = context;
            
        }

        public Account Get(string name)
        {
            return context.AccountSet.Single(a => a.Name == name);
        }


        public void Create(string name)
        {
            var account = new Account() { Name = name };
            context.AddObject(account);
            context.SaveChanges();
        }

        public void Update(string oldName, string newName)
        {
            var account = Get(oldName);
            account.Name = newName;
            context.UpdateObject(account);
            context.SaveChanges();
        }

        public void Delete(string name)
        {
            var account = Get(name);
            context.DeleteObject(account);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            DeleteAll(Name);
            DeleteAll(Rename);
        }

        private void DeleteAll(string name)
        {
            var accounts = context.AccountSet.Where(a => a.Name == name);
            foreach(var account in accounts)
            {
                context.DeleteObject(account);
            }
            context.SaveChanges();
        }
    }
}
