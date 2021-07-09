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
using WPFMetroManagement.NetFramework.UserControls;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class StationViewModel : BaseViewModel
    {
        private ObservableCollection<Station> _listStation;
        public ObservableCollection<Station> ListStation
        {
            get => _listStation;
            set { _listStation = value; OnPropertyChanged(); }
        }

        #region Declare for Detail and Update Station Information
        private Station _selectedStation;
        public Station SelectedStation
        {
            get => _selectedStation;

            set
            {
                _selectedStation = value;
                if (SelectedStation == null) return;
                txtUpdateStationID = SelectedStation.id.ToString();
                txtUpdateStationName = SelectedStation.stationName;
                txtUpdateStationLocation = SelectedStation.locationDescription;
                int fixStatus = SelectedStation.statusStatus.IndexOf(" ");
                txtUpdateStationStatus = SelectedStation.statusStatus.Substring(fixStatus + 1);
                OnPropertyChanged();
            }
        }

        private string _updateStaionID;
        private string _updateStationName;
        private string _updateStationLocation;
        private string _updateStationStatus;
        private ComboBox _cbbUpdateStationStatus;
        private bool _IconUpdateStation;

        public string txtUpdateStationID { get => _updateStaionID; set { _updateStaionID = value; OnPropertyChanged(); } }
        public string txtUpdateStationName { get => _updateStationName; set { _updateStationName = value; OnPropertyChanged(); } }
        public string txtUpdateStationLocation { get => _updateStationLocation; set { _updateStationLocation = value; OnPropertyChanged(); } }
        public string txtUpdateStationStatus { get => _updateStationStatus; set { _updateStationStatus = value; OnPropertyChanged(); } }
        public string txtSearchStation { get => _txtSearch; set { _txtSearch = value; OnPropertyChanged(); InitListStation(); } }
        public ComboBox cbbUpdateStationStatus { get => _cbbUpdateStationStatus; set { _cbbUpdateStationStatus = value; OnPropertyChanged(); } }
        public ICommand SaveUpdateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public bool iconUpdateStation { get => _IconUpdateStation; set { _IconUpdateStation = value; OnPropertyChanged(); } }
        #endregion

        #region Declare for Adding new Station
        private string _txtSearch;
        private string _txtAddStationID;
        private string _txtAddStationName;
        private string _txtAddStationLocation;
        private string _txtAddStationStatus;

        public string txtAddStationID { get => _txtAddStationID; set { _txtAddStationID = value; OnPropertyChanged(); } }
        public string txtAddStationName { get => _txtAddStationName; set { _txtAddStationName = value; OnPropertyChanged(); } }
        public string txtAddStationLocation { get => _txtAddStationLocation; set { _txtAddStationLocation = value; OnPropertyChanged(); } }
        public string txtAddStationStatus { get => _txtAddStationStatus; set { _txtAddStationStatus = value; OnPropertyChanged(); } }
        public ICommand AddCommand { set; get; }
        public ICommand SaveAddingStationCommand { set; get; }
        #endregion

        public StationViewModel()
        {
            InitListStation();


            SelectedStation = ListStation.Count > 0 ? ListStation[0] : new Station();
            iconUpdateStation = false;

            #region Adding new Station func
            AddCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                int StationIndexNext = ListStation.Count + 1;
                p.IsPopupOpen = true;
                txtAddStationID = (StationIndexNext).ToString();
            });
            SaveAddingStationCommand = new RelayCommand<PopupBox>((p) => { return txtAddStationName != "" ? true : false; }, (p) =>
            {
                int fixStatus = txtAddStationStatus.IndexOf(" ");
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Station] ON");
                DataProvider.Ins.DB.Stations.Add(new Station { id = (ListStation.Count + 1), stationName = txtAddStationName, locationDescription = txtAddStationLocation, map = "ne", statusStatus = txtAddStationStatus.Substring(fixStatus + 1) });
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Station] OFF");
                DataProvider.Ins.DB.SaveChanges();
                InitListStation();
                p.IsPopupOpen = false;
            });
            #endregion

            #region Update Station information func
            UpdateCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                txtUpdateStationID = SelectedStation.id.ToString();
                txtUpdateStationName = SelectedStation.stationName;
                txtUpdateStationLocation = SelectedStation.locationDescription;
                txtUpdateStationStatus = SelectedStation.statusStatus;

                iconUpdateStation = true;
                for (int i = 1; i < 5; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[1].Visibility = Visibility.Collapsed;
                    stackPanel.Children[2].Visibility = Visibility.Visible;
                }
                (p.Children[5] as Canvas).Visibility = Visibility.Visible;
            });
            SaveUpdateCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                int fixStatus = txtUpdateStationStatus.IndexOf(" ");
                SelectedStation.stationName = txtUpdateStationName;
                SelectedStation.locationDescription = txtUpdateStationLocation;
                SelectedStation.statusStatus = txtUpdateStationStatus.Substring(fixStatus + 1);
                DataProvider.Ins.DB.SaveChanges();
                InitListStation();


                iconUpdateStation = false;
                for (int i = 1; i < 5; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[2].Visibility = Visibility.Collapsed;
                    stackPanel.Children[1].Visibility = Visibility.Visible;
                }
                (p.Children[5] as Canvas).Visibility = Visibility.Collapsed;

            });
            #endregion
        }

        private void InitListStation()
        {
            if (txtSearchStation == null || txtSearchStation.Equals(""))
                ListStation = new ObservableCollection<Station>(DataProvider.Ins.DB.Stations);
            else
                ListStation = new ObservableCollection<Station>(DataProvider.Ins.DB.Stations.Where(x => x.stationName.ToLower().Contains(txtSearchStation.ToLower())));
        }
    }
}
