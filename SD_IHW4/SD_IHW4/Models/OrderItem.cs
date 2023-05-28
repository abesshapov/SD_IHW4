using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SD_IHW4.Models
{
    /// <summary>
    /// Order dishes entity.
    /// </summary>
    [DataContract]
    public class OrderItem
    {
        /// <summary>
        /// Dish name.
        /// </summary>
        [DataMember, Required]
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// Amount.
        /// </summary>
        [DataMember, Required]
        public int Quantity {
            get;
            set;
        }
    }
}