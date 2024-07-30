using WebBanHangOnline.Models;

namespace WebBanHangOnline.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<TbCategory>
    {
        void Update(TbCategory category);
        
    }
}
