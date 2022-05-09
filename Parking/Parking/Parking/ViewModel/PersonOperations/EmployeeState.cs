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
        public EmployeeRecord EmpRec { get; set; }

        public bool PesoneCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            bool PhotoCompare;
            if (obj1.Photo is null && obj2.Photo is null)
                PhotoCompare = true;
            else
            {
                if ((obj1.Photo is null && !(obj2.Photo is null)) || (!(obj1.Photo is null) && obj2.Photo is null))
                    PhotoCompare = false;
                else
                    PhotoCompare = obj1.Photo.Length == obj2.Photo.Length;
            }


            return obj1.Patronimic == obj2.Patronimic && obj1.SecondName == obj2.SecondName &&
                   obj1.FirstName == obj2.FirstName && obj1.Male==obj2.Male && obj1.Female==obj2.Female &&
                   obj1.DayBirthday == obj2.DayBirthday && PhotoCompare;
        }

        public bool ContactsCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.PhoneNumber == obj2.PhoneNumber && obj1.Adress == obj2.Adress;
        }

        public bool EmployeePositionCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.PositionId == obj2.PositionId;
        }

        public bool EmployeeCompare(EmployeeRecord obj1, EmployeeRecord obj2)
        {
            return obj1.Salary == obj2.Salary  && obj1.Description==obj1.Description &&
                    obj1.HireDate==obj2.HireDate && obj1.FireDate==obj2.FireDate;
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
                EmployeeId = obj1.EmployeeId,
                PersonId = obj1.PersonId,
                PositionId = obj1.PositionId,
                ContactsId = obj1.ContactsId,

                Salary = obj1.Salary,
                HireDate = obj1.HireDate,
                FireDate = obj1.FireDate,
                Description = obj1.Description,

                SecondName = obj1.SecondName,
                FirstName = obj1.FirstName,
                Patronimic = obj1.Patronimic,
                PYB = obj1.SecondName + " " + obj1.FirstName + " " + obj1.Patronimic,
                Male = obj1.Male,
                Female = !obj1.Male,
                DayBirthday = obj1.DayBirthday,
                Photo = obj1.Photo,

                PhoneNumber = obj1.PhoneNumber,
                Adress = obj1.Adress,
                SomeUser = new User
                {
                    UserId = obj1.SomeUser.UserId,
                    AccessName = obj1.SomeUser.AccessName,
                    Login = obj1.SomeUser.Login,
                    Pass = obj1.SomeUser.Pass
                },
                Position = obj1.Position
            };


        }
    }
}
