using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        [Required]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int State { get; set; }

        public virtual Student Student { get; set; }
    }
}
