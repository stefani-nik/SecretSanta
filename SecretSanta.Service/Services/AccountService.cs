using System;
using System.Linq;
using System.Net.Http;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Service.IServices;
using System.Web;
using SecretSanta.Models;

namespace SecretSanta.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Session> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _usersService;

        public AccountService(IRepository<Session> repository, IUnitOfWork unitOfWork, IUserService usersService)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._usersService = usersService;
        }

        public void CreateUserSession(string username, string authToken)
        {
            var user = this._usersService.GetUserByUsername(username);

            if (user == null)
            {
                throw new Exception();
            }

            var userId = user.Id;

            var userSession = new Session
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AuthToken = authToken,
                ExpirationDateTime = DateTime.Now + new TimeSpan(0, 30, 0)
            };

            this._repository.Add(userSession);
            this._unitOfWork.Commit();
        }

        public void DeleteExpiredSessions()
        {
            var userSessions = this._repository
                .GetAll()
                .Where(session => session.ExpirationDateTime < DateTime.Now);

            foreach (var session in userSessions)
            {
                this._repository.Delete(session);
            }

            this._unitOfWork.Commit();
        }

        public Session ReValidateSession(string authToken)
        {
            var userSession = this._repository
                .GetAll()
                .FirstOrDefault(session => session.AuthToken == authToken);

            if (userSession == null || userSession.ExpirationDateTime < DateTime.Now)
            {
                return null;
            }

            userSession.ExpirationDateTime = DateTime.Now + new TimeSpan(0, 30, 0);
            this._unitOfWork.Commit();

            return userSession;
        }

        public bool InvalidateUserSession()
        {
            string authToken = GetCurrentBearerAuthrorizationToken();

            if (authToken == null)
            {
                throw new ArgumentNullException();
            }

            var userSession = this._repository
                .GetAll()
                .FirstOrDefault(session =>
                session.AuthToken == authToken);

            if (userSession != null)
            {
                this._repository.Delete(userSession);
                this._unitOfWork.Commit();

                return true;
            }

            return false;
        }

        private HttpRequestMessage CurrentRequest => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];

        private string GetCurrentBearerAuthrorizationToken()
        {
            string authToken = null;
            if (CurrentRequest.Headers.Authorization != null && CurrentRequest.Headers.Authorization.Scheme == "Bearer")
            {
                authToken = CurrentRequest.Headers.Authorization.Parameter;
            }

            return authToken;
        }
    }
}
