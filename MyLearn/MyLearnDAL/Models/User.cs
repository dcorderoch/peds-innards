using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    /// <summary>
    /// User database model
    /// </summary>
    [Table("User")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }  
        [MaxLength(50)]
        [Required]     
        [Index(IsUnique = true)]   
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [MaxLength(15)]
        [Required]
        public string PhoneNum { get; set; }

        public byte[] Photo { get; set; }
        [Required]
        public DateTime InDate { get; set; }
        [Required]
        public int TRepo { get; set; }
        [Required]
        public int IsActive { get; set; }
        [ForeignKey("Country")]
        [Required]
        public Guid CountryId { get; set; }
        [ForeignKey("Role")]
        [Required]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Country Country { get; set; }

    }
}
