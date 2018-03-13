using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Web.Http.Cors;
using System.Data.Entity;
using Boxy.Domain.Entities;


namespace Boxy.WebUI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult Get()
        {
            var prod = db.Products.Include(a => a.Files).ToList();
            return Ok(prod.ToList());
        }
        //public IEnumerable<Product> Get()
        //{
        //    userCart = (UserCart)HttpContext.Current.Session["UserCart"];
          //  var prod = db.Products.Include(a=>a.Files).ToList();
          //  return prod;
        //}

        public Product GetProduct(int? id)
        {
            var prod = db.Products.Include(a => a.Files).ToList().Where(a=>a.Id == id).FirstOrDefault();
            return prod;
        }

        public IEnumerable<Product> GetProducts(string alias)
        {
            return db.Products.Include(a=>a.Category).Include(b=>b.Files).ToList().Where(b=>b.Category.Alias == alias);
        }

    }
}