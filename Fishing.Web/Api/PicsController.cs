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
using Fishing.Core;
using Fishing.Web.Api.Models;

namespace Fishing.Web.Api
{
    public class PicsController : ApiController
    {
        private MattMillerTimeEntities db = new MattMillerTimeEntities();

        [Route("api/pics/latest/{number}")]
        public IEnumerable<PicViewModel> GetLatestPics(int number)
        {
            return db.Pics.OrderByDescending(p => p.upload_date).Take(number)
                .ToList()
                .Select(p => PicViewModel.FromPic(p));
        }

        [Route("api/pics/categories/{categoryIds}")]
        public IEnumerable<PicViewModel> GetPicsByCategoryIds(string categoryIds)
        {
            var ids = categoryIds.Split(',');
            var idList = ids.Select(s => int.Parse(s.Trim()));
            return db.PicCategories
                    .Where(p => idList.Contains(p.category_id))
                    .Select(pc => pc.Pic)
                    .OrderByDescending(p => p.upload_date)
                    .ToList()
                    .Select(p => PicViewModel.FromPic(p));
        }

        // GET: api/Pics/5
        [ResponseType(typeof(Pic))]
        public IHttpActionResult GetPic(int id)
        {
            Pic pic = db.Pics.Find(id);
            if (pic == null)
            {
                return NotFound();
            }

            return Ok(pic);
        }

        // PUT: api/Pics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPic(int id, Pic pic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pic.id)
            {
                return BadRequest();
            }

            db.Entry(pic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PicExists(id))
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

        // POST: api/Pics
        [ResponseType(typeof(Pic))]
        public IHttpActionResult PostPic(Pic pic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pics.Add(pic);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pic.id }, pic);
        }

        // DELETE: api/Pics/5
        [ResponseType(typeof(Pic))]
        public IHttpActionResult DeletePic(int id)
        {
            Pic pic = db.Pics.Find(id);
            if (pic == null)
            {
                return NotFound();
            }

            db.Pics.Remove(pic);
            db.SaveChanges();

            return Ok(pic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PicExists(int id)
        {
            return db.Pics.Count(e => e.id == id) > 0;
        }
    }
}