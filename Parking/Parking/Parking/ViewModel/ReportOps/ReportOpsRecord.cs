
namespace Parking.ViewModel.ReportOps
{
    public class ReportOpsRecord
    {
        public int ParkPlaceId { get; set; }
        public int ParkPlaceNumber { get; set; }
        public bool FreeStatus { get; set; }
        public bool Released { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public int UserId { get; set; }
        public string UserData { get; set; }

    }
}
