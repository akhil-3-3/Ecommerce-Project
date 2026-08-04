using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class AddProductTypeDto
    {
        public string ProductTypeName { get; set; } = string.Empty;
        
    }
    public class UpdateProductTypeDto
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; } = string.Empty;
    }
    public class ProductTypeResponseDto
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
