﻿using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels.User
{
    public class shopDetailVM
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category {  get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? Detail {  get; set; }
        public int? Quantity { get; set; }
    }
}
