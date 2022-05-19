using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ReportOps.MoneyMoveReportOps
{
    public class StuffMoneyRecord: Helpes.ObservableObject
    {

       
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public int UserId { get; set; }

        private string pIB;
        public string PIB
        {
            get { return pIB; }
            set
            {
                if (pIB != value)
                {
                    pIB = value;
                    OnPropertyChanged2(nameof(PIB));
                }
            }
        }

        private DateTime startDateWorking;
        public DateTime StartDateWorking
        {
            get { return startDateWorking; }
            set
            {
                if (startDateWorking != value)
                {
                    startDateWorking = value;
                    OnPropertyChanged2(nameof(StartDateWorking));
                }
            }
        }


       
    }
}
