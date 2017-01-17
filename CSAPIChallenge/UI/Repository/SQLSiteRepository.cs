using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UI.Models
{
    public class SQLSiteRepository : ISiteRepository
    {
        ApplicationDbContext csUserDB = new ApplicationDbContext();

        public IQueryable<CSUser> GetAllCSUsers()
        {
            return csUserDB.CSUsers;
        }

        public CSUser GetCSUserByID(int CSUserID)
        {
            return csUserDB.CSUsers.FirstOrDefault(mi => mi.CSUserID == CSUserID);
        }

        public void DeleteCSUser(CSUser csUser)
        {
            //Must enable Cascade Delete for threads and posts
            csUserDB.CSUsers.Remove(csUser);
            csUserDB.SaveChanges();
        }

        
        public ApplicationUser GetUserByID(string UserID)
        {
            return csUserDB.Users.Single(u => u.Id == UserID);
        }

        public void AddCSUser(CSUser csUser)  // sm
        {
            csUserDB.CSUsers.Add(csUser);
            csUserDB.SaveChanges();
        }

        

        public void Dispose()
        {
            csUserDB?.Dispose();
        }

        
    }
}
