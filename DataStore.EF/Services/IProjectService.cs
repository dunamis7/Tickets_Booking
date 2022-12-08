using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.EF.Services
{
    public interface IProjectService
    {
        //Add a Project
        Task AddProjectAsync (Project project);

        //Get all Projects
        Task<IEnumerable<Project>> GetAllProjectsAsync();

        //Return a project
        Task<Project> GetAProjectAsync(int id);

        //Delete a project

        Task<Project> DeleteProjectAsync(int id);    

        //Update a Project
        Task UpdateProjectAsync (Project project);

        //Return tickets for a project
        Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int pid);


    }
}
