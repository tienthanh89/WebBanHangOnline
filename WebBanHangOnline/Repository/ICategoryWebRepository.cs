using WebBanHangOnline.Models;
namespace WebBanHangOnline.Repository
{
    public interface ICategoryWebRepository
    {
        TbCategoryWeb Add(TbCategoryWeb categoryWeb);
        TbCategoryWeb Update(TbCategoryWeb categoryWeb);
        TbCategoryWeb Delete(int id);
        TbCategoryWeb GetByName(string name);
        IEnumerable<TbCategoryWeb> GetAllCategoryWeb();
    }
}
