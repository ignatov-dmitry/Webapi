using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxy.Domain.Entities
{
    public class UploadFile
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
