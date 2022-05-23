using Parking.Helpes;
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
   public class VehicleColor:Helpes.ObservableObject, IColorTypeInterface
    {
        public VehicleColor()
        {
            Vehicles = new ObservableCollection<Vehicle>();
        }

        public int VehicleColorId { get; set; }

        [Column("ColorName", TypeName = "nvarchar")]
        [MaxLength(8)]
        private string colorName;
        public string ColorName
        {
            get { return colorName; }
            set
            {
                if (value != colorName)
                {
                    colorName = value;
                    OnPropertyChanged(nameof(ColorName));
                }
            }
        }

        public virtual ObservableCollection <Vehicle>  Vehicles { get; set; }
    }
}
