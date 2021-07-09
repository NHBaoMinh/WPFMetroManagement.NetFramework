using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WPFMetroManagement.NetFramework.Model;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {
        private ObservableCollection<Company> _listCompany;
        public ObservableCollection<Company> ListCompany
        {
            get => _listCompany;
            set { _listCompany = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Line> _listLine;
        public ObservableCollection<Line> ListLine
        {
            get => _listLine;
            set { _listLine = value; OnPropertyChanged(); }
        }

        #region Declare for Detail and Update Company Information
        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;

            set
            {
                _selectedCompany = value;
                if (SelectedCompany == null) return;
                txtUpdateCompanyID = SelectedCompany.id.ToString();
                txtUpdateCompanyName = SelectedCompany.name;
                txtUpdateCompanyWebsiteAddress = SelectedCompany.websiteAddress;
                txtUpdateCompanyAddress = SelectedCompany.addressCompany;
                txtUpdateCompanyPhone = SelectedCompany.phone;
                initListLines();
                OnPropertyChanged();
            }
        }

        private void initListLines()
        {
            ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.idCompany == SelectedCompany.id));
        }

        private string _updateCompanyID;
        private string _updateCompanyName;
        private string _updateCompanyWebsiteAddress;
        private string _updateCompanyAddress;
        private string _updateCompanyPhone;
        private bool _IconUpdateCompany;

        public string txtUpdateCompanyID { get => _updateCompanyID; set { _updateCompanyID = value; OnPropertyChanged(); } }
        public string txtUpdateCompanyName { get => _updateCompanyName; set { _updateCompanyName = value; OnPropertyChanged(); } }
        public string txtUpdateCompanyWebsiteAddress { get => _updateCompanyWebsiteAddress; set { _updateCompanyWebsiteAddress = value; OnPropertyChanged(); } }
        public string txtUpdateCompanyAddress { get => _updateCompanyAddress; set { _updateCompanyAddress = value; OnPropertyChanged(); } }
        public string txtSearchCompany { get => _txtSearch; set { _txtSearch = value; OnPropertyChanged(); InitListCompany(); } }
        public string txtUpdateCompanyPhone { get => _updateCompanyPhone; set { _updateCompanyPhone = value; OnPropertyChanged(); } }
        public ICommand SaveUpdateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public bool iconUpdateCompany { get => _IconUpdateCompany; set { _IconUpdateCompany = value; OnPropertyChanged(); } }
        #endregion

        #region Declare for Adding new Company
        private string _txtSearch;
        private string _txtAddCompanyID;
        private string _txtAddCompanyName;
        private string _txtAddCompanyWebsiteAddress;
        private string _txtAddCompanyAddress;
        private string _txtAddCompanyPhone;

        public string txtAddCompanyID { get => _txtAddCompanyID; set { _txtAddCompanyID = value; OnPropertyChanged(); } }
        public string txtAddCompanyName { get => _txtAddCompanyName; set { _txtAddCompanyName = value; OnPropertyChanged(); } }
        public string txtAddCompanyWebsiteAddress { get => _txtAddCompanyWebsiteAddress; set { _txtAddCompanyWebsiteAddress = value; OnPropertyChanged(); } }
        public string txtAddComapnyAddress { get => _txtAddCompanyAddress; set { _txtAddCompanyAddress = value; OnPropertyChanged(); } }
        public string txtAddComapnyPhone { get => _txtAddCompanyPhone; set { _txtAddCompanyPhone = value; OnPropertyChanged(); } }
        public ICommand AddCommand { set; get; }
        public ICommand SaveAddingCompanyCommand { set; get; }
        #endregion

        public CompanyViewModel()
        {
            InitListCompany();

            SelectedCompany = ListCompany.Count > 0 ? ListCompany[0] : new Company();
            iconUpdateCompany = false;
            initListLines();

            #region Adding new Company func
            AddCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                int CompanyIndexNext = ListCompany.Count + 1;
                p.IsPopupOpen = true;
                txtAddCompanyID = (CompanyIndexNext).ToString();
            });
            SaveAddingCompanyCommand = new RelayCommand<PopupBox>((p) => { return txtAddCompanyName != "" ? true : false; }, (p) =>
            {
                bool flag = isNumberPhone(txtAddComapnyPhone);
                if (!flag || txtAddComapnyPhone == null || txtAddComapnyPhone.Equals(""))
                {
                    MessageBox.Show("Phone incorrectly!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Company] ON");
                    DataProvider.Ins.DB.Companies.Add(new Company { id = (ListCompany.Count + 1), name = txtAddCompanyName, websiteAddress = txtAddCompanyWebsiteAddress, phone = txtAddComapnyPhone, addressCompany = txtAddComapnyAddress });
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Company] OFF");
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] ON");
                    string s = "user" + (ListCompany.Count + 1);
                    string pass = MD5Hash(Base64Encode(s));
                    DataProvider.Ins.DB.Users.Add(new User { id = (ListCompany.Count + 2),  userName = s, userPass = pass, idCompany = ListCompany.Count + 1});
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] OFF");
                    DataProvider.Ins.DB.SaveChanges();
                    InitListCompany();
                    p.IsPopupOpen = false;
                }
            });
            #endregion

            #region Update Company information func
            UpdateCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                txtUpdateCompanyID = SelectedCompany.id.ToString();
                txtUpdateCompanyName = SelectedCompany.name;
                txtUpdateCompanyWebsiteAddress = SelectedCompany.websiteAddress;
                txtUpdateCompanyAddress = SelectedCompany.addressCompany;
                txtUpdateCompanyPhone = SelectedCompany.phone;

                iconUpdateCompany = true;
                for (int i = 1; i < 6; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[1].Visibility = Visibility.Collapsed;
                    stackPanel.Children[2].Visibility = Visibility.Visible;
                }
                (p.Children[6] as Canvas).Visibility = Visibility.Visible;
            });
            SaveUpdateCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool flag = isNumberPhone(txtUpdateCompanyPhone);
                if (!flag)
                {
                    MessageBox.Show("Phone incorrectly!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    SelectedCompany.name = txtUpdateCompanyName;
                    SelectedCompany.websiteAddress = txtUpdateCompanyWebsiteAddress;
                    SelectedCompany.addressCompany = txtUpdateCompanyAddress;
                    SelectedCompany.phone = txtUpdateCompanyPhone;
                    DataProvider.Ins.DB.SaveChanges();
                }

                InitListCompany();
                iconUpdateCompany = false;
                for (int i = 1; i < 6; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[2].Visibility = Visibility.Collapsed;
                    stackPanel.Children[1].Visibility = Visibility.Visible;
                }
                (p.Children[6] as Canvas).Visibility = Visibility.Collapsed;

            });
            #endregion
        }

        private void InitListCompany()
        {
            if (txtSearchCompany == null || txtSearchCompany.Equals(""))
                ListCompany = new ObservableCollection<Company>(DataProvider.Ins.DB.Companies);
            else
                ListCompany = new ObservableCollection<Company>(DataProvider.Ins.DB.Companies.Where(x => x.name.ToLower().Contains(txtSearchCompany.ToLower()) || x.phone.ToLower().Contains(txtSearchCompany.ToLower())));
        }

        private bool isNumberPhone(String num)
        {
            return num[0] == '0' && (num.Length == 10 || num.Length == 11) && isDigit(num);
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
        private bool isDigit(string num)
        {
            foreach (char c in num)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
