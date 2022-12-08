using API1.Filters;
using API1.Models;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [ApiController] //tells the class this is an API controller and not an MVC controller
    [Route("api/tickets")] //tells the compiler to hit this class if the route contains api and the name of the controller(tickets)
   // [Version1DiscontinueResourceFilter] //check if condition in the Version1DiscontinueResourceFilter class is met
    public class TicketsController : ControllerBase //we implement controllerbase for webapi
    {
        [HttpGet] //attribute for the httpverb for our function
                  // [Route("api/tickets")] //attribute routing for the endpoint to hit 
        public IActionResult Get()
        {
            return Ok("Reading all the tickets");
        }

        [HttpGet("{id}")]
        public IActionResult GetbyID(int id)
        {
            return Ok($"Returning ticket with id {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket) //find out why [FromBody did not work]
        {
            return Ok(ticket); // Automatically, objects are serialized in json
        }


        [HttpPost]
        [Route("/api/v2/tickets")] //creating a version two to apply action filter validations
        [Tickets_EnsureEnteredDate]//action filter to ensure that version two will have an entered date when there is an owner
        [Tickets_EnsureEnteredDateIsFuture] //acttion filter to ensure date for creating ticket is today or in the future
        [Tickets_EnteredDateBeforeDueDate]
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            return Ok(ticket); // Automatically, objects are serialized in json
        }



        [HttpPut]
        public IActionResult Put([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting a ticket with id {id}");
        }
    }
}
