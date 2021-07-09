using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMetroManagement.NetFramework.Model;
using WPFMetroManagement.NetFramework.Views;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string _userName;
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LogInCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public LoginViewModel()
        {
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            LogInCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            });

            CloseWindowCommand = new RelayCommand<LoginWindow>((p) => true, (p) => p.Close());
        }

        private void Login(Window p)
        {
            try
            {
                if (p == null)
                    return;
                string passEncode = MD5Hash(Base64Encode(Password));
                var account = DataProvider.Ins.DB.Users.Where(x => x.userName == UserName && x.userPass == passEncode).First() as User;
                if (account != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    if (account.idCompany != null)
                    {
                        Const.userId = (int)account.idCompany;
                        Const.isAdmin = false;
                    }
                    else
                    {
                        Const.userId = 0;
                        Const.isAdmin = true;
                    }
                    p.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Account incorrect!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Account incorrect!");
            }
        }

        private string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
