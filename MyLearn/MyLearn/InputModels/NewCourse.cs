﻿using System.Collections.Generic;

namespace MyLearn.InputModels
{
    public class NewCourse
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ProfUserId { get; set; }
        public string UniversityId { get; set; }
        public float Group { get; set; }
        public float MinGrade { get; set; }
        public List<Badge> Badges { get; set; }
    }
}