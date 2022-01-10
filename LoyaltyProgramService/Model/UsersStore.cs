using System;
using System.Collections.Generic;
using System.Linq;
using LoyaltyProgramService.Model;

namespace LoyaltyProgramService.Model
{
    public class UsersStore: IUsersStore
    {
        private readonly List<LoyaltyProgramUser> _users = new List<LoyaltyProgramUser>();

        public LoyaltyProgramUser Get(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public void Save(LoyaltyProgramUser user)
        {
            var userIndex = _users.FindIndex(u => u.Id == user.Id);
            if (user.Id > 0 && userIndex >= 0)
            {
                _users[userIndex] = user;
            }
            else
            {
                user.Id = _users.Count + 1;
                _users.Add(user);
            }
        }
    }

    public interface IUsersStore
    {
        LoyaltyProgramUser Get(int userId);
        void Save(LoyaltyProgramUser user);
    }
}
