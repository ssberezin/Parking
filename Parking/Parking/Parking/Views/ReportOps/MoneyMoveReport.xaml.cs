using Parking.ViewModel.ReportOps.MoneyMoveReportOps;
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
using System.Windows.Shapes;

namespace Parking.Views.ReportOps
{
    /// <summary>
    /// Логика взаимодействия для MoneyMoveReport.xaml
    /// </summary>
    public partial class MoneyMoveReport : Window
    {
        public MoneyMoveReport()
        {
            InitializeComponent();
            DataContext = new MoneyMoveReportContext();
        }
    }
}
