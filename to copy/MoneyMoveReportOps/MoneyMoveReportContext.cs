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
                    OnPropertyChanged(nameof(SelectedStuffRec));
                }
            }
        }

        public ObservableCollection<StuffMoneyRecord> StuffMoneyRecords { get; set; }

        IDialogService dialogService;
        IShowWindowService showWindow;
        public MoneyMoveReportContext()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            StuffMoneyRecords = new ObservableCollection<StuffMoneyRecord>();
            PropertyChanged += ChangeSelectedRecord;
        }

        private void ChangeSelectedRecord(object sender, PropertyChangedEventArgs e)
        {
            SelectedStuffRec.MoneyMoveRepDetailRecords = MoneyMoveRepDetailRecords(SelectedStuffRec.UserId);
        }

        private void DefaultDataLoad()
        {

            //db.Database.ExecuteSqlCommand
            //         (@"
            //       create proc sp_GetPreviousStuffRecord
            //as
            //select emp.EmployeeId '0_EmployeeId', pers.SecondName +' '+pers.FirstName+' '+pers.Patronimic '1_PIB', emp.HireDate '2_HireDate', pers.PersonId '3_PersId', us.UserId '4_UserId'
            //From People Pers 
            //join Employees Emp on Pers.PersonId=Emp.SomePerson_PersonId
            //join Users us on us.SomeEmployee_EmployeeId=emp.EmployeeId
            //join ParkingPlaceLogs PPl on PPl.SomeUser_UserId=us.UserId
            //            ");
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

        private ObservableCollection <MoneyMoveRepDetailRecord> MoneyMoveRepDetailRecords(int userId)
        {

            ObservableCollection<MoneyMoveRepDetailRecord> tmpCollection = new ObservableCollection<MoneyMoveRepDetailRecord>();

            //db.Database.ExecuteSqlCommand
            //         (@"
            //            create proc sp_GetDetalesStuffRecord
            //            @userId int
            //as
            //Select veh.VehicleId '0_VehicleId',veh.RegNumber '1_RegNumber', ppl.ParkingPlaceLogId '2_ParkingPlaceLogId', 
            //					ppl.Money '3_Money', ppl.PayingDate '4_PayingDate', ppl.DeadLine '5_DeadLine'
            //From Vehicles veh
            //join ParkingPlaces PP on Veh.ParkingPlace_ParkingPlaceId = Pp.ParkingPlaceId
            //join ParkingPlaceLogs PPl on pp.ParkingPlaceId = Ppl.SomeParkingPlace_ParkingPlaceId
            //join Users Us on Ppl.SomeUser_UserId = us.UserId
            //where us.UserId = @userId
            //            ");

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
                    command.Parameters.Add(firstParam);

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

                            tmpCollection.Add(record);
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

            return null;

        }



    }
}
