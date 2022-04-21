using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model
{
    public class ParkingPlace: Helpes.ObservableObject
    {        
        public ParkingPlace()
        {
            ParkingPlaceLogs = new ObservableCollection<ParkingPlaceLog>();
           
           
        }

        public int ParkingPlaceId { get; set; }

        //is it busy or free or booked
        private bool? freeStatus;
        public bool? FreeStatus
        {
            get { return freeStatus; }
            set
            {
                if (value != freeStatus)
                {
                    freeStatus = value;
                    OnPropertyChanged(nameof(FreeStatus));
                }
            }
        }

        private int parkPlaceNumber;
        public int ParkPlaceNumber
        {
            get { return parkPlaceNumber; }
            set
            {
                if (value != parkPlaceNumber)
                {
                    parkPlaceNumber = value;
                    OnPropertyChanged(nameof(ParkPlaceNumber));
                }
            }
        }

        //is vehicle on the parking place or not?
        private bool released;
        public bool Released
        {
            get { return released; }
            set
            {
                if (value != released)
                {
                    released = value;
                    OnPropertyChanged(nameof(Released));
                }
            }
        }


        public virtual ObservableCollection<ParkingPlaceLog> ParkingPlaceLogs { get; set; }


        public virtual Client SomeClient { get; set; }

    }
}
