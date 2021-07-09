using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMetroManagement.NetFramework.Model;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class TicketSellingReportViewModel : BaseViewModel
    {
        #region Declare Variable
        private int count = 0;
        private int sumRegular = 0;
        private int sumMonthly = 0;
        private int i1, i2, i3, i4, i0 = 0;
        private int y1, y2, y3, y4, y0 = 0;
        private string _txtRegularTotal;
        public string txtRegularTotal { get => _txtRegularTotal; set { _txtRegularTotal = value; OnPropertyChanged(); } }
        private string _txtMonthlyTotal;
        public string txtMonthlyTotal { get => _txtMonthlyTotal; set { _txtMonthlyTotal = value; OnPropertyChanged(); } }
        private string _txtTotal;
        public string txtTotal { get => _txtTotal; set { _txtTotal = value; OnPropertyChanged(); } }
        private string _txtTotalCount;
        public string txtTotalCount { get => _txtTotalCount; set { _txtTotalCount = value; OnPropertyChanged(); } }
        private string _companyName;
        public string CompanyName { get => _companyName; set { _companyName = value; OnPropertyChanged(); } }
        private Func<double, string> formatter;
        public Func<double, string> Formatter { get => formatter; set { formatter = value; OnPropertyChanged(); } }
        private string[] _days;
        public string[] Days { get => _days; set { _days = value; OnPropertyChanged(); } }
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection = value; OnPropertyChanged(); } }
        public ICommand InitColumnChartCommand { get; set; }
        #endregion
        public TicketSellingReportViewModel()
        {
            initVariable();
            InitColumnChartCommand = new RelayCommand<Grid>(p => true, p => InitColumnChart());
            InitColumnChart();
        }

        private void InitColumnChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Regular Ticket",Values=new ChartValues<int>{2, 1, i2, i1, i0}
                    //Title="Regular",Values=new ChartValues<int>{i4,i3,i2,i1,i0}
                },
                new LineSeries
                {
                    Title="Monthly Ticket",Values=new ChartValues<int>{1, 0, y2, y1, y0}
                    //Title="Monthly",Values=new ChartValues<int>{y4, y3, y2, y1, y0}
                },
            };

            Days = new[] { DateTime.Now.AddDays(-4).ToString("m"), DateTime.Now.AddDays(-3).ToString("m"), DateTime.Now.AddDays(-2).ToString("m"), DateTime.Now.AddDays(-1).ToString("m"), DateTime.Now.ToString("m") };
            Formatter = value => value.ToString();
        }

        private void initVariable()
        {
            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>(DataProvider.Ins.DB.Tickets.Where(x => x.Line.idCompany == Const.userId && x.isSell == true));
            CompanyName = "Company name: " + DataProvider.Ins.DB.Companies.Where(x => x.id == Const.userId).First().name;
            foreach (var item in tickets)
            {
                count++;
                if (item.tType.Equals("Regular"))
                    sumRegular = (int)(sumRegular + item.price);
                else if (item.tType.Equals("Monthly"))
                    sumMonthly = (int)(sumMonthly + item.price);
            }
            txtTotalCount = count.ToString();
            txtRegularTotal = sumRegular.ToString() + " vnđ";
            txtMonthlyTotal = sumMonthly.ToString() + " vnđ";
            txtTotal = (sumMonthly + sumRegular).ToString() + " vnđ";
            initCountTicketSell(tickets);
        }

        private void initCountTicketSell(ObservableCollection<Ticket> tickets)
        {
            foreach (var item in tickets)
            {
                if (item.registerDate != null)
                {
                    if (item.tType.Equals("Regular"))
                    {
                        //(!((item.registerDate - DateTime.Now.AddDays(-1)) > TimeSpan.FromMilliseconds(1d)))
                        if (DateTime.Compare((DateTime)item.registerDate, DateTime.Now.AddDays(-4)) == 0)
                            i4++;
                        if (DateTime.Compare((DateTime)item.registerDate, DateTime.Now.AddDays(-3)) == 0)
                            i3++;
                        if (item.registerDate.Value.Day == DateTime.Now.AddDays(-2).Day)
                            i2++;
                        if (item.registerDate.Value.Day == DateTime.Now.AddDays(-1).Day)
                            i1++;
                        if (item.registerDate.Value.Day == DateTime.Now.Day)
                            i0++;
                    }
                    else if (item.tType.Equals("Monthly"))
                    {
                        if (DateTime.Compare((DateTime)item.registerDate, DateTime.Now.AddDays(-4)) == 0)
                            y4++;
                        if (DateTime.Compare((DateTime)item.registerDate, DateTime.Now.AddDays(-3)) == 0)
                            y3++;
                        if (item.registerDate.Value.Day == DateTime.Now.AddDays(-2).Day)
                            y2++;
                        if (item.registerDate.Value.Day == DateTime.Now.AddDays(-1).Day)
                            y1++;
                        if (item.registerDate.Value.Day == DateTime.Now.Day)
                            y0++;
                    }
                }
            }
        }
    }
}
