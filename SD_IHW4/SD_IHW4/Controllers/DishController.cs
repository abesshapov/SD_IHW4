using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using SD_IHW4.Models;

namespace SD_IHW4.Controllers
{
    /// <summary>
    /// Dishes controller.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class DishController : Controller
    {
        /// <summary>
        /// Dish creation, allowed to manager.
        /// </summary>
        /// <param name="name"> Unique dish name. </param>
        /// <param name="description"> Dish description. </param>
        /// <param name="price"> Dish price. </param>
        /// <param name="quantity"> Amount of dishes. </param>
        /// <param name="token"> Current session token. </param>
        /// <returns> Post result. </returns>
        [HttpPost("create/{token}")]
        public IActionResult Post(string name, string description, decimal price, int quantity, string token) {
            if (name is null || description is null || token is null) {
                return new BadRequestObjectResult("All fields must be filled.");
            }
            Int64 res = AuthManagement.CheckSession(token);
            if (res >= 0 && AuthManagement.GetUserInfo(res).Role == "manager") {
                Dish dish = new Dish(name, description, price, quantity);
                if (DishManagement.CreateDish(dish)) {
                    return new OkObjectResult("Dish created.");
                }
                return new BadRequestObjectResult("Dish is existent.");
            }
            return new BadRequestObjectResult("Unauthorized source.");
        }

        /// <summary>
        /// Amount of dishes correction.
        /// </summary>
        /// <param name="name"> Dish name. </param>
        /// <param name="delta"> Amount of subtraction. </param>
        /// <param name="token"> Session token. </param>
        /// <returns> Operation result. </returns>
        [HttpPost("change_quantity/{token}")]
        public IActionResult Post(string name, int delta, string token) {
            if (name is null) {
                return new BadRequestObjectResult("All fields must be filled.");
            }
            Int64 res = AuthManagement.CheckSession(token);
            if (res >= 0 && AuthManagement.GetUserInfo(res).Role == "manager")
            {
                Int64 dishId = DishManagement.FindDish(name);
                if (dishId < 0) {
                    return new NotFoundObjectResult("Dish is non-existent.");
                }
                int newQuantity = DishManagement.ChangeQuantity(dishId, delta);
                return new OkObjectResult($"Dish corrected, new amount: {newQuantity}");
            }
            return new BadRequestObjectResult("Unauthorized source.");
        }

        /// <summary>
        /// Dish removal.
        /// </summary>
        /// <param name="name"> Dish name. </param>
        /// <param name="token"> Session token. </param>
        /// <returns> Operation result. </returns>
        [HttpPost("delete/{token}")]
        public IActionResult Post(string name, string token) {
            if (name is null || token is null) {
                return new BadRequestObjectResult("All fields must be filled.");
            }
            Int64 res = AuthManagement.CheckSession(token);
            if (res >= 0 && AuthManagement.GetUserInfo(res).Role == "manager") {
                Int64 dishId = DishManagement.FindDish(name);
                if (dishId < 0) {
                    return new NotFoundObjectResult("Dish is non-existent.");
                }
                if (!DishManagement.DeleteDish(dishId)) {
                    return new BadRequestObjectResult("Error surfaced while deleting.");
                }
                return new OkObjectResult("Dish deleted.");
            }
            return new BadRequestObjectResult("Unauthorized source.");
        }

        /// <summary>
        /// Dish information pull.
        /// </summary>
        /// <param name="name"> Dish name. </param>
        /// <param name="token"> Session token. </param>
        /// <returns> Operation result. </returns>
        [HttpGet("info/{token}")]
        public IActionResult Get(string name, string token) {
            if (name is null || token is null) {
                return new BadRequestObjectResult("All fields must be filled.");
            }
            Int64 res = AuthManagement.CheckSession(token);
            if (res >= 0 && AuthManagement.GetUserInfo(res).Role == "manager") {
                Int64 dishId = DishManagement.FindDish(name);
                if (dishId < 0)
                {
                    return new NotFoundObjectResult("Dish is non-existent.");
                }
                Dish dish = DishManagement.GetDishInfo(dishId);
                if (dish == null)
                {
                    return new BadRequestObjectResult("Error surfaced.");
                }
                return new OkObjectResult(dish);
            }
            return new BadRequestObjectResult("Unauthorized source.");
        }

        /// <summary>
        /// Menu pull.
        /// </summary>
        /// <returns> Menu. </returns>
        [HttpGet("menu")]
        public IActionResult Get()
        {
            var menu = DishManagement.GetMenu();
            if (menu.Count == 0) {
                return new NotFoundObjectResult("Menu is empty.");
            }
            return new OkObjectResult(menu);
        }
    }
}