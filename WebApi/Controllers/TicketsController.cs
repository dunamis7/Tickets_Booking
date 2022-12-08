
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API1.Controllers
{
    [ApiController] //tells the class this is an API controller and not an MVC controller
    [Route("api/tickets")] //tells the compiler to hit this class if the route contains api and the name of the controller(tickets)

    public class TicketsController : ControllerBase //we implement controllerbase for webapi
    {
        private readonly BugsContext _db;

        public TicketsController(BugsContext db)
        {
            _db = db;
        }


        [HttpGet] //attribute for the httpverb for our function
                  // [Route("api/tickets")] //attribute routing for the endpoint to hit 
        public IActionResult Get()
        {
            var ticket = _db.Tickets.ToList();
            return Ok(ticket);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyID(int id)
        {
            var ticket = _db.Tickets.Find(id);
            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            _db.SaveChanges();

            return CreatedAtAction(
             nameof(GetbyID),
             new { id = ticket.TicketID },
             ticket
             );
        }


        //[HttpPost]
        //public IActionResult Create([FromBody] Project project)
        //{
        //    _db.Projects.Add(project);
        //    _db.SaveChanges();

        //    return CreatedAtAction(
        //        nameof(GetbyID),
        //        new { id = project.ProjectID },
        //        project
        //        );

        //}





        [HttpPut("{id}")]
        public IActionResult Put(Ticket ticket, int id)
        {
            //if(id!=ticket.TicketID) return BadRequest();
            //if (_db.Tickets.Find(id) == null) return NotFound();
            //_db.Update(ticket);
            //_db.SaveChanges();
            //return Ok(ticket);


            if (id != ticket.TicketID) return BadRequest();

            _db.Entry(ticket).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                if (_db.Tickets.Find(id) == null) return NotFound();
                throw;
            }


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var ticket = _db.Tickets.Find(id);
           if(ticket==null) return NotFound();

            _db.Tickets.Remove(ticket);
            _db.SaveChanges();
            return Ok(ticket);
            
        }
    }
}
