using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    OnPropertyChanged(nameof(SelectedRecord));
                }
            }
        }


        public MainViewModel()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
        }

        int incomUserId { get; set; }

        public MainViewModel(int UserId)
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            incomUserId = UserId;
            Records = new ObservableCollection<ParkingPlaceRecord>();
            DefaultDataLoad();
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

        private void DefaultDataLoad2()
        {

            //here we are forming data for datagid at SecondWindow.xaml. The main info.

            ObservableCollection<ParkingPlaceRecord> tmpRecords = new ObservableCollection<ParkingPlaceRecord>();
            string sqlExpression = "sp_GetparkingPlacesRecords";
            
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
                                    ParkPlaceNumber = (int)result.GetValue(1),
                                    FreeStatus = (bool)result.GetValue(2),
                                    Released = (bool)result.GetValue(3)
                                },
                                SomeClient = new Client
                                {
                                    ClientId = (int)result.GetValue(4),
                                    OrgName = (string)result.GetValue(5)
                                },
                                SomePerson = new Person
                                {
                                    PersonId = (int)result.GetValue(6),
                                    SecondName = (string)result.GetValue(7),
                                    FirstName = (string)result.GetValue(8),
                                    Patronimic = (string)result.GetValue(9)
                                },
                                SomeContacts = new Contacts
                                {
                                    ContactsId = (int)result.GetValue(10),
                                    Phone = (string)result.GetValue(11)
                                },
                                SomeVehicle = new Vehicle
                                {
                                    VehicleId = (int)result.GetValue(12),
                                    RegNumber = (string)result.GetValue(13),
                                    Color = (string)result.GetValue(14),
                                    TypeName = (string)result.GetValue(15)
                                },
                                SomeParkingPlaceLog = new ParkingPlaceLog
                                {
                                    ParkingPlaceLogId = (int)result.GetValue(16),
                                    DeadLine = (DateTime)result.GetValue(17)
                                }


                            };
                            tmpRecords.Add(record);
                        };

                        int ParkingPlaceeCount=0;

                        //attempt to know count of parking places
                        sqlExpression = "sp_GetparkingPlacesCount";
                        command = new SqlCommand(sqlExpression, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        result = command.ExecuteReader();

                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                ParkingPlaceeCount = (int)result.GetValue(0);
                            }
                        }
                        else
                            dialogService.ShowMessage("Щось пішло не так при зчитуванні кількості паркувальних місць");

                        int j = 0;
                        for (int i = 1; i <= ParkingPlaceeCount; i++)
                        {
                            if (j< tmpRecords.Count() && tmpRecords[j].SomeParkingPlace.ParkPlaceNumber == i)
                            {
                                Records.Add(tmpRecords[j]);
                                j++;
                            }
                            else
                            {
                                ParkingPlaceRecord pprecord = new ParkingPlaceRecord();
                                pprecord.SomeParkingPlace = new ParkingPlace
                                {
                                    ParkingPlaceId = i,
                                    ParkPlaceNumber = i,
                                    FreeStatus = true,
                                    Released = false
                                };
                                Records.Add(pprecord);
                            }                           
                        }
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

    }
}
