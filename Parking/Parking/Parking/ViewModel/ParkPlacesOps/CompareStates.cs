using Parking.Helpes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ParkPlacesOps
{
    public class CompareStates
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
        public int FreeparkPlace { get; set; }


        public bool CompareObjects(CompareStates obj1, CompareStates obj2)
        {
            bool OwnerCompare = obj1.Owner == obj2.Owner;
            bool OwnerSecondNameCompare = obj1.OwnerSecondName == obj2.OwnerSecondName;
            bool OwnerFirstNameCompare = obj1.OwnerFirstName == obj2.OwnerFirstName;
            bool PatronimicCompare = obj1.OwnerPatronimic == obj2.OwnerPatronimic;
            bool OwnerPersSexCompare = obj1.OwnerSex == obj2.OwnerSex;
            
            bool OwnerPhoneCompare = obj1.OwnerPhone == obj2.OwnerPhone;    
            bool VPhotoCompare = obj1.VPhoto.Length==obj2.VPhoto.Length;
            bool TrustSecondnameCompare = obj1.TrustSecondName == obj2.TrustSecondName;
            bool TrustFirstNameCompare = obj1.TrustFirstName == obj2.TrustFirstName;
            bool TrustPatronimicCompare = obj1.TrustPatronimic == obj2.TrustPatronimic;
            bool TrustSexCompare = obj1.TrustPersSex == obj2.TrustPersSex;   
            
            bool TrustPhone = obj1.TrustPhone == obj2.TrustPhone;
            bool RegNamberCompare = obj1.RegNumber == obj2.RegNumber;
            bool ColorCompare = obj1.Color == obj2.Color;
            bool VTypeCompare = obj1.VType == obj2.VType;
            bool ProlongDateCompare = obj1.ProlongDate == obj2.ProlongDate;
            bool DeadLinecompare = obj1.DeadLine == obj2.DeadLine;
            bool CoastCompare = obj1.Coast == obj2.Coast;
            bool GreeparkPalceCompare = obj1.FreeparkPlace == obj2.FreeparkPlace;
            bool AdressCompare = obj1.Adress == obj2.Adress;

            return OwnerCompare == OwnerSecondNameCompare == OwnerFirstNameCompare==
                    PatronimicCompare ==OwnerPersSexCompare == 
                    OwnerPhoneCompare ==TrustSecondnameCompare ==TrustFirstNameCompare ==
                    TrustPatronimicCompare ==TrustSexCompare ==
                    TrustPhone ==RegNamberCompare ==ColorCompare ==
                    VTypeCompare ==ProlongDateCompare==DeadLinecompare ==CoastCompare ==
                    GreeparkPalceCompare==AdressCompare;

        }

    }
}
