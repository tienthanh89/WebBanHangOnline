using System.Linq.Expressions;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Data.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly WebBanHangOnlineContext _db;
        public ShoppingCartRepository(WebBanHangOnlineContext db) :base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }
    }
}
