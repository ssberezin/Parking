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
                    OnPropertyChanged4(nameof(ToSave));
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
                    OnPropertyChanged(nameof(TaxCode));
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
            PropertyChanged3 += TaxCodeChange;
            PropertyChanged4 += AddNewdata;
        }



        private void DefaultDataLoad()
        {
            lib = new Library();
            MindateRestriction = DateTime.Now.AddYears(-18);

            DefaultPhoto = "default_avatar.png";

            Statuses = new ObservableCollection<string> { "адмінісмтратор", "мастер-адмін", "без статусу" };

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
                            record.SomeEmployee.EmployeeId = (int)result.GetValue(0);
                            record.SomeEmployee.Salary = (decimal)result.GetValue(1);
                            record.SomeEmployee.HireDate = (DateTime)result.GetValue(2);
                            if (!(result.GetValue(3) is System.DBNull))
                                record.SomeEmployee.FireDate = (DateTime)result.GetValue(3);
                            record.SomeEmployee.Description = (string)result.GetValue(4);

                            record.SomePerson.PersonId = (int)result.GetValue(5);
                            record.SomePerson.SecondName = (string)result.GetValue(6);
                            record.SomePerson.FirstName = (string)result.GetValue(7);
                            record.SomePerson.Patronimic = (string)result.GetValue(8);
                            record.PYB = record.SomePerson.SecondName + " " + record.SomePerson.FirstName + " " + record.SomePerson.Patronimic;
                            record.Male = (bool)result.GetValue(9);
                            record.Female = !record.Male;
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
                            record.SomeUser.Login = (string)result.GetValue(19);
                            record.SomeUser.Pass = (string)result.GetValue(18);

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
            CurrentRecord = SelectetRecord;
            CurrentRecord.Status = CurrentRecord.SomeUser.AccessName;
            CurrentPosition = new EmployeePosition { EmployeePositionId = CurrentRecord.SomeEmpPosition.EmployeePositionId,
                                                     PositionName = CurrentRecord.SomeEmpPosition.PositionName};

            TaxCode = CurrentRecord.SomePerson.TaxCode.Value.ToString();

            
            PreviousState = EmpState.SetState(SelectetRecord);

        }

        private void TaxCodeChange(object sender, PropertyChangedEventArgs e)
        {
            TaxCode = lib.TaxCodeValidation(TaxCode);            
        }

        private void AddNewdata(object sender, PropertyChangedEventArgs e)
        {
            SelectetRecord = null;
            CurrentRecord = new EmployeeRecord();
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
                        CurrentRecord.SomePerson.Photo = null;
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

                        CurrentRecord.SomeEmpPosition.EmployeePositionId = CurrentPosition.EmployeePositionId;
                        CurrentRecord.SomeEmpPosition.PositionName = CurrentPosition.PositionName;
                        
                        if (long.TryParse(TaxCode, out long tmp))
                            CurrentRecord.SomePerson.TaxCode = tmp;
                        CurrentState = EmpState.SetState(CurrentRecord);

                        if (!ToSave)
                            SaveNewData();
                        else
                            Editdata();

                        PreviousState = EmpState.SetState(CurrentRecord);
                    }
                    ));
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

                        if (!(db.Persons.Where(p => p.TaxCode == CurrentRecord.SomePerson.TaxCode && p.PersonId != CurrentRecord.SomePerson.PersonId).FirstOrDefault() is null))
                            ownerPerson.TaxCode = CurrentRecord.SomePerson.TaxCode;
                        else
                        {
                            dialogService.ShowMessage("Поточний ІНН вже є в базі данних. \n І він закріплений за іншою особою.\n Відкорегуйте данні");
                            return;
                        };
                        ownerPerson.FirstName = CurrentRecord.SomePerson.FirstName;
                        ownerPerson.SecondName = CurrentRecord.SomePerson.SecondName;
                        ownerPerson.Patronimic = CurrentRecord.SomePerson.Patronimic;
                        ownerPerson.Sex = CurrentRecord.Male? CurrentRecord.Male: CurrentRecord.Female;
                        ownerPerson.DayBirthday = CurrentRecord.SomePerson.DayBirthday;
                        ownerPerson.Photo = CurrentRecord.SomePerson.Photo;
                        

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
                        if (CurrentState.SomeUser.AccessName != PreviousState.SomeUser.AccessName &&
                            CurrentState.SomeUser.AccessName == "без статусу")
                        {
                            usver.Pass = null;
                            usver.Login = null;
                            usver.AccessName = CurrentState.SomeUser.AccessName;
                        }
                        else
                        {
                            if (!(db.Users.Where(p => p.Login == CurrentRecord.SomeUser.Login && p.SomeEmployee.SomePerson.PersonId != CurrentRecord.SomePerson.PersonId).FirstOrDefault() is null))
                                usver.Login = CurrentState.SomeUser.Login;
                            else
                            {
                                dialogService.ShowMessage("Поточний логін вже зайнятий. \n Відкорегуйте данні");
                                return;
                            };
                            usver.Pass = CurrentState.SomeUser.Pass;                        
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
                            Sex = CurrentRecord.Male,
                            DayBirthday = CurrentRecord.SomePerson.DayBirthday,
                            Photo = CurrentRecord.SomePerson.Photo
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
                    db.Users.Add(usver);

                   
                    emp1.SomePerson = pers1;
                    emp1.Users.Add(usver);
                    emp1.OwnerCompany = db.OwnerCompany.Find(1);
                    emp1.EmployeePositions.Add(db.EmployeePositions.Find(CurrentPosition.EmployeePositionId));
                    db.Employees.Add(emp1);

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

       
        
    }
}
