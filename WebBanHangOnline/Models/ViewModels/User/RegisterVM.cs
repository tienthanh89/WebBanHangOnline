using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels.User
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="*")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 ký tự")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; } = true;

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)] 
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "*")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Số điện thoại")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage ="Chưa đúng định dạng Email")]
        public string Email { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? ImageUrl { get; set; }
    }
}
