using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearnDAL.Models
{
    /// <summary>
    /// User database model
    /// </summary>
    class User
    {

        public Guid UserId { get; set; }
            
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNum { get; set; }

        public byte[] Photo { get; set; }

        public DateTime InDate { get; set; }

        public int TRepo { get; set; }

        public int IsActive { get; set; }

        public virtual Guid LocationId { get; set; }

        public virtual int RoleId { get; set; }

    }
}
