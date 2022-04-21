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

        public virtual ParkingPlace SomeParkingPlace { get; set; }
        public virtual User SomeUser { get; set; }
    }
}
