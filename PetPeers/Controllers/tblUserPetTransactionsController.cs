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
    public class tblUserPetTransactionsController : ApiController
    {
        private dbPetPeersEntities db = new dbPetPeersEntities();

        // GET: api/tblUserPetTransactions
        public IQueryable<tblUserPetTransaction> GettblUserPetTransactions()
        {
            return db.tblUserPetTransactions;
        }

        // GET: api/tblUserPetTransactions/5
        [ResponseType(typeof(tblUserPetTransaction))]
        public IHttpActionResult GettblUserPetTransaction(long id)
        {
            tblUserPetTransaction tblUserPetTransaction = db.tblUserPetTransactions.Find(id);
            if (tblUserPetTransaction == null)
            {
                return NotFound();
            }

            return Ok(tblUserPetTransaction);
        }

        // PUT: api/tblUserPetTransactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUserPetTransaction(long id, tblUserPetTransaction tblUserPetTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUserPetTransaction.Id)
            {
                return BadRequest();
            }

            db.Entry(tblUserPetTransaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserPetTransactionExists(id))
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

        // POST: api/tblUserPetTransactions
        [ResponseType(typeof(tblUserPetTransaction))]
        public IHttpActionResult PosttblUserPetTransaction(tblUserPetTransaction tblUserPetTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUserPetTransactions.Add(tblUserPetTransaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblUserPetTransaction.Id }, tblUserPetTransaction);
        }

        // DELETE: api/tblUserPetTransactions/5
        [ResponseType(typeof(tblUserPetTransaction))]
        public IHttpActionResult DeletetblUserPetTransaction(long id)
        {
            tblUserPetTransaction tblUserPetTransaction = db.tblUserPetTransactions.Find(id);
            if (tblUserPetTransaction == null)
            {
                return NotFound();
            }

            db.tblUserPetTransactions.Remove(tblUserPetTransaction);
            db.SaveChanges();

            return Ok(tblUserPetTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserPetTransactionExists(long id)
        {
            return db.tblUserPetTransactions.Count(e => e.Id == id) > 0;
        }
    }
}