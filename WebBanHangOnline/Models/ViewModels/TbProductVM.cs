using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBanHangOnline.Models.ViewModels
{
    public class TbProductVM
    {
        public TbProduct? TbProduct { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList{ get; set; }
    }
}
