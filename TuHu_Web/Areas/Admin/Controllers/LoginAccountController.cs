using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TuHu_Web.Models;


namespace TuHu_Web.Areas.Admin.Controllers
{
    public class LoginAccountController : Controller
    {
        private Model1 db = new Model1();
        // GET: Admin/LoginAccount
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login data, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                Login check = db.Logins.FirstOrDefault(x => x.UserName == data.UserName && x.Password == data.Password);
                if (check == null)
                {
                    ModelState.AddModelError("", "Tài khoản mật khẩu không đúng!");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(data.UserName, false);
                    if(ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }    
                    else
                    {
                        return Redirect(ReturnUrl);
                    }    
                }
            }
            return View(data);
        }
    }
}