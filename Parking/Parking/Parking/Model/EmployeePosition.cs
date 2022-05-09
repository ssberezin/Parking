using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Model
{
    public class EmployeePosition : Helpes.ObservableObject
    {



        public int  EmployeePositionId { get; set; }

        [Column("Position", TypeName = "nvarchar")]
        [MaxLength(100)]
        private string positionName;
        public string PositionName
        {
            get { return positionName; }
            set
            {
                if (value != positionName)
                {
                    positionName = value;
                    OnPropertyChanged(nameof(PositionName));
                }
            }
        }

        public virtual Employee SomeEmployee { get; set; }
    }
}
