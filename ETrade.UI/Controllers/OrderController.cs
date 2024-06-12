using Etrade.DAL.Abstract;
using Etrade.Data.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderDAL _IorderDAL;

        public OrderController(IOrderDAL orderDAL)
        {
            _IorderDAL = orderDAL;
        }

        public IActionResult Index()
        {
            return View(_IorderDAL.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_IorderDAL.Get(id));
        }

        public IActionResult Edit(int id)
        {
            var order = _IorderDAL.Get(id);
            var model = new OrderStateViewModel()
            {
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                IsCompleted = order.OrderState == EnumOrderState.Completed,
                OrderState = order.OrderState
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(OrderStateViewModel model)
        {
            var order = _IorderDAL.Get(model.OrderId);

            if (model.IsCompleted)
            {
                order.OrderState = EnumOrderState.Completed;
            }
            else
            {
                switch (order.OrderState)
                {
                    case EnumOrderState.Waiting:
                        order.OrderState = EnumOrderState.Waiting;
                        break;
                    case EnumOrderState.Preparing:
                        order.OrderState = EnumOrderState.Preparing;
                        break;
                    case EnumOrderState.Shipped:
                        order.OrderState = EnumOrderState.Shipped;
                        break;
                    case EnumOrderState.Completed:
                        order.OrderState = EnumOrderState.Completed;
                        break;
                }
            }

            _IorderDAL.Update(order);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var order = _IorderDAL.Get(id);

            if (order != null)
                _IorderDAL.Delete(order);

            return RedirectToAction("Index");
        }
    }
}
