using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMetroManagement.NetFramework.Model;
using WPFMetroManagement.NetFramework.UserControls;
using WPFMetroManagement.NetFramework.Views;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _userName;
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }
        private Visibility _setAdmin;
        public Visibility SetAdmin { get => _setAdmin; set { _setAdmin = value; OnPropertyChanged(); } }
        private Visibility _setUser;
        public Visibility SetUser { get => _setUser; set { _setUser = value; OnPropertyChanged(); } }
        public ICommand ItemSelectedCommand { get; set; }
        public ICommand LoadContentCommand { get; set; }
        public Grid content { get; set; }
        public MainWindowViewModel()
        {
            ItemSelectedCommand = new RelayCommand<TreeView>((a) => { if (a != null) return true; return false; }, (a) =>
            {
                FrameworkElement tmp = a;

                while (!(tmp is Expander))
                {
                    tmp = tmp.Parent as FrameworkElement;
                }
                (tmp as Expander).IsExpanded = false;

                switch ((a.SelectedItem as TreeViewItem).Header.ToString())
                {
                    case "Logout":
                        using (null)
                            logout(a);
                        break;
                    case "About us":
                        content.Children.Clear();
                        content.Children.Add(new AboutUsUC() { });
                        break;
                    case "Management":
                        content.Children.Clear();
                        content.Children.Add(new DefaultMainLoadUC() { });
                        break;
                    case "Contact":
                        content.Children.Clear();
                        content.Children.Add(new ContactUC() { });
                        break;
                    case "Company":
                        content.Children.Clear();
                        content.Children.Add(new CompanyUC() { });
                        break;
                    case "Station":
                        content.Children.Clear();
                        content.Children.Add(new StationUC() { });
                        break;
                    case "Lines":
                        content.Children.Clear();
                        content.Children.Add(new LineUC() { });
                        break;
                    case "Ticket View":
                        content.Children.Clear();
                        if (Const.isAdmin)
                            content.Children.Add(new AdminTicketViewUC() { });
                        else
                            content.Children.Add(new TicketsView() { });
                        break;
                    case "Ticket Manage":
                        content.Children.Clear();
                        content.Children.Add(new TicketSellingUC() { });
                        break;
                    case "Revenue":
                        content.Children.Clear();
                        content.Children.Add(new TicketSellingReportUC() { });
                        break;
                }
            }
            );
            LoadContentCommand = new RelayCommand<Grid>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                //SET DEFAULT CONTENT FOR CONTAINER HERE
                string name;
                if (Const.userId == 0)
                    name = "admin";
                else
                {
                    var company = DataProvider.Ins.DB.Companies.Where(x => x.id == Const.userId).First() as Company;
                    name = company.name;
                }
                UserName = name;
                content = p;
                p.Children.Add(new DefaultMainLoadUC() { });
                if (UserName == "admin")
                    admin();
                else
                    user();
            }
            );
        }

        private void admin()
        {
            SetAdmin = Visibility.Visible;
            SetUser = Visibility.Collapsed;
        }

        private void user()
        {
            SetAdmin = Visibility.Collapsed;
            SetUser = Visibility.Visible;
        }

        private void logout(TreeView a)
        {
            //LoginWindow login = new LoginWindow();

            //FrameworkElement parent = a;
            //while (parent.Parent != null)
            //{
            //    parent = parent.Parent as FrameworkElement;
            //}

            //Window w = parent as Window;
            //login.Show();
            //w.Close();
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
