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
    public class SportProgramsController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetSportProgram")]
        public IQueryable<SportProgram> GetSportProgram()
        {
            return db.SportProgram;
        }

        
        [ResponseType(typeof(SportProgram))]
        [Route("Robolife/GetSportProgramById/{id}")]
        public IHttpActionResult GetSportProgram(int id)
        {
            SportProgram sportProgram = db.SportProgram.Find(id);
            if (sportProgram == null)
            {
                return NotFound();
            }

            return Ok(sportProgram);
        }
        [ResponseType(typeof(SportProgram))]
        [Route("Robolife/GetSportProgramByPersonId/{id}")]
        public List<SportProgram> GetSportProgramByPersonId(int id)
        {
            List<SportProgram> sportProgram = db.SportProgram.Where(x=>x.PersonID==id).ToList();
            if (sportProgram == null)
            {
                return db.SportProgram.ToList();
            }

            return sportProgram;
        }

        
        [ResponseType(typeof(void))]
        [Route("Robolife/PutSportProgram/{id}/{sportProgram}")]
        public IHttpActionResult PutSportProgram(int id, SportProgram sportProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sportProgram.ID)
            {
                return BadRequest();
            }

            db.Entry(sportProgram).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportProgramExists(id))
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

        
        [ResponseType(typeof(SportProgram))]
        [Route("Robolife/PostSportProgram/{sportProgram}")]
        public IHttpActionResult PostSportProgram(SportProgram sportProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SportProgram.Add(sportProgram);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sportProgram.ID }, sportProgram);
        }

        
        [ResponseType(typeof(SportProgram))]
        [Route("Robolife/DeleteSportProgram/{id}")]
        public IHttpActionResult DeleteSportProgram(int id)
        {
            SportProgram sportProgram = db.SportProgram.Find(id);
            if (sportProgram == null)
            {
                return NotFound();
            }

            db.SportProgram.Remove(sportProgram);
            db.SaveChanges();

            return Ok(sportProgram);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SportProgramExists(int id)
        {
            return db.SportProgram.Count(e => e.ID == id) > 0;
        }
    }
}