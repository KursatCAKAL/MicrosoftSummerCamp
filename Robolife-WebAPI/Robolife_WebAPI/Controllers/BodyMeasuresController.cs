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
    public class BodyMeasuresController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetBodyMeasure")]
        public IQueryable<BodyMeasure> GetBodyMeasure()
        {
            return db.BodyMeasure;
        }

        [Route("Robolife/GetBodyMeasure/{id}")]
        [ResponseType(typeof(BodyMeasure))]
        public IHttpActionResult GetBodyMeasure(int id)
        {
            BodyMeasure bodyMeasure = db.BodyMeasure.Find(id);
            if (bodyMeasure == null)
            {
                return NotFound();
            }

            return Ok(bodyMeasure);
        }

        [Route("Robolife/PutBodyMeasure/{id},{bodyMeasure}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBodyMeasure(int id, BodyMeasure bodyMeasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bodyMeasure.ID)
            {
                return BadRequest();
            }

            db.Entry(bodyMeasure).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyMeasureExists(id))
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

        [Route("Robolife/PostBodyMeasure/{bodyMeasure}")]
        [ResponseType(typeof(BodyMeasure))]
        public IHttpActionResult PostBodyMeasure(BodyMeasure bodyMeasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BodyMeasure.Add(bodyMeasure);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bodyMeasure.ID }, bodyMeasure);
        }

        [Route("Robolife/DeletBodyMeasure/{id}")]
        [ResponseType(typeof(BodyMeasure))]
        public IHttpActionResult DeleteBodyMeasure(int id)
        {
            BodyMeasure bodyMeasure = db.BodyMeasure.Find(id);
            if (bodyMeasure == null)
            {
                return NotFound();
            }

            db.BodyMeasure.Remove(bodyMeasure);
            db.SaveChanges();

            return Ok(bodyMeasure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BodyMeasureExists(int id)
        {
            return db.BodyMeasure.Count(e => e.ID == id) > 0;
        }
    }
}