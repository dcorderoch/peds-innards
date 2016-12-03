using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class StudentStats
    {
        public decimal PromedioProyectos { get; set; }
        public decimal PromedioCursos { get; set; }
        public int CursosAprobados { get; set; }
        public int CursosReprobados { get; set; }
        public int ProyectosExitosos { get; set; }
        public int ProyectosFallidos { get; set; }
    }
}