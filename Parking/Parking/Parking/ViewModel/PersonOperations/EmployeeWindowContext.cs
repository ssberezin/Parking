using EasyEncryption;
using Parking.Helpes;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parking.ViewModel.PersonOperations
{
   public class EmployeeWindowContext:Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;
        int UserId { get; set; }

        //these both for compare EmployeeWindow data state before some actions
        EmployeeRecord PreviousState { get; set; }
        EmployeeRecord CurrentState { get; set; }

        EmployeeState EmpState;
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
                    OnPropertyChanged2(nameof(SelectetRecord));
                }
            }
        }

        //for have an opportunity of choice betwin save new data and edittinig current data
        private bool toSave;
        public bool ToSave
        {
            get { return toSave; }
            set
            {
                if (value != toSave)
                {
                    toSave = value;
                    OnPropertyChanged5(nameof(ToSave));
                }
            }
        }

        //don't give user an opportunity to press the 'save' button
        private bool toSaveForBtn;
        public bool ToSaveForBtn
        {
            get { return toSaveForBtn; }
            set
            {
                if (value != toSaveForBtn)
                {
                    toSaveForBtn = value;
                    OnPropertyChanged(nameof(ToSaveForBtn));
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
                    OnPropertyChanged(nameof(CurrentPosition));
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
            EmployeePositions = new ObservableCollection<EmployeePosition>();
            EmpState = new EmployeeState();
            

            DefaultDataLoad();
            FillEmployeePositions();//filling by data collection of employee positions

            PropertyChanged2 += GetRecordDetales;       
            PropertyChanged5 += AddNewdata;
             
        }



        private void DefaultDataLoad()
        {
            lib = new Library();
            MindateRestriction = DateTime.Now.AddYears(-18);

            DefaultPhoto = "default_avatar.png";

            Statuses = new ObservableCollection<string> { "адмінісмтратор", "мастер-адмін", "без статусу" };

            CurrentRecord = new EmployeeRecord();

            PreviousState = EmpState.SetState(CurrentRecord);

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
                        
                        while (result.Read())
                        {
                            EmployeeRecord record = new EmployeeRecord();
                            record.SomeEmployee.EmployeeId = (int)result.GetValue(0);
                            record.SomeEmployee.Salary = (decimal)result.GetValue(1);
                            record.SomeEmployee.HireDate = (DateTime)result.GetValue(2);
                            if (!(result.GetValue(3) is System.DBNull))
                                record.SomeEmployee.FireDate = (DateTime)result.GetValue(3);
                            record.SomeEmployee.Description = result.GetValue(4) is DBNull? "":(string)result.GetValue(4);

                            record.SomePerson.PersonId = (int)result.GetValue(5);
                            record.SomePerson.SecondName = (string)result.GetValue(6);
                            record.SomePerson.FirstName = (string)result.GetValue(7);
                            record.SomePerson.Patronimic = (string)result.GetValue(8);
                            record.PYB = record.SomePerson.SecondName + " " + record.SomePerson.FirstName + " " + record.SomePerson.Patronimic;
                            record.SomePerson.Sex = (bool)result.GetValue(9);
                            
                            record.SomePerson.DayBirthday = (DateTime)result.GetValue(10);
                            if (!(result.GetValue(11) is System.DBNull))
                                record.SomePerson.Photo = (byte[])result.GetValue(11);
                            if (!(result.GetValue(21) is System.DBNull))
                                record.SomePerson.TaxCode = (long)result.GetValue(21);

                            record.SomeContacts.ContactsId = (int)result.GetValue(12);
                            record.SomeContacts.Phone = (string)result.GetValue(13);
                            record.SomeContacts.Adress = (string)result.GetValue(14);

                            record.SomeUser.AccessName = (string)result.GetValue(15);
                            record.SomeUser.UserId = (int)result.GetValue(20);
                            record.SomeUser.Login = result.GetValue(19) is null ? null: (string)result.GetValue(19);
                            //record.SomeUser.Pass = result.GetValue(18) is null ? null : (string)result.GetValue(18);

                            record.SomeEmpPosition.PositionName = (string)result.GetValue(16);
                            record.SomeEmpPosition.EmployeePositionId = (int)result.GetValue(17);


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
            ToSaveForBtn = true;//activating 'save' button 




            CurrentRecord = SelectetRecord is null ? new EmployeeRecord() : SelectetRecord;

            CurrentRecord.SomeEmployee.HireDate = SelectetRecord is null ? DateTime.Now :SelectetRecord.SomeEmployee.HireDate;

            CurrentRecord.Status = SelectetRecord is null ?"без статусу" :CurrentRecord.SomeUser.AccessName;

            

            CurrentPosition = SelectetRecord is null ?
                new EmployeePosition
                { EmployeePositionId = 1, PositionName = "адміністратор" } :
                new EmployeePosition
                {
                    EmployeePositionId = CurrentRecord.SomeEmpPosition.EmployeePositionId,
                    PositionName = CurrentRecord.SomeEmpPosition.PositionName
                };

         

            
            PreviousState = EmpState.SetState(SelectetRecord);

        }

        private void AddNewdata(object sender, PropertyChangedEventArgs e)
        {
            if (ToSave)
            {
                SelectetRecord = null;
                CurrentRecord = new EmployeeRecord();
               // SelectetRecord = SetTestdata();
               // TmpPhone = SelectetRecord.SomeContacts.Phone;
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
           CurrentRecord.SomePerson.Photo = File.ReadAllBytes(path);
        }

        private RelayCommand deletePersonPhotoCommand;
        public RelayCommand DeletePersonPhotoCommand => deletePersonPhotoCommand ?? (deletePersonPhotoCommand = new RelayCommand(
                    (obj) =>
                    {
                        if (CurrentRecord is null)
                            return;
                        CurrentRecord.SomePerson.Photo = null;
                    }
                    ));

        private RelayCommand savedataCommand;
        public RelayCommand SavedataCommand => savedataCommand ?? (savedataCommand = new RelayCommand(
                    (obj) =>
                    {
                        SaveData();
                    }
                    ));

        private void SaveData()
        {
            if (!CheckInputValidationData())
                return;

            CurrentRecord.SomeEmpPosition.EmployeePositionId = CurrentPosition.EmployeePositionId;
            CurrentRecord.SomeEmpPosition.PositionName = CurrentPosition.PositionName;

            CurrentState = EmpState.SetState(CurrentRecord);

            if (ToSave)
            {
                SaveNewData();
                DefaultDataLoad();//update EployeeRecords
                                  //ToSave = !ToSave;//diable adding new data mode
                                  //SelectetRecord = null;
            }
            else
                Editdata();//editing of current data

            PreviousState = EmpState.SetState(CurrentRecord);
        }

        private void Editdata()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    if (!EmpState.PesoneCompare (PreviousState, CurrentState))
                    {
                        Person ownerPerson = db.Persons.Find(CurrentRecord.SomePerson.PersonId);
                        db.Entry(ownerPerson).State = EntityState.Modified;


                      
                        if (db.Persons.Where(p => p.TaxCode == CurrentRecord.SomePerson.TaxCode && p.PersonId != CurrentRecord.SomePerson.PersonId).FirstOrDefault() is null)
                            ownerPerson.TaxCode = CurrentRecord.SomePerson.TaxCode;
                        else
                        {
                            dialogService.ShowMessage("Поточний ІНН вже є в базі данних. \n І він закріплений за іншою особою.\n Відкорегуйте данні");
                            return;
                        };
                        ownerPerson.FirstName = CurrentRecord.SomePerson.FirstName;
                        ownerPerson.SecondName = CurrentRecord.SomePerson.SecondName;
                        ownerPerson.Patronimic = CurrentRecord.SomePerson.Patronimic;
                        ownerPerson.Sex = CurrentRecord.SomePerson.Sex;
                        ownerPerson.DayBirthday = CurrentRecord.SomePerson.DayBirthday;
                        ownerPerson.Photo = CurrentRecord.SomePerson.Photo;

                        CurrentRecord.PYB = ownerPerson.SecondName + " " + ownerPerson.FirstName + " " + ownerPerson.Patronimic;
                    }

                    if (!EmpState.ContactsCompare(PreviousState, CurrentState))
                    {
                        Contacts ownerCTN = db.Contacts.Find(CurrentRecord.SomeContacts.ContactsId);
                        db.Entry(ownerCTN).State = EntityState.Modified;

                        if (!(db.Contacts.Where(p => p.Phone == CurrentRecord.SomeContacts.Phone && p.SomePerson.PersonId != CurrentRecord.SomePerson.PersonId).FirstOrDefault() is null))
                            ownerCTN.Phone = CurrentRecord.SomeContacts.Phone;
                        else
                        {
                            dialogService.ShowMessage("Поточний Номер телефона вже є в базі данних. \n І він закріплений за іншою особою.\n Відкорегуйте данні");
                            return;
                        };
                        ownerCTN.Adress = CurrentRecord.SomeContacts.Adress;                       
                        
                    }
                    
                    if (!EmpState.EmployeePositionCompare(PreviousState, CurrentState) ||
                        !EmpState.EmployeeCompare(PreviousState, CurrentState))
                    {

                        Employee emp1 = db.Employees.Find(CurrentRecord.SomeEmployee.EmployeeId);
                        db.Entry(emp1).State = EntityState.Modified;
                        emp1.EmployeePositions.Remove(db.EmployeePositions.Find(PreviousState.SomeEmpPosition.EmployeePositionId));
                        emp1.EmployeePositions.Add(db.EmployeePositions.Find(CurrentRecord.SomeEmpPosition.EmployeePositionId));
                        emp1.Salary = CurrentRecord.SomeEmployee.Salary;
                        emp1.FireDate = CurrentRecord.SomeEmployee.FireDate;
                        emp1.HireDate = CurrentRecord.SomeEmployee.HireDate;
                        emp1.Description = CurrentRecord.SomeEmployee.Description;                        
                    }

                    if (!(EmpState.StatusCompare(PreviousState, CurrentState)))
                    {
                        User usver = db.Users.Find(CurrentRecord.SomeUser.UserId);
                          db.Entry(usver).State = EntityState.Modified;
                        if (db.Users.Where(u => u.AccessName == "мастер-адмін").Count() < 2 && PreviousState.SomeUser.AccessName=="мастер-адмін" && CurrentRecord.SomeUser.AccessName!="мастер-адмін")
                        {
                            dialogService.ShowMessage("В системі має бути хочаб один користувач зі статусом мастер-адмін");
                            return;
                        }
                            ;
                        if (CurrentState.SomeUser.AccessName != PreviousState.SomeUser.AccessName &&
                            CurrentState.SomeUser.AccessName == "без статусу")
                        {
                            usver.Pass = null;
                            usver.Login = null;
                            usver.AccessName = CurrentState.SomeUser.AccessName;
                        }
                        else
                        {
                            User tmUser = db.Users.Where(p => p.Login == CurrentRecord.SomeUser.Login && p.SomeEmployee.SomePerson.PersonId != CurrentRecord.SomePerson.PersonId).FirstOrDefault();
                            if (tmUser is null)
                                usver.Login = CurrentState.SomeUser.Login;
                            else
                            {
                                dialogService.ShowMessage("Поточний логін вже зайнятий. \n Відкорегуйте данні");
                                return;
                            };

                            if (CurrentState.SomeUser.Pass != null)
                            {
                                //if passwords are not equal 
                                if (!(SHA.Equals(SHA.ComputeSHA256Hash(CurrentState.SomeUser.Pass), usver.Pass)))
                                {
                                    //....have a new password
                                    usver.Pass = SHA.ComputeSHA256Hash(CurrentState.SomeUser.Pass);
                                    CurrentState.SomeUser.Pass = null;
                                }
                            }
                            usver.AccessName = CurrentState.SomeUser.AccessName;
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
                  Person  pers1 = db.Persons.Where
                               (p => p.PersonId ==
                               (db.Contacts.Where(c => c.ContactsId ==
                                db.Contacts.Where(ct => ct.Phone == CurrentRecord.SomeContacts.Phone).FirstOrDefault().ContactsId).FirstOrDefault().SomePerson.PersonId)).FirstOrDefault();
                  Client cl=null;


                    if (pers1 != null)
                    {
                        cl = db.Clients.Where(c=>c.PersonCustomer.PersonId==pers1.PersonId).FirstOrDefault();
                        if (!EmpState.PesoneCompare(CurrentState, PreviousState))
                        {
                            if (dialogService.YesNoDialog("В базі данних цей телефон вже закріплений за \n" +
                                pers1.SecondName + " " + pers1.FirstName + " " + pers1.Patronimic + "\n" +
                                "Це та ж сама особа ?"))
                            {
                                Employee emp = db.Employees.Where(em => em.SomePerson.TaxCode == CurrentRecord.SomePerson.TaxCode).FirstOrDefault();
                                if (emp != null)
                                {                                    
                                    dialogService.ShowMessage("Ця особа вже э співробітником. Обіймання двох посад не припустиме.");
                                    return;

                                    // and if here we add the ability to add several positions to one employee
                                    //we need to compare other personal data with the previous ones                            
                                }

                            }
                            else
                            {
                                dialogService.ShowMessage(" В базі данних не може бути 2 однакових номера телефону. \n Відкорегуйте поточний номер телефона .");
                                return;
                            };
                        }
                        else
                        if (dialogService.YesNoDialog("В базі данних цей телефон вже закріплений за \n" +
                            pers1.SecondName + " " + pers1.FirstName + " " + pers1.Patronimic))
                        {
                        }

                    }
                    else
                    {
                        cl = new Client();
                        db.Clients.Add(cl);

                        pers1 = new Person
                        {
                            SecondName = CurrentRecord.SomePerson.SecondName,
                            FirstName = CurrentRecord.SomePerson.FirstName,
                            Patronimic = CurrentRecord.SomePerson.Patronimic,
                            Sex = CurrentRecord.SomePerson.Sex,
                            DayBirthday = CurrentRecord.SomePerson.DayBirthday,
                            Photo = CurrentRecord.SomePerson.Photo,
                            TaxCode = CurrentRecord.SomePerson.TaxCode
                        };
                        pers1.Clients.Add(cl);

                        Contacts ctn = new Contacts
                        {
                            Phone = CurrentRecord.SomeContacts.Phone,
                            Adress = CurrentRecord.SomeContacts.Adress
                        };
                        db.Contacts.Add(ctn);
                        pers1.ContactsData.Add(ctn);
                        pers1.Clients.Add(cl);

                        db.Persons.Add(pers1);
                    }

                    Employee emp1 = new Employee
                    {
                        Salary = CurrentRecord.SomeEmployee.Salary,
                        Description = CurrentRecord.SomeEmployee.Description,
                        FireDate = CurrentRecord.SomeEmployee.FireDate,
                        HireDate = CurrentRecord.SomeEmployee.HireDate
                    };

                    User usver = new User
                    {
                        Login = CurrentRecord.SomeUser.Login,
                        Pass = CurrentRecord.SomeUser.Pass,
                        AccessName = CurrentRecord.SomeUser.AccessName
                    };

                    User tmpUser = db.Users.Where(u => u.Login == usver.Login).FirstOrDefault();
                    if (!(tmpUser is null))
                    {
                        dialogService.ShowMessage("Вже э користувач з такми логіном.\n\t Выдкорегуйте.");
                        return;
                    }


                    db.Users.Add(usver);

                   
                    emp1.SomePerson = pers1;
                    emp1.Users.Add(usver);
                    emp1.OwnerCompany = db.OwnerCompany.Find(1);

                    EmployeePosition empPos = db.EmployeePositions.Find(CurrentPosition.EmployeePositionId);
                    emp1.EmployeePositions.Add(empPos);
                    
                    db.Entry(empPos).State = EntityState.Modified;

                    empPos.Employees.Add(emp1);

                    db.Employees.Add(emp1);

                    db.SaveChanges();
                   
                    ToSave = !ToSave;//diable adding new data mode                    

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

            if (!PersdataValidation(CurrentRecord.SomePerson))
            {
                dialogService.ShowMessage("Поля особистих данних не мають бути пустими");
                return false;
            }
            if (CurrentRecord.SomePerson.DayBirthday is null)
            {
                dialogService.ShowMessage("Не задана дата народження");
                return false;
            }
            if (CurrentRecord.SomePerson.TaxCode.ToString().Length != 10)
            {
                dialogService.ShowMessage("ІНН заданий не корректно");
                return false;
            }

            CurrentRecord.SomeContacts.Phone = lib.PhoneNumberValidation(CurrentRecord.SomeContacts.Phone);
            if (CurrentRecord.SomeContacts.Phone is null || CurrentRecord.SomeContacts.Phone.Length != 13 )
            {
                dialogService.ShowMessage("Номер телефона НЕ заданий або заданий не корректно");
                return false;
            }
            if (!ContactsValidation(CurrentRecord.SomeContacts))
            {
                dialogService.ShowMessage("Поля контактних данних не мають бути пустими");
                return false;
            }
            if (!EmployeeDataValidation(CurrentRecord.SomeEmployee))
            {
                dialogService.ShowMessage("Не задана дата найму або вказана не корректна сумма зарплатні");
                return false;

            }

            if (!UserdataValidationCheck(CurrentRecord.SomeUser))
            {
                dialogService.ShowMessage("Якщо користувач маэ статус , то поля\n пароля і логіна мають бути заповненими\nТакож " +
                    "користувач без статусу НЕ може мати ані логіна ані пароля");
                return false;
            }
            
            return true;
            
        }

        private bool PersdataValidation(Person pers)
        {
            return !(pers.SecondName is null || pers.SecondName == "" || pers.FirstName is null || pers.FirstName == "" ||
                pers.Patronimic is null || pers.Patronimic == "" || pers.DayBirthday is null );                   

        }

        private bool ContactsValidation(Contacts ctn)
        {
            return !(ctn.Phone is null || ctn.Phone == "" || ctn.Adress is null || ctn.Adress == "");

        }

        private bool EmployeeDataValidation(Employee emp)
        {
            return !(emp.HireDate is null || emp.Salary<=0  );
        }

        private bool UserdataValidationCheck( User usver)
        {
            if (usver.AccessName == "без статусу")
            {
                if (usver.Login != null)
                    return false;
            }
            else
                if (usver.Login is null || usver.Login == "")
                return false;
            return true;
        }


        private EmployeeRecord SetTestdata()
        {

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    return new EmployeeRecord
                    {
                        SomePerson = new Person
                        {
                            SecondName = "Федор",
                            FirstName = "Павлов",
                            Patronimic = "Груздэв",
                            Sex = true,
                            TaxCode = 3087360225,
                            DayBirthday = new DateTime(1985, 12, 11)
                        },
                        SomeContacts = new Contacts
                        {
                            Phone = "+380505064658",
                            Adress = "м.Київ, вул. м.Коцюбинського б.36, кв.21"
                        },
                        SomeEmployee = new Employee
                        {
                            Salary = 20000,
                            HireDate = DateTime.Now,
                           
                        },
                        SomeUser = new User
                        {
                            Login = "pavlusha",
                            Pass = "123456",
                            AccessName = "адміністратор"
                            
                        },
                        SomeEmpPosition = new EmployeePosition 
                        {
                            EmployeePositionId = 1,
                            PositionName = "адміністратор"
                        }
                       
                    };

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

        private RelayCommand closeWinCommand;
        public RelayCommand CloseWinCommand => closeWinCommand ?? (closeWinCommand = new RelayCommand(
                    (obj) =>
                    {
                      

                        CurrentState = EmpState.SetState(CurrentRecord);
                        if (EmpState.TotalCompare(CurrentState, PreviousState))
                            showWindow.CloseWindow(obj as Window);
                        else
                        {
                            if (dialogService.YesNoDialog("Зміни не було збережено. Зберегти?"))
                            {
                                SaveData();
                                showWindow.CloseWindow(obj as Window);
                            }
                            else
                                showWindow.CloseWindow(obj as Window);

                        };
                    }
                    ));


    }
}
