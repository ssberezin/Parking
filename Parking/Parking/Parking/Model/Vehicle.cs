using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model
{
    public class Vehicle: Helpes.ObservableObject
    {
        public Vehicle()
        {
           
        }
       

        public int VehicleId { get; set; }




        [Column("RegNumber", TypeName = "nvarchar")]
        [MaxLength(9)]
        private string regNumber;
        public string RegNumber
        {
            get { return regNumber; }
            set
            {
                if (value != regNumber)
                {
                    regNumber = value;
                    OnPropertyChanged(nameof(RegNumber));
                }
            }
        }


       

        [Column("DateOfmanufacture", TypeName = "datetime2")]

        private DateTime? dateOfmanufacture;
        public DateTime? DateOfmanufacture
        {
            get { return dateOfmanufacture; }
            set
            {
                if (dateOfmanufacture != value)
                {
                    dateOfmanufacture = value;
                    OnPropertyChanged(nameof(DateOfmanufacture));
                }
            }
        }

        [Column("VPhoto", TypeName = "image")]
        private byte[] vPhoto;
        public byte[] VPhoto
        {
            get { return vPhoto; }
            set
            {
                if (value != vPhoto)
                {
                    vPhoto = value;
                    OnPropertyChanged(nameof(VPhoto));
                }
            }
        }



        public virtual Client ClientOwner { get; set; }

        public virtual ParkingPlace ParkingPlace { get; set; }

        public virtual VehicleType  SomeVehicleType {get;set;}

        public virtual VehicleColor SomeVehicleColor { get; set; }

}
}
