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
using Parking.Views;

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
                        SecondWindow nextWindow;
                        nextWindow = new SecondWindow(Fuser.UserId);
                        showWindow.ShowWindow(nextWindow);
                        showWindow.CloseWindow(win);//closing authorization window
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

                    Client client1 = new Client { OrgName = "ТОВ \"Парковка\"", OrgDetals = "надання послуг з паркування" };

                    client1.Vehicles.Add(SomeVehicle1);

                    Contacts ct1 = new Contacts { Adress = "м.Київ, пр.Перемоги, б.84, кв.80", Phone="+380505080625" };

                    db.Contacts.Add(ct1);

                    Person pers1 = new Person { FirstName = "Іван",SecondName="Петров", Patronimic="Ігоревич", Male = true, DayBirthday=new DateTime(1978,10,15) };
                    pers1.ContactsData.Add(ct1);

                     db.Persons.Add(pers1);
                    db.SaveChanges();

                    //adiing trusted person
                    Client clientTrustPers1 = new Client { OrgName = "не вказано", OrgDetals = "не вказано" };
                    Contacts ctTrustPers1 = new Contacts { Adress = "м.Харків, вул.Барвінкова, б.45, кв.21", Phone = "+380956080621" };
                    db.Contacts.Add(ctTrustPers1);
                    Person pers2 = new Person { FirstName = "Ігор", SecondName = "Івнов", Patronimic = "Вікторович", Male = true, DayBirthday = new DateTime(1984, 09, 21) };
                    db.Persons.Add(pers2);
                    pers2.ContactsData.Add(ctTrustPers1);
                    pers2.Clients.Add(clientTrustPers1);
                    db.Clients.Add(clientTrustPers1);

                    db.Entry(pers1).State = EntityState.Modified;
                    pers1.TrustedPerson = pers2;

                    

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

                    pers1.Clients.Add(client1);

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
                         create proc sp_GetparkingPlacesRecord
                            @ppId int
                            as
                            Select PP.ParkingPlaceId 'ParkingPlaceId', PP.ParkPlaceNumber 'PlaceNumber', PP.FreeStatus 'FreeStatus', PP.Released 'WentInOrWentOut',
	                             Cl.ClientId 'ClientId', Cl.OrgName 'OrgName',
	                             Pers.PersonId 'PersonId',   Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
	                             Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone', Veh.VehicleId 'VehicleId', Veh.RegNumber 'VehicleRegNumber', Veh.Color 'VehicleColor', Veh.TypeName 'VehicleTypeName',
	                             PPL.ParkingPlaceLogId  'PPLId', PPL.DeadLine 'DeadLine', Pers.TrustedPerson_Id 'TrustedPerson_Id'
                            From ParkingPlaces as PP 
                            left join Clients as Cl on PP.SomeClient_ClientId=Cl.ClientId
                            left Join People as Pers on Cl.PersonCustomer_PersonId = pers.PersonId
                            left join Vehicles as Veh on Cl.ClientId=Veh.ClientOwner_ClientId
                            left join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            left join ParkingPlaceLogs as PPL on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId

                            Where PP.ParkingPlaceId = @ppId                      

                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                         create proc sp_GetTrustedPerson
                            @TrustPersId int
                            as
                            Select 
	                             Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
	                             Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone' 
                            From  People as Pers
                            join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            Where  pers.PersonId=@TrustPersId                 

                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                         Create proc sp_GetAllParkingPlaces
                            as
                            Select *
                            from ParkingPlaces              

                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                         create proc sp_GetEmployeesRecords
                            as
                            Select Emp.EmployeeId  'EmployeeId', Emp.Salary 'Salary', Emp.HireDate 'HireDate', Emp.FireDate 'FireDate', Emp.Description 'Description',
	                               pers.PersonId  'PersonId', pers.SecondName 'SecondName', pers.FirstName 'FirstName', pers.Patronimic 'Patronimic', pers.Male 'Male',
	                               pers.Female 'Female', pers.DayBirthday 'DayBirthday', pers.Photo 'Photo',
	                               ctn.ContactsId 'ContactsId', ctn.Phone 'Phone', ctn.Adress 'Adress', users.AccessName 'Status'
                            From Employees as Emp
                            join People as pers on Emp.SomePerson_PersonId=Pers.PersonId
                            join Contacts as ctn on Pers.PersonId=Ctn.SomePerson_PersonId
                            left join Users on Users.SomeEmployee_EmployeeId = Emp.EmployeeId              

                        ");

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

        private RelayCommand closeLogWinCommand;
        public RelayCommand CloseLogWinCommand => closeLogWinCommand ?? (closeLogWinCommand = new RelayCommand(
                    (obj) =>
                    {

                        showWindow.CloseWindow(obj as Window);
                    }
                    ));


    }
}
