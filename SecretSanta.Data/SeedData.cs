using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SecretSanta.Models;

namespace SecretSanta.Data
{
    public class SeedData : DropCreateDatabaseAlways<SecretSantaContext>
    {
        protected override void Seed(SecretSantaContext context)
        {
            var users = GetUsers(context);
            var groups = GetGroups(users);
            groups.ForEach(group => context.Groups.Add(group));
            context.Commit();

            GetSingleInvitation(groups[2], users[3]);
            context.Commit();

        }
        #region Users
        private static List<ApplicationUser> GetUsers(SecretSantaContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "johnDoe",
                    DisplayName = "John Doe"
                },
                new ApplicationUser
                {
                    UserName = "jimmy",
                    DisplayName = "Jim Rogers"
                },
                new ApplicationUser
                {
                    UserName = "testUser",
                    DisplayName = "Test User"
                },
                new ApplicationUser
                {
                   UserName = "Stefani",
                   DisplayName = "Stefani Nikolova"
                },
                new ApplicationUser
                {
                    UserName = "admin",
                    DisplayName = "Admin Admin"
                }

            };
            users.ForEach(user => manager.Create(user, "123456"));
            return users;
        }
        #endregion


        #region Groups
        private static List<Group> GetGroups(List<ApplicationUser> users)
        {
            var list = new List<Group>
            {
                new Group
                {
                    Name = "test",
                    Creator = users[0]
                  
                },
                new Group
                {
                    Name = "group",
                    Creator = users[3]
                },
                new Group
                {
                    Name = "neostana",
                    Creator = users[4]
                }
            };

            list[0].Members.Add(users[1]);
            list[0].Members.Add(users[2]);
            list[0].Members.Add(users[3]);
            
            list[1].Members.Add(users[0]);
            list[1].Members.Add(users[1]);
            list[1].Members.Add(users[2]);
            list[1].Members.Add(users[4]);


            return list;
        }
        #endregion


        #region Invitations 
        private static Invitation GetSingleInvitation(Group group,  ApplicationUser receiver)
        {
            return new Invitation
            {
                Group = group,
                Receiver = receiver
            };
        }
        #endregion

    }
}
