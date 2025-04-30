using System;
using LaundryApi.Data;
using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryApi.Services;

public class OrderImageService : IOrderImageService
{
    private readonly LaundryDbContext _context;

    public OrderImageService(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task UploadImagesAsync(long orderId, List<IFormFile> files)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
            throw new KeyNotFoundException("Order not found.");

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                var orderImage = new OrderImage
                {
                    OrderId = orderId,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Data = ms.ToArray()
                };

                _context.Add(orderImage);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderImage>> GetImagesByOrderIdAsync(long orderId)
    {
        return await _context.OrderImage
            .Where(img => img.OrderId == orderId)
            .ToListAsync();
    }

    public async Task DeleteImageAsync(long imageId)
    {
        var image = await _context.OrderImage.FindAsync(imageId);
        if (image != null)
        {
            _context.OrderImage.Remove(image);
            await _context.SaveChangesAsync();
        }
    }

}
