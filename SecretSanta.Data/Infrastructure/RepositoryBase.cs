namespace SecretSanta.Data.Infrastructure
{
    using IInfrastructure;
    using System.Linq;

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
    }
}