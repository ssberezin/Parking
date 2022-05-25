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
using System.ComponentModel;
using System.Data.Sql;
using System.Data;

using User = Parking.Model.User;

namespace Parking.ViewModel
{
   public class Authorezation: Helpes.ObservableObject
    {

        IDialogService dialogService;
        IShowWindowService showWindow;
        //if we have some problems with connection to SQL server
        //we have to set the other SQL-server name. This marker is for this one.

        private bool isConnection;
        public bool IsConnection
        {
            get { return isConnection; }
            set
            {
                if (value != isConnection)
                {
                    isConnection = value;
                    OnPropertyChanged2(nameof(IsConnection));
                }
            }
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

        //for hide right controls on main window if right sql server wasn't found
        private string  connectionWell;
        public string ConnectionWell
        {
            get { return connectionWell; }
            set
            {
                if (value != connectionWell)
                {
                    connectionWell = value;
                    OnPropertyChanged(nameof(ConnectionWell));
                }
            }
        }
        //for display right controls on main window if right sql server wasn't found
        private string sqlEditVisability;
        public string SqlEditVisability
        {
            get { return sqlEditVisability; }
            set
            {
                if (value != sqlEditVisability)
                {
                    sqlEditVisability = value;
                    OnPropertyChanged(nameof(SqlEditVisability));
                }
            }
        }

        private string newServerName;
        public string NewServerName
        {
            get { return newServerName; }
            set
            {
                if (value != newServerName)
                {
                    newServerName = value;
                    OnPropertyChanged(nameof(NewServerName));
                }
            }
        }

        

        Library lib;
        public Authorezation() 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            lib = new Library();
            IsConnection = true;
            if (lib.CheckSqlConnection())
            {
                ConnectionWell = "Visible";
                SqlEditVisability = "Collapsed";
            }
            else
            {
                ConnectionWell = "Collapsed";
                SqlEditVisability = "Visible";
                return;
            }
                        
            lib.AddSP(); //adding storage procedures to DB if it is the first app starting
            PreviosDataLoad(50);//adding some previous data at first start . 50 - here we set parkinplace count           
            
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
            
          //  passwordBox.Clear();

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


                    VehicleColor color = new VehicleColor { ColorName = "-не задано-" };
                    VehicleColor color1 = new VehicleColor { ColorName="червоний"};
                    VehicleColor color2 = new VehicleColor { ColorName = "чорний" };
                    VehicleColor color3 = new VehicleColor { ColorName = "синій" };
                    VehicleColor color4 = new VehicleColor { ColorName = "жовтий" };
                    VehicleColor color5 = new VehicleColor { ColorName = "фіолетвий" };
                    
                    VehicleColor color6 = new VehicleColor { ColorName = "помаранчевий" };

                    db.Colors.Add(color);
                    db.Colors.Add(color1);
                    db.Colors.Add(color2);
                    db.Colors.Add(color3);
                    db.Colors.Add(color2);
                    db.Colors.Add(color4);
                    db.Colors.Add(color5);
                    db.Colors.Add(color6);

                    db.SaveChanges();

                    VehicleType vtype2 = new VehicleType { TypeName = "-не задано-" };
                    VehicleType vtype = new VehicleType { TypeName = "легкове авто" };
                    VehicleType vtype1 = new VehicleType { TypeName = "мотоцикл" };
                    VehicleType vtype3 = new VehicleType { TypeName = "причеп" };

                    db.VehicleTypes.Add(vtype);
                    db.VehicleTypes.Add(vtype1);
                    db.VehicleTypes.Add(vtype2);
                    db.VehicleTypes.Add(vtype3);
                    db.SaveChanges();


                    Vehicle SomeVehicle1 = new Vehicle { RegNumber = "AE2865BO", DateOfmanufacture = new DateTime(2020, 12, 12) };
                    
                    SomeVehicle1.SomeVehicleColor = db.Colors.Find(2    );
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


                    parkingPlace1.Vehicles.Add(SomeVehicle1);


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
                IsConnection = false;
            }
        }



        private RelayCommand closeLogWinCommand;
        public RelayCommand CloseLogWinCommand => closeLogWinCommand ?? (closeLogWinCommand = new RelayCommand(
                    (obj) =>
                    {

                        showWindow.CloseWindow(obj as Window);
                    }
                    ));


        private RelayCommand newServerConnectionCommand;
        public RelayCommand NewServerConnectionCommand => newServerConnectionCommand ?? (newServerConnectionCommand = new RelayCommand(
                    (obj) =>
                    {
                       
                        win = obj as Window;
                        ChangeConnection(NewServerName);
                        lib.WriteServerNameToFile(NewServerName);
                        dialogService.ShowMessage("Необхідно перезапустити програму.");
                        showWindow.CloseWindow(win);
                        
                    }
                    ));

        private void ChangeConnection(string param)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");            
            connectionStringsSection.ConnectionStrings["ParkingDB"].ConnectionString = @"data source=" + param + ";Initial Catalog=ParkingDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;Connect Timeout = 1";
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");


        }

    }
}
