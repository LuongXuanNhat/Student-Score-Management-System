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
    public class SinhViens_62130516Controller : Base_62130516Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: SinhViens
        public async Task<ActionResult> Index()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            var sinhViens = db.SinhViens.Include(s => s.Lop);
            return View(await sinhViens.ToListAsync());
        }

        // GET: SinhViens/Details/5
        public async Task<ActionResult> Details(string id)
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
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public ActionResult Create()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaSV,TenSV,SDT,Email,GioiTinh,MaLop,DaXoa")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.SinhViens.Add(sinhVien);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: SinhViens/Edit/5
        public async Task<ActionResult> Edit(string id)
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
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaSV,TenSV,SDT,Email,GioiTinh,MaLop,DaXoa")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public async Task<ActionResult> Delete(string id)
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
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            db.SinhViens.Remove(sinhVien);
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
