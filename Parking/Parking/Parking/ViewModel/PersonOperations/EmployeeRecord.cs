using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.PersonOperations
{
    public class EmployeeRecord:Helpes.ObservableObject
    {
       
        public int EmployeeId { get; set; }

        public int PersonId { get; set; }

        public int ContactsId { get; set; }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged2(nameof(Description));
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    OnPropertyChanged2(nameof(FirstName));
                }
            }
        }

       
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                if (value != SecondName)
                {
                    secondName = value;
                    OnPropertyChanged2(nameof(SecondName));
                }
            }
        }

      
        private string patronimic;
        public string Patronimic
        {
            get { return patronimic; }
            set
            {
                if (value != Patronimic)
                {
                    patronimic = value;
                    OnPropertyChanged2(nameof(Patronimic));
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
                    OnPropertyChanged2(nameof(Male));
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
                    OnPropertyChanged2(nameof(Female));
                }
            }
        }

       
        private DateTime? dayBirthday;
        public DateTime? DayBirthday
        {
            get { return dayBirthday; }
            set
            {
                if (dayBirthday != value)
                {
                    dayBirthday = value;
                    OnPropertyChanged2(nameof(DayBirthday));
                }
            }
        }

        private DateTime? hireDate;
        public DateTime? HireDate
        {
            get { return hireDate; }
            set
            {
                if (hireDate != value)
                {
                    hireDate = value;
                    OnPropertyChanged2(nameof(HireDate));
                }
            }
        }

        private DateTime? fireDate;
        public DateTime? FireDate
        {
            get { return fireDate; }
            set
            {
                if (fireDate != value)
                {
                    fireDate = value;
                    OnPropertyChanged2(nameof(FireDate));
                }
            }
        }


        private byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set
            {
                if (value != photo)
                {
                    photo = value;
                    OnPropertyChanged2(nameof(Photo));
                }
            }
        }

        private string pYB;
        public string PYB
        {
            get { return pYB; }
            set
            {
                if (value != pYB)
                {
                    pYB = value;
                    OnPropertyChanged2(nameof(PYB));
                }
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                    OnPropertyChanged2(nameof(PhoneNumber));
                }
            }
        }

        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                if (value != adress)
                {
                    adress = value;
                    OnPropertyChanged2(nameof(Adress));
                }
            }
        }

        private string position;
        public string Position
        {
            get { return position; }
            set
            {
                if (value != position)
                {
                    position = value;
                    OnPropertyChanged2(nameof(Position));
                }
            }
        }

        private decimal salary;
        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (salary != value)
                {
                    salary = value;
                    OnPropertyChanged2(nameof(Salary));
                }
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged2(nameof(Status));
                }
            }
        }

    }
}
