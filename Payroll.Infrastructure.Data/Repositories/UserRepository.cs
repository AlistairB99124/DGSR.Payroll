using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public async Task<IdentityResult> ConfirmEmail(UserManager<User,string> userManager, string userId, string code)
        {
            var result = await userManager.ConfirmEmailAsync(userId, code);
            return result;
        }

        public void ExternalLogin()
        {
            throw new NotImplementedException();
        }

        public void ExternalLoginCallback()
        {
            throw new NotImplementedException();
        }

        public void ExternalLoginConfirmation()
        {
            throw new NotImplementedException();
        }

        public void ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public void ForgotPasswordConfirmation()
        {
            throw new NotImplementedException();
        }

        public User GetUser(string Email)
        {
            return _context.Users.Where(p=>p.Email==Email).FirstOrDefault();
        }

        public User GetUserById(string ID)
        {
            return _context.Users.Find(ID);
        }

        public async Task<SignInStatus> Login(SignInManager<User,string> signinManager, string email, string password, bool rememberMe)
        {
            var result = await signinManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
            return result;
        }

        public void Logoff()
        {
            throw new NotImplementedException();
        }
        

        public async Task<IdentityResult> Register(UserManager<User,string> userManager, User user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            return result;
        }

        public void ResetPassword()
        {
            throw new NotImplementedException();
        }

        public void ResetPasswordConfirmation()
        {
            throw new NotImplementedException();
        }

        public void SendCode()
        {
            throw new NotImplementedException();
        }

        public async Task<SignInStatus> VerifyCode(SignInManager<User,string> signinManager, string provider, string code, bool rememberMe, bool rememberBrowser)
        {
            var result = await signinManager.TwoFactorSignInAsync(provider, code, isPersistent: rememberMe, rememberBrowser: rememberBrowser);
            return result;
        }


    }
}
