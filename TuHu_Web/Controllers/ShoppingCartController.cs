using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHu_Web.Models;

namespace TuHu_Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        Model1 db =new Model1();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialItemCart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                return Json(new {count = cart.Items.Count}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            
            var checkProduct = db.Products.FirstOrDefault(x => x.Id_Product == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id_Product,
                    ProductName = checkProduct.Name_Product,
                    Quantity = quantity
                };

                item.ProductImg = checkProduct.Image;
                
                item.Price = (decimal)checkProduct.Price_Product;
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công!", code = 1, count = cart.Items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id,quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if(checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, count = cart.Items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }   
            return Json(new {Success = false});
        }
    }
}