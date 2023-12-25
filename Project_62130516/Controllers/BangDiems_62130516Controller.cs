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
using System.Globalization;

namespace Project_62130516.Controllers
{
    public class BangDiems_62130516Controller : Base_62130516Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: BangDiems_62130516
        public async Task<ActionResult> Index(string keyWord = null)
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            var bangDiems = db.BangDiems.Include(b => b.HocPhan).Include(b => b.SinhVien);
            if (!string.IsNullOrEmpty(keyWord))
            {
                bangDiems = bangDiems
                    .Where(b => b.MaSV.Contains(keyWord) || b.MaMH.Contains(keyWord) || b.SinhVien.Lop.TenLop.Contains(keyWord));
            }
            return View(await bangDiems.ToListAsync());
        }

        // GET: BangDiems_62130516/Details/5
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
            BangDiem bangDiem = await db.BangDiems.FindAsync(id);
            if (bangDiem == null)
            {
                return HttpNotFound();
            }
            return View(bangDiem);
        }

        // GET: BangDiems_62130516/Create
        public ActionResult Create()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            ViewBag.MaMH = new SelectList(db.HocPhans, "MaHP", "MaHP");
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "MaSV");
            return View();
        }

        // POST: BangDiems_62130516/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BangDiem bangDiem)
        {
            if (ModelState.IsValid)
            {
                bangDiem.DiemTong = bangDiem.DiemQT * 0.5m + bangDiem.DiemThi * 0.5m;
                bangDiem.Id = Guid.NewGuid();
                db.BangDiems.Add(bangDiem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaMH = new SelectList(db.HocPhans, "MaHP", "MaMon", bangDiem.MaMH);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV", bangDiem.MaSV);
            return View(bangDiem);
        }

        // GET: BangDiems_62130516/Edit/5
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
            BangDiem bangDiem = await db.BangDiems.FindAsync(id);
            if (bangDiem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMH = new SelectList(db.HocPhans, "MaHP", "MaHP", bangDiem.MaMH);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "MaSV", bangDiem.MaSV);
            return View(bangDiem);
        }

        // POST: BangDiems_62130516/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BangDiem bangDiem)
        {
            if (ModelState.IsValid)
            {
                bangDiem.DiemTong = bangDiem.DiemQT * 0.5m + bangDiem.DiemThi * 0.5m;
                db.Entry(bangDiem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaMH = new SelectList(db.HocPhans, "MaHP", "MaMon", bangDiem.MaMH);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV", bangDiem.MaSV);
            return View(bangDiem);
        }

        // GET: BangDiems_62130516/Delete/5
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
            BangDiem bangDiem = await db.BangDiems.FindAsync(id);
            if (bangDiem == null)
            {
                return HttpNotFound();
            }
            return View(bangDiem);
        }

        // POST: BangDiems_62130516/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            BangDiem bangDiem = await db.BangDiems.FindAsync(id);
            db.BangDiems.Remove(bangDiem);
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
