using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFMetroManagement.NetFramework.Model;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class ViewTicketViewModel : BaseViewModel
    {
        #region Declare variable
        private Ticket _selectedTicket;
        public Ticket SelectedTicket
        {
            get => _selectedTicket;

            set
            {
                _selectedTicket = value;
                if (SelectedTicket == null) return;
                if (SelectedTicket.isSell == true)
                {
                    if (SelectedTicket.isUsed == true)
                    {
                        isSellable = false;
                        isInvalid = false;
                    }
                    else
                    {
                        if (SelectedTicket.tType.Equals("Regular"))
                            isInvalid = true;
                        else
                            isInvalid = false;
                        isSellable = false;
                    }
                }
                else
                {
                    isSellable = true;
                    isInvalid = false;
                }

                OnPropertyChanged();
            }
        }
        private ObservableCollection<Ticket> _listSellTicket;
        public ObservableCollection<Ticket> ListSelledTicket
        {
            get => _listSellTicket;
            set { _listSellTicket = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Ticket> _listUnSellTicket;
        public ObservableCollection<Ticket> ListUnSellTicket
        {
            get => _listUnSellTicket;
            set { _listUnSellTicket = value; OnPropertyChanged(); }
        }
        private string _txtSearchTicket;
        public string txtSearchTicket { get => _txtSearchTicket; set { _txtSearchTicket = value; OnPropertyChanged(); InitListTickets(); } }
        private bool _isSellable;
        public bool isSellable { get => _isSellable; set { _isSellable = value; OnPropertyChanged(); } }
        private bool _isInvalid;
        public bool isInvalid { get => _isInvalid; set { _isInvalid = value; OnPropertyChanged(); } }
        public ICommand SellCommand { set; get; }
        public ICommand ValidCommand { set; get; }
        #endregion
        public ViewTicketViewModel()
        {
            InitListTickets();
            validTicketsCheck();
            SellCommand = new RelayCommand<Grid>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (SelectedTicket.tType.Equals("Monthly"))
                {
                    SelectedTicket.registerDate = DateTime.Now;
                    SelectedTicket.expiryDate = DateTime.Now.AddDays(30);
                }
                else
                    SelectedTicket.registerDate = DateTime.Now;
                SelectedTicket.isSell = true;
                DataProvider.Ins.DB.SaveChanges();
                InitListTickets();
            });

            ValidCommand = new RelayCommand<Grid>((p) => { return p == null ? false : true; }, (p) =>
            {
                SelectedTicket.isUsed = true;
                SelectedTicket.expiryDate = DateTime.Now;
                DataProvider.Ins.DB.SaveChanges();
                InitListTickets();
            });
        }

        private void validTicketsCheck()
        {
            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.isSell == true && x.expiryDate != null));
            foreach (var item in tickets)
            {
                int compare = DateTime.Compare((DateTime)item.expiryDate, DateTime.Now);
                if (compare > 0)
                {
                    item.isUsed = true;
                }
            }
        }

        private void InitListTickets()
        {
            if (txtSearchTicket == null || txtSearchTicket.Equals(""))
            {
                ListSelledTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.Line.idCompany == Const.userId && x.isSell == true && x.Line.idCompany == Const.userId));
                ListUnSellTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.Line.idCompany == Const.userId && x.isSell == false && x.Line.idCompany == Const.userId && x.Line.statusLine.Equals("Active")));
            }
            else
            {
                int i = int.Parse(txtSearchTicket);
                ListSelledTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.Line.idCompany == Const.userId && x.isSell == true && x.id == i));
                ListUnSellTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.Line.idCompany == Const.userId && x.isSell == false && x.id == i && x.Line.statusLine.Equals("Active")));
            }
        }
    }
}
