using System;

namespace SecretSanta.Data.IInfrastructure
{
    public interface IDbFactory : IDisposable
    {
        SecretSantaContext Init();
    }
}