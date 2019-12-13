using BugTrackerJon.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Helpers
{
    public class UserHelper
    {
            private static ApplicationDbContext db = new ApplicationDbContext();

            public static string GetAvatarPath()
            {
                return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).AvatarPath;
            }
            public static string GetUserFirstName()
            {
                return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).FirstName;
            }
            public static string GetUserLastName()
            {
                return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).LastName;
            }
            public static ApplicationUser GetUserId()
            {
                var userId = db.Users.Find(HttpContext.Current.User.Identity.GetUserId());
                return userId;

            }
    }
}