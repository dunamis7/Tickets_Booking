using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        [Required]
        [StringLength(50)] //Limit string length to 50
        public string Name { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
