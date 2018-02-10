using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SecretSanta.Data.IInfrastructure
{
    public interface IRepository<T> where T : class
    {

        T GetById(object id);

        IQueryable<T> GetAll { get; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        //int GetCount();
        //void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);
        //void Delete(Expression<Func<T, bool>> where);
        //T GetById(string id);
        //T GetById(int id);
        //IQueryable<T> GetAll();
        //IQueryable<T> GetMany(Expression<Func<T, bool>> where);
    }
}