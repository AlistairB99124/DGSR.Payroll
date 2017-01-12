using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        Task<IdentityResult> Register(UserManager<User, string> userManager, User user, string password);
        Task<SignInStatus> Login(SignInManager<User, string> signinManager, string email, string password, bool rememberMe);
        Task<SignInStatus> VerifyCode(SignInManager<User, string> signinManager, string provider, string code, bool rememberMe, bool rememberBrowser);
        Task<IdentityResult> ConfirmEmail(UserManager<User, string> userManager, string userId, string code);
        void ForgotPassword();
        void ForgotPasswordConfirmation();
        void ResetPassword();
        void ResetPasswordConfirmation();
        void ExternalLogin();
        void SendCode();
        void ExternalLoginCallback();
        void ExternalLoginConfirmation();
        void Logoff();
        User GetUser(string Email);
        User GetUserById(string ID);
    }
}
