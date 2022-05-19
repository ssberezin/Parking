
using System.Windows;


namespace Parking.Views.FilterOps
{
    /// <summary>
    /// Логика взаимодействия для FolterForReportWindow.xaml
    /// </summary>
    public partial class FolterForReportWindow : Window
    {
        public FolterForReportWindow(object DataContext)
        {
            InitializeComponent();
            this.DataContext = DataContext;
        }
    }
}
