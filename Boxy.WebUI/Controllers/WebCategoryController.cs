using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using Boxy.Domain.Entities;

namespace Boxy.WebUI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebCategoryController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IHttpActionResult Get()
        {
            return Ok(db.Categories.ToList().Skip(1));
        }
    }
}
