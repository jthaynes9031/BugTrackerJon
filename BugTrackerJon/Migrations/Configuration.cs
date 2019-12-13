using BugTrackerJon.Models;

namespace BugTrackerJon.Migrations
{
    using BugTrackerJon.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTrackerJon.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BugTrackerJon.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            #region Role Creation
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Project_Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project_Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "demoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoDeveloper@mailinator.com",
                    Email = "demoDeveloper@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "demoDeveloper",
                }, "LearnToCodeNow!");
            }

            if (!context.Users.Any(u => u.Email == "demoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoSubmitter@mailinator.com",
                    Email = "demoSubmitter@mailinator.com",
                    FirstName = "demo",
                    LastName = "Submitter",
                    DisplayName = "demoSubmitter",
                }, "LearnToCodeNow!");
            }

            if (!context.Users.Any(u => u.Email == "demoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoAdmin@mailinator.com",
                    Email = "demoAdmin@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "DemoAdmin",
                }, "LearnToCodeNow!");
            }


            if (!context.Users.Any(u => u.Email == "demoPM@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoPM@mailinator.com",
                    Email = "demoPM@mailinator.com",
                    FirstName = "Demo",
                    LastName = "ProjectManager",
                    DisplayName = "DemoPM",
                }, "LearnToCodeNow!");
            }

            if (!context.Users.Any(u => u.Email == "jthaynes9031@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jthaynes9031@gmail.com",
                    Email = "jthaynes9031@gmail.com",
                    FirstName = "Jonathan",
                    LastName = "Haynes",
                    DisplayName = "Jon",
                }, "Abc&123!");
            }

            var adminId = userManager.FindByEmail("jthaynes9031@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            adminId = userManager.FindByEmail("demoAdmin@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var projectManagerId = userManager.FindByEmail("demoPM@mailinator.com").Id;
            userManager.AddToRole(projectManagerId, "Project_Manager");

            var developerId = userManager.FindByEmail("demoDeveloper@mailinator.com").Id;
            userManager.AddToRole(developerId, "Developer");

            var submitterId = userManager.FindByEmail("demoSubmitter@mailinator.com").Id;
            userManager.AddToRole(submitterId, "Submitter");



            context.TicketStatus.AddOrUpdate(
            u => u.StatusName,
            new TicketStatus { StatusName = "Open", Description = "A newly created or simply unassigned ticket" },
            new TicketStatus { StatusName = "Assigned", Description = "A Ticket that has been assigned but has yet to be worked on" },
            new TicketStatus { StatusName = "In Progress", Description = "A Ticket that is being worked on" },
            new TicketStatus { StatusName = "Resolved", Description = "A Ticket that has been completed" },
            new TicketStatus { StatusName = "Archived", Description = "A ticket that has been Archived" }
            );
            context.TicketPriorities.AddOrUpdate(
            u => u.PriorityName,
            new TicketPriorities { PriorityName = "Low"},
            new TicketPriorities { PriorityName = "Medium"},
            new TicketPriorities { PriorityName = "High"},
            new TicketPriorities { PriorityName = "Very High"},
            new TicketPriorities { PriorityName = "I NEED IT"}
            );
            context.TicketTypes.AddOrUpdate(
            u => u.TypeName,
            new TicketType { TypeName = "Bug" },
            new TicketType { TypeName = "Feature Update" },
            new TicketType { TypeName = "Documentation Request" },
            new TicketType { TypeName = "Complaint" }
            );
        }

    }
}
#endregion