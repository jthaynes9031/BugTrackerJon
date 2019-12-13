using BugTrackerJon.Helpers;
using BugTrackerJon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerJon.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoles userRoles = new UserRoles();
        private HelperClass helper = new HelperClass();
        private UserProjectsHelper projHelper = new UserProjectsHelper();
        // GET: Admin
        public ActionResult ManageRoles()
        {
     
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");
            var users = new List<ManageRoleViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageRoleViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}",
                    RoleName = userRoles.ListUserRoles(user.Id).FirstOrDefault()
                });
            }
            return View(users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string role)
        {
            //Unenroll all selected users
            foreach (var userId in userIds)
            {
                var userRole = userRoles.ListUserRoles(userId).FirstOrDefault();
                if (userRole != null)
                {
                    userRoles.RemoveUserFromRole(userId, userRole);
                }
            }

            //step 2 add them back to selected role
            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in userIds)
                {
                    userRoles.AddUserToRole(userId, role);

                }

            }
            return RedirectToAction("Dashboard", "Home");

        }
        //Get

        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult ManageProject()
        {
            ViewBag.Projects = new MultiSelectList(db.Projects, "Id", "Name");
            ViewBag.Developers = new MultiSelectList(userRoles.UserInRole("Developer"), "Id", "Email");
            ViewBag.Submitters = new MultiSelectList(userRoles.UserInRole("Submitter"), "Id", "Email");

            if (User.IsInRole("Admin"))
            {
                ViewBag.ProjectManagerId = new SelectList(userRoles.UserInRole("Project_Manager"), "Id", "Email");
            }

            //Create View Model
            var myData = new List<ManageProjectViewModel>();
            ManageProjectViewModel userVm = null;
            foreach (var user in db.Users.ToList())
            {
                userVm = new ManageProjectViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}",
                    ProjectName = projHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                };

                if (userVm.ProjectName.Count() == 0)
                    userVm.ProjectName.Add("N/A");

                myData.Add(userVm);
            };

            return View(myData);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProject(List<int> projects, string projectManagerId, List<string> developers, List<string> submitters)
        {

            if (projects != null)
            {
                foreach (var projectId in projects)
                {

                    foreach (var user in helper.UsersOnProject(projectId).ToList())
                    {
                        projHelper.RemoveUserFromProject(user.Id, projectId);
                    }

                    if (!string.IsNullOrEmpty(projectManagerId))
                    {
                        projHelper.AddUserToProject(projectManagerId, projectId);
                    }

                    if (developers != null)
                    {
                        foreach (var developerId in developers)
                        {
                            projHelper.AddUserToProject(developerId, projectId);
                        }
                    }
                    if (submitters != null)
                    {
                        foreach (var submitterId in submitters)
                        {
                            projHelper.AddUserToProject(submitterId, projectId);
                        }
                    }
                }
            }

            return RedirectToAction("ManageProject");
        }

        //public ActionResult EditUser(string id)
        //{
        //    var user = db.Users.Find(id);
        //    var adminModel = new AdminUser();
        //    UserRoles helper = new UserRoles();
        //    var selected = helper.ListUserRoles(id);
        //    adminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
        //    adminModel.User.Id = user.Id;
        //    adminModel.User.DisplayName = user.DisplayName;

        //    return View(adminModel);
        //}

        //public ActionResult EditUser(AdminUser model)
        //{
        //    var user = db.Users.Find(model.User.Id);
        //    UserRoles helper = new UserRoles();
        //    foreach (var rolemv in db.Roles.Select(r => r.Id).ToList())
        //    {
        //        helper.RemoveUserFromRole(user.Id, rolemv);
        //    }
        //    foreach (var roleadd in db.Roles.Select(r => r.Id).ToList())
        //    {
        //        helper.AddUserToRole(user.Id, roleadd);
        //    }
        //    return RedirectToAction("Index");
        //}

        //[Authorize]
        //public ActionResult index()
        //{
        //    return View();
        //}
    }
}