using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model
{
   public class ParkingPlaceLog:Helpes.ObservableObject
    {
        public int ParkingPlaceLogId { get; set; }

        [Column(TypeName = "datetime2")]

        private DateTime? bookingDate;
        public DateTime? BookingDate
        {
            get { return bookingDate; }
            set
            {
                if (bookingDate != value)
                {
                    bookingDate = value;
                    OnPropertyChanged(nameof(BookingDate));
                }
            }
        }

        [Column(TypeName = "datetime2")]

        private DateTime? payingDate;
        public DateTime? PayingDate
        {
            get { return payingDate; }
            set
            {
                if (payingDate != value)
                {
                    payingDate = value;
                    OnPropertyChanged(nameof(PayingDate));
                }
            }
        }

        [Column(TypeName = "datetime2")]

        private DateTime? deadLine;
        public DateTime? DeadLine
        {
            get { return deadLine; }
            set
            {
                if (deadLine != value)
                {
                    deadLine = value;
                    OnPropertyChanged(nameof(DeadLine));
                }
            }
        }


        [Column(TypeName = "datetime2")]

        private DateTime? dateOfChange;
        public DateTime? DateOfChange
        {
            get { return dateOfChange; }
            set
            {
                if (dateOfChange != value)
                {
                    dateOfChange = value;
                    OnPropertyChanged(nameof(DateOfChange));
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
                    OnPropertyChanged(nameof(Money));
                }
            }
        }

        private bool released;
        public bool Released
        {
            get { return released; }
            set
            {
                if (value != released)
                {
                    released = value;
                    OnPropertyChanged(nameof(Released));
                }
            }

        }
        private bool? freeStatus;
        public bool? FreeStatus
        {
            get { return freeStatus; }
            set
            {
                if (value != freeStatus)
                {
                    freeStatus = value;
                    OnPropertyChanged(nameof(FreeStatus));
                }
            }
        }

        public virtual ParkingPlace SomeParkingPlace { get; set; }
        public virtual User SomeUser { get; set; }
    }
}
