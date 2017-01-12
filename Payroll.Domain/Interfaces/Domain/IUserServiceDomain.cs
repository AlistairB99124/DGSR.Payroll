using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Domain
{
    /// <summary>
    /// Stores all methods related to user registrations, logging and management
    /// </summary>
    public interface IUserServiceDomain
    {
        Task<IdentityResult> Register(UserManager<User, string> userManager, User user, string password);
        Task<SignInStatus> Login(SignInManager<User, string> signinManager, string email, string password, bool rememberMe);
        Task<SignInStatus> VerifyCode(SignInManager<User, string> signinManager, string provider, string code, bool rememberMe, bool rememberBrowser);
        Task<IdentityResult> ConfirmEmail(UserManager<User, string> userManager, string userId, string code);
        User GetUserById(string ID);
        void UpdateUser(User user);
        void ForgotPassword();
        void ForgotPasswordConfirmation();
        void ResetPassword();
        void ResetPasswordConfirmation();
        void ExternalLogin();
        void SendCode();
        void ExternalLoginCallback();
        void ExternalLoginConfirmation();
        void Logoff();
        void RegisterClerk(Clerk clerk);
        void RegisterAdmin(Admin admin);
        /// <summary>
        /// Gets User based on email address
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>User</returns>
        User GetUser(string Email);
    }
}
