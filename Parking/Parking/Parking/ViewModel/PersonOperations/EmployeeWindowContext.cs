using Parking.Helpes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.PersonOperations
{
   public class EmployeeWindowContext:Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;
        int UserId { get; set; }

        public ObservableCollection<EmployeeRecord> EmployeeRecords { get; set; }
        public ObservableCollection<string> Statuses { get; set; }

        private EmployeeRecord selectetRecord;
        public EmployeeRecord SelectetRecord
        {
            get { return selectetRecord; }
            set
            {
                if (value != selectetRecord)
                {
                    selectetRecord = value;
                    OnPropertyChanged(nameof(SelectetRecord));
                }
            }
        }

        private EmployeeRecord currentRecord;
        public EmployeeRecord CurrentRecord
        {
            get { return currentRecord; }
            set
            {
                if (value != currentRecord)
                {
                    currentRecord = value;
                    OnPropertyChanged2(nameof(CurrentRecord));
                }
            }
        }

        private string defaultPhoto;
        public string DefaultPhoto
        {
            set
            {
                if (defaultPhoto != value)
                {
                    defaultPhoto = value;
                    OnPropertyChanged(nameof(DefaultPhoto));
                }
            }
            get { return "/Parking;component/Images/" + defaultPhoto; }
        }

        public EmployeeWindowContext() { }
        public EmployeeWindowContext (int userId) 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            UserId = userId;
            EmployeeRecords = new ObservableCollection<EmployeeRecord>();
            DefaultDataLoad();
        }

        

            private void DefaultDataLoad()
        {
            DefaultPhoto = "default_avatar.png";

            Statuses = new ObservableCollection<string> { "адмінісмтратор","мастер-адмін","без статусу"};

            string sqlExpression = "sp_GetEmployeesRecords";

            var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                try
                {
                    EmployeeRecords.Clear();
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        EmployeeRecord record = new EmployeeRecord();
                        while (result.Read())
                        {
                            record.EmployeeId = (int)result.GetValue(0);
                            record.Salary = (decimal)result.GetValue(1);
                            record.HireDate = (DateTime)result.GetValue(2);
                            if (!(result.GetValue(3) is System.DBNull))
                                record.FireDate = (DateTime)result.GetValue(3);
                            record.Description = (string)result.GetValue(4);
                            record.PersonId = (int)result.GetValue(5);
                            record.SecondName = (string)result.GetValue(6);
                            record.FirstName = (string)result.GetValue(7);
                            record.Patronimic = (string)result.GetValue(8);
                            record.PYB = record.SecondName + " " + record.FirstName + " " + record.Patronimic;
                            record.Male = (bool)result.GetValue(9);
                            record.Female = (bool)result.GetValue(10);
                            record.DayBirthday = (DateTime)result.GetValue(11);
                            if (!(result.GetValue(12) is System.DBNull))
                                record.Photo = (byte[])result.GetValue(12);
                            record.ContactsId =(int) result.GetValue(13);
                            record.PhoneNumber = (string)result.GetValue(14);
                            record.Adress = (string)result.GetValue(15);
                            record.Status = (string)result.GetValue(16);

                            EmployeeRecords.Add(record);
                        };
                    }
                    else
                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про співробітників");
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

        private RelayCommand openFileDialogCommand;
        public RelayCommand OpenFileDialogCommand => openFileDialogCommand ?? (openFileDialogCommand = new RelayCommand(
                    (obj) =>
                    {
                        PathToFile();
                    }
                    ));

        private void PathToFile()
        {
            string path;
            path = dialogService.OpenFileDialog("C:\\");
            if (path == null)
                return;
           CurrentRecord.Photo = File.ReadAllBytes(path);
        }

        private RelayCommand deletePersonPhotoCommand;
        public RelayCommand DeletePersonPhotoCommand => deletePersonPhotoCommand ?? (deletePersonPhotoCommand = new RelayCommand(
                    (obj) =>
                    {
                        CurrentRecord.Photo = null;
                    }
                    ));


    }
}
