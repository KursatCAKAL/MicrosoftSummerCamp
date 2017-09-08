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
    public class ImagesController : ApiController
    {
        private Xamarin_KURSATCAKAL_WebAPIEntities db = new Xamarin_KURSATCAKAL_WebAPIEntities();

        [Route("Robolife/GetImages")]
        public IQueryable<Image> GetImage()
        {
            return db.Image;
        }

        [Route("Robolife/GetImageById/{id}")]
        [ResponseType(typeof(Image))]
        public IHttpActionResult GetImage(int id)
        {
            Image image = db.Image.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        [Route("Robolife/PutImage/{id}/{image}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImage(int id, Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != image.ID)
            {
                return BadRequest();
            }

            db.Entry(image).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        [Route("Robolife/PostImage/{image}")]
        [ResponseType(typeof(Image))]
        public IHttpActionResult PostImage(Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Image.Add(image);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = image.ID }, image);
        }

        [Route("Robolife/DeleteImage/{id}")]
        [ResponseType(typeof(Image))]
        public IHttpActionResult DeleteImage(int id)
        {
            Image image = db.Image.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            db.Image.Remove(image);
            db.SaveChanges();

            return Ok(image);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageExists(int id)
        {
            return db.Image.Count(e => e.ID == id) > 0;
        }
    }
}