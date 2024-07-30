using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Models.ViewModels
{
    public class OrderVM
    {
        public OrderHeader orderHeader { get; set; }
        public IEnumerable<OrderDetail> orderDetailList { get; set; }
    }
}
