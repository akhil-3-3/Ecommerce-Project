using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly CloudinaryService _cloudinaryService;

        public ImageService(
            IImageRepository imageRepository,
            CloudinaryService cloudinaryService)
        {
            _imageRepository = imageRepository;
            _cloudinaryService = cloudinaryService;
        }

        // ==========================
        // GET ALL IMAGES
        // ==========================

        public async Task<IEnumerable<ImageResponseDto>> GetAllImagesAsync()
        {
            return await _imageRepository.GetAllImagesAsync();
        }

        // ==========================
        // GET IMAGE BY ID
        // ==========================

        public async Task<ImageResponseDto?> GetImageByIdAsync(int id)
        {
            return await _imageRepository.GetImageByIdAsync(id);
        }

        // ==========================
        // GET IMAGE BY PRODUCT
        // ==========================

        public async Task<ImageResponseDto?> GetImageByProductIdAsync(int productId)
        {
            return await _imageRepository.GetImageByProductIdAsync(productId);
        }

        // ==========================
        // ADD IMAGE
        // ==========================

        public async Task<int> AddImageAsync(AddImageDto dto)
        {
            if (dto.ImageFiles == null || !dto.ImageFiles.Any())
                throw new Exception("No images uploaded.");

            int rowsAffected = 0;

            foreach (var image in dto.ImageFiles)
            {
                if (image == null || image.Length <= 0)
                    continue;

                var uploadResult =
                    await _cloudinaryService.UploadImageAsync(image);

                var imageDto = new ImageDto
                {
                    ProductId = dto.ProductId,
                    ImageUrl = uploadResult.Url.ToString(),
                    ImagePublicId = uploadResult.PublicId
                };

                rowsAffected +=
                    await _imageRepository.AddImageAsync(imageDto);
            }

            if (rowsAffected == 0)
                throw new Exception("No valid images were uploaded.");

            return rowsAffected;
        }

        // ==========================
        // UPDATE IMAGE
        // ==========================

        public async Task<int> UpdateImageAsync(UpdateImageDto dto)
        {
            var existingImage =
                await _imageRepository.GetImageByIdAsync(dto.ImageId);

            if (existingImage == null)
                throw new Exception("Image not found.");

            // Keep existing values by default
            string? imageUrl = existingImage.ImageUrl;
            string? imagePublicId = existingImage.ImagePublicId;

            // If a new image is provided, replace the Cloudinary image
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // Delete old Cloudinary image first
                if (!string.IsNullOrWhiteSpace(existingImage.ImagePublicId))
                {
                    await _cloudinaryService.DeleteImageAsync(
                        existingImage.ImagePublicId);
                }

                // Upload new image
                var uploadResult =
                    await _cloudinaryService.UploadImageAsync(
                        dto.ImageFile);

                imageUrl = uploadResult.Url.ToString();
                imagePublicId = uploadResult.PublicId;
            }

            // Update database record
            var updateDto = new UpdateImageDto
            {
                ImageId = dto.ImageId,
                ProductId = dto.ProductId,
                ImageFile = null,
                ImageUrl = imageUrl,
                ImagePublicId = imagePublicId
            };

            return await _imageRepository.UpdateImageAsync(updateDto);
        }

        // ==========================
        // DELETE IMAGE
        // ==========================

        public async Task<int> DeleteImageAsync(int id)
        {
            // Get image before deleting database row
            var existingImage =
                await _imageRepository.GetImageByIdAsync(id);

            if (existingImage == null)
                throw new Exception("Image not found.");

            // Delete from Cloudinary
            if (!string.IsNullOrWhiteSpace(existingImage.ImagePublicId))
            {
                await _cloudinaryService.DeleteImageAsync(
                    existingImage.ImagePublicId);
            }

            // Delete from database
            return await _imageRepository.DeleteImageAsync(id);
        }
    }
}