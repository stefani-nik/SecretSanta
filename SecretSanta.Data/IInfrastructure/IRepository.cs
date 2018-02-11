using System.Linq;

namespace SecretSanta.Data.IInfrastructure
{
    public interface IRepository<T> where T : class
    {

        T GetById(object id);

        IOrderedQueryable<T> GetAll { get; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}