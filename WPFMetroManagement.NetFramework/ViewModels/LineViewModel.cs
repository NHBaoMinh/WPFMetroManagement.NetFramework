using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMetroManagement.NetFramework.Model;
using WPFMetroManagement.NetFramework.UserControls;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class LineViewModel : BaseViewModel
    {
        private ObservableCollection<Line> _listLine;
        public ObservableCollection<Line> ListLine
        {
            get => _listLine;
            set { _listLine = value; OnPropertyChanged(); }
        }
        public ICommand LoadContentCommand { get; set; }
        private ObservableCollection<Model.Company> _Company;
        public ObservableCollection<Model.Company> Company { get => _Company; set { _Company = value; OnPropertyChanged(); } }
        private Visibility _setAdmin;
        public Visibility SetAdmin { get => _setAdmin; set { _setAdmin = value; OnPropertyChanged(); } }
        private BindableCollection<Station> _listStation { get; set; }
        public BindableCollection<Station> ListStation
        {
            get => _listStation;
            set { _listStation = value; OnPropertyChanged(); }
        }
        private Station _selectedStationStart;
        public Station selectedStationStart
        {
            get => _selectedStationStart;
            set
            {
                _selectedStationStart = value;
                OnPropertyChanged();
            }
        }
        private Station _selectedStationEnd;
        public Station selectedStationEnd
        {
            get => _selectedStationEnd;
            set
            {
                _selectedStationEnd = value;
                OnPropertyChanged();
            }
        }
        private Station _selectedAddStationStart;
        public Station selectedAddStationStart
        {
            get => _selectedAddStationStart;
            set
            {
                _selectedAddStationStart = value;
                OnPropertyChanged();
            }
        }
        private Station _selectedAddStationEnd;
        public Station selectedAddStationEnd
        {
            get => _selectedAddStationEnd;
            set
            {
                _selectedAddStationEnd = value;
                OnPropertyChanged();
            }
        }

        #region Declare for Detail and Update Line Information
        private Line _selectedLine;
        public Line SelectedLine
        {
            get => _selectedLine;

            set
            {
                _selectedLine = value;
                if (SelectedLine.id == 0) return;
                txtUpdateLineID = SelectedLine.id.ToString();
                txtUpdateCompanyID = SelectedLine.idCompany.ToString();
                txtUpdateLineName = SelectedLine.lineName;
                txtUpdateLineStart = SelectedLine.Station1.stationName;
                txtUpdateLineStop = SelectedLine.stops;
                txtUpdateLineEnd = SelectedLine.Station.stationName;
                int fixType = SelectedLine.lineType.IndexOf(" ");
                txtUpdateLineType = SelectedLine.lineType.Substring(fixType + 1);
                txtUpdateOpenTime = SelectedLine.openTime;
                txtUpdateWaitTime = SelectedLine.awTime;
                txtUpdateCloseTime = SelectedLine.closeTime;
                int fixStatus = SelectedLine.statusLine.IndexOf(" ");
                txtUpdateLineStatus = SelectedLine.statusLine.Substring(fixStatus + 1);
                OnPropertyChanged();
            }
        }
        private Model.Company _SelectedCompany;
        public Model.Company SelectedCompany
        {
            get => _SelectedCompany;
            set
            {
                _SelectedCompany = value;
                OnPropertyChanged();
            }
        }

        private string _updateLineID;
        private string _updateCompanyID;
        private string _updateLineName;
        private string _updateLineStart;
        private string _updateLineStop;
        private string _updateLineEnd;
        private ComboBox _cbbUpdateLineType;
        private string _updateLineType;
        private bool _IconUpdateLine;
        private string _updateOpenTime;
        private string _updateWaitTime;
        private string _updateCloseTime;
        private ComboBox _cbbUpdateLineStatus;
        private string _updateLineStatus;

        public string txtUpdateLineID { get => _updateLineID; set { _updateLineID = value; OnPropertyChanged(); } }
        public string txtUpdateCompanyID { get => _updateCompanyID; set { _updateCompanyID = value; OnPropertyChanged(); } }
        public string txtUpdateLineName { get => _updateLineName; set { _updateLineName = value; OnPropertyChanged(); } }
        public string txtUpdateLineStart { get => _updateLineStart; set { _updateLineStart = value; OnPropertyChanged(); } }
        public string txtUpdateLineStop { get => _updateLineStop; set { _updateLineStop = value; OnPropertyChanged(); } }
        public string txtUpdateLineEnd { get => _updateLineEnd; set { _updateLineEnd = value; OnPropertyChanged(); } }
        public string txtSearchLine { get => _txtSearch; set { _txtSearch = value; OnPropertyChanged(); InitListLines(); } }
        public string txtUpdateLineType { get => _updateLineType; set { _updateLineType = value; OnPropertyChanged(); } }
        public ComboBox cbbUpdateLineType { get => _cbbUpdateLineType; set { _cbbUpdateLineType = value; OnPropertyChanged(); } }
        public string txtUpdateOpenTime { get => _updateOpenTime; set { _updateOpenTime = value; OnPropertyChanged(); } }
        public string txtUpdateWaitTime { get => _updateWaitTime; set { _updateWaitTime = value; OnPropertyChanged(); } }
        public string txtUpdateCloseTime { get => _updateCloseTime; set { _updateCloseTime = value; OnPropertyChanged(); } }
        public string txtUpdateLineStatus { get => _updateLineStatus; set { _updateLineStatus = value; OnPropertyChanged(); } }
        public ComboBox cbbUpdateLineStatus { get => _cbbUpdateLineStatus; set { _cbbUpdateLineStatus = value; OnPropertyChanged(); } }
        public bool iconUpdateLine { get => _IconUpdateLine; set { _IconUpdateLine = value; OnPropertyChanged(); } }

        public ICommand SaveUpdateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        #endregion

        #region Declare for Adding new Station
        private string _txtSearch;
        private string _txtAddLineID;
        private string _txtAddIDCompany;
        private string _txtAddLineName;
        private string _txtAddLineStart;
        private string _txtAddLineStop;
        private string _txtAddLineEnd;
        private string _txtAddLineType;
        private string _txtAddOpenTime;
        private string _txtAddWaitTime;
        private string _txtAddCloseTime;
        private string _txtAddLineStatus;
        private string _txtAddOpenTimeH;
        private string _txtAddOpenTimeM;
        private string _txtAddWaitTimeH;
        private string _txtAddWaitTimeM;
        private string _txtAddCloseTimeH;
        private string _txtAddCloseTimeM;

        public string txtAddLineID { get => _txtAddLineID; set { _txtAddLineID = value; OnPropertyChanged(); } }
        public string txtAddIDCompany { get => _txtAddIDCompany; set { _txtAddIDCompany = value; OnPropertyChanged(); } }
        public string txtAddLineName { get => _txtAddLineName; set { _txtAddLineName = value; OnPropertyChanged(); } }
        public string txtAddLineStart { get => _txtAddLineStart; set { _txtAddLineStart = value; OnPropertyChanged(); } }
        public string txtAddLineStop { get => _txtAddLineStop; set { _txtAddLineStop = value; OnPropertyChanged(); } }
        public string txtAddLineEnd { get => _txtAddLineEnd; set { _txtAddLineEnd = value; OnPropertyChanged(); } }
        public string txtAddLineType { get => _txtAddLineType; set { _txtAddLineType = value; OnPropertyChanged(); } }
        public string txtAddOpenTime { get => _txtAddOpenTime; set { _txtAddOpenTime = value; OnPropertyChanged(); } }
        public string txtAddWaitTime { get => _txtAddWaitTime; set { _txtAddWaitTime = value; OnPropertyChanged(); } }
        public string txtAddCloseTime { get => _txtAddCloseTime; set { _txtAddCloseTime = value; OnPropertyChanged(); } }
        public string txtAddLineStatus { get => _txtAddLineStatus; set { _txtAddLineStatus = value; OnPropertyChanged(); } }

        public string txtAddOpenTimeH { get => _txtAddOpenTimeH; set { _txtAddOpenTimeH = value; OnPropertyChanged(); } }

        public string txtAddOpenTimeM { get => _txtAddOpenTimeM; set { _txtAddOpenTimeM = value; OnPropertyChanged(); } }
        public string txtAddWaitTimeH { get => _txtAddWaitTimeH; set { _txtAddWaitTimeH = value; OnPropertyChanged(); } }

        public string txtAddWaitTimeM { get => _txtAddWaitTimeM; set { _txtAddWaitTimeM = value; OnPropertyChanged(); } }
        public string txtAddCloseTimeH { get => _txtAddCloseTimeH; set { _txtAddCloseTimeH = value; OnPropertyChanged(); } }

        public string txtAddCloseTimeM { get => _txtAddCloseTimeM; set { _txtAddCloseTimeM = value; OnPropertyChanged(); } }


        public ICommand AddCommand { set; get; }
        public ICommand SaveAddingLineCommand { set; get; }
        #endregion

        public LineViewModel()
        {
            if (Const.isAdmin)
                SetAdmin = Visibility.Collapsed;
            ListStation = new BindableCollection<Station>(DataProvider.Ins.DB.Stations.Where(x => x.statusStatus.Equals("Normal")));

            LoadContentCommand = new RelayCommand<DataGrid>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                //Load Lines by Company Id
                if (Const.userId == 0)
                    ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines);
                else
                    ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.idCompany == Const.userId));
            }
            );
            InitListLines();

            SelectedLine = ListLine.Count > 0 ? ListLine[0] : new Line();
            iconUpdateLine = false;

            #region Adding new Line func
            AddCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                int LineIndexNext = ListLine.Count + 1;
                p.IsPopupOpen = true;
                txtAddLineID = (LineIndexNext).ToString();
                txtAddIDCompany = Const.userId.ToString();
            });
            SaveAddingLineCommand = new RelayCommand<PopupBox>((p) => { return txtAddLineName != "" ? true : false; }, (p) =>
            {
                try
                {
                    if (txtAddLineName == null)
                    {
                        MessageBox.Show("Line name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (selectedAddStationStart == null)
                    {
                        MessageBox.Show("Station Start cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (selectedAddStationEnd == null)
                    {
                        MessageBox.Show("Station End cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (selectedAddStationStart.id == selectedAddStationEnd.id)
                    {
                        MessageBox.Show("Station Start cannot be the same as Station End!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtAddLineType == null)
                    {
                        MessageBox.Show("Please enter type of line!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtAddLineStatus == null)
                    {
                        MessageBox.Show("Please enter line status!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtAddOpenTimeH == null || txtAddOpenTimeM == null)
                    {
                        MessageBox.Show("Open time input incorrectly!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtAddWaitTimeM == null)
                    {
                        if (txtAddWaitTimeH == null)
                            txtAddWaitTimeH = "0h";
                        MessageBox.Show("Wait time required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtAddCloseTimeH == null || txtAddCloseTimeM == null)
                    {
                        MessageBox.Show("Close time input incorrectly!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    int fixStatus = txtAddLineStatus.IndexOf(" ");
                    int fixType = txtAddLineType.IndexOf(" ");
                    int fixHours = txtAddLineType.IndexOf(" ");
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Line] ON");
                    DataProvider.Ins.DB.Lines.Add(new Line { id = (ListLine.Count + 1), idCompany = Const.userId, lineName = txtAddLineName, lineStartId = selectedAddStationStart.id, stops = txtAddLineStop, lineEndId = selectedAddStationEnd.id, lineType = txtAddLineType.Substring(fixType + 1), openTime = txtAddOpenTimeH.Substring(fixHours + 1) + ":" + txtAddOpenTimeM.Substring(fixHours + 1), awTime = txtAddWaitTimeH.Substring(fixHours + 1) + ":" + txtAddWaitTimeM.Substring(fixHours + 1), closeTime = txtAddCloseTimeH.Substring(fixHours + 1) + ":" + txtAddCloseTimeM.Substring(fixHours + 1), statusLine = txtAddLineStatus.Substring(fixStatus + 1) });
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Line] OFF");
                    DataProvider.Ins.DB.SaveChanges();
                    Clear();
                }
                catch (Exception e)
                {
                    if (e.Message == "An error occurred while updating the entries. See the inner exception for details.")
                    {

                    }
                }
                InitListLines();
                p.IsPopupOpen = false;
            });
            #endregion

            #region Update Line information func
            UpdateCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (SelectedLine.id == 0) return;
                txtUpdateLineID = SelectedLine.id.ToString();
                txtUpdateCompanyID = SelectedLine.idCompany.ToString();
                txtUpdateLineName = SelectedLine.lineName;
                txtUpdateLineStart = SelectedLine.Station1.stationName;
                txtUpdateLineStop = SelectedLine.stops;
                txtUpdateLineEnd = SelectedLine.Station.stationName;
                txtUpdateLineType = SelectedLine.lineType;
                txtUpdateOpenTime = SelectedLine.openTime;
                txtUpdateWaitTime = SelectedLine.awTime;
                txtUpdateCloseTime = SelectedLine.closeTime;
                txtUpdateLineStatus = SelectedLine.statusLine;

                iconUpdateLine = true;
                for (int i = 1; i < 11; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[1].Visibility = Visibility.Collapsed;
                    stackPanel.Children[2].Visibility = Visibility.Visible;
                }
                (p.Children[11] as Canvas).Visibility = Visibility.Visible;
            });
            SaveUpdateCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                try
                {
                    int fixStatus = txtUpdateLineStatus.IndexOf(" ");
                    int fixType = txtUpdateLineType.IndexOf(" ");
                    SelectedLine.idCompany = int.Parse(txtUpdateCompanyID);
                    SelectedLine.lineName = txtUpdateLineName;
                    if (selectedStationStart != null)
                    {
                        SelectedLine.lineStartId = selectedStationStart.id;
                        txtUpdateLineStart = selectedStationStart.stationName;
                    }
                    SelectedLine.stops = txtUpdateLineStop;
                    if (selectedStationEnd != null)
                    {
                        SelectedLine.lineEndId = selectedStationEnd.id;
                        txtUpdateLineEnd = selectedStationEnd.stationName;
                    }
                    SelectedLine.lineType = txtUpdateLineType.Substring(fixType + 1);
                    SelectedLine.openTime = txtUpdateOpenTime;
                    SelectedLine.awTime = txtUpdateWaitTime;
                    SelectedLine.closeTime = txtUpdateCloseTime;
                    SelectedLine.statusLine = txtUpdateLineStatus.Substring(fixStatus + 1);
                    if (String.IsNullOrWhiteSpace(txtUpdateLineName))
                    {
                        MessageBox.Show("Line name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(txtUpdateLineType))
                    {
                        MessageBox.Show("Please enter type of line!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(txtUpdateOpenTime))
                    {
                        MessageBox.Show("Please enter open time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(txtUpdateWaitTime))
                    {
                        MessageBox.Show("Please enter wait time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(txtUpdateCloseTime))
                    {
                        MessageBox.Show("Please enter close time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(txtUpdateLineStatus))
                    {
                        MessageBox.Show("Please enter line status!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (selectedStationStart != null && selectedStationEnd == null && selectedStationStart.stationName.Equals(SelectedLine.Station.stationName))
                    {
                        MessageBox.Show("Station Start cannot the same at Station End!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (selectedStationStart == null && selectedStationEnd != null && selectedStationEnd.stationName.Equals(SelectedLine.Station1.stationName))
                    {
                        MessageBox.Show("Station End cannot the same at Station Start!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (selectedStationStart != null && selectedStationEnd != null && selectedStationStart.stationName.Equals(selectedStationEnd.stationName))
                    {
                        MessageBox.Show("Station Start cannot the same at Station End!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                InitListLines();

                iconUpdateLine = false;
                for (int i = 1; i < 11; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[2].Visibility = Visibility.Collapsed;
                    stackPanel.Children[1].Visibility = Visibility.Visible;
                }
                (p.Children[11] as Canvas).Visibility = Visibility.Collapsed;

            });
            #endregion
        }

        private ObservableCollection<Line> initLines()
        {
            if (Const.userId == 0)
                return new ObservableCollection<Line>(DataProvider.Ins.DB.Lines);
            else
                return new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.idCompany == Const.userId));
        }

        private void Clear()
        {
            txtAddLineName = "";
            txtAddLineStart = "";
            txtAddLineStop = "";
            txtAddLineEnd = "";
            txtAddLineType = "";
        }
        private void InitListLines()
        {
            if (txtSearchLine == null || txtSearchLine.Equals(""))
            {
                if (Const.userId == 0)
                    ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines);
                else
                    ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.idCompany == Const.userId));
            }
            else
            {
                if (Const.userId == 0)
                    ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.lineName.ToLower().Contains(txtSearchLine.ToLower())));
                else
                    ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines.Where(x => x.lineName.ToLower().Contains(txtSearchLine.ToLower()) && x.idCompany == Const.userId));
            }

        }
    }
}