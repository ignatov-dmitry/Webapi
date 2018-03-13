using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Boxy.Domain.Entities;

namespace Boxy.WebUI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebUserCartController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [HttpPost]
        public Guid GetCart(OtherBuyer otherBuyer)
        {
            otherBuyer.OrderCode = Guid.NewGuid();
            db.OtherBuyer.Add(otherBuyer);
            db.SaveChanges();
            return otherBuyer.OrderCode;
        }
    }

}
