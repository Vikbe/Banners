using Banners.DataModel;
using Banners.DataModel.Data;
using Banners.DataModel.Models;
using System.Net;
using System.Web.Http;

namespace Banners.Controllers
{
    public class BannerController : ApiController
    {
        private readonly BannerRepository repo;

        public BannerController()
        {
            repo = new BannerRepository();
        }

        //Get existing banner
        [HttpGet]
        public IHttpActionResult Get(int id)
        {

            var banner = repo.Get(id);

            if (banner != null)
            {
                return Ok(banner);
            }

            return NotFound();
        }


        //Insert new banner
        [HttpPost]
        public IHttpActionResult Post([FromBody]Banner banner)
        {
            if (banner != null)
            {
                var result = repo.Add(banner); 

                if(result.Status == RepositoryActionStatus.Created)
                {
                    return Created(Request.RequestUri + "/" + banner.Id.ToString(), banner);
                }
            }

            return BadRequest();
        }

        //Update HTML of existing banner
        [HttpPut]
        public IHttpActionResult Put([FromBody] Banner banner)
        {
           if(banner != null)
            {
                 var result = repo.Update(banner); 

                if(result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedBanner = result.Entity;
                    return Ok(updatedBanner);
                } 
                else if(result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }

        //Delete Banner
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
           var result = repo.Delete(id);

            if (result.Status == RepositoryActionStatus.Deleted)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else if (result.Status == RepositoryActionStatus.NotFound)
            {
                return NotFound();
            }

            return BadRequest();
        }



    }
}
