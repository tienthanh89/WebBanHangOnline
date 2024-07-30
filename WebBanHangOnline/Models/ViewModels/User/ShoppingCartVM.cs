using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Models.ViewModels.User
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int CartTotal { get; set; }
    }
}
