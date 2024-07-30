using WebBanHangOnline.Data.Repository.IRepository;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;

namespace WebBanHangOnline.Data.Repository
{
    public class CategoryRepository : Repository<TbCategory>, ICategoryRepository
    {
        private readonly WebBanHangOnlineContext _db;
        public CategoryRepository(WebBanHangOnlineContext db) : base(db)
        {
            _db = db;
        }


        public void Update(TbCategory category)
        {
            _db.Update(category);
        }
    }
}
