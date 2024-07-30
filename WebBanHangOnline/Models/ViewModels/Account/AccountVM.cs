namespace WebBanHangOnline.Models.ViewModels.Account
{
    public class AccountVM
    {
        public string? id { get; set; }
        public string? userName { get; set; }
        public string? email { get; set; }
        public bool? emailConfirmed { get; set; }
        public string? phoneNumber { get; set; }
        public bool? isActive { get; set; }
        public DateTimeOffset? lockoutEnd { get; set; }
    }
}
