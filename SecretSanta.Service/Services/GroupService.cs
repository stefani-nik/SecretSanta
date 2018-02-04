using System;
using System.Linq;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;
using SecretSanta.Service.IServices;

namespace SecretSanta.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            if (groupRepository == null)
            {
                throw new ArgumentException(nameof(groupRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentException(nameof(unitOfWork));
            }

            this._groupRepository = groupRepository;
            this._unitOfWork = unitOfWork;
        }

        public Group GetGroupById(int groupId)
        {
            return this._groupRepository.GetGroupById(groupId);
        }

        public IQueryable<Group> GetPageOfGroups(string userId, int page)
        {
            const int recordsOnPage = 10;
            int skip = (page - 1)*recordsOnPage;
            return this._groupRepository.GetPageOfGroups(userId, recordsOnPage, skip);
        }

        public IQueryable<ApplicationUser> GetMembers(int groupId)
        {
            return this._groupRepository.GetMembers(groupId);
        }

        public void CreateGroup(Group group)
        {
            var checkIfExists = this._groupRepository.GetGroupByName(group.Name);

            if (checkIfExists != null)
            {
                throw  new ArgumentException("Groupname must be uniqe!");
            }

           _groupRepository.Add(group);
            this._unitOfWork.Commit();
            
        }

        public void RemoveUserFromGroup(int groupId, string userId)
        {
            var group = this._groupRepository.GetGroupById(groupId);
            var user = this._groupRepository.GetMembers(groupId).FirstOrDefault(u => u.Id == userId);

            if (group == null || user == null)
            {
                return;
            }

            group.Members.Remove(user);
            this._unitOfWork.Commit();
        }
    }
}
