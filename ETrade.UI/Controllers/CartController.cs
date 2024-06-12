using Etrade.DAL.Abstract;
using Etrade.Data.Models.Entities;
using Etrade.Data.Models.Helpers;
using Etrade.Data.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDAL _productDAL;
        private readonly IOrderDAL _orderDAL;

        public CartController(IProductDAL productDAL, IOrderDAL orderDAL)
        {
            _productDAL = productDAL;
            _orderDAL = orderDAL;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            if (cart == null)
                return View();

            ViewBag.Total = cart.Sum(i => i.Product.Price * i.Quantity).ToString("c");
            SessionHelper.Count = cart.Count;

            return View(cart);
        }
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<CartItem>();
                cart.Add(new CartItem { Product = _productDAL.Get(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = IsExits(cart, id);
                if (index < 0)
                    cart.Add(new CartItem { Product = _productDAL.Get(id), Quantity = 1 });
                else
                    cart[index].Quantity++;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = IsExits(cart, id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int IsExits(List<CartItem> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                    break;
                }
            }
            return -1;
        }
        [Authorize]
        public IActionResult CheckOut()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        [Authorize]
        public IActionResult CheckOut(ShippingDetails entity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunamamaktadır.");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart, entity);
                cart.Clear();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return View("Completed");
            }
            return View(entity);
        }

        private void SaveOrder(List<CartItem> cart, ShippingDetails entity)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Sum(i => i.Product.Price * i.Quantity);
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.Username = entity.UserName;

            order.AddressTitle = entity.AddressTitle;
            order.Address = entity.Address;
            order.City = entity.City;
            order.District = entity.District;
            order.Neighbourhood = entity.Neighbourhood;
            order.PostelCode = entity.PostalCode;

            order.OrderLines = new List<OrderLine>();

            foreach (var item in cart)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Quantity * item.Product.Price;
                orderline.ProductId = item.Product.Id;

                order.OrderLines.Add(orderline);
            }
            _orderDAL.Add(order);
        }
    }
}
