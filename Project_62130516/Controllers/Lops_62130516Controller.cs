using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_62130516.Models;

namespace Project_62130516.Controllers
{
    public class Lops_62130516Controller : Base_62130516Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: Lops_62130516
        public async Task<ActionResult> Index()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            return View(await db.Lops.ToListAsync());
        }

        // GET: Lops_62130516/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = await db.Lops.FindAsync(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Lops_62130516/Create
        public ActionResult Create()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            return View();
        }

        // POST: Lops_62130516/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaLop,TenLop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                lop.MaLop = Guid.NewGuid();
                db.Lops.Add(lop);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lop);
        }

        // GET: Lops_62130516/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = await db.Lops.FindAsync(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops_62130516/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaLop,TenLop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lop);
        }

        // GET: Lops_62130516/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = await db.Lops.FindAsync(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops_62130516/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Lop lop = await db.Lops.FindAsync(id);
            db.Lops.Remove(lop);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
