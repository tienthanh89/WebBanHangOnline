using WebBanHangOnline.Data.Repository.IRepository;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;

namespace WebBanHangOnline.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebBanHangOnlineContext _db;
        public ICategoryRepository Category { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(WebBanHangOnlineContext db )
        {
            _db = db;
            ShoppingCart = new ShoppingCartRepository(_db);
            Category = new CategoryRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
