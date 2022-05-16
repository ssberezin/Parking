using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ReportOps
{
    public class ParPlaceRecord:Helpes.ObservableObject
    {
        public ParPlaceRecord()
        {
           
        }

        public int ParkingPlaceId { get; set; }


        // public ParkingPlace PPlace { get; set; }


        private ParkingPlace pPlace;
        public ParkingPlace PPlace
        {
            get { return pPlace; }
            set
            {
                if (value != pPlace)
                {
                    pPlace = value;
                    OnPropertyChanged(nameof(PPlace));
                }
            }
        }

       




    }
}
