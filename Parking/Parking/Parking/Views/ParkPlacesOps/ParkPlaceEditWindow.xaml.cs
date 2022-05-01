using Parking.Helpes;
using Parking.ViewModel.ParkPlacesOps;
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

namespace Parking.Views.ParkPlacesOps
{
  
    public partial class ParkPlaceEditWindow : Window
    {
        public ParkPlaceEditWindow(int UserId, ParkingPlaceRecord parkRecord)
        {
            InitializeComponent();
            DataContext = new ParkPlaceWindowContext(UserId, parkRecord);
        }
    }
}
