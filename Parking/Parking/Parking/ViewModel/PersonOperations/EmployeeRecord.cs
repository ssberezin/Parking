using Parking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.PersonOperations
{
    public class EmployeeRecord:Helpes.ObservableObject
    {

        public EmployeeRecord()
        {
            SomeUser = new User();
            SomeContacts = new Contacts();
            SomeEmployee = new Employee();
            SomePerson = new Person();
            SomeEmpPosition = new EmployeePosition();
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
                    OnPropertyChanged(nameof(PYB));
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
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        private User someUser;
        public User SomeUser
        {
            get { return someUser; }
            set
            {
                if (value != someUser)
                {
                    someUser = value;
                    OnPropertyChanged(nameof(SomeUser));
                }
            }
        }

        private Person somePerson;
        public Person SomePerson
        {
            get { return somePerson; }
            set
            {
                if (value != somePerson)
                {
                    somePerson = value;
                    OnPropertyChanged(nameof(SomePerson));
                }
            }
        }

        private Contacts someContacts;
        public Contacts SomeContacts
        {
            get { return someContacts; }
            set
            {
                if (value != someContacts)
                {
                    someContacts = value;
                    OnPropertyChanged(nameof(SomeContacts));
                }
            }
        }

        private Employee someEmployee;
        public Employee SomeEmployee
        {
            get { return someEmployee; }
            set
            {
                if (value != someEmployee)
                {
                    someEmployee = value;
                    OnPropertyChanged(nameof(SomeEmployee));
                }
            }
        }

        private EmployeePosition someEmpPosition;
        public EmployeePosition SomeEmpPosition
        {
            get { return someEmpPosition; }
            set
            {
                if (value != someEmpPosition)
                {
                    someEmpPosition = value;
                    OnPropertyChanged(nameof(SomeEmpPosition));
                }
            }
        }

    }
}
