using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class ProfileService : IProfileService {

        private readonly IRepository<Profile> profileRepo;

        public ProfileService(IRepository<Profile> profileRepo) {
            this.profileRepo = profileRepo;
        }

        public Profile GetProfile(int id) {
            return profileRepo.Single(x => x.Id == id);
        }

        public void AddProfile(IEnumerable<Profile> profiles) {
            foreach (var profile in profiles) {
                profileRepo.Add(profile);
            }
            profileRepo.SaveChanges();
        }

        public void UpdateProfile(Profile profile) {
            var oldProfile = GetProfile(profile.Id);
            oldProfile.ContactNumber = profile.ContactNumber;
            oldProfile.Address = profile.Address;
            oldProfile.Description = profile.Description;
            oldProfile.Email = profile.Email;
            oldProfile.Designation = profile.Designation;
            oldProfile.PANNumber = profile.PANNumber;
            oldProfile.DateOfJoining = profile.DateOfJoining;
            oldProfile.DateOfLeaving = profile.DateOfLeaving;
            oldProfile.PreviousCompany = profile.PreviousCompany;
            oldProfile.PreviousJobProfile = profile.PreviousJobProfile;
            oldProfile.TotalWorkExperience = profile.TotalWorkExperience;
            oldProfile.ExperienceWithVE = profile.ExperienceWithVE;
            oldProfile.TIVRepresenting = profile.TIVRepresenting;
            oldProfile.AreaRepresenting = profile.AreaRepresenting;
            oldProfile.SAPCode = profile.SAPCode;
            oldProfile.TrainingLevel = profile.TrainingLevel;
            oldProfile.DateOfBirth = profile.DateOfBirth;
            profileRepo.SaveChanges();
        }

        public void DeleteProfile(int id) {
            var state = GetProfile(id);
            profileRepo.Delete(state);
            profileRepo.SaveChanges();
        }

        public IEnumerable<Profile> GetAllProfiles() {
            return profileRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Profile> FindProfiles(Func<Profile, bool> predicate) {
            return profileRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}