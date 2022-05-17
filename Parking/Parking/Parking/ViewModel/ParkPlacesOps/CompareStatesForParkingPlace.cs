using Parking.Helpes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ParkPlacesOps
{
    public class CompareStatesForParkingPlace
    {
        public string Owner { get; set; }
        public string OwnerSecondName { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerPatronimic { get; set; }
        public bool OwnerSex { get; set; }
        
        public string OwnerPhone { get; set; }
        public string Adress { get; set; }
        public byte[] VPhoto { get; set; }

        public DateTime DeadLine { get; set; }

        public string TrustSecondName { get; set; }
        public string TrustFirstName { get; set; }
        public string TrustPatronimic { get; set; }
        public bool TrustPersSex { get; set; }
        
        public string TrustPhone { get; set; }


        public string RegNumber { get; set; }
        public string Color { get; set; }
        public string VType { get; set; }

        public DateTime ProlongDate { get; set; }
        public decimal Coast { get; set; }
   

        
        public bool ReleasedPlace { get; set; }
        public bool ParkingPlaceFreeStatus { get; set; }


       

        public bool  ParkingPalceDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
           
            bool ReleasedParkPlaceCompare = obj1.ReleasedPlace == obj2.ReleasedPlace;
            bool ParkPlaceStatusCompare = obj1.ParkingPlaceFreeStatus == obj2.ParkingPlaceFreeStatus;

            return  ReleasedParkPlaceCompare == ParkPlaceStatusCompare;
        }

        public bool OwnerPersonDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            
            bool OwnerSecondNameCompare = obj1.OwnerSecondName == obj2.OwnerSecondName;
            bool OwnerFirstNameCompare = obj1.OwnerFirstName == obj2.OwnerFirstName;
            bool PatronimicCompare = obj1.OwnerPatronimic == obj2.OwnerPatronimic;
            bool OwnerPersSexCompare = obj1.OwnerSex == obj2.OwnerSex;

            return  OwnerSecondNameCompare == OwnerFirstNameCompare == PatronimicCompare == OwnerPersSexCompare;
        }

        public bool ClientDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            bool OwnerCompare = obj1.Owner == obj2.Owner;
            

            return obj1.Owner == obj2.Owner;
        }

        public bool OwnerContactsDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            bool OwnerPhoneCompare = obj1.OwnerPhone == obj2.OwnerPhone;
            bool AdressCompare = obj1.Adress == obj2.Adress;

            return OwnerPhoneCompare == AdressCompare;
        }

        public bool TrustPersonDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            bool TrustSecondnameCompare = obj1.TrustSecondName == obj2.TrustSecondName;
            bool TrustFirstNameCompare = obj1.TrustFirstName == obj2.TrustFirstName;
            bool TrustPatronimicCompare = obj1.TrustPatronimic == obj2.TrustPatronimic;
            bool TrustSexCompare = obj1.TrustPersSex == obj2.TrustPersSex;

            return TrustSecondnameCompare == TrustFirstNameCompare == TrustPatronimicCompare == TrustSexCompare;
        }

        public bool TrustContactsDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            return obj1.TrustPhone == obj2.TrustPhone; 
        }

        public bool VehicleDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            bool VPhotoCompare;
            if (obj1.VPhoto is null && obj2.VPhoto is null)
                VPhotoCompare = true;
            else
            {
                if ((obj1.VPhoto is null && !(obj2.VPhoto is null)) || (!(obj1.VPhoto is null) && obj2.VPhoto is null))
                    VPhotoCompare = false;
                else                 
                    VPhotoCompare= obj1.VPhoto.Length == obj2.VPhoto.Length; 
            }            
                
            bool RegNamberCompare = obj1.RegNumber == obj2.RegNumber;
            bool ColorCompare = obj1.Color == obj2.Color;
            

            return VPhotoCompare && RegNamberCompare && ColorCompare ;
        }

        public bool ParkingPlaceLogDataCompare(CompareStatesForParkingPlace obj1, CompareStatesForParkingPlace obj2)
        {
            bool ProlongDateCompare = obj1.ProlongDate == obj2.ProlongDate;
            bool CoastCompare = obj1.Coast == obj2.Coast;

            return ProlongDateCompare && CoastCompare;
        }

        public bool TotalCompare(CompareStatesForParkingPlace ob1, CompareStatesForParkingPlace obj2)
        {
            return ParkingPlaceLogDataCompare(ob1, obj2) && VehicleDataCompare(ob1, obj2) && TrustContactsDataCompare(ob1, obj2)
                    && TrustPersonDataCompare(ob1, obj2) && OwnerContactsDataCompare(ob1, obj2) && ClientDataCompare(ob1, obj2) &&
                    OwnerPersonDataCompare(ob1, obj2) && ParkingPalceDataCompare(ob1, obj2);
        }
    }
}
