using Parking.Helpes;
using Parking.Model;
using Parking.ViewModel.FilterOps;

using Parking.Views.ParkPlacesOps;
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
        public ObservableCollection<ParPlaceRecord> ParPlaceRecords { get; set; }
        
        

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
                    OnPropertyChanged8(nameof(StartHistoryDate));
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
                    OnPropertyChanged8(nameof(EndHistoryDate));
                }
            }
        }

        private FilterContext filter1;
        public FilterContext Filter1
        {
            get { return filter1; }
            set
            {
                if (filter1 != value)
                {
                    filter1 = value;
                    OnPropertyChanged8(nameof(Filter1));
                }
            }
        }

        private FilterContext filter2;
        public FilterContext Filter2
        {
            get { return filter2; }
            set
            {
                if (filter2 != value)
                {
                    filter2 = value;
                    OnPropertyChanged(nameof(Filter2));
                }
            }
        }

        private FilterContext filter3;
        public FilterContext Filter3
        {
            get { return filter3; }
            set
            {
                if (filter3 != value)
                {
                    filter3 = value;
                    OnPropertyChanged(nameof(Filter3));
                }
            }
        }

        private bool enableFilter2;
        public bool EnableFilter2
        {
            get { return enableFilter2; }
            set
            {
                if (enableFilter2 != value)
                {
                    enableFilter2 = value;
                    OnPropertyChanged(nameof(EnableFilter2));
                }
            }
        }

        private bool enableFilter3;
        public bool EnableFilter3
        {
            get { return enableFilter3; }
            set
            {
                if (enableFilter3 != value)
                {
                    enableFilter3 = value;
                    OnPropertyChanged(nameof(EnableFilter3));
                }
            }
        }


        int CurretnUserId { get; set; }

        Library lib;//for call methods from here

        



        public ReportOpsContext() 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
         
        }
        public ReportOpsContext(int inputUserid) 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            lib = new Library();
            SetDefaultFilters();
            CurretnUserId = inputUserid;
            OwnerRecords = new ObservableCollection<OwnerRecord>();
            ReportOpsRecords = new ObservableCollection<ReportOpsRecord>();
            ParPlaceRecords = new ObservableCollection<ParPlaceRecord>();

            StartHistoryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-1);
            EndHistoryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddHours(23).AddMinutes(59).AddSeconds(59);
            FillOwnerRecords();//default filling data 

            PropertyChanged += ChangeSelectedRecord;
            PropertyChanged2 += ChangeParkPlaceSelecteRecord;
        }

        private void SetDefaultFilters()
        {
            Filter1 = new FilterContext();
            Filter1.CheckArg3 = true;//it will be like only 'not free' parking places to found
            Filter2 = new FilterContext();
            Filter3 = new FilterContext();
        }

        private void ChangeSelectedRecord(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedRecord is null)
            {
                ParPlaceRecords.Clear();
                return;
            }

            if (OwnerRecords.Count() > 0)
            {
                //ParPlaceRecords = GetClientParkingPlaces(SelectedRecord.ClientId, StartHistoryDate, EndHistoryDate);
                ParPlaceRecords.Clear();
                foreach (var item in GetClientParkingPlaces(SelectedRecord.ClientId, StartHistoryDate, EndHistoryDate))
                    ParPlaceRecords.Add(item);
                EnableFilter2 = true;
            };
        }

        private void ChangeParkPlaceSelecteRecord(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedRecord is null)
            {
                ReportOpsRecords.Clear();
                return;
            }

            if (ParPlaceSelecteRecord is null)
                return;
            FillReportRecods(ParPlaceSelecteRecord.PPlace.ParkingPlaceId, StartHistoryDate, EndHistoryDate);
            EnableFilter3 = true;
        }


        private void FillOwnerRecords()

        {            
            OwnerRecords.Clear();

            //get record only from park places with 'not free' status
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
                                UserData = (string)result.GetValue(4),
                                Released = (bool)result.GetValue(5)


                            };
                            if (!(result.GetValue(4) is DBNull))                            
                                rec.EventDate = (DateTime)result.GetValue(2);                                                              
                            
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

        private ParkingPlaceRecord currentPPRecord;
        public ParkingPlaceRecord CurrentPPRecord
        {
            get { return currentPPRecord; }
            set
            {
                if (currentPPRecord != value)
                {
                    currentPPRecord = value;
                    OnPropertyChanged(nameof(CurrentPPRecord));
                }
            }
        }


        private RelayCommand editparkPlaceCommand;
        public RelayCommand EditparkPlaceCommand => editparkPlaceCommand ?? (editparkPlaceCommand = new RelayCommand(
                    (obj) =>
                    {                       
                        CurrentPPRecord = lib.GetPPRecord(ParPlaceSelecteRecord.PPlace.ParkingPlaceId);
                        ParkPlaceEditWindow parkWindow = new ParkPlaceEditWindow(CurretnUserId, CurrentPPRecord);
                        showWindow.ShowDialog(parkWindow);
                        //update visible data                       
                        ParPlaceRecords.Clear();
                        foreach (ParPlaceRecord item in GetClientParkingPlaces(SelectedRecord.ClientId, StartHistoryDate, EndHistoryDate))
                            ParPlaceRecords.Add(item);

                        FillReportRecods(CurrentPPRecord.SomeParkingPlace.ParkingPlaceId, StartHistoryDate, EndHistoryDate);

                    }
                    ));

        

        private RelayCommand callFilter1Command;
        public RelayCommand CallFilter1Command => callFilter1Command ?? (callFilter1Command = new RelayCommand(
                    (obj) =>
                    {
                        Filter1.DateArg1 = StartHistoryDate;
                        Filter1.DateArg2 = EndHistoryDate;
                        OwnerRecords.Clear();

                        if (Filter1.CheckArg4)
                        {

                            if (Filter1.CheckArg1)
                            {
                                foreach (var item in lib.GetFiltered1OwnerRecordsAllStatuses(Filter1.DateArg1.Value, Filter1.DateArg2.Value))
                                    if(item.OwnerName.Contains(Filter1.StrArg1))
                                           OwnerRecords.Add(item);
                            }
                            else
                                foreach (var item in lib.GetFiltered1OwnerRecordsAllStatuses(Filter1.DateArg1.Value, Filter1.DateArg2.Value))
                                    OwnerRecords.Add(item);
                        }
                        else
                        {
                            if (Filter1.CheckArg1)
                            {
                                foreach (var item in lib.GetFiltered1OwnerRecordsByStatusAndDate(Filter1.CheckArg2, Filter1.DateArg1.Value, Filter1.DateArg2.Value))
                                    if (item.OwnerName.Contains(Filter1.StrArg1))
                                        OwnerRecords.Add(item);
                            }
                            else                                
                                foreach (var item in lib.GetFiltered1OwnerRecordsByStatusAndDate(Filter1.CheckArg2, Filter1.DateArg1.Value, Filter1.DateArg2.Value))
                                    OwnerRecords.Add(item);
                        }
                        if (OwnerRecords.Count() == 0)
                        {
                            FillOwnerRecords();
                            dialogService.ShowMessage("Не було співпадінь. Завантажено актуальну \n" +
                                                       "інформацію щодо власників на зайнятих місцях");
                        }

                        EnableFilter2 = EnableFilter3 = false;
                    }
                    ));

        private RelayCommand callFilter2Command;
        public RelayCommand CallFilter2Command => callFilter2Command ?? (callFilter2Command = new RelayCommand(
                    (obj) =>
                    {
                        if (!Filter2.CheckArg1 && !Filter2.CheckArg2)
                        {
                            dialogService.ShowMessage("Не активовано жодного фыльтру.");
                            return;
                        }
                        ParPlaceRecords = lib.GetFilteredParPlaceRecords(Filter2.CheckArg1, Filter2.CheckArg2, Filter2.IntArg1, Filter2.CheckArg3, ParPlaceRecords);
                        
                        if (ParPlaceRecords.Count() == 0)
                        {
                            dialogService.ShowMessage("Немаэ співпадінь.\n Показуэмо попредны данні." );
                            ParPlaceRecords.Clear();
                            foreach (var item in GetClientParkingPlaces(SelectedRecord.ClientId, StartHistoryDate, EndHistoryDate))
                                ParPlaceRecords.Add(item);
                        }
                    }
                    ));

        private RelayCommand callFilter3Command;
        public RelayCommand CallFilter3Command => callFilter3Command ?? (callFilter3Command = new RelayCommand(
                    (obj) =>
                    {
                        if (!Filter3.CheckArg1 && !Filter3.CheckArg2 && !Filter3.CheckArg3)
                        {
                            dialogService.ShowMessage("Не активовано жодного фыльтру.");
                            return;
                        }
                        ReportOpsRecords = lib.GetFilteredParPlaceRecords(Filter3, StartHistoryDate, EndHistoryDate, ReportOpsRecords);

                        if (ReportOpsRecords.Count() == 0)
                        {
                            dialogService.ShowMessage("Немаэ співпадінь.\n Показуэмо попредны данні.");
                            ReportOpsRecords.Clear();
                            FillReportRecods(ParPlaceSelecteRecord.PPlace.ParkingPlaceId, StartHistoryDate, EndHistoryDate);
                        }
                    }
                    ));





        private RelayCommand clearFilter1Command;
        public RelayCommand ClearFilter1Command => clearFilter1Command ?? (clearFilter1Command = new RelayCommand(
                    (obj) =>
                    {
                        Filter1.ClearFilters();
                    }
                    ));

        private RelayCommand clearFilter2Command;
        public RelayCommand ClearFilter2Command => clearFilter2Command ?? (clearFilter2Command = new RelayCommand(
                    (obj) =>
                    {
                        Filter2.ClearFilters();
                    }
                    ));

        private RelayCommand clearFilter3Command;
        public RelayCommand ClearFilter3Command => clearFilter3Command ?? (clearFilter3Command = new RelayCommand(
                    (obj) =>
                    {
                        Filter3.ClearFilters();
                    }
                    ));

        private RelayCommand findDeptorsCommand;
        public RelayCommand FindDeptorsCommand => findDeptorsCommand ?? (findDeptorsCommand = new RelayCommand(
                    (obj) =>
                    {
                        
                        OwnerRecords.Clear();
                        foreach (var item in lib.GetDeptors(DateTime.Now))
                            OwnerRecords.Add(item);
                        if (OwnerRecords.Count() == 0)
                        {
                            int tmpId=1;
                            if (SelectedRecord !=null)
                                tmpId = SelectedRecord.ClientId;
                            FillOwnerRecords();
                            ReportOpsRecords.Clear();
                            ParPlaceRecords.Clear();
                            dialogService.ShowMessage("Не було співпадінь.");
                     
                        }

                        EnableFilter2 = EnableFilter3 = false;

                    }
                    ));





    }
}
