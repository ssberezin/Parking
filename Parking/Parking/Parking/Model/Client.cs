
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Parking.Model
{
    public class Client: Helpes.ObservableObject
    {

        public Client()
        {
            Vehicles = new ObservableCollection<Vehicle>();           
        }

        public int ClientId { get; set; }

        [Column("OrganizationName", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string orgName;
        public string OrgName
        {
            get { return orgName; }
            set
            {
                if (value != orgName)
                {
                    orgName = value;
                    OnPropertyChanged(nameof(OrgName));
                }
            }
        }

        [Column("OrganizationDetals", TypeName = "nvarchar")]
        [MaxLength(500)]
        private string orgDetals;
        public string OrgDetals
        {
            get { return orgDetals; }
            set
            {
                if (value != orgDetals)
                {
                    orgDetals = value;
                    OnPropertyChanged(nameof(OrgDetals));
                }
            }
        }
        public virtual Person PersonCustomer { get; set; }
        public virtual ObservableCollection<Vehicle> Vehicles { get; set; }      
    }
}
