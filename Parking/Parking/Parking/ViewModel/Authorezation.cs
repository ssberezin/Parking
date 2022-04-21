using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private void PreviosDataLoad(int parkingPlacesCount)
        {
           
            using (DBConteiner db = new DBConteiner())
            {
                try
                {

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

                    Person pers1 = db.Persons.Add(new Person { FirstName = "Іван",SecondName="Петров", Patronimic="Ігоревич", Male = true, DayBirthday=new DateTime(1978,10,15), Adress ="м.Київ, пр.Перемоги, б.84, кв.80" });

                    Employee emp1 = new Employee { Salary = 20000, HireDate = new DateTime (2018, 10,15), Position = "адміністратор", Description = "добрий працівник"};

                    User user1 = new User { AccessName = "мастер-адмін", Pass = "admin", Login ="admin" };

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
                    MessageBox.Show(ex.Message);
                }
                catch (OverflowException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddSP()
        {

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    db.Database.ExecuteSqlCommand
                        (@"
                            Create Proc sp_UserIdentification 
                            @login nvarchar(50),
                            @pass nvarchar(500)
                            as
                            Select UserId
                            From Users 
                            Where users.Login=@login and users.Pass=@pass                           

                        ");

                    db.SaveChanges();


                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (OverflowException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Data.Entity.Core.EntityException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



    }
}
