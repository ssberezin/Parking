using Parking.Helpes;
using Parking.Model;
using Parking.Views.CompanyOps;
using Parking.Views.ParkPlacesOps;
using Parking.Views.PersonOperations;
using Parking.Views.ReportOps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ReportOps.MoneyMoveReportOps
{
    public class MoneyMoveReportContext:Helpes.ObservableObject
    {

        private StuffMoneyRecord selectedStuffRec;
        public StuffMoneyRecord SelectedStuffRec
        {
            get { return selectedStuffRec; }
            set
            {
                if (selectedStuffRec != value)
                {
                    selectedStuffRec = value;
                    OnPropertyChanged2(nameof(SelectedStuffRec));
                }
            }
        }

        private decimal subTotal;
        public decimal SubTotal
        {
            get { return subTotal; }
            set
            {
                if (subTotal != value)
                {
                    subTotal = value;
                    OnPropertyChanged(nameof(SubTotal));
                }
            }
        }

        private decimal total;
        public decimal Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged(nameof(Total));
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

        private DateTime endtHistoryDate;
        public DateTime EndHistoryDate
        {
            get { return endtHistoryDate; }
            set
            {
                if (endtHistoryDate != value)
                {
                    endtHistoryDate = value;
                    OnPropertyChanged(nameof(EndHistoryDate));
                }
            }
        }

        public ObservableCollection<StuffMoneyRecord> StuffMoneyRecords { get; set; }
        public ObservableCollection<MoneyMoveRepDetailRecord> MoneyMoveRepDetailRecords { get; set; }

        IDialogService dialogService;
        IShowWindowService showWindow;
        Library lib;

        public MoneyMoveReportContext()
        {
            lib = new Library();
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            MoneyMoveRepDetailRecords = new ObservableCollection<MoneyMoveRepDetailRecord>();
            StuffMoneyRecords = new ObservableCollection<StuffMoneyRecord>();
            StartHistoryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-1);
            EndHistoryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddHours(23).AddMinutes(59).AddSeconds(59);
            DefaultDataLoad();
            Total = lib.GetTotalSum(StartHistoryDate, EndHistoryDate);
            PropertyChanged2 += ChangeSelectedRecord;
        }

        private void ChangeSelectedRecord(object sender, PropertyChangedEventArgs e)
        {
            GetMoneyMoveRepDetailRecords(SelectedStuffRec.UserId, out decimal sTotal, StartHistoryDate, EndHistoryDate);            
            SubTotal = sTotal;
        }

        private void DefaultDataLoad()
        {

          
            StuffMoneyRecords.Clear();
            string sqlExpression = "sp_GetPreviousStuffRecord";

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
                        StuffMoneyRecord record;

                        while (result.Read())
                        {
                            record = new StuffMoneyRecord()
                            {
                                
                                    EmployeeId = (int)result.GetValue(0),
                                    PIB = (string)result.GetValue(1),
                                    StartDateWorking = (DateTime)result.GetValue(2),
                                    PersonId = (int)result.GetValue(3),
                                    UserId = (int)result.GetValue(4)
                            };

                            StuffMoneyRecords.Add(record);
                        };
                    }
                    else
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про персонал");
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

        private void GetMoneyMoveRepDetailRecords(int userId, out decimal sTotal, DateTime startDate, DateTime enddate)
        {
            sTotal = 0;
            

            string sqlExpression = "sp_GetDetalesStuffRecord";

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
                        ParameterName = "@userId",
                        Value = userId
                    };
                    SqlParameter secondParam = new SqlParameter
                    {
                        ParameterName = "@startDate",
                        Value = startDate
                    };
                    SqlParameter thirdParam = new SqlParameter
                    {
                        ParameterName = "@endDate",
                        Value = enddate
                    };
                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        MoneyMoveRepDetailRecord record;
                        

                        while (result.Read())
                        {
                            record = new MoneyMoveRepDetailRecord()
                            {

                                VehicleId = (int)result.GetValue(0),
                                RegNumber = (string)result.GetValue(1),
                                PPlogId = (int)result.GetValue(2),                                
                                PayingDate = (DateTime)result.GetValue(4),
                                DeadLine = (DateTime)result.GetValue(5),
                                Money = (decimal)result.GetValue(3)
                            };

                            MoneyMoveRepDetailRecords.Add(record);
                            sTotal += record.Money;
                        };                        
                    }
                    else
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про деталі дій персоналу");
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

        private RelayCommand callFilterByDateCommand;
        public RelayCommand CallFilterByDateCommand => callFilterByDateCommand ?? (callFilterByDateCommand = new RelayCommand(
                    (obj) =>
                    {
                        GetMoneyMoveRepDetailRecords(SelectedStuffRec.UserId, out decimal sTotal, StartHistoryDate, EndHistoryDate);
                        SubTotal = sTotal;
                        Total = lib.GetTotalSum(StartHistoryDate, EndHistoryDate);
                    }
                    ));




    }
}
