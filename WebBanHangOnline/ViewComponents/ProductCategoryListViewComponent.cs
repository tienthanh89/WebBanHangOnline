﻿using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ViewModels;

namespace WebBanHangOnline.ViewComponents
{
    public class ProductCategoryListViewComponent: ViewComponent
    {
        private readonly WebBanHangDemoContext _db;
        public ProductCategoryListViewComponent(WebBanHangDemoContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var item = _db.TbProductCategories.Select(x => new ProductCategoryListVM
            {
                Id = x.Id,
                Title = x.Title,
                Count = _db.TbProducts.Where(u=>u.ProductCategoryId == x.Id).Count(),
            });

            return View(item);
        }
    }
}