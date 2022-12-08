using API1.Models.ModelValidations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace API1.Models
{
    public class Ticket
    {
        //[FromQuery(Name ="tid")] //mapping tid from contoller to ticketID property by query string
        //public int TicketID { get; set; }
        //[FromRoute(Name ="pid")]//mapping pid from contoller to ProjectID property by routing
        //public int ProjectID { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }


        public int? TicketID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        [Tickets_EnsureDueDateIsFuture] // using custom annotations to make sure due date is in the future
        [Tickets_EnsureDueDateForTicketOwner] //using custom annotations to make sure duedate is provided
        public DateTime? DueDate { get; set; } //every nuallable field has the hasvalue method to verify if it has a value or not

        public DateTime? EnteredDate { get; set; }
    }
}
