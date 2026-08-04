using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }  
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? VolumeMl { get; set; }
        public string Gender { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreadtedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
