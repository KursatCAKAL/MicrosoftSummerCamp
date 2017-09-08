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
    public class NutrationProgramsController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetNutrationProgram/")]
        public IQueryable<NutrationProgram> GetNutrationProgram()
        {
            return db.NutrationProgram;
        }


        [ResponseType(typeof(NutrationProgram))]
        [Route("Robolife/GetNutrationProgramById/{id}")]
        public IHttpActionResult GetNutrationProgram(int id)
        {
            NutrationProgram nutrationProgram = db.NutrationProgram.Find(id);
            if (nutrationProgram == null)
            {
                return NotFound();
            }

            return Ok(nutrationProgram);
        }
        [Route("Robolife/GetNutrationProgramByPersonId/{id}")]
        public List<NutrationProgram> GetNutrationProgramByPersonId(int id)
        {
            List<NutrationProgram> nutrationProgram = db.NutrationProgram.Where(x => x.PersonID == id).ToList();
            if (nutrationProgram == null)
            {
                return db.NutrationProgram.ToList();
            }

            return nutrationProgram;
        }


        [ResponseType(typeof(void))]
        [Route("Robolife/PutNutrationProgram/{id}/{nutrationProgram}")]
        public IHttpActionResult PutNutrationProgram(int id, NutrationProgram nutrationProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nutrationProgram.ID)
            {
                return BadRequest();
            }

            db.Entry(nutrationProgram).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutrationProgramExists(id))
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

        [ResponseType(typeof(NutrationProgram))]
        [Route("Robolife/PostNutrationProgram/{nutrationProgram}")]
        public IHttpActionResult PostNutrationProgram(NutrationProgram nutrationProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NutrationProgram.Add(nutrationProgram);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nutrationProgram.ID }, nutrationProgram);
        }

        [ResponseType(typeof(NutrationProgram))]
        [Route("Robolife/DeleteNutrationProgramById/{id}")]
        public IHttpActionResult DeleteNutrationProgram(int id)
        {
            NutrationProgram nutrationProgram = db.NutrationProgram.Find(id);
            if (nutrationProgram == null)
            {
                return NotFound();
            }

            db.NutrationProgram.Remove(nutrationProgram);
            db.SaveChanges();

            return Ok(nutrationProgram);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NutrationProgramExists(int id)
        {
            return db.NutrationProgram.Count(e => e.ID == id) > 0;
        }
    }
}