using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model
{
   public class User : Helpes.ObservableObject
    {

        public User()
        {
            ParkingPlaceLogs = new ObservableCollection<ParkingPlaceLog>();
            AccessName = "без статусу";
        }
        public int UserId { get; set; }

        [Column("AccessName", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string accessName;
        public string AccessName
        {
            get { return accessName; }
            set
            {
                if (value != accessName)
                {
                    accessName = value;
                    OnPropertyChanged(nameof(AccessName));
                }
            }
        }

        [Column("Login", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                if (value != login)
                {
                    login = value;
                    OnPropertyChanged(nameof(login));
                }
            }
        }

        [Column("Pass", TypeName = "nvarchar")]
        [MaxLength(500)]
        private string pass;
        public string Pass
        {
            get { return pass; }
            set
            {
                if (value != pass)
                {
                    pass = value;
                    OnPropertyChanged(nameof(Pass));
                }
            }
        }

        public virtual Employee SomeEmployee { get; set; }
        public virtual ObservableCollection<ParkingPlaceLog> ParkingPlaceLogs { get; set; }

    }
}
