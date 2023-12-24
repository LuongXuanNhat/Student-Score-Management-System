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
    public class GiangViens_62130516Controller : Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: GiangViens_62130516
        public async Task<ActionResult> Index()
        {
            var giangViens = db.GiangViens.Include(g => g.User).Include(x=>x.HocPhans);
            return View(await giangViens.ToListAsync());
        }

        // GET: GiangViens_62130516/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = await db.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // GET: GiangViens_62130516/Create
        public ActionResult Create()
        {
            var magv = db.Users.Include(x => x.PhanQuyenTaiKhoans);
            var roleAmin = db.PhanQuyens.FirstOrDefault(x=>x.TenQuyen.ToLower().Equals("admin"));
            magv = magv.Where(x => x.PhanQuyenTaiKhoans.Any(y=> y.MaQuyen != roleAmin.Id));

            ViewBag.MaGV = new SelectList(magv, "Id", "TenDangNhap");
            return View();
        }

        // POST: GiangViens_62130516/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaGV,TenGV,SDT,Email,DateOfBirth,DaXoa")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                db.GiangViens.Add(giangVien);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.Users, "Id", "TenDangNhap", giangVien.MaGV);
            return View(giangVien);
        }

        // GET: GiangViens_62130516/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = await db.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.Users, "Id", "TenDangNhap", giangVien.MaGV);
            return View(giangVien);
        }

        // POST: GiangViens_62130516/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaGV,TenGV,SDT,Email,DateOfBirth,DaXoa")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangVien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.Users, "Id", "TenDangNhap", giangVien.MaGV);
            return View(giangVien);
        }

        // GET: GiangViens_62130516/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = await db.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // POST: GiangViens_62130516/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            GiangVien giangVien = await db.GiangViens.FindAsync(id);
            db.GiangViens.Remove(giangVien);
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
