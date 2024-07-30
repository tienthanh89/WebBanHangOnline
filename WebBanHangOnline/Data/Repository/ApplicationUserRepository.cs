using WebBanHangOnline.Data.Repository.IRepository;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Data.Repository
{
    public class ApplicationUserRepository: Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly WebBanHangOnlineContext _db;
        public ApplicationUserRepository(WebBanHangOnlineContext db) :base(db) 
        {
            _db = db;
        }
    }
}
