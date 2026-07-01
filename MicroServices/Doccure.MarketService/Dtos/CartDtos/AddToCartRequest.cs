namespace Doccure.MarketService.Dtos.CartDtos
{
    public class AddToCartRequest
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        public string? ImageUrl { get; set; }
    }
}
