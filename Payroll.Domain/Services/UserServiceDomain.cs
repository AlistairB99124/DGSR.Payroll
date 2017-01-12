using Payroll.Domain.Interfaces.Domain;
using Payroll.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserServiceDomain: ServiceDomainBase, IUserServiceDomain
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IClerkRepository _clerkRepository;

        public UserServiceDomain(IUserRepository userRepository, IAdminRepository adminRepository, IClerkRepository clerkRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _clerkRepository = clerkRepository;
        }

        public Task<IdentityResult> ConfirmEmail(UserManager<User, string> userManager, string userId, string code)
        {
            return _userRepository.ConfirmEmail(userManager, userId, code);
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

        public Task<SignInStatus> Login(SignInManager<User, string> signinManager, string email, string password, bool rememberMe)
        {
            return _userRepository.Login(signinManager, email, password, rememberMe);
        }

        public void Logoff()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Register(UserManager<User, string> userManager, User user, string password)
        {
            return _userRepository.Register(userManager, user, password);
        }

        public void RegisterAdmin(Admin admin)
        {
            try
            {
                StartTransaction();
                _adminRepository.Register(admin);
                PersistTransaction();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }            
        }

        public void RegisterClerk(Clerk clerk)
        {
            try
            {
                StartTransaction();
                _clerkRepository.Register(clerk);
                PersistTransaction();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
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

        public Task<SignInStatus> VerifyCode(SignInManager<User, string> signinManager, string provider, string code, bool rememberMe, bool rememberBrowser)
        {
            return _userRepository.VerifyCode(signinManager, provider, code, rememberMe, rememberBrowser);
        }

        public User GetUser(string Email)
        {
            return _userRepository.GetUser(Email);
        }

        public User GetUserById(string ID)
        {
            return _userRepository.GetUserById(ID);
        }

        public void UpdateUser(User user)
        {
            try
            {
                StartTransaction();
                _userRepository.Edit(user);
                PersistTransaction();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
