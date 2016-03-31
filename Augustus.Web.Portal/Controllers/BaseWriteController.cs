using Augustus.CRM;
using Augustus.Domain.Interfaces;
using Augustus.Web.Portal.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public abstract class BaseWriteController<T> : BaseCrmController where T : IDomainObject
    {
        protected string bindInclude;

        protected abstract IQuery<T> GetQuery(CrmContext context);
        protected abstract void SetCreateViewBag(CrmContext context, Guid? parentId);
        protected abstract void SetEditViewBag(CrmContext context, Guid id);
        protected abstract string GetDefaultUrl(Guid id);
        protected abstract string GetParentUrl(Guid? parentId);

        // GET: {Controller}/Create/{id}
        public async Task<ActionResult> Create(Guid? id)
        {
            using (var context = await GetCrmContext())
            {
                var query = GetQuery(context);

                SetCreateViewBag(context, id);

                return View();
            }
        }

        // POST: /{controller}/Create/{id}
        [HttpPost]
        public async Task<ActionResult> Create(Guid? id, T model)
        {
            using (var context = await GetCrmContext())
            {
                if (!ModelState.IsValid)
                {
                    SetCreateViewBag(context, id);
                    return View(model);
                }

                var query = GetQuery(context);
                var newId = query.Create(model);
                return Redirect(GetDefaultUrl(newId));
            }
        }

        // GET: {Controller}/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = GetQuery(context);
                var model = query.GetItem(id);
                SetEditViewBag(context, id);
                return View(model);
            }
        }

        // POST: {Controller}/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, T model)
        {
            // [Bind(Include = bindInclude)] removed for now.

            using (var context = await GetCrmContext())
            {
                if (!ModelState.IsValid)
                {
                    SetEditViewBag(context, id);
                    return View(model);
                }

                var query = GetQuery(context);
                model.Id = id;
                query.Update(model);
                return Redirect(GetDefaultUrl(id));
            }
        }

        // GET: /{controller}/Delete/{id}
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = GetQuery(context);
                var parentId = query.Delete(id);

                return Redirect(GetParentUrl(parentId));
            }
        }
    }
}