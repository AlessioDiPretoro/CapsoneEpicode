using Stones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stones.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        //mostra gli elementi del carrello
        public ActionResult ShowCart()
        {
            if (Session["cart"] != null)
            {
                List<_Cart> cart = (List<_Cart>)Session["cart"];

                List<Product> products = new List<Product>();
                foreach (_Cart p in cart)
                {
                    Product thisP = db.Product.Find(p.idProduct);
                    products.Add(thisP);
                }

                return View(products);
            }
            return View();
        }

        //aggiunge elementi al carrello in sessione
        public ActionResult AddToCart(int id)
        {
            if (Session["cart"] == null)
            {
                List<_Cart> carrello = new List<_Cart>();
                Session["cart"] = carrello;
            }

            List<_Cart> cart = (List<_Cart>)Session["cart"];
            if (!cart.Any(item => item.idProduct == id))
            {
                _Cart item = new _Cart { idProduct = id };
                cart.Add(item);
                Session["cart"] = cart;
                return RedirectToAction("Gallery", "Products");
            }
            else
            {
                TempData["AlreadyInCart"] = "L'articolo è già presente nel carrello";
                return RedirectToAction($"Details/{id}", "Products");
            }
        }

        public ActionResult Delete(int id)
        {
            List<_Cart> cart = (List<_Cart>)Session["cart"];
            _Cart itemToRemove = cart.FirstOrDefault(item => item.idProduct == id);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                Session["cart"] = cart;
            }
            return RedirectToAction("ShowCart");
        }
    }
}