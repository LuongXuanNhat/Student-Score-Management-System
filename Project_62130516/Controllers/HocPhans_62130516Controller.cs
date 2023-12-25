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
    public class HocPhans_62130516Controller : Base_62130516Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: HocPhans_62130516
        public async Task<ActionResult> Index()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            var hocPhans = db.HocPhans.Include(h => h.GiangVien).Include(h => h.MonHoc).Include(x=>x.BangDiems);
            return View(await hocPhans.ToListAsync());
        }

        // GET: HocPhans_62130516/Details/5
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
            HocPhan hocPhan = await db.HocPhans.FindAsync(id);
            if (hocPhan == null)
            {
                return HttpNotFound();
            }
            return View(hocPhan);
        }

        // GET: HocPhans_62130516/Create
        public ActionResult Create()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV");
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMH", "TenMon");
            return View();
        }

        // POST: HocPhans_62130516/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaHP,MaMon,MaGV")] HocPhan hocPhan)
        {
            if (ModelState.IsValid)
            {
                db.HocPhans.Add(hocPhan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", hocPhan.MaGV);
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMH", "TenMon", hocPhan.MaMon);
            return View(hocPhan);
        }

        // GET: HocPhans_62130516/Edit/5
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
            HocPhan hocPhan = await db.HocPhans.FindAsync(id);
            if (hocPhan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", hocPhan.MaGV);
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMH", "TenMon", hocPhan.MaMon);
            return View(hocPhan);
        }

        // POST: HocPhans_62130516/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaHP,MaMon,MaGV")] HocPhan hocPhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocPhan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", hocPhan.MaGV);
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMH", "TenMon", hocPhan.MaMon);
            return View(hocPhan);
        }

        // GET: HocPhans_62130516/Delete/5
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
            HocPhan hocPhan = await db.HocPhans.FindAsync(id);
            if (hocPhan == null)
            {
                return HttpNotFound();
            }
            return View(hocPhan);
        }

        // POST: HocPhans_62130516/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HocPhan hocPhan = await db.HocPhans.FindAsync(id);
            db.HocPhans.Remove(hocPhan);
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
