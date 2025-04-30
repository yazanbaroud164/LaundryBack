using System;
using LaundryApi.Models;

namespace LaundryApi.Interfaces;

public interface IOrderImageService
{
    Task UploadImagesAsync(long orderId, List<IFormFile> files);
    Task<List<OrderImage>> GetImagesByOrderIdAsync(long orderId);
    Task DeleteImageAsync(long imageId);
    
}
