using WebBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Repository;
namespace WebBanHangOnline.ViewComponents
{
    public class CategoryWebViewComponent : ViewComponent
    {
        private readonly ICategoryWebRepository _categoryWeb;
        public CategoryWebViewComponent(ICategoryWebRepository categoryWeb)
        {
            _categoryWeb = categoryWeb;
        }

        public IViewComponentResult Invoke()
        {
            var categoryWeb = _categoryWeb.GetAllCategoryWeb();
            return View(categoryWeb);
        }
    }
}
