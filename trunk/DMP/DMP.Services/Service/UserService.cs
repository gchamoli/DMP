using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class UserService : IUserService {

        private readonly IRepository<User> userRepo;
        private readonly IMembershipService membershipService;

        public UserService(IRepository<User> userRepo, IMembershipService membershipService) {
            this.userRepo = userRepo;
            this.membershipService = membershipService;
            this.membershipService = membershipService;
        }

        public User GetUser(int id) {
            return userRepo.Single(x => x.Id == id);
        }

        public void AddUser(IEnumerable<User> users) {
            foreach (var user in users) {
                userRepo.Add(user);
            }
            userRepo.SaveChanges();
        }

        public void UpdateUser(User user) {
            var toUpdate = GetUser(user.Id);
            toUpdate.Name = user.Name;
            toUpdate.Role = user.Role;
            toUpdate.Description = user.Description;
            toUpdate.ParentId = user.ParentId;
            userRepo.SaveChanges();
        }

        public void DeleteUser(int id) {
            var user = GetUser(id);
            user.ObjectInfo.DeletedDate = DateTime.Now;
            userRepo.SaveChanges();
        }

        public IEnumerable<User> GetUsers() {
            var data = userRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
            return data;
        }

        public IEnumerable<User> FindUsers(Func<User, bool> predicate) {
            var data = userRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
            return data;
        }

        public User GetUserByUserName(string name) {
            var user = membershipService.GetUserByUserName(name);
            if (null == user) {
                return new User();
            }
            var users = userRepo.Find(x => x.Email == user.Email);
            return users.Count() == 1 ? users.First() : null;
        }
    }
}
