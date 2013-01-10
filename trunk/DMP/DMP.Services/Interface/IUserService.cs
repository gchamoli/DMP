using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IUserService {

        User GetUser(int id);

        void AddUser(IEnumerable<User> user);

        void UpdateUser(User user);

        void DeleteUser(int id);

        IEnumerable<User> GetUsers();

        IEnumerable<User> FindUsers(Func<User, bool> predicate);

        User GetUserByUserName(string name);

    }
}
