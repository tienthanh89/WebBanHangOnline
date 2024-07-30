using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebBanHangOnline.Data.Repository.IRepository;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;

namespace WebBanHangOnline.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly WebBanHangOnlineContext _db;
        public DbSet<T> dbSet { get; set; }
        public Repository(WebBanHangOnlineContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
            // _db.Set<T> = _db.Category
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet.Where(filter);

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
