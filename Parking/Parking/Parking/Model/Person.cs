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
            ContactsData = new ObservableCollection<Contacts>();
        }

        [Key]
        public int PersonId { get; set; }

        [ForeignKey("TrustedPerson")]
        public int? TrustedPerson_Id { get; set; }
        
        public Person TrustedPerson { get; set; }

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


        public virtual ObservableCollection<Client> Clients { get; set; }
        public virtual ObservableCollection<Employee> Employees { get; set; }

        public virtual ObservableCollection<Contacts> ContactsData { get; set; }
       
    }
}
