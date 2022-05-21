
using System;

namespace Parking.ViewModel.ReportOps
{
    public class ReportOpsRecord:Helpes.ObservableObject
    {
       
        public int VehicleId { get; set; }
        // public string VehicleNumber { get; set;}
        private string vehicleNumber;
        public string VehicleNumber
        {
            get { return vehicleNumber; }
            set
            {
                if (vehicleNumber != value)
                {
                    vehicleNumber = value;
                    OnPropertyChanged(nameof(VehicleNumber));
                }
            }
        }
        public DateTime EventDate { get; set; }      
       
        public int UserId { get; set; }
        public string UserData { get; set; }

    }
}
