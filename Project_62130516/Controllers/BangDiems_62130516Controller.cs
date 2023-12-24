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
    public class BangDiems_62130516Controller : Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: BangDiems_62130516
        public async Task<ActionResult> Index()
        {
            var bangDiems = db.BangDiems.Include(b => b.HocPhan).Include(b => b.SinhVien);
            return View(await bangDiems.ToListAsync());
        }

        // GET: BangDiems_62130516/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
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
            ViewBag.MaMH = new SelectList(db.HocPhans, "MaHP", "MaHP");
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "MaSV");
            return View();
        }

        // POST: BangDiems_62130516/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MaSV,MaMH,DiemQT,DiemThi")] BangDiem bangDiem)
        {
            if (ModelState.IsValid)
            {
                bangDiem.DiemTong = 1.0*(bangDiem.DiemQT * 0.5 + bangDiem.DiemThi * 0.5);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangDiem bangDiem = await db.BangDiems.FindAsync(id);
            if (bangDiem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMH = new SelectList(db.HocPhans, "MaHP", "MaMon", bangDiem.MaMH);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV", bangDiem.MaSV);
            return View(bangDiem);
        }

        // POST: BangDiems_62130516/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MaSV,MaMH,DiemQT,DiemThi")] BangDiem bangDiem)
        {
            if (ModelState.IsValid)
            {
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
