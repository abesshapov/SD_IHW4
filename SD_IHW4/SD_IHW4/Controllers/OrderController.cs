using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SD_IHW4.Models;

namespace SD_IHW4.Controllers
{
    /// <summary>
    /// Orders controller.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class OrdersController : Controller
    {
        [HttpPost("create/{token}")]
        public IActionResult Post(string token, ICollection<OrderItem> dishes, string specialRequests) {
            if (dishes.Count == 0) {
                return new BadRequestObjectResult("Empty order is uncreatable.");
            }
            Int64 userId = AuthManagement.CheckSession(token);
            if (userId < 0) {
                return new BadRequestObjectResult("Unauthorized user.");
            }
            var dishList = new List<(Int64, int, decimal)>();
            foreach (var dishInfo in dishes) {
                Int64 id = DishManagement.FindDish(dishInfo.Name);
                if (id < 0) {
                    return new NotFoundObjectResult($"Dish {dishInfo.Name} is non-existent.");
                }
                var dish = DishManagement.GetDishInfo(id);
                if (dish.Quantity < dishInfo.Quantity) {
                    return new BadRequestObjectResult($"Not enough dishes, dishes left: {dish.Quantity}");
                }

                dishList.Add((id, dishInfo.Quantity, dish.Price));
            }
            Int64 orderId = OrderManagement.CreateOrder(new Order(userId, "in making", specialRequests));
            if (orderId < 0) {
                return new BadRequestObjectResult("Error surfaced while creating an order.");
            }
            foreach (var dishItem in dishList) {
                OrderManagement.CreateOrderDish(new OrderDish(orderId, dishItem.Item1, dishItem.Item2, dishItem.Item3));
                DishManagement.ChangeQuantity(dishItem.Item1, -dishItem.Item2);
            }
            return new OkObjectResult("Order created.");
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(Int64 id)
        {
            var info = OrderManagement.GetOrderInfo(id);
            if (info == null) {
                return new NotFoundObjectResult("Order is non-existent.");
            }

            return new OkObjectResult(info);
        }
    }
}