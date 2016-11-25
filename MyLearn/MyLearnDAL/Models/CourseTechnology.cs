﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("CourseTechnology")]
    public class CourseTechnology
    {
        [Key]
        [ForeignKey("Technology")]
        [Column(Order = 0)]
        public Guid TechnologyId { get; set; }
        [Key]
        [ForeignKey("Course")]
        [Column(Order = 1)]
        public Guid CourseId { get; set; }

        public virtual Technology Technology { get; set; }
        public virtual Course Course { get; set; }
    }
}
