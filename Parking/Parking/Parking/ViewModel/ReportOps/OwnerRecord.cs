
using System;
using System.Collections.ObjectModel;


namespace Parking.ViewModel.ReportOps
{
    public  class OwnerRecord:Helpes.ObservableObject
    {
        public OwnerRecord()
        {
            ReportOpsRecords = new ObservableCollection<ReportOpsRecord>();
        }

        public int ClientId { get; set; }
        

        private string ownerName;
        public string OwnerName //its marker for block an opportunity of parking place number change
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
        public DateTime MaxDeadLine //its marker for block an opportunity of parking place number change
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
        public ObservableCollection<ReportOpsRecord> ReportOpsRecords { get; set; }

    }
}
