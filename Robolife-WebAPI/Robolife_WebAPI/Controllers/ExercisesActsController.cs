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
    public class ExercisesActsController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetExercisesAct")]
        public IQueryable<ExercisesAct> GetExercisesAct()
        {
            return db.ExercisesAct;
        }

        [Route("Robolife/GetExercisesActById/{id}")]
        [ResponseType(typeof(ExercisesAct))]
        public IHttpActionResult GetExercisesAct(int id)
        {
            ExercisesAct exercisesAct = db.ExercisesAct.Find(id);
            if (exercisesAct == null)
            {
                return NotFound();
            }

            return Ok(exercisesAct);
        }

      


        [ResponseType(typeof(void))]
        [Route("Robolife/PutExercisesAct/{id}/{exerciseAct}")]
        public IHttpActionResult PutExercisesAct(int id, ExercisesAct exercisesAct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exercisesAct.ID)
            {
                return BadRequest();
            }

            db.Entry(exercisesAct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExercisesActExists(id))
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

        
        [ResponseType(typeof(ExercisesAct))]
        [Route("Robolife/PostExercisesAct/{exerciseAct}")]

        public IHttpActionResult PostExercisesAct(ExercisesAct exercisesAct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExercisesAct.Add(exercisesAct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exercisesAct.ID }, exercisesAct);
        }

        [Route("Robolife/DeleteExercisesAct/{id}")]
        [ResponseType(typeof(ExercisesAct))]
        public IHttpActionResult DeleteExercisesAct(int id)
        {
            ExercisesAct exercisesAct = db.ExercisesAct.Find(id);
            if (exercisesAct == null)
            {
                return NotFound();
            }

            db.ExercisesAct.Remove(exercisesAct);
            db.SaveChanges();

            return Ok(exercisesAct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExercisesActExists(int id)
        {
            return db.ExercisesAct.Count(e => e.ID == id) > 0;
        }
    }
}