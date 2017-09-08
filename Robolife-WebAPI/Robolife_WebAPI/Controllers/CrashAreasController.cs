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
    public class CrashAreasController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetCrashArea")]
        public IQueryable<CrashArea> GetCrashArea()
        {
            return db.CrashArea;
        }

        [Route("Robolife/GetCrashAreaById/{id}")]
        [ResponseType(typeof(CrashArea))]
        public IHttpActionResult GetCrashArea(int id)
        {
            CrashArea crashArea = db.CrashArea.Find(id);
            if (crashArea == null)
            {
                return NotFound();
            }

            return Ok(crashArea);
        }

        [Route("Robolife/PutCrashArea/{id}/{crashArea}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCrashArea(int id, CrashArea crashArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != crashArea.ID)
            {
                return BadRequest();
            }

            db.Entry(crashArea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrashAreaExists(id))
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

        [Route("Robolife/PostCrashArea/{crashArea}")]
        [ResponseType(typeof(CrashArea))]
        public IHttpActionResult PostCrashArea(CrashArea crashArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CrashArea.Add(crashArea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = crashArea.ID }, crashArea);
        }

        [Route("Robolife/DeleteCrashArea/{id}")]
        [ResponseType(typeof(CrashArea))]
        public IHttpActionResult DeleteCrashArea(int id)
        {
            CrashArea crashArea = db.CrashArea.Find(id);
            if (crashArea == null)
            {
                return NotFound();
            }

            db.CrashArea.Remove(crashArea);
            db.SaveChanges();

            return Ok(crashArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CrashAreaExists(int id)
        {
            return db.CrashArea.Count(e => e.ID == id) > 0;
        }
    }
}