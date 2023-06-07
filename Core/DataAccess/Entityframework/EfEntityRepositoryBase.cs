using Core.Entities;
using Core.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Entityframework
{
    //Hangi TEntity'yi (yani tabloyu) verirsek onun  IEntityRepository'sini implemente edecek

    public class EfEntityRepositoryBase<TEntity, TContext> :IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {

        private readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }
        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            //using içine yazdığımız nesneler using bitince GarbageCollector tarafından bellekten atılır
            var addedEntity = _context.Entry(entity);  //eklenen 'entity'yi git veritabanıyla ilişkilendir. referansı yakala
            addedEntity.State = EntityState.Added;    //o aslında eklenecek bir nesne
        }

        public void Delete(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)  //tek data getirecek metotumuz
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                      int index = 0, int size = 10,
                                      bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }
      

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }

        
    }
}
