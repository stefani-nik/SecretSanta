namespace SecretSanta.Data.IInfrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}