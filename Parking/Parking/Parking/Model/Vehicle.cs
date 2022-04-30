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
       

        public int VehicleId { get; set; }

        


       [Column("RegNumber", TypeName = "nvarchar")]
        [MaxLength(8)]
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


        [Column("Color", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        [Column("TypeName", TypeName = "nvarchar")]
        [MaxLength(8)]
        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set
            {
                if (value != typeName)
                {
                    typeName = value;
                    OnPropertyChanged(nameof(TypeName));
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
                    OnPropertyChanged(nameof(vPhoto));
                }
            }
        }



        public virtual Client ClientOwner { get; set; }
    

    }
}
