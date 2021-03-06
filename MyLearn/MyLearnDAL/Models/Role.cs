﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearnDAL.Models
{
    /// <summary>
    /// Role Database model
    /// </summary>
    
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; } 
        public string Description { get; set; }  
    }
}
