using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFMetroManagement.NetFramework.UserControls
{
    /// <summary>
    /// Interaction logic for StationUC.xaml
    /// </summary>
    public partial class StationUC : UserControl
    {
        public StationUC()
        {
            InitializeComponent();
            var dockPanel = this.FindName("pnlStationInformation") as DockPanel;
            for (int i = 1; i < 5; i++)
            {
                StackPanel stackPanel = dockPanel.Children[i] as StackPanel;
                stackPanel.Children[2].Visibility = Visibility.Collapsed;
                stackPanel.Children[1].Visibility = Visibility.Visible;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
