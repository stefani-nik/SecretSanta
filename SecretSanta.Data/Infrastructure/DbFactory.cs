using SecretSanta.Data.IInfrastructure;

namespace SecretSanta.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        SecretSantaContext _dbContext;

        public SecretSantaContext Init()
        {
            return _dbContext ?? (_dbContext = new SecretSantaContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}