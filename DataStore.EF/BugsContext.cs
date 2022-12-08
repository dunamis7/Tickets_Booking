using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataStore.EF
{
    public class BugsContext : DbContext
    {
        public BugsContext(DbContextOptions options) : base(options) 
        {

        }


        //These properties corresponds to the table in the database
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        //Creating Relationships between tables
        // type override then space to get the onmodel creating function
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tickets)//many tickets
                .WithOne(t => t.Project)// has one project
                .HasForeignKey(t => t.ProjectID); // foreign key

            //seeding(adding initial values to project)
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectID=1, Name="Project 1"},
                new Project { ProjectID=2, Name="Project 2"}
                );

            ///adding initial values to Ticket
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { TicketID=1, Title = "Bug number 1", ProjectID =1},
                new Ticket { TicketID=2, Title = "Bug number 2", ProjectID =1},
                new Ticket { TicketID=3, Title = "Bug number 3", ProjectID =2}
                );
        }
    }
}
