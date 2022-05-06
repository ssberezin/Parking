using Parking.Helpes;
using Parking.Model;
using Parking.Views.CompanyOps;
using Parking.Views.ParkPlacesOps;
using Parking.Views.PersonOperations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel
{
    public class MainViewModel: Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;
        public ObservableCollection<ParkingPlaceRecord> Records { get; set; }

        private ParkingPlaceRecord selectedRecord;
        public ParkingPlaceRecord SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                if (selectedRecord != value)
                {
                    selectedRecord = value;
                    OnPropertyChanged2(nameof(SelectedRecord));
                }
            }
        }

        private ParkingPlaceRecord currentRecord;
        public ParkingPlaceRecord CurrentRecord
        {
            get { return currentRecord; }
            set
            {
                if (currentRecord != value)
                {
                    currentRecord = value;
                    OnPropertyChanged(nameof(currentRecord));
                }
            }
        }

        private string outOfDeadLine;
        public string OutOfDeadLine
        {
            get { return outOfDeadLine; }
            set
            {
                if (outOfDeadLine != value)
                {
                    outOfDeadLine = value;
                    OnPropertyChanged(nameof(OutOfDeadLine));
                }
            }
        }

        

        private string deadLine;
        public string DeadLine
        {
            get { return deadLine; }
            set
            {
                if (deadLine != value)
                {
                    deadLine = value;
                    OnPropertyChanged(nameof(DeadLine));
                }
            }
        }

        private string visaBility;
        public string VisaBility
        {
            get { return visaBility; }
            set
            {
                if (visaBility != value)
                {
                    visaBility = value;
                    OnPropertyChanged(nameof(VisaBility));
                }
            }
        }
        public MainViewModel()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            VisaBility = "Visible";//for display vehicle type  and  owner infor
        }

        int IncomUserId { get; set; }

        public MainViewModel(int UserId)
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            VisaBility = "Visible";//for display vehicle type  and  owner infor            
            IncomUserId = UserId;
            Records = new ObservableCollection<ParkingPlaceRecord>();
            DefaultDataLoad();

            PropertyChanged2 += ChangeSelectedRecord;
        }


        private void DefaultDataLoad()
        {
            Records.Clear();
            //here we are forming data for datagid at SecondWindow.xaml. The main info.


            string sqlExpression = "sp_GetAllParkingPlaces";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        ParkingPlaceRecord record;

                        while (result.Read())
                        {
                            record = new ParkingPlaceRecord()
                            {
                                SomeParkingPlace = new ParkingPlace
                                {
                                    ParkingPlaceId = (int)result.GetValue(0),
                                    ParkPlaceNumber = (int)result.GetValue(2),
                                    FreeStatus = (bool)result.GetValue(1),
                                    Released = (bool)result.GetValue(3)                                    
                                }
                            };

                            Records.Add(record);
                        };                        
                    }
                    else
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про паркувальні місця");
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

        
        private void ChangeSelectedRecord(object sender, PropertyChangedEventArgs e)
        {
            if(Records.Count()!=0)
                UpdateRecord();
        }

        private void UpdateRecord()
        {

            string sqlExpression = "sp_GetparkingPlacesRecord";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter firstParam = new SqlParameter
                    {
                        ParameterName = "@ppId",
                        Value = SelectedRecord.SomeParkingPlace.ParkingPlaceId
                    };
                    command.Parameters.Add(firstParam);

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {

                        while (result.Read())
                        {
                            CurrentRecord = new ParkingPlaceRecord()
                            {
                                SomeParkingPlace = new ParkingPlace
                                {
                                    ParkingPlaceId = (int)result.GetValue(0),
                                    ParkPlaceNumber = (int)result.GetValue(1),
                                    FreeStatus = (bool)result.GetValue(2),
                                    Released = (bool)result.GetValue(3)
                                }
                            };


                            if (!CurrentRecord.SomeParkingPlace.FreeStatus.Value)
                            {

                                CurrentRecord.SomeClient = new Client
                                {
                                    ClientId = (int)result.GetValue(4),
                                    OrgName = result.GetValue(4) is System.DBNull ? (string)result.GetValue(7) + " " + (string)result.GetValue(8) + " " + (string)result.GetValue(7) : (string)result.GetValue(5)
                                };

                                CurrentRecord.SomePerson = new Person
                                {
                                    PersonId = (int)result.GetValue(6),
                                    SecondName = (string)result.GetValue(7),
                                    FirstName = (string)result.GetValue(8),
                                    Patronimic = (string)result.GetValue(9),
                                    Sex = (bool)result.GetValue(21)
                                };
                                CurrentRecord.FemaleOwnPers = !CurrentRecord.SomePerson.Sex;

                                CurrentRecord.SomeContacts = new Contacts
                                {
                                    ContactsId = (int)result.GetValue(10),
                                    Phone = (string)result.GetValue(11),
                                    Adress = result.GetValue(18) is System.DBNull ? null : (string)result.GetValue(18)
                                };
                                CurrentRecord.SomeVehicle = new Vehicle
                                {
                                    VehicleId = (int)result.GetValue(12),
                                    RegNumber = (string)result.GetValue(13),
                                    Color = (string)result.GetValue(14),
                                    VPhoto = result.GetValue(22) is System.DBNull ? null: (byte[])result.GetValue(22)

                                };
                                CurrentRecord.SomeParkingPlaceLog = new ParkingPlaceLog
                                {
                                    ParkingPlaceLogId = (int)result.GetValue(15),
                                    DeadLine = (DateTime)result.GetValue(16)
                                };
                                CurrentRecord.SomeVehicleType = new VehicleType
                                {
                                    VehicleTypeId = (int)result.GetValue(19),
                                    TypeName = (string)result.GetValue(20)
                                };


                                if (CurrentRecord.SomeClient.OrgName == null || CurrentRecord.SomeClient.OrgName == "не вказано")
                                    CurrentRecord.SomeClient.OrgName = CurrentRecord.SomePerson.SecondName + " " + CurrentRecord.SomePerson.FirstName + " " + CurrentRecord.SomePerson.Patronimic;

                                if (!(result.GetValue(18) is System.DBNull))
                                    CurrentRecord.SomePerson.TrustedPerson_Id = (int?)result.GetValue(17);

                                if (CurrentRecord.SomePerson.TrustedPerson_Id != null)
                                {

                                    GetTrustedPerson(CurrentRecord.SomePerson.TrustedPerson_Id.Value, out Person nPerson, out Contacts nContacts);
                                    CurrentRecord.TrustedPerson = nPerson;
                                    CurrentRecord.FemaleTrustPers = !CurrentRecord.TrustedPerson.Sex;
                                    CurrentRecord.TrContacts = nContacts;

                                }
                                VisaBility = "Visible";
                                DateTime dt = (DateTime)result.GetValue(16);
                                DeadLine = new DateTime(dt.Year, dt.Month, dt.Day).ToString("dd/MM/yyyy");
                                OutOfDeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine > DateTime.Now ? "не просрочено" : "просрочено";

                            }
                            else
                            {
                                //in case if parking place is free
                                VisaBility = "Hidden";
                                CurrentRecord.SomeClient = new Client
                                {
                                    OrgDetals = "не задано",
                                    OrgName = "не задано",
                                    
                                };
                                CurrentRecord.SomePerson = new Person();                                
                                CurrentRecord.FemaleOwnPers = !CurrentRecord.SomePerson.Sex;
                                CurrentRecord.SomeContacts = new Contacts();
                                CurrentRecord.SomeVehicle = new Vehicle();

                                CurrentRecord.SomeParkingPlaceLog = new ParkingPlaceLog { DeadLine=new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day )};
                                
                                CurrentRecord.SomeVehicleType = new VehicleType
                                {
                                    VehicleTypeId = 1,
                                    TypeName = "легкове авто"
                                };
                                CurrentRecord.TrustedPerson = new Person();
                                CurrentRecord.FemaleTrustPers = !CurrentRecord.TrustedPerson.Sex;
                                CurrentRecord.TrContacts = new Contacts();
                                DateTime dt = DateTime.Now; ;
                                DeadLine = new DateTime(dt.Year, dt.Month, dt.Day).ToString("dd/MM/yyyy");
                                OutOfDeadLine = "не просрочено";
                            }

                        };
                       
                       
                    }
                    else
                    {
                       
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про паркувальні місця");
                    }
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

        private void GetTrustedPerson(int trustedPersId,out Person nPerson, out Contacts nContacts )
        {

            nPerson = new Person();
            nContacts = new Contacts();

            string sqlExpression = "sp_GetTrustedPerson";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {
                    
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter firstParam = new SqlParameter
                    {
                        ParameterName = "@TrustPersId",
                        Value = trustedPersId
                    };

                    command.Parameters.Add(firstParam);

                    
                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            nPerson.PersonId = CurrentRecord.SomePerson.TrustedPerson_Id.Value;
                            nPerson.SecondName = (string)result.GetValue(0);
                            nPerson.FirstName = (string)result.GetValue(1);
                            nPerson.Patronimic = (string)result.GetValue(2);
                            nPerson.Sex = (bool)result.GetValue(5);


                            nContacts = new Contacts { ContactsId = (int)result.GetValue(3), Phone = (string)result.GetValue(4) };
                        };
                        
                    }
                    else
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про паркувальні місця");
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

        private RelayCommand callStuffListCommand;
        public RelayCommand CallStuffListCommand => callStuffListCommand ?? (callStuffListCommand = new RelayCommand(
                    (obj) =>
                    {                        
                        EmployeeWindow empWindow = new EmployeeWindow(IncomUserId);
                        showWindow.ShowDialog(empWindow);
                    }
                    ));

        private RelayCommand editparkPlaceCommand;
        public RelayCommand EditparkPlaceCommand => editparkPlaceCommand ?? (editparkPlaceCommand = new RelayCommand(
                    (obj) =>
                    {
                       // AddtestData();
                        ParkPlaceEditWindow parkWindow = new ParkPlaceEditWindow(IncomUserId, CurrentRecord);
                        showWindow.ShowDialog(parkWindow);
                        DefaultDataLoad();//update all data in the window
                    }
                    ));

        //private void UpdateRecords()
        //{
        //    foreach (ParkingPlaceRecord item in Records)
        //        if (item.SomeParkingPlace.ParkPlaceNumber == CurrentRecord.SomeParkingPlace.ParkPlaceNumber)
        //        {
        //            item.SomeParkingPlace.Released = CurrentRecord.SomeParkingPlace.Released;
        //            item.SomeParkingPlace.FreeStatus = CurrentRecord.SomeParkingPlace.FreeStatus;
        //            break;
        //        }
        //}
        private void AddtestData()
        {
            CurrentRecord.SomePerson.SecondName = "Петров";
            CurrentRecord.SomePerson.FirstName = "Іван";
            CurrentRecord.SomePerson.Patronimic = "Дмитрович";
            CurrentRecord.SomePerson.Sex = true;
            CurrentRecord.SomeContacts.Phone = "+380509632514";
            CurrentRecord.SomeContacts.Adress = "м.Київ, вул. Героїв АТО, б.37";
            CurrentRecord.TrustedPerson.SecondName = "Кірієнко";
            CurrentRecord.TrustedPerson.FirstName = "Ірина";
            CurrentRecord.TrustedPerson.Patronimic = "Вікторівна";
            CurrentRecord.TrContacts.Phone = "+380975214652";
            CurrentRecord.TrustedPerson.Sex = false;
            CurrentRecord.SomeVehicle.Color = "Синій";
            CurrentRecord.SomeVehicleType.TypeName = "легковий";
            CurrentRecord.SomeVehicle.RegNumber = "AE2965KO";

        }

        private RelayCommand callCompanyInfoWindowCommand;
        public RelayCommand CallCompanyInfoWindowCommand => callCompanyInfoWindowCommand ?? (callCompanyInfoWindowCommand = new RelayCommand(
                    (obj) =>
                    {
                        if (CheckAccessRights())
                        {
                            CompanyInfo compWindow = new CompanyInfo();
                            showWindow.ShowDialog(compWindow);
                        }
                        else
                            dialogService.ShowMessage("Не достатньо прав доступу");
                        
                    }
                    ));
        private bool CheckAccessRights()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    User usver = db.Users.Find(IncomUserId);
                    if (usver != null)
                    {
                        return usver.AccessName == "мастер-адмін" ? true : false;
                    }
                    else
                    {
                        dialogService.ShowMessage("Проблеми зі считуванням данних \nпри спробі перевірки прав доступу");
                        return false;
                    }
                   
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
            return false;
        }

        

    }
}
