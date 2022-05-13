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
      
        
        private OwnerRecord selectedRecord;
        public OwnerRecord SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                if (selectedRecord != value)
                {
                    selectedRecord = value;
                    OnPropertyChanged(nameof(selectedRecord));
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
            FillOwnerRecords(false, StartHistoryDate, EndHistoryDate);//default filling data . Only for hired parking places


        }

        private void ChangeSekectedRecord(object sender, PropertyChangedEventArgs e)
        {
            SelectedRecord.ReportOpsRecords = GetReportData(SelectedRecord.ClientId);
        }




        private void FillOwnerRecords(bool freeStatus, DateTime startDate, DateTime endDate)

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
                    SqlParameter firstParam = new SqlParameter
                    {
                        ParameterName = "@freeStatus",
                        Value = freeStatus
                    };

                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@startDate",
                        Value = new DateTime(startDate.Year, startDate.Month, startDate.Day)
                    };

                    SqlParameter thirdParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = new DateTime(endDate.Year, endDate.Month, endDate.Day).AddDays(1)
                    };
                    
                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);
                    
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

                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних клієнтів");
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

        private ObservableCollection< ReportOpsRecord> GetReportData(int clientId)

        {

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
                        ParameterName = "@clId",
                        Value = clientId
                    };
                    command.Parameters.Add(firstParam);
                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ReportOpsRecord rec = new ReportOpsRecord
                            {
                                ParkPlaceId = (int)result.GetValue(0),
                                ParkPlaceNumber = (int)result.GetValue(1),
                                FreeStatus = (bool)result.GetValue(2),
                                Released = (bool)result.GetValue(3),
                                UserId = (int)result.GetValue(4),
                                UserData = (string)result.GetValue(5)
                            };
                            if (!(result.GetValue(4) is DBNull))
                            {
                                DateTime dat = (DateTime)result.GetValue(4);
                                rec.EventDate = dat.ToString("dd/MM/yyyy"); 
                                rec.EventTime = dat.ToString("HH/mm/ss");                                
                            };
                            records.Add(rec);

                        };
                        return records;
                    }
                    else
                    {

                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних клієнтів");
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
            return null;
        }

    }
}
