using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.EF.Services
{
    public class ProjectService : IProjectService
    {
        private readonly BugsContext _db;

        public ProjectService(BugsContext db)
        {
            _db = db;
        }
        public async Task AddProjectAsync(Project project)
        {
           await _db.AddAsync(project);
           await _db.SaveChangesAsync();
        }

       public async Task<Project> DeleteProjectAsync(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            _db.Projects.Remove(project);
           await _db.SaveChangesAsync();

            return project;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
           var project = await _db.Projects.ToListAsync(); 
            return project;
        }

        public async Task<Project> GetAProjectAsync(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            return project;
        }

       public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int pid)
        {
            var tickets = await _db.Tickets.Where(t => t.ProjectID == pid).ToListAsync();

            return tickets;
        }

        public async Task UpdateProjectAsync(Project project)
        {
             _db.Entry(project).State = EntityState.Modified;
             
              await  _db.SaveChangesAsync();  
        }
    }
}
