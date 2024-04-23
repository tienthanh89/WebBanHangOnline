using WebBanHangOnline.Models;
namespace WebBanHangOnline.Repository
{
    public class CategoryWebRepository : ICategoryWebRepository
    {
        private readonly WebBanHangDemoContext _db;
        public CategoryWebRepository(WebBanHangDemoContext context)
        {
            _db = context;
        }

        public TbCategoryWeb Add(TbCategoryWeb categoryWeb)
        {
            _db.TbCategoryWebs.Add(categoryWeb);
            _db.SaveChanges();
            return categoryWeb;
        }

        public TbCategoryWeb Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TbCategoryWeb> GetAllCategoryWeb()
        {
            return _db.TbCategoryWebs;
        }

        public TbCategoryWeb GetByName(string? name)
        {
            return _db.TbCategoryWebs.Find(name);
        }

        public TbCategoryWeb Update(TbCategoryWeb categoryWeb)
        {
            _db.Update(categoryWeb);
            _db.SaveChanges ();
            return categoryWeb;
        }
    }
}
