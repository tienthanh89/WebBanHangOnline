using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Tên đăng nhập:")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20,ErrorMessage ="Tên đăng nhập tối da 20 ký tự.")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu:")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
