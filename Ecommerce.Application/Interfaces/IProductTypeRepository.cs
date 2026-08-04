using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductTypeResponseDto>> GetAllProductTypesAsync();
        Task<ProductTypeResponseDto?> GetProductTypeByIdAsync(int id);
        Task<int> AddProductTypeAsync(AddProductTypeDto dto);
        Task<int> UpdateProductTypeAsync(UpdateProductTypeDto dto);
        Task<int> DeleteProductTypeAsync(int id);

    }
}
