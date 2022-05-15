using Parking.Helpes;
using Parking.Model;
using Parking.Views.PrintOps;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Parking.ViewModel.ReportOps
{
    public class ReportOpsContext:Helpes.ObservableObject
        
    {

        IDialogService dialogService;
        IShowWindowService showWindow;

        public ObservableCollection<OwnerRecord> OwnerRecords { get; set; }
        public ObservableCollection<ReportOpsRecord> ReportOpsRecords { get; set; }


        private OwnerRecord selectedRecord;
        public OwnerRecord SelectedRecord
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

        private ReportOpsRecord reportSelecteRecord;
        public ReportOpsRecord ReportSelecteRecord
        {
            get { return reportSelecteRecord; }
            set
            {
                if (reportSelecteRecord != value)
                {
                    reportSelecteRecord = value;
                    OnPropertyChanged(nameof(ReportSelecteRecord));
                }
            }
        }

        private ParPlaceRecord parPlaceSelecteRecord;
        public ParPlaceRecord ParPlaceSelecteRecord
        {
            get { return parPlaceSelecteRecord; }
            set
            {
                if (parPlaceSelecteRecord != value)
                {
                    parPlaceSelecteRecord = value;
                    OnPropertyChanged2(nameof(ParPlaceSelecteRecord));
                }
            }
        }


        private DateTime startHistoryDate;
        public DateTime StartHistoryDate
        {
            get { return startHistoryDate; }
            set
            {
                if (startHistoryDate != value)
                {
                    startHistoryDate = value;
                    OnPropertyChanged(nameof(StartHistoryDate));
                }
            }
        }

        private DateTime endHistoryDate;
        public DateTime EndHistoryDate
        {
            get { return endHistoryDate; }
            set
            {
                if (endHistoryDate != value)
                {
                    endHistoryDate = value;
                    OnPropertyChanged(nameof(EndHistoryDate));
                }
            }
        }


        public ReportOpsContext() 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
        }
        public ReportOpsContext(int inputUserid) 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            OwnerRecords = new ObservableCollection<OwnerRecord>();
            ReportOpsRecords = new ObservableCollection<ReportOpsRecord>(); 
            StartHistoryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-1);
            EndHistoryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddHours(23).AddMinutes(59).AddSeconds(59);
            FillOwnerRecords();//default filling data 

            PropertyChanged += ChangeSelectedRecord;
            PropertyChanged2 += ChangeParkPlaceSelecteRecord;
        }

        private void ChangeSelectedRecord(object sender, PropertyChangedEventArgs e)
        {
            SelectedRecord.ParPlaceRecords = GetClientParkingPlaces(SelectedRecord.ClientId, StartHistoryDate, EndHistoryDate);
            ParPlaceSelecteRecord = null;
            ReportOpsRecords.Clear();   

        }

        private void ChangeParkPlaceSelecteRecord(object sender, PropertyChangedEventArgs e)
        {
            //ParPlaceSelecteRecord.ReportOpsRecords = SelectedRecord.ParPlaceRecords.
            FillReportRecods(ParPlaceSelecteRecord.PPlace.ParkingPlaceId, StartHistoryDate, EndHistoryDate);

        }


        private void FillOwnerRecords()

        {

            
            OwnerRecords.Clear();

            string sqlExpression = "sp_GetClientInfoForReport";

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

                        while (result.Read())
                        {                           
                            OwnerRecord rec = new OwnerRecord
                            {
                                ClientId = (int)result.GetValue(0),
                                OwnerName = result.GetValue(1) is DBNull ? "" : (string)result.GetValue(1),
                                PersonId = (int)result.GetValue(2),
                                MaxDeadLine = (DateTime)result.GetValue(4)
                            };
                            

                            if (rec.OwnerName == "" || rec.OwnerName == "не задано")
                                rec.OwnerName = (string)result.GetValue(3);
                            OwnerRecords.Add(rec);
                           
                        };
                    }
                    else
                    {

                        dialogService.ShowMessage("Немає збігів");
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

        private ObservableCollection<ParPlaceRecord> GetClientParkingPlaces(int clientId, DateTime startDate, DateTime endDate)

        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day).AddHours(23).AddMinutes(59).AddSeconds(59);

            ObservableCollection<ParPlaceRecord> records = new ObservableCollection<ParPlaceRecord>();
            string sqlExpression = "sp_GetClientParkPlaces";

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
                        ParameterName = "@clId",
                        Value = clientId
                    };
                  
                    command.Parameters.Add(firstParam);
                    

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ParPlaceRecord rec = new ParPlaceRecord 
                            {
                                PPlace = new ParkingPlace
                                {
                                    ParkingPlaceId = (int)result.GetValue(0),
                                    ParkPlaceNumber = (int)result.GetValue(1),
                                    FreeStatus = (bool)result.GetValue(2),
                                    Released = (bool)result.GetValue(3)
                                }
                                
                            };
                            
                            records.Add(rec);

                        };
                        return records;
                    }
                    else
                        dialogService.ShowMessage("Немаэ збіжносией");
                    
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
            return null;
        }




        private ObservableCollection<ReportOpsRecord> GetReportData(int ppId, DateTime startDate, DateTime endDate )

        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day).AddDays(1);

            ObservableCollection<ReportOpsRecord> records = new ObservableCollection<ReportOpsRecord>();
            string sqlExpression = "sp_GetClRepRecord";

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
                        Value = ppId
                    };
                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@startDate",
                        Value = startDate
                    };
                    SqlParameter thirdParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = endDate
                    };

                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ReportOpsRecord rec = new ReportOpsRecord
                            {
                                VehicleId = (int)result.GetValue(0),
                                VehicleNumber = (string)result.GetValue(1),                                
                                UserId = (int)result.GetValue(3),
                                UserData = (string)result.GetValue(4)
                            };
                            if (!(result.GetValue(4) is DBNull))
                            {
                                DateTime dat = (DateTime)result.GetValue(2);
                                rec.EventDate = dat.ToString("dd/MM/yyyy"); 
                                rec.EventTime = dat.ToString("HH/mm/ss");                                
                            };
                            records.Add(rec);

                        };
                        return records;
                    }
                    else
                        dialogService.ShowMessage("Немає збіжностей");
                    
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
            return null;
        }


        private void FillReportRecods(int ppId, DateTime startDate, DateTime endDate)

        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day).AddDays(1);

            ReportOpsRecords.Clear();
            string sqlExpression = "sp_GetClRepRecord";

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
                        Value = ppId
                    };
                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@startDate",
                        Value = startDate
                    };
                    SqlParameter thirdParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = endDate
                    };

                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ReportOpsRecord rec = new ReportOpsRecord
                            {
                                VehicleId = (int)result.GetValue(0),
                                VehicleNumber = (string)result.GetValue(1),
                                UserId = (int)result.GetValue(3),
                                UserData = (string)result.GetValue(4)
                            };
                            if (!(result.GetValue(4) is DBNull))
                            {
                                DateTime dat = (DateTime)result.GetValue(2);
                                rec.EventDate = dat.ToString("dd/MM/yyyy");
                                rec.EventTime = dat.ToString("HH/mm/ss");
                            };
                           ReportOpsRecords.Add(rec);

                        };                        
                    }
                    else
                        dialogService.ShowMessage("Немає збіжностей");

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

        private RelayCommand showCommandCommand;
        public RelayCommand ShowCommandCommand => showCommandCommand ?? (showCommandCommand = new RelayCommand(
                    (obj) =>
                    {

                        //ReportOpsRecords = GetReportData(ParPlaceSelecteRecord.PPlace.ParkingPlaceId, StartHistoryDate, EndHistoryDate);
                        FillReportRecods(ParPlaceSelecteRecord.PPlace.ParkingPlaceId, StartHistoryDate, EndHistoryDate);

                    }
                    ));

    }
}
