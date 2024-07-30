using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;

namespace WebBanHangOnline.Helper
{
    public class UpdateOrderHeader
    {
        private readonly WebBanHangOnlineContext _db;

        public UpdateOrderHeader(WebBanHangOnlineContext db)
        {
            _db = db;
        }
        public void UpdateStatus(Guid id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null) {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStringPaymentID(Guid id, string sessionId, string paymentIntentId) {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if(!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
