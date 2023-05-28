using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SD_IHW4.Models
{
    /// <summary>
    /// Dish entity.
    /// </summary>
    [DataContract]
    public class Dish {
        /// <summary>
        /// Name of the dish.
        /// </summary>
        [DataMember, Required]
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// Dish description.
        /// </summary>
        [DataMember, Required]
        public string Description {
            get;
            set;
        }

        /// <summary>
        /// Dish price.
        /// </summary>
        [DataMember, Required]
        public decimal Price {
            get;
            set;
        }

        /// <summary>
        /// Amount of dishes.
        /// </summary>
        [DataMember, Required]
        public int Quantity {
            get;
            set;
        }

        /// <summary>
        /// Dish creation.
        /// </summary>
        /// <param name="name">Dish name.</param>
        /// <param name="description">Dish description.</param>
        /// <param name="price">Dish price.</param>
        /// <param name="quantity">Amount of dishes.</param>
        public Dish(string name, string description, decimal price, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}