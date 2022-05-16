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
            
            var passwordBox = obj as PasswordBox;
            User user1 = new User { Login = UserLogin};
             user1.Pass = SHA.ComputeSHA256Hash(passwordBox.Password);
            
            passwordBox.Clear();

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    User Fuser = db.Users.Where(u => u.Login == user1.Login && u.Pass == user1.Pass && u.SomeEmployee.FireDate==null && u.AccessName!="без статусу").FirstOrDefault();
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

                    Client client1 = new Client { OrgName = "ТОВ \"Прайм\"", OrgDetals = "надання послуг з паркування" };

                    client1.Vehicles.Add(SomeVehicle1);

                    Contacts ct1 = new Contacts { Adress = "м.Київ, пр.Перемоги, б.84, кв.80", Phone="+380505080625" };

                    db.Contacts.Add(ct1);

                    Person pers1 = new Person { FirstName = "Іван",SecondName="Петров", Patronimic="Ігоревич", Sex = true,  DayBirthday=new DateTime(1978,10,15), TaxCode=3254123669 };
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

                    Company OwnerCompany = new Company { OrgName = "ФОП Макаренко В.О.", RegNumber=659845, TaxCode="3258741226",OrgAdress = "м. Одесса, пр.Вернадського, б.36" };
                    db.OwnerCompany.Add(OwnerCompany);

                    EmployeePosition empPosition = new EmployeePosition { PositionName = "адміністратор"};
                    db.EmployeePositions.Add(empPosition);

                    Employee emp1 = new Employee { Salary = 20000, HireDate = new DateTime (2018, 10,15), Description = "добрий працівник"};
                    db.Employees.Add(emp1);

                    empPosition.Employees.Add(emp1);
                    emp1.EmployeePositions.Add(empPosition);

                    db.SaveChanges();

                    OwnerCompany.Employes.Add(emp1);

                    User user1 = new User { AccessName = "мастер-адмін", Pass = SHA.ComputeSHA256Hash("admin"), Login = "admin" };

                    
                    ParkingPlace parkingPlace1 = db.ParkingPlaces.Where(pp => pp.ParkPlaceNumber == 1).FirstOrDefault();
                    db.Entry(parkingPlace1).State = EntityState.Modified;
                    parkingPlace1.FreeStatus = false;
                    

                    ParkingPlaceLog log1 = new ParkingPlaceLog() { BookingDate = DateTime.Now, 
                                                                   DeadLine = DateTime.Now.AddDays(30), 
                                                                   Money = 1500, 
                                                                   PayingDate = DateTime.Now ,
                                                                   FreeStatus  = parkingPlace1.FreeStatus.Value,
                                                                   Released = parkingPlace1.Released, 
                                                                   DateOfChange = DateTime.Now
                                                                 };

                    parkingPlace1.ParkingPlaceLogs.Add(log1);

                    user1.ParkingPlaceLogs.Add(log1);
                    
                    emp1.SomePerson = pers1;

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

        //adding storage procedures to DB
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
	                           ctn.ContactsId '12_ContactsId', ctn.Phone '13_Phone', ctn.Adress '14_Adress', users.AccessName '15_Status', 
	                           EmpPos.PositionName '16_Position', EmpPos.EmployeePositionId '17_EmployeePositionId', users.Pass '18_Pass', users.Login '19_Login', Users.UserId  '20_UserId',
	                           pers.TaxCode '21 TaxCode'
	   
                        From Employees as Emp
                        join People as pers on Emp.SomePerson_PersonId=Pers.PersonId
                        join Contacts as ctn on Pers.PersonId=Ctn.SomePerson_PersonId
                        left join Users on Users.SomeEmployee_EmployeeId = Emp.EmployeeId
                        join EmployeePositionEmployees EEemp on EEemp.Employee_EmployeeId=Emp.EmployeeId
						join EmployeePositions EmpPos on EmpPos.EmployeePositionId=EEemp.EmployeePosition_EmployeePositionId
                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                     create proc sp_GetPPHistory
                        @clId int, 
                        @startDate date,
                        @endDate date,
                        @ppNumber int
                        as
                        Select PP.ParkPlaceNumber '1_ParkPlaceNumber', PPL.DateOfChange '2_DateOfEvent',  PPl.Released '4_Released'
                        From ParkingPlaces PP
                        join ParkingPlaceLogs PPl on PPL.SomeParkingPlace_ParkingPlaceId=PP.ParkingPlaceId
                        join Clients Cl on Cl.ClientId=PP.SomeClient_ClientId
                        where Cl.ClientId=@clId and Pp.ParkPlaceNumber= @ppNumber and Ppl.DateOfChange>=@startDate and ppl.DateOfChange<=@endDate
                        ");

                    db.Database.ExecuteSqlCommand
                       (@"
                    create proc sp_GetClientInfoForReport
                            as
                            Select Cl.ClientId '0_ClientId' ,  Cl.OrgName '1_OrgName', Pers.PersonId '2_PersonId', Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic '3_FIO',
					                             MAX(ppl.DeadLine) '4_Max deadline'
                            From Clients Cl
                            join People Pers on Cl.PersonCustomer_PersonId=Pers.PersonId
                            join ParkingPlaces PP on Cl.ClientId=PP.SomeClient_ClientId
                            join ParkingPlaceLogs PPl on PP.ParkingPlaceId=Ppl.SomeParkingPlace_ParkingPlaceId
                            where pp.FreeStatus = 0
                            group by Cl.ClientId ,  Cl.OrgName , Pers.PersonId , Pers.SecondName + ' ' + pers.FirstName+' '+ pers.Patronimic			 

                        ");
                    db.Database.ExecuteSqlCommand
                     (@"
                   create proc sp_GetClRepRecord
                        @ppId int,
                        @startDate date,
                        @endDate date
                        as
                        Select  veh.VehicleId '0_VehicleId',veh.RegNumber '1_RegNumber',
		                        PPl.DateOfChange '2_DateOfChange'
	                           ,Us.UserId '3_UserId' 
	                           ,Pers.SecondName+' '+Pers.FirstName+' '+pers.Patronimic '4_PIB'
	   
                        From Clients Cl
                        join ParkingPlaces PP on Cl.ClientId=Pp.SomeClient_ClientId
                        join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=pp.ParkingPlaceId
                        join Vehicles Veh on cl.ClientId=Veh.ClientOwner_ClientId 
                        join.Users us on ppl.SomeUser_UserId=us.UserId
                        join People Pers on cl.PersonCustomer_PersonId=pers.PersonId

                        where pp.ParkingPlaceId=@ppId and Ppl.DateOfChange>=@startDate and Ppl.DateOfChange <@endDate

                        ");
                    db.Database.ExecuteSqlCommand
                    (@"
                     create proc sp_GetClientParkPlaces
                        @clId int
                        as
                        Select PP.ParkingPlaceId '0_ParkingPlaceId', pp.ParkPlaceNumber '1_ParkPlaceNumber', pp.FreeStatus '2_FreeStatus', pp.Released '3_Released'	  
                        From Clients Cl
                        join ParkingPlaces PP on Cl.ClientId=Pp.SomeClient_ClientId
                        join ParkingPlaceLogs PPl on Ppl.SomeParkingPlace_ParkingPlaceId=ppl.ParkingPlaceLogId

                        where cl.ClientId=@clId and pp.FreeStatus = 0
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
