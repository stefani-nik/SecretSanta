using SecretSanta.Data;
using SecretSanta.Data.IInfrastructure;

namespace SecretSanta.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {

        private readonly SecretSantaContext _dbContext;

        public RepositoryBase(SecretSantaContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IOrderedQueryable<T> GetAll => (IOrderedQueryable<T>)this._dbContext.DbSet<T>();

        public void Add(T entity)
        {
            this._dbContext.SetAdded(entity);
        }

        public void Delete(T entity)
        {
            this._dbContext.SetDeleted(entity);
        }

        public T GetById(object id)
        {
            return this._dbContext.DbSet<T>().Find(id);
        }

        public void Update(T entity)
        {
            this._dbContext.SetUpdated(entity);
        }
        //// Properties
        //private readonly SecretSantaContext _dataContext;
        //private readonly DbSet<T> _dbSet;
        ////protected DbFactory DbFactory { get; }

        //public RepositoryBase() { }
        //public RepositoryBase(SecretSantaContext dataContext)
        //{
        //    this._dataContext = dataContext;
        //    _dbSet = _dataContext.Set<T>();
        //}

        ////Implementation
        //public virtual int GetCount()
        //{
        //    return _dbSet.ToList().Count;
        //}


        //public virtual void Add(T entity)
        //{
        //    this._dbSet.Add(entity);
        //}

        //public virtual void Update(T entity)
        //{
        //    _dbSet.Attach(entity);
        //    _dataContext.Entry(entity).State = EntityState.Modified;
        //}

        //public virtual void Delete(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}

        //public virtual void Delete(Expression<Func<T, bool>> where)
        //{
        //    IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
        //    foreach (T obj in objects)
        //        _dbSet.Remove(obj);
        //}

        //public virtual T GetById(string id)
        //{
        //    return _dbSet.Find(id);
        //}

        //public virtual T GetById(int id)
        //{
        //    return _dbSet.Find(id);
        //}

        //public virtual IQueryable<T> GetAll()
        //{
        //    return _dbSet;
        //}

        //public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        //{
        //    return _dbSet.Where(where);
        //}

        //public T Get(Expression<Func<T, bool>> where)
        //{
        //    return _dbSet.Where(where).FirstOrDefault<T>();
        //}
    }
}