using Parking.ViewModel.VehicleTypesOps;
using System.Windows;

namespace Parking.Views.VehicleTypesOps
{
    /// <summary>
    /// Логика взаимодействия для VehicleTypesOpsWin.xaml
    /// </summary>
    public partial class VehicleTypesOpsWin : Window
    {
        public VehicleTypesOpsWin()
        {
            InitializeComponent();
            DataContext = new VehicleTypesOpsContext();
        }
    }
}
