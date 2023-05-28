using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SD_IHW4.Models
{
    /// <summary>
    /// User entity.
    /// </summary>
    [DataContract]
    public class User {
        /// <summary>
        /// User email.
        /// </summary>
        [DataMember, Required]
        public string Email {
            get;
            set;
        }

        /// <summary>
        /// User name.
        /// </summary>
        [DataMember, Required]
        public string UserName {
            get;
            set;
        }

        /// <summary>
        /// User role.
        /// </summary>
        [DataMember, Required]
        public string Role {
            get;
            set;
        }

        public User() {

        }

        public User(string email, string userName, string role) {
            Email = email;
            UserName = userName;
            Role = role;
        }

        public override bool Equals(object obj) {
            return obj is User && this.Email == ((User)obj).Email;
        }
    }
}