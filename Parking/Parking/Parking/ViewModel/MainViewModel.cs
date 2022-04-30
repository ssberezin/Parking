using Parking.Helpes;
using Parking.Model;
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

                            if (!SelectedRecord.SomeParkingPlace.FreeStatus.Value)
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
                                    Patronimic = (string)result.GetValue(9)

                                };
                                CurrentRecord.SomeContacts = new Contacts
                                {
                                    ContactsId = (int)result.GetValue(10),
                                    Phone = (string)result.GetValue(11)
                                };
                                CurrentRecord.SomeVehicle = new Vehicle
                                {
                                    VehicleId = (int)result.GetValue(12),
                                    RegNumber = (string)result.GetValue(13),
                                    Color = (string)result.GetValue(14),
                                    TypeName = (string)result.GetValue(15)
                                };
                                CurrentRecord.SomeParkingPlaceLog = new ParkingPlaceLog
                                {
                                    ParkingPlaceLogId = (int)result.GetValue(16),
                                    DeadLine = (DateTime)result.GetValue(17)
                                };

                                if (CurrentRecord.SomeClient.OrgName == null || CurrentRecord.SomeClient.OrgName == "не вказано")
                                    CurrentRecord.SomeClient.OrgName = CurrentRecord.SomePerson.SecondName + " " + CurrentRecord.SomePerson.FirstName + " " + CurrentRecord.SomePerson.Patronimic;

                                if (!(result.GetValue(18) is System.DBNull))
                                    CurrentRecord.SomePerson.TrustedPerson_Id = (int?)result.GetValue(18);

                                if (CurrentRecord.SomePerson.TrustedPerson_Id != null)
                                {

                                    GetTrustedPerson(CurrentRecord.SomePerson.TrustedPerson_Id.Value, out Person nPerson, out Contacts nContacts);
                                    CurrentRecord.TrustedPerson = nPerson;
                                    CurrentRecord.TrContacts = nContacts;                                    
                                    
                                }
                                VisaBility = "Visible";                               
                                DateTime dt = (DateTime)result.GetValue(17);
                                DeadLine = new DateTime(dt.Year, dt.Month, dt.Day).ToString("dd/MM/yyyy");                                
                                OutOfDeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine > DateTime.Now ? "не просрочено" : "просрочено";

                            }
                            else
                                VisaBility = "Hidden";
                                    
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
                        CallStuffListWindow(IncomUserId);
                    }
                    ));
        private void CallStuffListWindow(int UserId)
        {

            EmployeeWindow empWindow = new EmployeeWindow(UserId);
            showWindow.ShowDialog(empWindow);

            //here we are needding to update record and selected record
            //if (TMPStaticClass.CurrentOrder != null)
            //{
            //    Records record = GetRecord(TMPStaticClass.CurrentOrder.OrderLineId);
            //    Records.Add(record);
            //}
            //TMPStaticClass.CurrentOrder = null;
        }


    }
}
