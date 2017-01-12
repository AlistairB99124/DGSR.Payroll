using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Domain;
using Payroll.Infrastructure.Data.Configuration;
using Payroll.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Payroll.UI.WPF.Pages.Account
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private bool IsAuthenticated;
        private MainWindow mainWindow;
        public Login()
        {
            InitializeComponent();
            mainWindow = new MainWindow();
            IsAuthenticated = mainWindow.IsAuthenticated;
        }

        private async void btnLocalLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtEmail.Text;
            string password = txtPassword.Text;
            bool result = await VerifyUserNamePassword(username, password);
            if (result)
            {
                IsAuthenticated = true;
                this.Content = mainWindow.Content;
            }

        }
        private async Task<bool> VerifyUserNamePassword(string userName, string password)
        {
            var usermanager = new UserManager<User>(new UserStore<User>(new ContextBank()));
            return await usermanager.FindAsync(userName, password) != null;
        }
    }
}
