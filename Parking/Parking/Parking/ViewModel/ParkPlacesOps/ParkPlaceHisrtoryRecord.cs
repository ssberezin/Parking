using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ParkPlacesOps
{
    public class ParkPlaceHisrtoryRecord
    {
        public int PPNumber { get; set; }
        public string DateOfEvent { get; set; }
        public string TimeOfEvent { get; set; }        
        public bool Released { get; set; }


    }
}
