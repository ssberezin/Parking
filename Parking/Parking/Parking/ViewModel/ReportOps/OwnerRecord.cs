
using Parking.Model;
using System;
using System.Collections.ObjectModel;


namespace Parking.ViewModel.ReportOps
{
    public  class OwnerRecord:Helpes.ObservableObject
    {
        public OwnerRecord()
        {
            ParPlaceRecords = new ObservableCollection<ParPlaceRecord>();
        }

        public int ClientId { get; set; }
        

        private string ownerName;
        public string OwnerName 
        {
            get { return ownerName; }
            set
            {
                if (ownerName != value)
                {
                    ownerName = value;
                    OnPropertyChanged(nameof(OwnerName));
                }
            }
        }


        

        public int PersonId { get; set; }

        

        private DateTime maxDeadLine;
        public DateTime MaxDeadLine 
        {
            get { return maxDeadLine; }
            set
            {
                if (maxDeadLine != value)
                {
                    maxDeadLine = value;
                    OnPropertyChanged(nameof(MaxDeadLine));
                }
            }
        }

        
         public ObservableCollection<ParPlaceRecord> ParPlaceRecords { get; set; }
        //public ObservableCollection<ReportOpsRecord> ReportOpsRecords { get; set; }

    }
}
