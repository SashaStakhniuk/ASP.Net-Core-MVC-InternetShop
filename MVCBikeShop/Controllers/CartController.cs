using Microsoft.AspNetCore.Mvc;
using MVCBikeShop.Extensions;
using MVCBikeShop.Models;
using MVCBikeShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Controllers
{
    public class CartController : Controller
    {
        BikeContext context;
        public CartController(BikeContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string returnUrl)
        {
            Cart cart = GetCart();
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public IActionResult AddToCart(int bikeId,string returnUrl)
        {
            Bike bike = context.Bikes.FirstOrDefault(x => x.BikeId == bikeId);
            if (bike != null)
            {
                //додати в корзину
                var cart = GetCart();
                cart.AddItem(bike, 1);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index",new { returnUrl });
        }
        public IActionResult RemoveOneFromCart(int bikeId, string returnUrl)
        {
            Bike bike = context.Bikes.FirstOrDefault(x => x.BikeId == bikeId);
            if (bike != null)
            {
                //видалити із корзини
                var cart = GetCart();
                cart.AddItem(bike, -1);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public IActionResult RemoveFromCart(int bikeId, string returnUrl)
        {
            Bike bike = context.Bikes.FirstOrDefault(x => x.BikeId == bikeId);
            if(bike!=null)
            {
                //видалити із корзини
                var cart = GetCart();
                cart.RemoveLine(bike);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.SetObjectAsJson("Cart",cart);
            }
            return cart;
        }
    }
   
}
