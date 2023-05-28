using System;

namespace SD_IHW4.Models
{
    /// <summary>
    /// Dish of the order entity.
    /// </summary>
    public class OrderDish
    {
        /// <summary>
        /// Order id.
        /// </summary>
        public Int64 OrderID {
            get;
        }

        /// <summary>
        /// Dish of the order id.
        /// </summary>
        public Int64 DishID {
            get;
        }

        /// <summary>
        /// Amount.
        /// </summary>
        public int Quantity {
            get;
        }

        /// <summary>
        /// Order price.
        /// </summary>
        public decimal Price {
            get;
        }

        public OrderDish(Int64 orderId, Int64 dishId, int quantity, decimal price) {
            OrderID = orderId;
            DishID = dishId;
            Quantity = quantity;
            Price = price;
        }
    }
}