
using Exam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;






namespace Exam.Repository
{

        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly StudentEntites _db;
            internal DbSet<T> dbSet;

            public Repository(StudentEntites db)
            {
                _db = db;
                dbSet = _db.Set<T>();

            _db.States.Include(u => u.Country);
            _db.Cities.Include(u => u.States);
            _db.Students.Include(u => u.Country).Include(u => u.States).Include(u => u.Cities).Include(u=> u.ContactInfo);
            
        }

            public void Add(T entity)
            {
                dbSet.Add(entity);
            }

            public T Get(Expression<Func<T, bool>> filter, string includeProperties = null, bool tracked = false)
            {
                IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

                if (filter != null)
                    query = query.Where(filter);

                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }

            public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
            {
                IQueryable<T> query = dbSet;

                if (filter != null)
                    query = query.Where(filter);

                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.ToList();
            }

        public void Remove(T entity)
        {
            var entry = _db.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }


        public void RemoveRange(IEnumerable<T> entities)
            {
                dbSet.RemoveRange(entities);
            }
        }
    

}
