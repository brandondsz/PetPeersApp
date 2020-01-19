using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PetPeers.Models;

namespace PetPeers.Controllers
{
    public class tblUsersController : ApiController
    {
        private dbPetPeersEntities db = new dbPetPeersEntities();

        // GET: api/tblUsers
        public IQueryable<tblUser> GettblUsers()
        {
            return db.tblUsers;
        }

        public string Login(string username, string password)
        {
            try
            {
                var user = db.tblUsers.FirstOrDefault(x => x.UserName == username);
                if (user == null)
                {
                    throw new Exception($"Invalid UserName. User {username} does not exist.");
                }

                if (user.Password != password)
                {
                    throw new Exception($"Invalid Credentials please check your username and password.");
                }
                //add logic to signin
                return "True";
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IHttpActionResult Register(int id, tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUsers.Add(tblUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblUserExists(tblUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblUser.UserId }, tblUser);
        }

        // GET: api/tblUsers/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult GettblUser(long id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return Ok(tblUser);
        }

        // PUT: api/tblUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUser(long id, tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUser.UserId)
            {
                return BadRequest();
            }

            db.Entry(tblUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblUsers
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult PosttblUser(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUsers.Add(tblUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblUserExists(tblUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblUser.UserId }, tblUser);
        }

        // DELETE: api/tblUsers/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult DeletetblUser(long id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            db.SaveChanges();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(long id)
        {
            return db.tblUsers.Count(e => e.UserId == id) > 0;
        }
    }
}