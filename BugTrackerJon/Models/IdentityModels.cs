using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTrackerJon.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name ="Display Name")]
        public string DisplayName { get; set; }
        public string AvatarPath { get; set; }

        public ApplicationUser()
        {
            //Notifications = new HashSet<TicketNotifications>();
            Histories = new HashSet<TicketHistories>();
            Attachments = new HashSet<TicketAttachments>();
            Comments = new HashSet<TicketComments>();
            Project = new HashSet<Projects>();
        }
        public virtual ICollection<Projects> Project { get; set; }
        //public virtual ICollection<TicketNotifications>Notifications { get; set; }
        public virtual ICollection<TicketComments>Comments { get; set; }
        public virtual ICollection<TicketHistories>Histories { get; set; }
        public virtual ICollection<TicketAttachments>Attachments { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.Tickets> Tickets { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.Projects> Projects { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketStatus> TicketStatus { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketAttachments> TicketAttachments { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketComments> TicketComments { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketHistories> TicketHistories { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketNotifications> TicketNotifications { get; set; }

        public System.Data.Entity.DbSet<BugTrackerJon.Models.TicketPriorities> TicketPriorities { get; set; }
    }
}