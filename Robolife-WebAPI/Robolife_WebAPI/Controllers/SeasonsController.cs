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
    public class SeasonsController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetSeason")]
        public IQueryable<Season> GetSeason()
        {
            return db.Season;
        }

        [Route("Robolife/GetSeasonById/{id}")]
        [ResponseType(typeof(Season))]
        public IHttpActionResult GetSeason(int id)
        {
            Season season = db.Season.Find(id);
            if (season == null)
            {
                return NotFound();
            }

            return Ok(season);
        }

        [Route("Robolife/PutSeason/{id}/{season}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeason(int id, Season season)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != season.ID)
            {
                return BadRequest();
            }

            db.Entry(season).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeasonExists(id))
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

        [Route("Robolife/PostSeason/{season}")]
        [ResponseType(typeof(Season))]
        public IHttpActionResult PostSeason(Season season)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Season.Add(season);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SeasonExists(season.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = season.ID }, season);
        }

        [Route("Robolife/DeleteSeason/{id}")]
        [ResponseType(typeof(Season))]
        public IHttpActionResult DeleteSeason(int id)
        {
            Season season = db.Season.Find(id);
            if (season == null)
            {
                return NotFound();
            }

            db.Season.Remove(season);
            db.SaveChanges();

            return Ok(season);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeasonExists(int id)
        {
            return db.Season.Count(e => e.ID == id) > 0;
        }
    }
}