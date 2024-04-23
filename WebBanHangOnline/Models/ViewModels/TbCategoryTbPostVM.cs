using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBanHangOnline.Models.ViewModels
{
    //public class TbCategoryTbNewsVM
    //{
    //    public TbPost? Post { get; set; }
    //    public IEnumerable<string>? CategoryList { get; set; }
    //}


    public class TbCategoryTbPostVM
    {
        public TbPost? Post { get; set; }
        public TbCategory? Category { get; set; }
    }
}
