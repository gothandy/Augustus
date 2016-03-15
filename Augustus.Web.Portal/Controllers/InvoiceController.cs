﻿using Augustus.CRM;
using Augustus.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class InvoiceController : CrmBaseController
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        // GET: Invoice/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            IEnumerable<WorkDoneItemEntity> items;

            using (OrgQueryable org = await GetOrgQueryable())
            {
                ViewBag.Invoice = (from a in org.Invoices
                                   where a.Id == id
                                   select a).Single();

                items = (from i in org.WorkDoneItems
                         where i.InvoiceId == id
                         orderby i.WorkDoneDate descending
                         select i).AsEnumerable();
            }

            return View(items);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
