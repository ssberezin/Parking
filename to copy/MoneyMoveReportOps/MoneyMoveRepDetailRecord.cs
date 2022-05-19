using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ReportOps.MoneyMoveReportOps
{
    public class MoneyMoveRepDetailRecord:Helpes.ObservableObject
    {
        public int VehicleId { get; set; }
        public int PPlogId { get; set; }

        private string regNumber;
        public string RegNumber
        {
            get { return regNumber; }
            set
            {
                if (regNumber != value)
                {
                    regNumber = value;
                    OnPropertyChanged2(nameof(RegNumber));
                }
            }
        }
        private decimal money;
        public decimal Money
        {
            get { return money; }
            set
            {
                if (money != value)
                {
                    money = value;
                    OnPropertyChanged2(nameof(Money));
                }
            }
        }
        private DateTime payingDate;
        public DateTime PayingDate
        {
            get { return payingDate; }
            set
            {
                if (payingDate != value)
                {
                    payingDate = value;
                    OnPropertyChanged2(nameof(PayingDate));
                }
            }
        }

        private DateTime deadLine;
        public DateTime DeadLine
        {
            get { return deadLine; }
            set
            {
                if (deadLine != value)
                {
                    deadLine = value;
                    OnPropertyChanged2(nameof(DeadLine));
                }
            }
        }

    }
}
