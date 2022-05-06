using Parking.Helpes;
using Parking.Model;
using Parking.ViewModel.PrintOps;
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

namespace Parking.Views.PrintOps
{
    /// <summary>
    /// Логика взаимодействия для PrintBlank.xaml
    /// </summary>
    public partial class PrintBlank : Window
    {
        public PrintBlank(ParkingPlaceRecord parkRecord, string inputFIO, Company comp)
        {
            InitializeComponent();
            DataContext = new PrintBlankContext(parkRecord, inputFIO, comp);
        }
    }
}
