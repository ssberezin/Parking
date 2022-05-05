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

                    VehicleType vtype = new VehicleType { TypeName = "Легкове авто" };
                    db.VehicleTypes.Add(vtype);

                    

                    Vehicle SomeVehicle1 = new Vehicle { Color = "Чорний", RegNumber = "AE2865BO", DateOfmanufacture = new DateTime(2020, 12, 12) };

                    vtype.Vehicles.Add(SomeVehicle1);
                    db.SaveChanges();

                    Client client1 = new Client { OrgName = "ТОВ \"Парковка\"", OrgDetals = "надання послуг з паркування" };

                    client1.Vehicles.Add(SomeVehicle1);

                    Contacts ct1 = new Contacts { Adress = "м.Київ, пр.Перемоги, б.84, кв.80", Phone="+380505080625" };

                    db.Contacts.Add(ct1);

                    Person pers1 = new Person { FirstName = "Іван",SecondName="Петров", Patronimic="Ігоревич", Sex = true,  DayBirthday=new DateTime(1978,10,15) };
                    pers1.ContactsData.Add(ct1);

                     db.Persons.Add(pers1);
                    db.SaveChanges();

                    //adiing trusted person
                    Client clientTrustPers1 = new Client { OrgName = "не вказано", OrgDetals = "не вказано" };
                    Contacts ctTrustPers1 = new Contacts { Adress = "м.Харків, вул.Барвінкова, б.45, кв.21", Phone = "+380956080621" };
                    db.Contacts.Add(ctTrustPers1);
                    Person pers2 = new Person { FirstName = "Ігор", SecondName = "Івнов", Patronimic = "Вікторович",Sex = true,  DayBirthday = new DateTime(1984, 09, 21) };
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
                         Create proc sp_GetparkingPlacesRecord
                            @ppId int
                            as
                            Select PP.ParkingPlaceId '0_ParkingPlaceId', PP.ParkPlaceNumber '1_PlaceNumber', PP.FreeStatus '2_FreeStatus', PP.Released '3_Released',
	                             Cl.ClientId '4_ClientId', Cl.OrgName '5_OrgName',
	                             Pers.PersonId '6_PersonId',   Pers.SecondName '7_SecondName', Pers.FirstName '8_FirstName', Pers.Patronimic '9_Patronimic',
	                             Ctn.ContactsId '10_ContactsId', Ctn.Phone '11_Phone', Veh.VehicleId '12_VehicleId', Veh.RegNumber '13_VehicleRegNumber', Veh.Color '14_VehicleColor',
	                             PPL.ParkingPlaceLogId  '15_PPLId', PPL.DeadLine '16_DeadLine', Pers.TrustedPerson_Id '17_TrustedPerson_Id', Ctn.Adress '18_DriversAdress',
	                             VT.VehicleTypeId '19_VehicleTypeId', VT.TypeName  '20_VTypeName', Pers.Sex '21_Pers_Sex', Veh.VPhoto '22_VPhoto'
                            From ParkingPlaces as PP 
                            left join Clients as Cl on PP.SomeClient_ClientId=Cl.ClientId
                            left Join People as Pers on Cl.PersonCustomer_PersonId = pers.PersonId
                            left join Vehicles as Veh on Cl.ClientId=Veh.ClientOwner_ClientId
                            left join Contacts as Ctn on Ctn.SomePerson_PersonId=Pers.PersonId
                            left join ParkingPlaceLogs as PPL on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
                            left join Vehicletypes as VT on VT.VehicleTypeId=Veh.SomeVehicleType_VehicleTypeId     
                            Where PP.ParkingPlaceId = @ppId

                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                         create proc sp_GetTrustedPerson
                            @TrustPersId int
                            as
                            Select 
	                             Pers.SecondName 'SecondName', Pers.FirstName 'FirstName', Pers.Patronimic 'Patronimic',
	                             Ctn.ContactsId 'ContactsId', Ctn.Phone 'Phone', Pers.Sex 'TrustPersSex' 
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
                            Select Emp.EmployeeId  '0_EmployeeId', Emp.Salary '1_Salary', Emp.HireDate '2_HireDate', Emp.FireDate '3_FireDate', Emp.Description '4_Description',
	                               pers.PersonId  '5_PersonId', pers.SecondName '6_SecondName', pers.FirstName '7_FirstName', pers.Patronimic '8_Patronimic', pers.Sex '9_Sex',
	                               pers.DayBirthday '10_DayBirthday', pers.Photo '11_Photo',
	                               ctn.ContactsId '12_ContactsId', ctn.Phone '13_Phone', ctn.Adress '14_Adress', users.AccessName '15_Status', emp.Position '16_Position'
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
