using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models.ViewModels
{
    public class DbSetTbNewsVM
    {
        public DbSet<TbNews>? dbSetTbNews { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

    }
}
