
using Core.Models;
using DataStore.EF;
using DataStore.EF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;


        // use ctrl . to help create the field after the creation of the constructor
        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {
            var project = await _service.GetAllProjectsAsync();
            return Ok(project);
        }


        [HttpGet("{id}")]
        public async Task <IActionResult> GetbyID(int id)
        {
            var project = await _service.GetAProjectAsync(id); 
            if(project == null)
            {
                return NotFound(); // if project is not found, return the NotFound method
            }
            return Ok(project);// if project is found, return it back to the user
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody]Project project)
        {
            await _service.AddProjectAsync(project);
       

            return CreatedAtAction(
                nameof(GetbyID),
                new { id = project.ProjectID},
                project
                );
           
        }



        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, Project project)
        {
            if (id != project.ProjectID) return BadRequest();

           // var data = await _service.GetAProjectAsync(id);

           // if (data == null) return NotFound();

            await _service.UpdateProjectAsync(project);
   
      
        
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task  <IActionResult> Delete(int id)
        {
            var project = _service.GetAProjectAsync(id);
            if (project == null) return NotFound();

            await _service.DeleteProjectAsync(id);

            return Ok(project);
        }





        [HttpGet]
        [Route("/api/projects/{pid}/tickets")] //bring slash to override the route rule on the controller
        public async Task <IActionResult> GetProjectTickets(int pID)
        {
            var tickets = await _service.GetProjectTicketsAsync(pID);

      
            if (tickets == null)
            {
                return NotFound();
            }

            return Ok(tickets);
        }
    }
}
