using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.Entity
{
    public class OrderHeader
    {
        [Key]
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDay { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        
    }


    
}
