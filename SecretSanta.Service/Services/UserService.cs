﻿using System;
using System.Collections.Generic;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;
using SecretSanta.Service.IServices;

namespace SecretSanta.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IApplicationUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            if (userRepository == null)
            {
                throw new ArgumentException(nameof(userRepository));
            }

            if (unitOfWork == null)
            {
                throw  new ArgumentException(nameof(unitOfWork));
            }

            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public IEnumerable<ApplicationUser> GetPageOfUsers(int page, string orderBy, string searchPattern = null)
        {
            int recordsOnPage = 10;
            int skip = (page - 1)*recordsOnPage;
            return _userRepository.GetPageOfUsers(recordsOnPage, skip, orderBy, searchPattern);
        }

        public IEnumerable<Group> GetUserGroups(string username, int page)
        {
            int recordsOnPage = 10;
            int skip = (page - 1) * recordsOnPage;
            return _userRepository.GetPageOfGroups(username, recordsOnPage, skip);
        }
       
        public void SaveUser()
        {
            _unitOfWork.Commit();
        }
    }
}
