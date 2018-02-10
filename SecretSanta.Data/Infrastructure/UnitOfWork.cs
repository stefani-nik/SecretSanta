namespace SecretSanta.Data.Infrastructure
{
    using SecretSanta.Data.IInfrastructure;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly  SecretSantaContext _dbContext;

        public UnitOfWork(SecretSantaContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public void Commit()
        {
            this._dbContext.Commit();
        }
    }
}