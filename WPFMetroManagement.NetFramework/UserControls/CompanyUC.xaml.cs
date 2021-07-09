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
    /// Interaction logic for CompanyUC.xaml
    /// </summary>
    public partial class CompanyUC : UserControl
    {
        public CompanyUC()
        {
            InitializeComponent();
            var dockPanel = this.FindName("pnlCompanyInformation") as DockPanel;
            for (int i = 1; i < 6; i++)
            {
                StackPanel stackPanel = dockPanel.Children[i] as StackPanel;
                stackPanel.Children[2].Visibility = Visibility.Collapsed;
                stackPanel.Children[1].Visibility = Visibility.Visible;
            }
        }
    }
}
