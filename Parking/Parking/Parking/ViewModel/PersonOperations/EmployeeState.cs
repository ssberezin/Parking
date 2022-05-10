using Parking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.PersonOperations
{
    public  class EmployeeState: Helpes.ObservableObject
    {
        public EmployeeState()
        {
            
        }

        

        public bool PesoneCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            bool PhotoCompare;
            if (obj1.SomePerson.Photo is null && obj2.SomePerson.Photo is null)
                PhotoCompare = true;
            else
            {
                if ((obj1.SomePerson.Photo is null && !(obj2.SomePerson.Photo is null)) || (!(obj1.SomePerson.Photo is null) && obj2.SomePerson.Photo is null))
                    PhotoCompare = false;
                else
                    PhotoCompare = obj1.SomePerson.Photo.Length == obj2.SomePerson.Photo.Length;
            }


            return obj1.SomePerson.Patronimic == obj2.SomePerson.Patronimic && obj1.SomePerson.SecondName == obj2.SomePerson.SecondName &&
                   obj1.SomePerson.FirstName == obj2.SomePerson.FirstName && obj1.SomePerson.Sex == obj2.SomePerson.Sex && 
                   obj1.SomePerson.DayBirthday == obj2.SomePerson.DayBirthday && PhotoCompare && obj1.SomePerson.TaxCode == obj2.SomePerson.TaxCode;
        }

        public bool ContactsCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.SomeContacts.Phone == obj2.SomeContacts.Phone && obj1.SomeContacts.Adress == obj2.SomeContacts.Adress;
        }

        public bool EmployeePositionCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.SomeEmpPosition.EmployeePositionId == obj2.SomeEmpPosition.EmployeePositionId;
        }

        public bool EmployeeCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.SomeEmployee.Salary == obj2.SomeEmployee.Salary  && obj1.SomeEmployee.Description ==obj1.SomeEmployee.Description &&
                    obj1.SomeEmployee.HireDate ==obj2.SomeEmployee.HireDate && obj1.SomeEmployee.FireDate ==obj2.SomeEmployee.FireDate;
        }

        public bool StatusCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.SomeUser.AccessName == obj2.SomeUser.AccessName && obj1.SomeUser.Login == obj2.SomeUser.Login &&
                   obj1.SomeUser.Pass == obj2.SomeUser.Pass;
        }


        public EmployeeRecord SetState(EmployeeRecord obj1)
        {
            return new EmployeeRecord
            {
                SomeEmployee = new Employee
                {
                    EmployeeId = obj1.SomeEmployee.EmployeeId,
                    Salary = obj1.SomeEmployee.Salary,
                    HireDate = obj1.SomeEmployee.HireDate,
                    FireDate = obj1.SomeEmployee.FireDate,
                    Description = obj1.SomeEmployee.Description

                },

                SomePerson = new Person 
                {
                    PersonId = obj1.SomePerson.PersonId,
                    SecondName = obj1.SomePerson.SecondName,
                    FirstName = obj1.SomePerson.FirstName,
                    Patronimic = obj1.SomePerson.Patronimic,                    
                    Sex = obj1.SomePerson.Sex,                    
                    DayBirthday = obj1.SomePerson.DayBirthday,
                    Photo = obj1.SomePerson.Photo,
                    TaxCode = obj1.SomePerson.TaxCode,
                },

                PYB = obj1.SomePerson.SecondName + " " + obj1.SomePerson.FirstName + " " + obj1.SomePerson.Patronimic,
                

                SomeContacts = new Contacts 
                {
                    ContactsId = obj1.SomeContacts.ContactsId,
                    Phone = obj1.SomeContacts.Phone,
                    Adress = obj1.SomeContacts.Adress
                },
                
                SomeUser = new User
                {
                    UserId = obj1.SomeUser.UserId,
                    AccessName = obj1.SomeUser.AccessName,
                    Login = obj1.SomeUser.Login,
                    Pass = obj1.SomeUser.Pass
                },
                SomeEmpPosition = new EmployeePosition 
                {
                    EmployeePositionId = obj1.SomeEmpPosition.EmployeePositionId,
                    PositionName = obj1.SomeEmpPosition.PositionName
                }
            };


        }
    }
}
