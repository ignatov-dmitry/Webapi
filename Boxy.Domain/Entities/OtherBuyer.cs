﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxy.Domain.Entities
{
    public class OtherBuyer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid OrderCode { get; set; }
        public ICollection<CartLine> CartLines { get; set; }
        public OtherBuyer()
        {
            CartLines = new List<CartLine>();
        }
    }
}
