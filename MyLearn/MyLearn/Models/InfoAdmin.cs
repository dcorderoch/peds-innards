using System.Collections.Generic;

namespace MyLearn.Models
{
    public class InfoAdmin
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Tecnologias { get; set; }
        public List<string> Universidades { get; set; }
    }
}