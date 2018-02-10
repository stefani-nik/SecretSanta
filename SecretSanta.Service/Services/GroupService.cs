using System;
using System.Collections.Generic;
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

        public Group GetGroupByName(string groupName)
        {
            return this._groupRepository.GetGroupByName(groupName);
        }

        public IQueryable<Group> GetPageOfGroups(string username, int page)
        {
            const int recordsOnPage = 10;
            int skip = (page - 1)*recordsOnPage;
            return this._groupRepository.GetPageOfGroups(username, recordsOnPage, skip);
        }

        public ICollection<ApplicationUser> GetMembers(int groupId)
        {
            return this._groupRepository.GetMembers(groupId);
        }

        public void CreateGroup(Group group)
        {
           
           _groupRepository.Add(group);
            this._unitOfWork.Commit();
            
        }

        public void AddMember(string groupName , ApplicationUser user)
        {
            var group = this._groupRepository.GetGroupByName(groupName);

            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            group.Members.Add(user);
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

        public void ChangeState(int groupId)
        {
            var group = this._groupRepository.GetGroupById(groupId);
            group.State = ConnectionsState.Connected;
            _unitOfWork.Commit();
        }
    }
}
