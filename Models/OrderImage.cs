using System;

namespace LaundryApi.Models;

public class OrderImage
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public byte[] Data { get; set; } = null!;

    public Order Order { get; set; } = null!;
}
