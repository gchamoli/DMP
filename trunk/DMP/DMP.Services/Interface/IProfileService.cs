using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IProfileService {
        Profile GetProfile(int id);

        void AddProfile(IEnumerable<Profile> profiles);

        void UpdateProfile(Profile profile);

        void DeleteProfile(int id);

        IEnumerable<Profile> GetAllProfiles();

        IEnumerable<Profile> FindProfiles(Func<Profile, bool> predicate);
    }
}