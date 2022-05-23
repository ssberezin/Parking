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
    public class VehicleType : Helpes.ObservableObject, IColorTypeInterface
    {
        public VehicleType()        
        {
            Vehicles = new ObservableCollection<Vehicle>();
        }

        public int VehicleTypeId { get; set; }

        [Column("TypeName", TypeName = "nvarchar")]
        [MaxLength(55)]
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

        public virtual ObservableCollection<Vehicle> Vehicles { get; set; }
        
}
}
