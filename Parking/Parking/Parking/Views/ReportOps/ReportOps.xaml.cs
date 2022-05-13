using Parking.ViewModel.ReportOps;
using System.Windows;


namespace Parking.Views.ReportOps
{
    /// <summary>
    /// Логика взаимодействия для ReportOps.xaml
    /// </summary>
    public partial class ReportOps : Window
    {
        public ReportOps(int inputUserId)
        {
            InitializeComponent();
            DataContext = new ReportOpsContext(inputUserId);
        }
    }
}
