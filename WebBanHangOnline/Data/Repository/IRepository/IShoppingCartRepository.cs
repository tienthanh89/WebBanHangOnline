
using WebBanHangOnline.Data.Repository.IRepository;
using WebBanHangOnline.Models.Entity;
namespace WebBanHangOnline.Data.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
    }
}
