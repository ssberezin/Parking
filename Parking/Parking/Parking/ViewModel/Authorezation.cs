using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EasyEncryption;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;

namespace Parking.ViewModel
{
   public class Authorezation: Helpes.ObservableObject
    {

        IDialogService dialogService;
        IShowWindowService showWindow;
        public Authorezation() 
        {
            
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            AddSP();
            PreviosDataLoad(50);
        }

        
        
        private string userLogin;
        public string UserLogin
        {
            get { return userLogin; }
            set
            {
                if (value != userLogin)
                {
                    userLogin = value;
                    OnPropertyChanged(nameof(UserLogin));
                }
            }
        }

        //ForCloseLogWinCommand 
        //for closing authorezation window after authorezation
        Window win;
        private RelayCommand forCloseLogWinCommand;
        public RelayCommand ForCloseLogWinCommand => forCloseLogWinCommand ?? (forCloseLogWinCommand = new RelayCommand(
                    (obj) =>
                    {
                        win = obj as Window;
                    }
                    ));

        private RelayCommand checkPersoneCommand;
        public RelayCommand CheckPersoneCommand => checkPersoneCommand ?? (checkPersoneCommand = new RelayCommand(
                    (obj) =>
                    {                        
                            CheckUser(obj);                      
                    }
                    ));

        private void CheckUser(object obj)
        {
            
            //Window win = obj as Window;
            var passwordBox = obj as PasswordBox;
            User user1 = new User { Login = UserLogin};
             user1.Pass = SHA.ComputeSHA256Hash(passwordBox.Password);
            
            passwordBox.Clear();

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    User Fuser = db.Users.Where(u => u.Login == user1.Login && u.Pass == user1.Pass && u.SomeEmployee.FireDate==null).FirstOrDefault();
                    if (Fuser != null)
                    {
                       //start main window
                    }
                    else
                        dialogService.ShowMessage("Не вірна пара логін-пароль або дані застарілі");

                }
                catch (ArgumentNullException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
            }
        }



        private void PreviosDataLoad(int parkingPlacesCount)
        {
           
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    if (db.Persons.Count() != 0)
                        return;

                    for (int i = 1; i <= parkingPlacesCount; i++)
                    {
                        db.ParkingPlaces.Add(new ParkingPlace {FreeStatus=true, ParkPlaceNumber=i });
                    }

                    db.SaveChanges();

                    Vehicle SomeVehicle1 = new Vehicle { Color = "Чорний", RegNumber = "AE2865BO", DateOfmanufacture = new DateTime(2020, 12, 12), TypeName = "легкове авто" };
                    //Vehicle Somevehicle2 = db.Vehicles.Add(new Vehicle { Color = "Зелений", RegNumber = "AА2662BВ", DateOfmanufacture = new DateTime(2019, 10, 1), TypeName = "легкове авто" });
                    //Vehicle Somevehicle3 = db.Vehicles.Add(new Vehicle { Color = "Жовтий", RegNumber = "AE3855BХ", DateOfmanufacture = new DateTime(2015, 10, 25), TypeName = "легкове авто" });
                    //Vehicle Somevehicle4 = db.Vehicles.Add(new Vehicle { Color = "Коричневий", RegNumber = "AE2865BO", DateOfmanufacture = new DateTime(2020, 12, 12), TypeName = "легкове авто" });
                   // db.Vehicles.Add(SomeVehicle1);

                    Client client1 = new Client { OrgName = "ТОВ \"Парковка\"", OrgDetals = "надання послуг з паркування" };

                    client1.Vehicles.Add(SomeVehicle1);

                    Contacts ct1 = new Contacts { Adress = "м.Київ, пр.Перемоги, б.84, кв.80", Phone="+380505080625" };

                    db.Contacts.Add(ct1);

                    Person pers1 = new Person { FirstName = "Іван",SecondName="Петров", Patronimic="Ігоревич", Male = true, DayBirthday=new DateTime(1978,10,15) };
                    pers1.ContactsData.Add(ct1);

                    db.Persons.Add(pers1);

                    Employee emp1 = new Employee { Salary = 20000, HireDate = new DateTime (2018, 10,15), Position = "адміністратор", Description = "добрий працівник"};

                    User user1 = new User { AccessName = "мастер-адмін", Pass = SHA.ComputeSHA256Hash("admin"), Login = "admin" };

                    ParkingPlace parkingPlace1 = db.ParkingPlaces.Where(pp => pp.ParkPlaceNumber == 1).FirstOrDefault();
                    db.Entry(parkingPlace1).State = EntityState.Modified;
                    parkingPlace1.FreeStatus = false;

                    ParkingPlaceLog log1 = new ParkingPlaceLog() { BookingDate = DateTime.Now, DeadLine = DateTime.Now.AddDays(30), Money = 1500, PayingDate = DateTime.Now };

                    parkingPlace1.ParkingPlaceLogs.Add(log1);


                    user1.ParkingPlaceLogs.Add(log1);

                    emp1.Users.Add(user1);

                    pers1.Employees.Add(emp1);                

                   
                    client1.ParkingPlaces.Add(parkingPlace1);


                    db.Clients.Add(client1);                  


                    
                     db.SaveChanges();


                }
                catch (ArgumentNullException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
            }
        }

        private void AddSP()
        {

            using (DBConteiner db = new DBConteiner())
            {
                if (db.Persons.Count() != 0)
                    return;
                try
                {
                    db.Database.ExecuteSqlCommand
                        (@"
                           Create Proc sp_UserIdentification 
                            @login nvarchar(50),
                            @pass nvarchar(500)

                            as
                            Select Users.UserId
                            From Users 
                            Where users.Login=@login and users.Pass=@pass                     

                        ");
                    //db.Database.ExecuteSqlCommand
                    //    (@"
                    //     Create Proc sp_UserIdentification 
                    //        @login nvarchar(50),
                    //        @pass nvarchar(500)
                    //        as
                    //        Select UserId
                    //        From Users 
                    //        Where users.Login=@login and users.Pass=@pass                            
                    //        ");

                    db.SaveChanges();


                }
                catch (ArgumentNullException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (OverflowException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                     dialogService.ShowMessage(ex.Message);
                }
            }
        }



    }
}
