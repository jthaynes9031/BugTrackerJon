﻿using BugTrackerJon.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Helpers
{
    public class HelperClass
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }
        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Projects proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);
                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserOnProject(userId, projectId))
            {
                Projects proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);

                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        }
        public ICollection<ApplicationUser> UsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }
        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Project.All(p => p.Id != projectId)).ToList();
        }
    }
    // Duplicate >>>>>>>>>>>>>>>>>>>>>>>>>>>
    public class UserProjectsHelper
    {
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new
       UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db = new ApplicationDbContext();
        public bool IsOnProject(string userId, int projectId)
        {

            if (db.Projects.Find(projectId).Users.Contains(db.Users.Find(userId)))
            {
                return true;
            }
            return false;
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsOnProject(userId, projectId))
            {
                var project = db.Projects.Find(projectId);
                project.Users.Add(db.Users.Find(userId));
                project.AssignedUserId = userId;
                db.Entry(project).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        }
        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsOnProject(userId, projectId))
            {
                var project = db.Projects.Find(projectId);
                project.Users.Remove(db.Users.Find(userId));
                db.Entry(project).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        }
        public ICollection<Projects>ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var projects = user.Project.ToList();
            return (projects);
        }
        public ICollection<ApplicationUser> ListUsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }
        public ICollection<Projects> ListProjectsForUser(string userId)
        {
            return db.Projects.Where(x => x.AssignedUserId == userId).ToList();
        }
        public ICollection<ApplicationUser> ListUsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Project.All(p => p.Id != projectId)).ToList();
        }
    }
}
  

