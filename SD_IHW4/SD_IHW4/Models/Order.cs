using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SD_IHW4.Models
{
    /// <summary>
    /// Order entity.
    /// </summary>
    [DataContract]
    public class Order
    {
        /// <summary>
        /// Id of the customer.
        /// </summary>
        [DataMember, Required]
        public Int64 UserID {
            get;
            set;
        }

        /// <summary>
        /// Order status.
        /// </summary>
        [DataMember, Required]
        public String Status {
            get;
            set;
        }

        /// <summary>
        /// Order requests.
        /// </summary>
        [DataMember]
        public String SpecialRequests {
            get;
            set;
        }

        /// <summary>
        /// Order creation time.
        /// </summary>
        [DataMember, Required]
        public DateTime CreatedAt {
            get;
            set;
        }

        /// <summary>
        /// Order update time.
        /// </summary>
        [DataMember, Required]
        public DateTime UpdatedAt {
            get;
            set;
        }

        public Order(long userId, string status, string specialRequests) {
            UserID = userId;
            Status = status;
            SpecialRequests = specialRequests;
        }

        public Order(long userId, string status, string specialRequests, DateTime createdAt, DateTime updatedAt) {
            UserID = userId;
            Status = status;
            SpecialRequests = specialRequests;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}