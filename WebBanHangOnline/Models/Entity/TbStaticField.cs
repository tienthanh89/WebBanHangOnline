using Microsoft.AspNetCore.Identity;

namespace WebBanHangOnline.Models.Entity
{
    public static class TbStaticField
    {
        public const string Role_Admin = "Admin";
        public const string Role_Customer= "Customer";

        public const string StatusPending = "Chờ xác nhận";
        public const string StatusApproved = "Đã xác nhận";
        public const string StatusInProcess = "Đang chuẩn bị hàng";
        public const string StatusShipped = "Đã giao hàng";
        public const string StatusCancelled = "Đã hủy";
        public const string StatusRefunded= "Refunded"; 

        public const string PaymentStatusPending= "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment= "ApprovedForDelayPayment";
        public const string PaymentStatusRejected= "Rejected";
    }
}
