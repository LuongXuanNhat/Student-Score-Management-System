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
    public class Users_62130516Controller : Base_62130516Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            var users = db.Users.Include(u => u.GiangVien).Include(x=>x.PhanQuyenTaiKhoans);
            var roles = await db.PhanQuyens.ToListAsync();

            foreach (var item in users)
            {
                if(item.PhanQuyenTaiKhoans != null)
                {
                    foreach (var pq in item.PhanQuyenTaiKhoans)
                    {
                        var role = roles.FirstOrDefault(x => x.Id == pq.MaQuyen);
                        if (role != null)
                        {
                            item.TenQuyen += role.TenQuyen;
                        }
                    }
                }
            }
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
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
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (_CurrentUserId == null)
            {
                Session["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account_62130516");
            }
            ViewBag.Id = new SelectList(db.GiangViens, "MaGV", "TenGV");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TenDangNhap,MatKhau,DaXoa")] User user)
        { 
            if (ModelState.IsValid)
            {
                var users = await db.Users.ToListAsync();
                var checkTenDanghap = users.FirstOrDefault(x => x.TenDangNhap.Equals(user.TenDangNhap));
                if (checkTenDanghap != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập bị trùng! Hãy nhập một giá trị khác";
                    return View();
                }
                var checkUserId = users.FirstOrDefault(x => x.Id.Equals(user.Id));
                if (checkUserId != null)
                {
                    ViewBag.ErrorMessage = "Mã người dùng bị trùng! Hãy nhập một giá trị khác";
                    return View();
                }
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.GiangViens, "MaGV", "TenGV", user.Id);
            return View(user);
        }

        // GET: Users/Edit/5
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
            User user = await db.Users.FindAsync(id);
            var quyens = await db.PhanQuyens.ToListAsync();
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.GiangViens, "MaGV", "TenGV", user.Id);
            ViewBag.Quyens = quyens;
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TenDangNhap,MatKhau,DaXoa")] User user, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var users = await db.Users.ToListAsync();
                var checkTenDanghap = users.FirstOrDefault(x => x.TenDangNhap.Equals(user.TenDangNhap));
                if (checkTenDanghap != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập bị trùng! Hãy nhập một giá trị khác";
                    return View();
                }

                string maQuyen = form["TenQuyen"];
                if(maQuyen == null)
                {
                    ViewBag.ErrorMessage = "Quyền bị trống! Hãy nhập một giá trị khác";
                    return View();
                }
                var userRole = new PhanQuyenTaiKhoan()
                {
                    Id = Guid.NewGuid(),
                    MaQuyen = Guid.Parse(maQuyen),
                    UserId = user.Id
                };
                db.PhanQuyenTaiKhoans.Add(userRole);
                db.Entry(user).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.GiangViens, "MaGV", "TenGV", user.Id);
            return View(user);
        }

        // GET: Users/Delete/5
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
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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
