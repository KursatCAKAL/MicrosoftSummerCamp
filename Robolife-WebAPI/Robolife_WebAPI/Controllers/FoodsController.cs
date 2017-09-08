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
    public class FoodsController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetFoods")]
        public IQueryable<Food> GetFood()
        {
            return db.Food;
        }

        [Route("Robolife/GetFoodById/{id}")]
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        [Route("Robolife/PutFood/{id}/{food}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.ID)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        [Route("Robolife/PostFood/{food}")]
        [ResponseType(typeof(Food))]
        public IHttpActionResult PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Food.Add(food);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = food.ID }, food);
        }

        [Route("Robolife/DeleteFood/{id}")]
        [ResponseType(typeof(Food))]
        public IHttpActionResult DeleteFood(int id)
        {
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            db.Food.Remove(food);
            db.SaveChanges();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.Food.Count(e => e.ID == id) > 0;
        }
    }
}