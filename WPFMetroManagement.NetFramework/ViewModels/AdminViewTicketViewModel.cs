using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMetroManagement.NetFramework.Model;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class AdminViewTicketViewModel : BaseViewModel
    {
        #region Declare variable
        private ObservableCollection<Ticket> _listTicket;
        public ObservableCollection<Ticket> ListTicket
        {
            get => _listTicket;
            set { _listTicket = value; OnPropertyChanged(); }
        }
        private string _txtSearchTicket;
        public string txtSearchTicket { get => _txtSearchTicket; set { _txtSearchTicket = value; OnPropertyChanged(); InitListTickets(); } }

        private void InitListTickets()
        {
            if (txtSearchTicket == null || txtSearchTicket.Equals(""))
                ListTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets);
            else
            {
                if (isNumber(txtSearchTicket))
                {
                    int i = int.Parse(txtSearchTicket);
                    ListTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.id == i));
                }
                else
                    ListTicket = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.Line.lineName.ToLower().Contains(txtSearchTicket)));
            }
        }
        #endregion
        public AdminViewTicketViewModel()
        {
            InitListTickets();
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
