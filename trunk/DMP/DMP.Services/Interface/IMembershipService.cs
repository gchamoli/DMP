using System.Web.Security;


namespace DMP.Services.Interface {
    public interface IMembershipService {

        int MinPasswordLength { get; }
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string email, string password);
        void UpdateUser(MembershipUser user);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        MembershipUser GetUserByUserName(string userName);
        bool IsInRole(string userName, string role);
        bool DeleteUser(string userName);
        string ResetPassword(string userName);
    }
}
