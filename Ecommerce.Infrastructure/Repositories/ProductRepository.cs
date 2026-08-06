using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : ConnectionRepository, IProductRepository
    {
        public ProductRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<ProductResponseDtos>> GetAllProductsAsync()
        {
            using var connection = GetConnection();

            var productDictionary = new Dictionary<int, ProductResponseDtos>();

            await connection.QueryAsync<ProductResponseDtos, ImageResponseDto, ProductResponseDtos>(
                "sp_GetAllProducts",
                (product, image) =>
                {
                    if (!productDictionary.TryGetValue(product.ProductId, out var existingProduct))
                    {
                        existingProduct = product;
                        existingProduct.Images = new List<ImageResponseDto>();

                        productDictionary.Add(product.ProductId, existingProduct);
                    }

                    if (image != null && image.ImageId != 0)
                    {
                        existingProduct.Images.Add(image);
                    }

                    return existingProduct;
                },
                splitOn: "ImageId",
                commandType: CommandType.StoredProcedure
            );

            return productDictionary.Values;
        }
        public async Task<ProductResponseDtos?> GetProductByIdAsync(int id)
        {
            using var connection = GetConnection();

            ProductResponseDtos? product = null;

            await connection.QueryAsync<ProductResponseDtos, ImageResponseDto, ProductResponseDtos>(
                "sp_GetProductById",
                (p, image) =>
                {
                    if (product == null)
                    {
                        product = p;
                        product.Images = new List<ImageResponseDto>();
                    }

                    if (image != null && image.ImageId != 0)
                    {
                        product.Images.Add(image);
                    }

                    return product;
                },
                new
                {
                    ProductId = id
                },
                splitOn: "ImageId",
                commandType: CommandType.StoredProcedure
            );

            return product;
        }

        public async Task<int> AddProductAsync(AddProductDtos dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddProduct",
                new
                {
                    dto.CategoryId,
                    dto.BrandId,
                    dto.ProductName,
                    dto.Description,
                    dto.VolumeML,
                    dto.Gender,
                    dto.Price,
                    dto.Discount
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateProductAsync(UpdateProductDtos dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateProduct",
                new
                {
                    dto.ProductId,
                    dto.CategoryId,
                    dto.BrandId,
                    dto.ProductName,
                    dto.Description,
                    dto.VolumeML,
                    dto.Gender,
                    dto.Price,
                    dto.Discount
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteProduct",
                new
                {
                    ProductId = id
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ProductResponseDtos>> SearchProductsAsync(string keyword)
        {
            using var connection = GetConnection();

            var productDictionary = new Dictionary<int, ProductResponseDtos>();

            await connection.QueryAsync<ProductResponseDtos, ImageResponseDto, ProductResponseDtos>(
                "sp_SearchProducts",
                (product, image) =>
                {
                    if (!productDictionary.TryGetValue(product.ProductId, out var existingProduct))
                    {
                        existingProduct = product;
                        existingProduct.Images = new List<ImageResponseDto>();

                        productDictionary.Add(product.ProductId, existingProduct);
                    }

                    if (image != null && image.ImageId != 0)
                    {
                        existingProduct.Images.Add(image);
                    }

                    return existingProduct;
                },
                new
                {
                    Keyword = keyword
                },
                splitOn: "ImageId",
                commandType: CommandType.StoredProcedure
            );

            return productDictionary.Values;
        }
    }
}