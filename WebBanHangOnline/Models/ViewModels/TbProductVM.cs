using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBanHangOnline.Models.ViewModels
{
    public class TbProductVM
    {
        public TbProduct tbProduct { get; set; }
        public IEnumerable<SelectListItem> CategoryList{ get; set; }
    }
}
