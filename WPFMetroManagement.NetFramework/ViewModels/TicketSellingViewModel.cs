using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMetroManagement.NetFramework.Model;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class TicketSellingViewModel : BaseViewModel
    {
        #region Declare variable
        private int num = 0;
        private int price = 0;
        private string _txtSearch;
        public string txtSearchLine { get => _txtSearch; set { _txtSearch = value; OnPropertyChanged(); InitListLines(); } }

        private void InitListLines()
        {
            if (txtSearchLine == null || txtSearchLine.Equals(""))
            {
                ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.idCompany == Const.userId && x.statusLine.Equals("Active")));
            }
            else
            {
                ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.lineName.ToLower().Contains(txtSearchLine.ToLower()) && x.idCompany == Const.userId && x.statusLine.Equals("Active")));
            }
        }
        private Line _selectedLine;
        public Line SelectedLine
        {
            get => _selectedLine;

            set
            {
                _selectedLine = value;
                if (SelectedLine == null) return;
                txtLineName = SelectedLine.lineName;
                OnPropertyChanged();
            }
        }
        private string _txtLineName;
        public string txtLineName { get => _txtLineName; set { _txtLineName = value; OnPropertyChanged(); } }
        private string _txtTicketType;
        public string txtTicketType { get => _txtTicketType; set { _txtTicketType = value; OnPropertyChanged(); } }
        private string _txtNumberCreate;
        public string txtNumberCreate { get => _txtNumberCreate; set { _txtNumberCreate = value; OnPropertyChanged(); } }
        private string _txtPriceCreate;
        public string txtPriceCreate { get => _txtPriceCreate; set { _txtPriceCreate = value; OnPropertyChanged(); } }
        private string _txtPriceUpdate;
        public string txtPriceUpdate { get => _txtPriceUpdate; set { _txtPriceUpdate = value; OnPropertyChanged(); } }
        public ICommand CreateCommand { set; get; }
        public ICommand UpdateCommand { set; get; }

        private ObservableCollection<Line> _listLine;
        public ObservableCollection<Line> ListLine
        {
            get => _listLine;
            set { _listLine = value; OnPropertyChanged(); }
        }
        #endregion

        public TicketSellingViewModel()
        {
            InitListLines();

            CreateCommand = new RelayCommand<Grid>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (validCreate())
                {
                    int fix = txtTicketType.IndexOf(" ");
                    string s = txtTicketType.Substring(fix + 1);
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Ticket] ON");
                    for (int i = 1; i <= num; i++)
                    {
                        DataProvider.Ins.DB.Tickets.Add(new Ticket { id = i, idRoute = SelectedLine.id, tType = s, price = price, isUsed = false, isSell = false });
                    }
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Station] OFF");
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Initialize Tickets success!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    clear();
                }
                else
                {
                    MessageBox.Show("Invalid Input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            UpdateCommand = new RelayCommand<Grid>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (validUpdate())
                {
                    int fix = txtTicketType.IndexOf(" ");
                    string s = txtTicketType.Substring(fix + 1);
                    ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.idRoute == SelectedLine.id && x.tType == s));
                    if (tickets.Count() == 0)
                    {
                        MessageBox.Show("This kind of ticket haven't created yet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        foreach (var item in tickets)
                        {
                            if (item.isSell == false)
                                item.price = price;
                        }
                        MessageBox.Show("Update Tickets price success!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        DataProvider.Ins.DB.SaveChanges();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        }

        private void clear()
        {
            txtNumberCreate = txtPriceCreate = txtPriceUpdate = "";
        }

        private bool validUpdate()
        {
            if (txtTicketType == null)
                return false;
            if (!isNumber(txtPriceUpdate))
                return false;
            price = int.Parse(txtPriceUpdate);
            if (price < 1000)
                return false;
            return true;
        }
        private bool validCreate()
        {
            if (txtTicketType == null)
                return false;
            if (!(isNumber(txtNumberCreate) && isNumber(txtPriceCreate)))
                return false;
            num = int.Parse(txtNumberCreate);
            price = int.Parse(txtPriceCreate);
            if (num < 1 || num > 100 || price < 1000)
                return false;
            return true;
        }
        private bool isNumber(String s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
