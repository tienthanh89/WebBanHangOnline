using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBanHangOnline.Models.ViewModels
{
    //public class TbCategoryTbNewsVM
    //{
    //    public TbNews? News { get; set; }
    //    public IEnumerable<string>? CategoryList { get; set; }
    //}


    public class TbCategoryTbNewsVM
    {
        public TbNews? News { get; set; }
        public TbCategory? Category { get; set; }
    }
}
