using Augustus.CRM;
using Augustus.Domain.Interfaces;
using Augustus.Web.Portal.Interfaces;
using Augustus.Web.Portal.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public abstract class BaseWriteController<TViewModel, TModel> : BaseCrmController
        where TViewModel : IWriteModelView<TModel>, new()
        where TModel : IDomainObject, new()

    {
        protected string bindInclude;

        protected abstract IQuery<TModel> GetQuery(CrmContext context);
        protected abstract void RefreshCreateViewModel(CrmContext context, Guid? parentId, ref TViewModel model);
        protected abstract void RefreshEditViewModel(CrmContext context, Guid id, ref TViewModel model);
        protected abstract string GetDefaultUrl(Guid id);
        protected abstract string GetParentUrl(Guid? parentId);

        // GET: {Controller}/Create/{id}
        public async Task<ActionResult> Create(Guid? id)
        {
            using (var context = await GetCrmContext())
            {
                TViewModel viewModel = new TViewModel();
                viewModel.DomainModel = new TModel();

                RefreshCreateViewModel(context, id, ref viewModel);

                return View(viewModel);
            }
        }

        // POST: /{controller}/Create/{id}
        [HttpPost]
        public async Task<ActionResult> Create(Guid? id, TViewModel model)
        {
            using (var context = await GetCrmContext())
            {
                if (!ModelState.IsValid)
                {
                    RefreshCreateViewModel(context, id, ref model);
                    return View(model);

                }

                var query = GetQuery(context);
                var newId = query.Create(model.DomainModel);
                return Redirect(GetDefaultUrl(newId));
            }
        }

        // GET: {Controller}/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = GetQuery(context);
                var model = new TViewModel();
                model.DomainModel = query.GetItem(id);
                RefreshEditViewModel(context, id, ref model);
                return View(model);
            }
        }

        // POST: {Controller}/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, TViewModel model)
        {
            // [Bind(Include = bindInclude)] removed for now.

            using (var context = await GetCrmContext())
            {
                if (!ModelState.IsValid)
                {
                    RefreshEditViewModel(context, id, ref model);
                    return View(model);
                }

                var query = GetQuery(context);
                model.DomainModel.Id = id;
                query.Update(model.DomainModel);
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