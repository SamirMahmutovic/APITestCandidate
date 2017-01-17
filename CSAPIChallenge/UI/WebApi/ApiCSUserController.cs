using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.Models;

namespace UI.WebApi
{
    public class ApiCSUserController : ApiController
    {
        ISiteRepository repository = new SQLSiteRepository();

        public IQueryable<CSUser> GetAllCSUsers()
        {
            var users = repository.GetAllCSUsers();
            return users.AsQueryable<CSUser>();
        }

        public IHttpActionResult GetCSUser(int id)
        {
            CSUser user;
            try
            {
                user = repository.GetCSUserByID(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }

        public IHttpActionResult PostCSUser(CSUser csUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repository.AddCSUser(csUser);
            // sm The return value is a URL link that will return the newly added CSUser
            return CreatedAtRoute("DefaultApi", new { id = csUser.CSUserID }, csUser);
        }

        public IHttpActionResult DeleteCSUser(int id)
        {
            // sm ------ start
            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;

            if (!headers.Contains("secretkey") || (headers.Contains("secretkey")
                && headers.GetValues("secretkey").First() != "secret"))
            {
                // return Unauthorized();  this does not work as response.IsSuccessStatusCode==true  (why????). 
              //  return NotFound();
                return BadRequest();
            }
            // sm end

            CSUser csUser = repository.GetCSUserByID(id);
            if (csUser == null)
            {
                return NotFound();
            }
            repository.DeleteCSUser(csUser);
            return Ok(csUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (repository != null)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
