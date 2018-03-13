using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxy.Domain.Entities
{
   
    public class CartLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int count { get; set; }
        public int? OtherBuyerId { get; set; }
        public OtherBuyer OtherBuyer { get; set; }

    }
}
