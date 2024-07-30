namespace WebBanHangOnline.Models.ViewModels
{
    public class CartItemVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; } 
        public int Quantity { get; set; }
        public decimal? Bill => Price * Quantity;
        public string? ImageUrl { get; set; }
    }
}
