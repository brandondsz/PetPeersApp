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
    // [Route("api")]
    public class tblPetsController : ApiController
    {

        private dbPetPeersEntities db = new dbPetPeersEntities();

        // GET: api/tblPets
        [HttpGet]
        public List<tblPet> GettblPets()
        {

            db.Configuration.ProxyCreationEnabled = false;
            return db.tblPets.ToList();
        }

        // GET: api/tblPets/5
        //[Route("api")]
        [ResponseType(typeof(tblPet))]
        public IHttpActionResult GettblPet(long id)
        {
            tblPet tblPet = db.tblPets.Find(id);
            if (tblPet == null)
            {
                return NotFound();
            }

            return Ok(tblPet);
        }

        // PUT: api/tblPets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblPet(long id, tblPet tblPet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblPet.PetId)
            {
                return BadRequest();
            }

            db.Entry(tblPet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblPetExists(id))
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

        // POST: api/tblPets
        [ResponseType(typeof(tblPet))]
        public IHttpActionResult PosttblPet(tblPet tblPet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblPets.Add(tblPet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblPetExists(tblPet.PetId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblPet.PetId }, tblPet);
        }

        // DELETE: api/tblPets/5
        [ResponseType(typeof(tblPet))]
        public IHttpActionResult DeletetblPet(long id)
        {
            tblPet tblPet = db.tblPets.Find(id);
            if (tblPet == null)
            {
                return NotFound();
            }

            db.tblPets.Remove(tblPet);
            db.SaveChanges();

            return Ok(tblPet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblPetExists(long id)
        {
            return db.tblPets.Count(e => e.PetId == id) > 0;
        }

        [HttpPost]
        public bool BuyPet(int petId, int userId)
        {
            var pet = db.tblPets.FirstOrDefault(x => x.PetId == petId);

            pet.IsSold = true;

            db.SaveChanges();

            db.tblUserPetTransactions.Add(new tblUserPetTransaction
            {
                PetId = petId,
                UserId = userId,
                BuyDate = DateTime.Now,

            });
            db.SaveChanges();
            return true;
        }
    }
}