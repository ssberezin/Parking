﻿using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
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

        //these both for compare EmployeeWindow data state before some actions
        EmployeeState PreviousState { get; set; }
        EmployeeState CurrentState { get; set; }

        
        public DateTime MindateRestriction { get; set; }//for min employee age restriction

        public ObservableCollection<EmployeeRecord> EmployeeRecords { get; set; }
        public ObservableCollection<EmployeePosition> EmployeePositions { get; set; }
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

        private string taxCode;
        public string TaxCode
        {
            get { return taxCode; }
            set
            {
                if (value != taxCode)
                {
                    taxCode = value;
                    OnPropertyChanged2(nameof(TaxCode));
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
                    OnPropertyChanged(nameof(CurrentRecord));
                }
            }
        }

        private EmployeePosition currentPosition;
        public EmployeePosition CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                if (value != currentPosition)
                {
                    currentPosition = value;
                    OnPropertyChanged2(nameof(CurrentPosition));
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

        Library lib;
        public EmployeeWindowContext() { }
        public EmployeeWindowContext (int userId) 
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            UserId = userId;
            EmployeeRecords = new ObservableCollection<EmployeeRecord>();
            DefaultDataLoad();
            FillEmployeePositions();//filling by data collection of employee positions
           
            PropertyChanged += GetRecordDetales;
            PropertyChanged2 += TaxCodeChange;
        }

        

            private void DefaultDataLoad()
        {
            lib = new Library();
            MindateRestriction = DateTime.Now.AddYears(-18);

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
                            record.Female = !record.Male;
                            record.DayBirthday = (DateTime)result.GetValue(10);
                            if (!(result.GetValue(11) is System.DBNull))
                                record.Photo = (byte[])result.GetValue(11);
                            record.ContactsId =(int) result.GetValue(12);
                            record.PhoneNumber = (string)result.GetValue(13);
                            record.Adress = (string)result.GetValue(14);

                            record.SomeUser.AccessName = (string)result.GetValue(15);
                            record.SomeUser.UserId = (int)result.GetValue(20);
                            record.SomeUser.Login = (string)result.GetValue(19);
                            record.SomeUser.Pass = (string)result.GetValue(18);

                            record.Position = (string)result.GetValue(16);
                            record.PositionId = (int)result.GetValue(17);
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

        private void FillEmployeePositions()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    var res = db.EmployeePositions.ToList();
                    if (res != null)                    
                        foreach (EmployeePosition item in res)                        
                            EmployeePositions.Add(item);                        
                    
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

        private void GetRecordDetales(object sender, PropertyChangedEventArgs e)
        {
            CurrentRecord = SelectetRecord;
            CurrentPosition = new EmployeePosition 
                                    { EmployeePositionId = SelectetRecord.PositionId,
                                      PositionName = SelectetRecord.Position };
            PreviousState.EmpRec = PreviousState.SetState(SelectetRecord);

        }

        private void TaxCodeChange(object sender, PropertyChangedEventArgs e)
        {
            TaxCode = lib.TaxCodeValidation(TaxCode);            
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

        private RelayCommand savedataCommand;
        public RelayCommand SavedataCommand => savedataCommand ?? (savedataCommand = new RelayCommand(
                    (obj) =>
                    {
                        if (!CheckInputValidationData())
                        {
                            dialogService.ShowMessage("Значення ІНН співробітника вказано НЕ корректно\n" +
                                "\t\t Потрібно відкорегувати.");
                            return;
                        }

                        CurrentRecord.PositionId = CurrentPosition.EmployeePositionId;
                        CurrentRecord.Position = CurrentPosition.PositionName;
                        CurrentState.EmpRec = CurrentState.SetState(CurrentRecord);
                        if (long.TryParse(TaxCode, out long tmp))
                            CurrentRecord.TaxCode = tmp;

                       
                        Editdata();
                        PreviousState.EmpRec = CurrentState.SetState(CurrentRecord);
                    }
                    ));
        private void Editdata()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    if (!PreviousState.PesoneCompare (PreviousState.EmpRec, CurrentState.EmpRec))
                    {
                        Person ownerPerson = db.Persons.Find(CurrentRecord.PersonId);
                        db.Entry(ownerPerson).State = EntityState.Modified;
                        ownerPerson.FirstName = CurrentRecord.FirstName;
                        ownerPerson.SecondName = CurrentRecord.SecondName;
                        ownerPerson.Patronimic = CurrentRecord.Patronimic;
                        ownerPerson.Sex = CurrentRecord.Male? CurrentRecord.Male: CurrentRecord.Female;
                        ownerPerson.DayBirthday = CurrentRecord.DayBirthday;
                        ownerPerson.Photo = CurrentRecord.Photo;
                    }

                    if (!PreviousState.ContactsCompare(PreviousState.EmpRec, CurrentState.EmpRec))
                    {
                        Contacts ownerCTN = db.Contacts.Find(CurrentRecord.ContactsId);
                        db.Entry(ownerCTN).State = EntityState.Modified;
                        ownerCTN.Adress = CurrentRecord.Adress;
                        ownerCTN.Phone = CurrentRecord.PhoneNumber;                        
                    }
                    
                    if (!PreviousState.EmployeePositionCompare(PreviousState.EmpRec, CurrentState.EmpRec) ||
                        !PreviousState.EmployeeCompare(PreviousState.EmpRec, CurrentState.EmpRec))
                    {
                        Employee emp1 = db.Employees.Find(CurrentRecord.EmployeeId);
                        db.Entry(emp1).State = EntityState.Modified;
                        emp1.EmployeePositions.Remove(db.EmployeePositions.Find(PreviousState.EmpRec.PositionId));
                        emp1.EmployeePositions.Add(db.EmployeePositions.Find(CurrentRecord.PositionId));
                        emp1.Salary = CurrentRecord.Salary;
                        emp1.FireDate = CurrentRecord.FireDate;
                        emp1.HireDate = CurrentRecord.HireDate;
                        emp1.Description = CurrentRecord.Description;                        
                    }

                    if (!(PreviousState.StatusCompare(PreviousState.EmpRec, CurrentState.EmpRec)))
                    {
                        User usver = db.Users.Find(CurrentRecord.SomeUser.UserId);
                          db.Entry(usver).State = EntityState.Modified;
                        if (CurrentState.EmpRec.SomeUser.AccessName != PreviousState.EmpRec.SomeUser.AccessName &&
                            CurrentState.EmpRec.SomeUser.AccessName == "без статусу")
                        {
                            usver.Pass = null;
                            usver.Login = null;
                            usver.AccessName = CurrentState.EmpRec.SomeUser.AccessName;
                        }
                        else
                        {
                            usver.Pass = CurrentState.EmpRec.SomeUser.Pass;
                            usver.Login = CurrentState.EmpRec.SomeUser.Login;
                            usver.AccessName = CurrentState.EmpRec.SomeUser.AccessName;
                        }                       
                        
                    }
                    db.SaveChanges();
                    dialogService.ShowMessage("Виконано");
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

        
        private void SaveNewData()
        {
            EmployeeRecord tmpEmpRec;
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    Employee emp1 = db.Employees.Where(emp => emp.SomePerson.TaxCode == CurrentRecord.TaxCode).FirstOrDefault();
                    if (emp1 is null)
                    {

                    }
                    else
                    {
                        dialogService.ShowMessage("Вже є співробітник з такми ІНН. \nПеревірте вірність введених данних");
                        return;
                    }



                    db.SaveChanges();
                    dialogService.ShowMessage("Виконано");
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
        private bool CheckInputValidationData()
        {
            return TaxCode.Length == 10 ? true:false;
        }

       
        ////check and return EmployeeRecord if there is some employe with such phone number
        //private bool ChaeckExistingEmployeeByPhoneNumber(string phone, out EmployeeRecord  tmpRec)
        //{
        //    tmpRec = null;
        //    string sqlExpression = "sp_GetEmployeeByPhoneNumber";

        //    var connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
        //    var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

        //    using (SqlConnection connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            SqlCommand command = new SqlCommand(sqlExpression, connection);
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlParameter firstParam = new SqlParameter
        //            {
        //                ParameterName = "@PhoneNumber",
        //                Value = CurrentRecord.PhoneNumber
        //            };
        //            command.Parameters.Add(firstParam);

        //            SqlDataReader result = command.ExecuteReader();

        //            if (result.HasRows)
        //            {
                        
        //                while (result.Read())
        //                {
        //                    tmpRec = new EmployeeRecord 
        //                    { 
        //                        ContactsId = (int)result.GetValue(0),
        //                        PhoneNumber = (string)result.GetValue(1),
        //                        Adress = (string)result.GetValue(2),
                                
        //                        PersonId = (int)result.GetValue(3),
        //                        SecondName = (string)result.GetValue(4),
        //                        FirstName = (string)result.GetValue(5),
        //                        Patronimic = (string)result.GetValue(6),
        //                        Male = (bool)result.GetValue(7),
        //                        Female = !(bool)result.GetValue(7),
        //                        Photo = result.GetValue(8) is System.DBNull ? null : (byte[])result.GetValue(8),
                                
        //                        EmployeeId = (int)result.GetValue(9),
        //                        Description = (string)result.GetValue(10),
        //                        Salary = (decimal)result.GetValue(11)
        //                    };
        //                    if (!(result.GetValue(12) is System.DBNull))
        //                        tmpRec.FireDate = (DateTime)result.GetValue(12);
        //                    if (!(result.GetValue(13) is System.DBNull))
        //                        tmpRec.HireDate = (DateTime)result.GetValue(13);                            
        //                }


        //            }
        //            else
        //                return false;
                    
        //        }

        //        catch (ArgumentNullException ex)
        //        {
        //            dialogService.ShowMessage(ex.Message);
        //        }
        //        catch (OverflowException ex)
        //        {
        //            dialogService.ShowMessage(ex.Message);
        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            dialogService.ShowMessage(ex.Message);
        //        }
        //        catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
        //        {
        //            dialogService.ShowMessage(ex.Message);
        //        }
        //        catch (System.Data.Entity.Core.EntityException ex)
        //        {
        //            dialogService.ShowMessage(ex.Message);
        //        }

        //    }

        //    return false;
        //}
    }
}
