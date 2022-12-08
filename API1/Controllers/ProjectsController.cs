using API1.Models;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all projects");
        }


        [HttpGet("{id}")]
        public IActionResult GetbyID(int id)
        {
            return Ok($"Getting projects with id {id}");
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Creating a new project");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"Updating project with id {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting project with id {id}");
        }


        //Demonstrate model binding by route data and by query string

        //[HttpGet]
        //[Route("/api/projects/{pid}/tickets")] //bring slash to override the route rule on the controller
        //public IActionResult GetProjectTicket(int pid, [FromQuery] int tid) //the [FromQuery] syntax specifies explicitly that our tid data comes from a query string
        //{
        //    if(tid == 0)
        //    {
        //        return Ok($"Returning all the tickets with project id {pid}");
        //    }
        //    return Ok($"Getting data with project id {pid} and tickets id {tid}");
        //}


        //Model binding from route and query using an object

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")] //bring slash to override the route rule on the controller
        public IActionResult GetProjectTicket( [FromQuery] Ticket ticket ) //the [FromQuery] syntax specifies explicitly that our entire ticket data comes from a query string but pid is overridden in models to come from route
        {
            if(ticket == null)
            {
                return BadRequest("Parameters not provided properly");
            }
            if (ticket.TicketID == 0)
            {
                return Ok($"Returning all the tickets with project id {ticket.ProjectID}");
            }
            return Ok($"Getting data with project id {ticket.ProjectID}, tickets id {ticket.TicketID}, a title {ticket.Title} and a description {ticket.Description}");
        }
    }
}
