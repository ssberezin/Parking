using Parking.Helpes;
using Parking.ViewModel.DictionaryOps;
using Parking.ViewModel.VehicleTypesOps;
using System.Windows;

namespace Parking.Views.VehicleTypesOps
{
    /// <summary>
    /// Логика взаимодействия для VehicleTypesOpsWin.xaml
    /// </summary>
    public partial class VehicleTypesOpsWin : Window
    {
        public VehicleTypesOpsWin(IColorTypeInterface obj)
        {
            InitializeComponent();
            DataContext = new DictOpsContext(obj);
        }
    }
}
