using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuHu_Web.Models;

namespace TuHu_Web.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();   

        public ActionResult Index()
        {
            List<cart> carts = new List<cart>();
            if (Session["cartUser"] as List<cart> == null)
            {
                Session["cartUser"] = new List<cart>();
            }
            else
            {
                carts = Session["cartUser"] as List<cart>;
            }

            
            
       

            return View(db.Products.ToList());
        }

        public ActionResult Menu(int index = 0)
        {
            List<cart> carts = new List<cart>();
            if (Session["cartUser"] as List<cart> == null)
            {
                Session["cartUser"] = new List<cart>();
            }
            else
            {
                carts = Session["cartUser"] as List<cart>;
            }

     
            ViewBag.currentIndex = index;
            ViewBag.quantityCart = carts.Count;

            return View(db.Products.ToList());
        }

        public ActionResult FoodDetail(int id = 1)
        {
            List<cart> carts = new List<cart>();
            if (Session["cartUser"] as List<cart> == null)
            {
                Session["cartUser"] = new List<cart>();
            }
            else
            {
                carts = Session["cartUser"] as List<cart>;
            }

           
            ViewBag.quantityCart = carts.Count;

            return View(db.Products.ToList().FirstOrDefault(item => item.Id_Product == id));
        }

        public ActionResult Cart(bool success = false)
        {
            List<cart> carts = Session["cartUser"] as List<cart>;
            ViewBag.listFood = db.Products.ToList();
            ViewBag.quantityCart = carts == null ? 0 : carts.Count;

            if (success) ViewBag.success = success;

            return View(carts);
        }

        public ActionResult InsertCart()
        {
            List<cart> carts = new List<cart>();
            if (Session["cartUser"] as List<cart> == null)
            {
                Session["cartUser"] = new List<cart>();
            }
            else
            {
                carts = Session["cartUser"] as List<cart>;
            }

            string userName = Session["userName"] as string == null ? "admin" : Session["userName"] as string;

            foreach (var item in carts)
            {
                item.id = -1;
                item.food = null;
                item.account = null;


               
                db.SaveChanges();
            }


            Session["cartUser"] = null;

            return RedirectToAction("Cart", new { success = true });
        }

        public ActionResult UpdateCart(int? id, int? quantity, bool plus)
        {
            List<cart> carts = new List<cart>();
            if (Session["cartUser"] as List<cart> == null)
            {
                return RedirectToAction("Cart");
            }
            else
            {
                carts = Session["cartUser"] as List<cart>;
            }

            var checkCart = carts.Find(item => item.idFood == id);

            if (checkCart != null && quantity > 0)
            {
                if (plus)
                {
                    carts[carts.FindIndex(item => item.idFood == checkCart.idFood)].quantity = checkCart.quantity + 1;
                }
                else
                {
                    if (checkCart.quantity - 1 != 0)
                    {
                        carts[carts.FindIndex(item => item.idFood == checkCart.idFood)].quantity = checkCart.quantity - 1;
                    }
                }
            }

            Session["cartUser"] = carts;

            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCart(int? id)
        {
            if (id != null)
            {
                List<cart> carts = new List<cart>();
                if (Session["cartUser"] as List<cart> == null)
                {
                    Session["cartUser"] = new List<cart>();
                }
                else
                {
                    carts = Session["cartUser"] as List<cart>;
                }
                var checkCart = carts.Find(item => item.idFood == id);
                if (checkCart != null)
                {
                    carts.Remove(checkCart);
                }
                Session["cartUser"] = carts;
            }
            return RedirectToAction("Cart");
        }

        public ActionResult AddCart(int? id, string view = "Index", int quantity = 1)
        {
            int index = 1;
            if (id != null)
            {
                List<cart> carts = new List<cart>();
                if (Session["cartUser"] as List<cart> == null)
                {
                    Session["cartUser"] = new List<cart>();
                }
                else
                {
                    carts = Session["cartUser"] as List<cart>;
                }

                var userName = Session["userName"] == null ? "admin" : Session["userName"] as string;
                var checkCart = carts.Find(item => item.idFood == id);

                if (checkCart == null)
                {
                    cart cart = new cart();
                    cart.idFood = id;
                    cart.userName = userName;
                    cart.quantity = quantity;
                    cart.food = db.Products.ToList().Find(item => item.Id_Product == id);
                    cart.account = db.Logins.ToList().Find(item => item.UserName == userName);

                    carts.Add(cart);
                }
                else
                {
                    carts[carts.FindIndex(item => item.id == checkCart.id)].quantity = checkCart.quantity + quantity;
                }



                Session["cartUser"] = carts;

                var idCategory = db.Products.ToList().FirstOrDefault(item => item.Id_Product == id);
          
            }

            if (view != "index") view = "Menu";

            return RedirectToAction(view);
        }
        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult KhuyenMai()
        {
            return View();
        }

    }
}