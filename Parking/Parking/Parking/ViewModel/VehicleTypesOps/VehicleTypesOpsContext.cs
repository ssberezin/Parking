using Parking.Helpes;
using Parking.Model;
using Parking.Views.CompanyOps;
using Parking.Views.ParkPlacesOps;
using Parking.Views.PersonOperations;
using Parking.Views.ReportOps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.VehicleTypesOps
{
    public class VehicleTypesOpsContext:Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;
        
        public ObservableCollection<VehicleType> VehicleTypes { get; set; }


        private VehicleType vType;
        public VehicleType VType
        {
            get { return vType; }
            set
            {
                if (vType != value)
                {
                    vType = value;
                    OnPropertyChanged(nameof(VType));
                }
            }
        }

        Library lib;
        
        public VehicleTypesOpsContext()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            lib = new Library();
            VehicleTypes = new ObservableCollection<VehicleType>();
        }



    }
}
