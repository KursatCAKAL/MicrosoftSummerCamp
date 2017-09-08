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
using Robolife_WebAPI.Models;

namespace Robolife_WebAPI.Controllers
{
    public class BodyAnalizeResultsController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();


        [Route("Robolife/GetBodyAnalizeResult")]
        public IQueryable<BodyAnalizeResult> GetBodyAnalizeResult()
        {
            return db.BodyAnalizeResult;
        }

        [Route("Robolife/GetBodyAnalizeResultById/{id}")]
        [ResponseType(typeof(BodyAnalizeResult))]
        public IHttpActionResult GetBodyAnalizeResult(int id)
        {
            BodyAnalizeResult bodyAnalizeResult = db.BodyAnalizeResult.Find(id);
            
            if (bodyAnalizeResult == null)
            {
                return NotFound();
            }

            return Ok(bodyAnalizeResult);
        }

        [Route("Robolife/PutBodyAnalizeResult/{id},{bodyAnalizeResult}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBodyAnalizeResult(int id, BodyAnalizeResult bodyAnalizeResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bodyAnalizeResult.ID)
            {
                return BadRequest();
            }

            db.Entry(bodyAnalizeResult).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyAnalizeResultExists(id))
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

        [Route("Robolife/PostBodyAnalizeResult/{bodyAnalizeResult}")]
        [ResponseType(typeof(BodyAnalizeResult))]
        public IHttpActionResult PostBodyAnalizeResult(BodyAnalizeResult bodyAnalizeResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BodyAnalizeResult.Add(bodyAnalizeResult);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bodyAnalizeResult.ID }, bodyAnalizeResult);
        }

        [Route("Robolife/DeleteBodyAnalizeResult/{id}")]
        [ResponseType(typeof(BodyAnalizeResult))]
        public IHttpActionResult DeleteBodyAnalizeResult(int id)
        {
            BodyAnalizeResult bodyAnalizeResult = db.BodyAnalizeResult.Find(id);
            if (bodyAnalizeResult == null)
            {
                return NotFound();
            }

            db.BodyAnalizeResult.Remove(bodyAnalizeResult);
            db.SaveChanges();

            return Ok(bodyAnalizeResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BodyAnalizeResultExists(int id)
        {
            return db.BodyAnalizeResult.Count(e => e.ID == id) > 0;
        }
    }
}