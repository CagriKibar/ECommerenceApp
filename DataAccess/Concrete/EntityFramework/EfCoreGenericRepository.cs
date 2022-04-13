using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext, new()
    {
        public void Create(T entity)
        {
            using (var Context=new TContext())
            {
                Context.Set<T>().Add(entity);
                Context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var Context = new TContext())
            {
                Context.Set<T>().Remove(entity);
                Context.SaveChanges();
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                         ? context.Set<T>().ToList()
                         : context.Set<T>().Where(filter).ToList();
            }
        }

        public T GetById(int id)
        {
            using (var Context = new TContext())
            {
               return Context.Set<T>().Find(id);
            }
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            using (var Context = new TContext())
            {
               return Context.Set<T>().Where(filter).SingleOrDefault();
            }
        }

        public void Update(T entity)
        {
            using (var Context = new TContext())
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
    }
}
