using Project_62130516.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project_62130516.Controllers
{
    public class Account_62130516Controller : Base_62130516Controller
    {
        private Project_62130516Entities db = new Project_62130516Entities();
        // GET: Account_62130516
        public ActionResult Login()
        {
            if(_CurrentUserId != null)
            {
                return Logout();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "TenDangNhap,MatKhau")] User user)
        {
            string returnUrl = Session["ReturnUrl"] as string;
            var check = await db.Users.FirstOrDefaultAsync(x => x.TenDangNhap.Equals(user.TenDangNhap));
            if (check != null)
            {
                if (check.MatKhau.Equals(user.MatKhau))
                {
                    var userrole = await db.PhanQuyenTaiKhoans.FirstOrDefaultAsync(x => x.UserId.Equals(check.Id));
                    if(userrole != null)
                    {
                        var id = userrole.MaQuyen;
                        var role = db.PhanQuyens.FirstOrDefault(x => x.Id.Equals(id));
                        Session["Role"] = role.TenQuyen;
                    }
                    Session["UserId"] = check.Id;
                    if(returnUrl != null)
                    {
                        Session.Remove("ReturnUrl");
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "BangDiems_62130516");
                } else
                {
                    ViewBag.ErrorMessage = "Mật khẩu không đúng";
                    return View("Login");
                }
                
            } else
            {
                ViewBag.ErrorMessage = "Tên đăng nhập không đúng";
                return View("Login");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,TenDangNhap,MatKhau")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.GiangViens, "MaGV", "TenGV", user.Id);
            return View(user);
        }
        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("Role");
            return RedirectToAction("Login");
        }
    }
}