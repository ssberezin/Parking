using Parking.Helpes;
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
    public class Person: ObservableObject
    {
        public Person()
        {
            Clients = new ObservableCollection<Client>();
            Employees = new ObservableCollection<Employee>();
        }


        public int PersonId { get; set; }

        [Column("FirstName", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        [Column("SecondName", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                if (value != SecondName)
                {
                    secondName = value;
                    OnPropertyChanged(nameof(SecondName));
                }
            }
        }

        [Column("Patronimic", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string patronimic;
        public string Patronimic
        {
            get { return patronimic; }
            set
            {
                if (value != Patronimic)
                {
                    patronimic = value;
                    OnPropertyChanged(nameof(Patronimic));
                }
            }
        }

        [Column("Photo", TypeName = "image")]
        private byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set
            {
                if (value != photo)
                {
                    photo = value;
                    OnPropertyChanged(nameof(Photo));
                }
            }
        }

        private bool male;
        public bool Male
        {
            get { return male; }
            set
            {
                if (value != male)
                {
                    male = value;
                    OnPropertyChanged(nameof(Male));
                }
            }
        }

        private bool female;
        public bool Female
        {
            get { return female; }
            set
            {
                if (value != female)
                {
                    female = value;
                    OnPropertyChanged(nameof(Female));
                }
            }
        }

        [Column(TypeName = "datetime2")]        
        private DateTime? dayBirthday;
        public DateTime? DayBirthday
        {
            get { return dayBirthday; }
            set
            {
                if (dayBirthday != value)
                {
                    dayBirthday = value;
                    OnPropertyChanged(nameof(DayBirthday));
                }
            }
        }

        [Column("Adress", TypeName = "nvarchar")]
        [MaxLength(500)]
        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                if (value != adress)
                {
                    adress = value;
                    OnPropertyChanged(nameof(Adress));
                }
            }
        }

        public virtual ObservableCollection<Client> Clients { get; set; }
        public virtual ObservableCollection<Employee> Employees { get; set; }
    }
}
