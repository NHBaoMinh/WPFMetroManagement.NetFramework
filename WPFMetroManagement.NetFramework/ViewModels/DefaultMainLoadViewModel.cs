using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMetroManagement.NetFramework.Model;

namespace WPFMetroManagement.NetFramework.ViewModels
{
    public class DefaultMainLoadViewModel : BaseViewModel
    {
        private ObservableCollection<Line> _listLine;
        public ObservableCollection<Line> ListLine { get => _listLine; set { _listLine = value; OnPropertyChanged(); } }
        public DefaultMainLoadViewModel()
        {
            ListLine = new ObservableCollection<Line>(DataProvider.Ins.DB.Lines);
          
        }
    }
}
