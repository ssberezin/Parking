using Parking.Helpes;
using Parking.Model;
using Parking.Views.PrintOps;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parking.ViewModel.ParkPlacesOps
{

    public class ParkPlaceWindowContext : Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;

        public ObservableCollection<int> FreeParkingPlacesList { get; set; }
        public ObservableCollection<VehicleType> VehicleTypeList { get; set; }

        public ObservableCollection<VehicleColor> VehicleColors { get; set; }


        public ObservableCollection<ParkPlaceHisrtoryRecord> ParkPlaceHisrtoryRecords { get; set; }

        private ParkingPlaceRecord currentRecord;
        public ParkingPlaceRecord CurrentRecord
        {
            get { return currentRecord; }
            set
            {
                if (currentRecord != value)
                {
                    currentRecord = value;
                    OnPropertyChanged(nameof(CurrentRecord));
                }
            }
        }
        private VehicleColor selectedColor;
        public VehicleColor SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }


        private bool parkingPlaceStatus;
        public bool ParkingPlaceStatus
        {
            get { return parkingPlaceStatus; }
            set
            {
                if (parkingPlaceStatus != value)
                {
                    parkingPlaceStatus = value;
                    OnPropertyChanged(nameof(ParkingPlaceStatus));
                }
            }
        }

        private bool notInPlace;
        public bool NotInPlace
        {
            get { return notInPlace; }
            set
            {
                if (notInPlace != value)
                {
                    notInPlace = value;
                    OnPropertyChanged(nameof(NotInPlace));
                }
            }
        }



        private bool notFree;
        public bool NotFree
        {
            get { return notFree; }
            set
            {
                if (notFree != value)
                {
                    notFree = value;
                    OnPropertyChanged(nameof(NotFree));
                }
            }
        }

        private string newVType;
        public string NewVType
        {
            get { return newVType; }
            set
            {
                if (newVType != value)
                {
                    newVType = value;
                    OnPropertyChanged(nameof(NewVType));
                }
            }
        }

        private VehicleType vType;
        public VehicleType VType
        {
            get { return vType; }
            set
            {
                if (vType != value)
                {
                    vType = value;
                    OnPropertyChanged(nameof(VType));
                }
            }
        }
        private int freeparkPlace;
        public int FreeparkPlace
        {
            get { return freeparkPlace; }
            set
            {
                if (freeparkPlace != value)
                {
                    freeparkPlace = value;
                    OnPropertyChanged(nameof(FreeparkPlace));
                }
            }
        }

        private string prolonDaysCount;
        public string ProlonDaysCount
        {
            get { return prolonDaysCount; }
            set
            {
                if (prolonDaysCount != value)
                {
                    prolonDaysCount = value;
                    OnPropertyChanged(nameof(ProlonDaysCount));
                }
            }
        }

        private DateTime nextDeadLine;
        public DateTime NextDeadLine
        {
            get { return nextDeadLine; }
            set
            {
                if (nextDeadLine != value)
                {
                    nextDeadLine = value;
                    OnPropertyChanged(nameof(NextDeadLine));
                }
            }
        }

        private DateTime prolongDate;
        public DateTime ProlongDate
        {
            get { return prolongDate; }
            set
            {
                if (prolongDate != value)
                {
                    prolongDate = value;
                    OnPropertyChanged2(nameof(ProlongDate));
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
                    OnPropertyChanged7(nameof(StartHistoryDate));
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
                    OnPropertyChanged7(nameof(EndHistoryDate));
                }
            }
        }

        private string coast;
        public string Coast
        {
            get { return coast; }
            set
            {
                if (coast != value)
                {
                    coast = value;
                    OnPropertyChanged6(nameof(Coast));
                }
            }
        }

        private string ownerPhone1;
        public string OwnerPhone1
        {
            get { return ownerPhone1; }
            set
            {
                if (ownerPhone1 != value)
                {
                    ownerPhone1 = value;
                    OnPropertyChanged3(nameof(OwnerPhone1));
                }
            }
        }

        private string trustPhone;
        public string TrustPhone
        {
            get { return trustPhone; }
            set
            {
                if (trustPhone != value)
                {
                    trustPhone = value;
                    OnPropertyChanged4(nameof(TrustPhone));
                }
            }
        }

        private string regNumber;
        public string RegNumber
        {
            get { return regNumber; }
            set
            {
                if (regNumber != value)
                {
                    regNumber = value;
                    OnPropertyChanged5(nameof(RegNumber));
                }
            }
        }




        private string messageForChangeParkPlace;
        public string MessageForChangeParkPlace
        {
            get { return messageForChangeParkPlace; }
            set
            {
                if (messageForChangeParkPlace != value)
                {
                    messageForChangeParkPlace = value;
                    OnPropertyChanged(nameof(MessageForChangeParkPlace));
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

        private bool newDataAddedSaved;
        public bool NewDataAddedSaved //its marker for block an opportunity of parking place number change
        {
            get { return newDataAddedSaved; }
            set
            {
                if (newDataAddedSaved != value)
                {
                    newDataAddedSaved = value;
                    OnPropertyChanged(nameof(NewDataAddedSaved));
                }
            }
        }

        private string lastPayDateMessage;
        public string LastPayDateMessage
        {
            get { return lastPayDateMessage; }
            set
            {
                if (lastPayDateMessage != value)
                {
                    lastPayDateMessage = value;
                    OnPropertyChanged(nameof(LastPayDateMessage));
                }
            }
        }


        private DateTime? LastPayDate { get; set; }
        private decimal LastPaying { get; set; }


        CompareStatesForParkingPlace PreviousState { get; set; }
        CompareStatesForParkingPlace CurrentState { get; set; }

        Library lib;//for using some methodes 


        public int UserId { get; set; }

        public ParkPlaceWindowContext()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
        }

        //For editing
        public ParkPlaceWindowContext(int userId, ParkingPlaceRecord parkRecord)
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
            DefaultDataLoad(userId, parkRecord);
        }

        private void DefaultDataLoad(int userId, ParkingPlaceRecord parkRecord)
        {
            lib = new Library();
            UserId = userId;
            CurrentRecord = parkRecord;
            saved = true;//set editting mode
            //set default value of radio batton 'Released'
            if (CurrentRecord.SomePerson.PersonId == 0)
                if (CurrentRecord.SomePerson.PersonId == 0)
                {
                    CurrentRecord.SomeParkingPlace.Released = !parkRecord.SomeParkingPlace.Released;

                }
            //if there is some client by this parking place we have an opportunity to change parking place number
            NewDataAddedSaved = CurrentRecord.SomeClient.ClientId == 0 ? false : true;
            CurrentRecord.SomeClient.OrgName = CurrentRecord.SomeClient.OrgName == null ? "фізична особа" : CurrentRecord.SomeClient.OrgName;
            NextDeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            SelectedColor = CurrentRecord.VehColor;
            OwnerPhone1 = CurrentRecord.SomeContacts.Phone;
            TrustPhone = CurrentRecord.TrContacts.Phone;
            RegNumber = CurrentRecord.SomeVehicle.RegNumber;
            VType = CurrentRecord.SomeVehicleType;
            NewVType = VType.TypeName;

            if (CurrentRecord.SomeParkingPlace.FreeStatus.Value)
            {
                CurrentRecord.SomeParkingPlace.FreeStatus = false;//by dafault we have 'not free status' when we want to rent a parking place
            }
            NotFree = !CurrentRecord.SomeParkingPlace.FreeStatus.Value;

            NotInPlace = !CurrentRecord.SomeParkingPlace.Released;
            ProlongDate = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;

            Coast = "0";
            FreeParkingPlacesList = new ObservableCollection<int>();
            VehicleTypeList = new ObservableCollection<VehicleType>();
            ParkPlaceHisrtoryRecords = new ObservableCollection<ParkPlaceHisrtoryRecord>();
            VehicleColors = new ObservableCollection<VehicleColor>();
            StartHistoryDate = DateTime.Now.AddMonths(-1);
            EndHistoryDate = DateTime.Now;


            FillFreeParkingPlacesList();//for display parking places in combox 
            FillVehicleTypeList();
            FillVehicleColorsList();
            GetLatPayInfo(CurrentRecord.SomeVehicle.VehicleId);//set info about last paying
            if (parkRecord.SomeClient.ClientId != 0)
                FillHistoryList(parkRecord.SomeClient.ClientId, parkRecord.SomeParkingPlace.ParkPlaceNumber, StartHistoryDate, EndHistoryDate);



            DefaultPhoto = "default_vehicle_picture.png";
            PreviousState = SetState();//remember current data state for compare in future befor save

            PropertyChanged2 += ChangeProlongData;
            PropertyChanged3 += NumberValidationPhone1;
            PropertyChanged4 += NumberValidationPhone2;
            PropertyChanged5 += RegNumberValidation;
            PropertyChanged6 += CoastValidation;
            PropertyChanged7 += ChangeHistoryList;
        }


        private void ChangeHistoryList(object sender, PropertyChangedEventArgs e)
        {
            FillHistoryList(CurrentRecord.SomeClient.ClientId, CurrentRecord.SomeParkingPlace.ParkPlaceNumber, StartHistoryDate, EndHistoryDate);
        }

        private void ChangeProlongData(object sender, PropertyChangedEventArgs e)
        {
            int res = ProlongDate.Subtract(CurrentRecord.SomeParkingPlaceLog.DeadLine.Value).Days;
            if (res > 0)
                ProlonDaysCount = res.ToString() + " дн.";
        }


        private void NumberValidationPhone1(object sender, PropertyChangedEventArgs e)
        {
            OwnerPhone1 = lib.PhoneNumberValidation(OwnerPhone1);
        }

        private void NumberValidationPhone2(object sender, PropertyChangedEventArgs e)
        {
            TrustPhone = lib.PhoneNumberValidation(TrustPhone);
        }



        private static readonly Regex regexRegNumer = new Regex("[^0-9A-Z]+"); //regex that matches disallowed text
        private void RegNumberValidation(object sender, PropertyChangedEventArgs e)
        {
            if (RegNumber != "не задано")
            {
                if (!(RegNumber is null))
                    RegNumber = RegNumber.ToUpper();

                if (regexRegNumer.IsMatch(RegNumber) || RegNumber.Length > 8)
                {
                    RegNumber = RegNumber.Remove(RegNumber.Length - 1, 1);
                }
            }
        }

        private static readonly Regex regexCoast = new Regex("[^0-9,.]+"); //regex that matches disallowed text
        private void CoastValidation(object sender, PropertyChangedEventArgs e)
        {
            if (regexCoast.IsMatch(Coast))
            {
                Coast = Coast.Remove(Coast.Length - 1, 1);
            }

        }


        //uses in previous data loading

        private void FillVehicleTypeList()
        {

            VehicleTypeList.Clear();
            using (DBConteiner db = new DBConteiner())
            {
                try
                {

                    var VTlist = db.VehicleTypes.ToList();
                    if (!(VTlist is null))
                    {
                        foreach (var item in VTlist)
                            VehicleTypeList.Add(
                                new VehicleType
                                {
                                    VehicleTypeId = item.VehicleTypeId,
                                    TypeName = item.TypeName
                                });
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

        private void FillVehicleColorsList()
        {

            VehicleColors.Clear();
            using (DBConteiner db = new DBConteiner())
            {
                try
                {

                    var VClist = db.Colors.ToList();
                    if (!(VClist is null))
                    {
                        foreach (var item in VClist)
                            VehicleColors.Add(
                                new VehicleColor
                                {
                                    VehicleColorId = item.VehicleColorId,
                                    ColorName = item.ColorName
                                });

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

        private void FillFreeParkingPlacesList()
        {
            FreeParkingPlacesList.Clear();

            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    var result = db.ParkingPlaces.Where(p => p.FreeStatus == true).ToList();
                    if (result != null)
                    {
                        foreach (ParkingPlace item in result)
                            FreeParkingPlacesList.Add(item.ParkPlaceNumber);

                        FreeparkPlace = FreeParkingPlacesList[0];
                        MessageForChangeParkPlace = "Вільних місць " + FreeParkingPlacesList.Count();
                    }
                    else
                    {
                        FreeparkPlace = 0;
                        MessageForChangeParkPlace = "Немаэ вільних місць";
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

        private void FillHistoryList(int clientId, int ppnumber, DateTime startDate, DateTime endDate)
        {
            ParkPlaceHisrtoryRecords.Clear();

            string sqlExpression = "sp_GetPPHistory";

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

                    SqlParameter fourthParam = new SqlParameter
                    {
                        ParameterName = "@ppNumber",
                        Value = ppnumber
                    };

                    command.Parameters.Add(firstParam);
                    command.Parameters.Add(secondParam);
                    command.Parameters.Add(thirdParam);
                    command.Parameters.Add(fourthParam);

                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {

                        while (result.Read())
                        {
                            DateTime date = (DateTime)result.GetValue(1);
                            ParkPlaceHisrtoryRecord rec = new ParkPlaceHisrtoryRecord
                            {
                                PPNumber = (int)result.GetValue(0),
                                Released = (bool)result.GetValue(2)

                            };
                            rec.DateOfEvent = date.ToString("dd/MM/yyyy");
                            rec.TimeOfEvent = date.ToString("HH:mm:ss");
                            if (ParkPlaceHisrtoryRecords.Count() == 0)
                                ParkPlaceHisrtoryRecords.Add(rec);
                            else
                            {
                                if (!(ParkPlaceHisrtoryRecords[ParkPlaceHisrtoryRecords.Count() - 1].Released == rec.Released &&
                                      ParkPlaceHisrtoryRecords[ParkPlaceHisrtoryRecords.Count() - 1].DateOfEvent == rec.DateOfEvent))
                                    ParkPlaceHisrtoryRecords.Add(rec);
                            }

                        };
                    }
                    else
                    {

                        dialogService.ShowMessage("Щось пішло не так при зчитуванні данних про паркувальні місця.\n" +
                                                  "Можливо некоректно задано проміжок дати");
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
            CurrentRecord.SomeVehicle.VPhoto = File.ReadAllBytes(path);
        }

        private RelayCommand deleteVehiclePhotoCommand;
        public RelayCommand DeleteVehiclePhotoCommand => deleteVehiclePhotoCommand ?? (deleteVehiclePhotoCommand = new RelayCommand(
                    (obj) =>
                    {
                        CurrentRecord.SomeVehicle.VPhoto = null;
                    }
                    ));

        private CompareStatesForParkingPlace SetState()
        {
            CompareStatesForParkingPlace tmp = new CompareStatesForParkingPlace();


            //ParkingPlace's data

            tmp.ParkingPlaceFreeStatus = CurrentRecord.SomeParkingPlace.FreeStatus.Value;
            tmp.ReleasedPlace = CurrentRecord.SomeParkingPlace.Released;

            //Owner's data
            tmp.Owner = CurrentRecord.SomeClient.OrgName;
            tmp.OwnerSecondName = CurrentRecord.SomePerson.SecondName;
            tmp.OwnerFirstName = CurrentRecord.SomePerson.FirstName;
            tmp.OwnerPatronimic = CurrentRecord.SomePerson.Patronimic;
            tmp.OwnerSex = CurrentRecord.SomePerson.Sex;

            //Owner's contacts data
            tmp.OwnerPhone = CurrentRecord.SomeContacts.Phone;
            tmp.Adress = CurrentRecord.SomeContacts.Adress;

            //Trusted person's data           
            tmp.TrustSecondName = CurrentRecord.TrustedPerson.SecondName;
            tmp.TrustFirstName = CurrentRecord.TrustedPerson.FirstName;
            tmp.TrustPatronimic = CurrentRecord.TrustedPerson.Patronimic;
            tmp.TrustPersSex = CurrentRecord.TrustedPerson.Sex;


            //vehicle's data
            tmp.VPhoto = CurrentRecord.SomeVehicle.VPhoto is null ? null : lib.CopyPhoto(CurrentRecord.SomeVehicle.VPhoto);
            tmp.RegNumber = CurrentRecord.SomeVehicle.RegNumber;
            tmp.Color = CurrentRecord.VehColor.ColorName;
            tmp.VType = CurrentRecord.SomeVehicleType.TypeName;
            //GetVehicleColor()
            //trusted person's data
            tmp.TrustPhone = CurrentRecord.TrContacts.Phone;

            tmp.ProlongDate = ProlongDate;
            tmp.Coast = CurrentRecord.SomeParkingPlaceLog.Money;

            return tmp;
        }

        private RelayCommand savedataCommand;

        public RelayCommand SavedataCommand => savedataCommand ?? (savedataCommand = new RelayCommand(
                    (obj) =>
                    {

                        //check existing color
                        string color = (string)obj;
                        lib.CheckColor(color, out int colId);
                        CurrentRecord.VehColor = new VehicleColor { VehicleColorId = colId, ColorName = color };


                        if (VType is null)
                        {
                            lib.CheckVehicleType(NewVType, out int vTypeId);
                            CurrentRecord.SomeVehicleType = new VehicleType { VehicleTypeId = vTypeId, TypeName = NewVType };
                            VType = CurrentRecord.SomeVehicleType;
                        }



                        CurrentRecord.SomeVehicle.RegNumber = RegNumber;
                        CurrentRecord.SomeContacts.Phone = OwnerPhone1;
                        CurrentRecord.TrContacts.Phone = TrustPhone;
                        CurrentRecord.SomeParkingPlace.Released = !NotInPlace;
                        CurrentRecord.SomeParkingPlaceLog.DeadLine = ProlongDate;
                        CurrentRecord.VehColor = SelectedColor;




                        if (decimal.TryParse(Coast, out decimal tmp))
                            CurrentRecord.SomeParkingPlaceLog.Money = tmp;


                        CurrentState = SetState();
                        SaveData();

                    }
                    ));

        //if new colorname does not exist in DB we'll add it 


        bool saved { get; set; }// if false - we can save new data, if true - only edit

        private void SaveData()
        {
            if (!saved)
                AddnewData();
            else
                EditData();

        }


        private void AddnewData()
        {

            if (CurrentRecord.SomeParkingPlace.FreeStatus.Value && CurrentRecord.SomeParkingPlaceLog.DeadLine.Value < DateTime.Now)
            {
                dialogService.ShowMessage("Необхідно змінити статус паркомісця на \"зайнято\"");
                return;
            };


            CompareStatesForParkingPlace compare = new CompareStatesForParkingPlace();
            if (!ValidationInputData())
                return;




            //next we have to save data to DB
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    lib.GetPersonAndContactsIds(CurrentRecord.SomeContacts.Phone, CurrentRecord.SomePerson, out int? ctnId, out int? persId);

                    Client Cl;
                    Person OwnerPerson;
                    Contacts OwnerContacts;// = db.Contacts.Where(ctn => ctn.Phone == CurrentRecord.SomeContacts.Phone).FirstOrDefault();

                    if (ctnId is null)
                        return;

                    if (ctnId is null || ctnId == 0 && persId == 0)
                    {
                        OwnerPerson = CurrentRecord.SomePerson;
                        db.Persons.Add(OwnerPerson);

                        OwnerContacts = CurrentRecord.SomeContacts;
                        db.Contacts.Add(OwnerContacts);

                        OwnerPerson.ContactsData.Add(OwnerContacts);
                        OwnerPerson.ContactsData.Add(OwnerContacts);

                        Cl = new Client { OrgName = CurrentRecord.SomeClient.OrgName };
                        db.Clients.Add(Cl);

                        Cl.PersonCustomer = OwnerPerson;
                    }
                    else
                    {
                        OwnerContacts = db.Contacts.Find(ctnId);
                        OwnerPerson = db.Persons.Find(persId);
                        Cl = db.Clients.Where(cl => cl.PersonCustomer.PersonId == OwnerPerson.PersonId).FirstOrDefault();
                    }



                    lib.GetPersonAndContactsIds(CurrentRecord.TrContacts.Phone, CurrentRecord.TrustedPerson, out int? trCtnId, out int? trPersId);

                    if (trCtnId is null)
                        return;
                    Person TrustPerson;
                    Contacts TrustContacts;// = db.Contacts.Where(ctn => ctn.Phone == CurrentRecord.TrContacts.Phone).FirstOrDefault();
                    Client TrCl;
                    if (trCtnId is null || trCtnId == 0 && trPersId == 0)
                    {
                        TrustPerson = CurrentRecord.TrustedPerson;
                        db.Persons.Add(TrustPerson);

                        TrustContacts = CurrentRecord.TrContacts;
                        db.Contacts.Add(TrustContacts);

                        TrustPerson.ContactsData.Add(TrustContacts);

                        OwnerPerson.TrustedPerson = TrustPerson;
                        TrCl = new Client { OrgName = "не задано" };

                        db.Clients.Add(TrCl);

                        TrCl.PersonCustomer = TrustPerson;

                    }
                    else
                    {
                        TrustContacts = db.Contacts.Find(trCtnId);
                        TrustPerson = db.Persons.Find(trPersId);
                        TrCl = db.Clients.Where(cl => cl.PersonCustomer.PersonId == TrustPerson.PersonId).FirstOrDefault();
                        // db.Entry(editableVehicle).State = EntityState.Modified;

                        if (OwnerPerson.TrustedPerson != null && OwnerPerson.TrustedPerson.PersonId != 0)
                        {
                            db.Entry(OwnerPerson).State = EntityState.Modified;
                            OwnerPerson.TrustedPerson = TrustPerson;
                        }
                        else
                            OwnerPerson.TrustedPerson = TrustPerson;

                    }




                    Vehicle newVehicle = db.Vehicles.Where(veh => veh.RegNumber == CurrentRecord.SomeVehicle.RegNumber).FirstOrDefault();
                    if (newVehicle is null)
                    {
                        newVehicle = new Vehicle
                        {
                            RegNumber = CurrentRecord.SomeVehicle.RegNumber,
                            SomeVehicleType = db.VehicleTypes.Find(VType.VehicleTypeId),
                            VPhoto = CurrentRecord.SomeVehicle.VPhoto
                        };
                        newVehicle.SomeVehicleColor = db.Colors.Find(CurrentRecord.VehColor.VehicleColorId);
                        db.Vehicles.Add(newVehicle);
                        newVehicle.ClientOwner = Cl;
                    }
                    else
                    {
                        ParkingPlace pp = lib.GetPPByVehNumber(newVehicle.RegNumber);
                        if (pp != null)
                        {
                            dialogService.ShowMessage("Цей транспортний зазіб вже стоїть " +
                                "на паркувальному місці \"" + pp.ParkPlaceNumber + "\".\n" +
                                "Відкорегуйте реэстраційний номер ТЗ ");
                            return;
                        };
                    }



                    User user = db.Users.Find(UserId);
                    db.Entry(user).State = EntityState.Modified;


                    ParkingPlaceLog parkingPlaceLog = new ParkingPlaceLog();
                    parkingPlaceLog.DeadLine = new DateTime(ProlongDate.Year, ProlongDate.Month, ProlongDate.Day);
                    parkingPlaceLog.Money = CurrentRecord.SomeParkingPlaceLog.Money;

                    parkingPlaceLog.PayingDate = DateTime.Now;
                    parkingPlaceLog.DateOfChange = DateTime.Now;
                    db.ParkingPlaceLogs.Add(parkingPlaceLog);

                    user.ParkingPlaceLogs.Add(parkingPlaceLog);


                    ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);

                    parkingPlaceLog.SomeParkingPlace = parkingPlace;

                    db.Entry(parkingPlace).State = EntityState.Modified;
                    parkingPlace.FreeStatus = false;
                    parkingPlace.Released = CurrentRecord.SomeParkingPlace.Released;
                    parkingPlace.Vehicles.Clear();
                    parkingPlace.Vehicles.Add(newVehicle);


                    db.SaveChanges();

                    saved = true;

                    NextDeadLine = ProlongDate;

                    NewDataAddedSaved = true;//allow an opportunity of changing parking plase number                    

                    PreviousState = SetState();

                    FillFreeParkingPlacesList();
                    FillHistoryList(Cl.ClientId, CurrentRecord.SomeParkingPlace.ParkPlaceNumber, StartHistoryDate, EndHistoryDate);
                    
                    GetLatPayInfo(CurrentRecord.SomeParkingPlaceLog.PayingDate, CurrentRecord.SomeParkingPlaceLog.Money);

                    dialogService.ShowMessage("Ok");
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
        private void EditData()
        {

            CompareStatesForParkingPlace compare = new CompareStatesForParkingPlace();
            if (!ValidationInputData())
                return;


            //next we have to edit data in DB
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    if (!compare.ParkingPalceDataCompare(PreviousState, CurrentState))
                    {
                        ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);
                        db.Entry(parkingPlace).State = EntityState.Modified;
                        parkingPlace.FreeStatus = CurrentRecord.SomeParkingPlace.FreeStatus;
                        parkingPlace.Released = CurrentRecord.SomeParkingPlace.Released;

                        User user = db.Users.Find(UserId);
                        db.Entry(user).State = EntityState.Modified;

                        ParkingPlaceLog parkingPlaceLog = new ParkingPlaceLog
                        {
                            DateOfChange = DateTime.Now,
                            FreeStatus = parkingPlace.FreeStatus.Value,
                            Released = parkingPlace.Released,
                            BookingDate = CurrentRecord.SomeParkingPlaceLog.BookingDate,
                            DeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine
                        };

                        if (CurrentRecord.SomeParkingPlaceLog.Money != 0)
                            parkingPlaceLog.PayingDate = DateTime.Now;

                        db.ParkingPlaceLogs.Add(parkingPlaceLog);

                        user.ParkingPlaceLogs.Add(parkingPlaceLog);
                        parkingPlace.ParkingPlaceLogs.Add(parkingPlaceLog);

                    }

                    if (!compare.OwnerPersonDataCompare(PreviousState, CurrentState))
                    {
                        Person ownerPerson = db.Persons.Find(CurrentRecord.SomePerson.PersonId);
                        db.Entry(ownerPerson).State = EntityState.Modified;
                        ownerPerson.FirstName = CurrentRecord.SomePerson.FirstName;
                        ownerPerson.SecondName = CurrentRecord.SomePerson.SecondName;
                        ownerPerson.Patronimic = CurrentRecord.SomePerson.Patronimic;
                        ownerPerson.Sex = CurrentRecord.SomePerson.Sex;
                    }

                    if (!compare.TrustPersonDataCompare(PreviousState, CurrentState))
                    {
                        Person trustPerson = db.Persons.Find(CurrentRecord.TrustedPerson.PersonId);
                        db.Entry(trustPerson).State = EntityState.Modified;
                        trustPerson.FirstName = CurrentRecord.TrustedPerson.FirstName;
                        trustPerson.SecondName = CurrentRecord.TrustedPerson.SecondName;
                        trustPerson.Patronimic = CurrentRecord.TrustedPerson.Patronimic;
                        trustPerson.Sex = CurrentRecord.TrustedPerson.Sex;
                    }

                    if (!compare.OwnerContactsDataCompare(PreviousState, CurrentState))
                    {
                        Contacts ownerCTN = db.Contacts.Find(CurrentRecord.SomeContacts.ContactsId);
                        db.Entry(ownerCTN).State = EntityState.Modified;
                        ownerCTN.Adress = CurrentRecord.SomeContacts.Adress;
                        ownerCTN.Phone = CurrentRecord.SomeContacts.Phone;
                    }

                    if (!compare.TrustContactsDataCompare(PreviousState, CurrentState))
                    {
                        Contacts trustCTN = db.Contacts.Find(CurrentRecord.TrContacts.ContactsId);
                        db.Entry(trustCTN).State = EntityState.Modified;
                        trustCTN.Phone = CurrentRecord.TrContacts.Phone;
                    }

                    if (!compare.ClientDataCompare(PreviousState, CurrentState))
                    {
                        Client editableClient = db.Clients.Find(CurrentRecord.SomeClient.ClientId);
                        db.Entry(editableClient).State = EntityState.Modified;
                        editableClient.OrgName = CurrentRecord.SomeClient.OrgName;
                    }

                    if (!compare.VehicleDataCompare(PreviousState, CurrentState))
                    {
                        Vehicle editableVehicle = db.Vehicles.Find(CurrentRecord.SomeVehicle.VehicleId);
                        db.Entry(editableVehicle).State = EntityState.Modified;
                        editableVehicle.RegNumber = CurrentRecord.SomeVehicle.RegNumber;
                        editableVehicle.VPhoto = CurrentRecord.SomeVehicle.VPhoto;

                    }
                    if (!compare.VehicleColoCompare(PreviousState, CurrentState))
                    {
                        Vehicle editableVehicle = db.Vehicles.Find(CurrentRecord.SomeVehicle.VehicleId);
                        db.Entry(editableVehicle).State = EntityState.Modified;
                        editableVehicle.SomeVehicleColor = db.Colors.Find(CurrentRecord.VehColor.VehicleColorId);
                    }

                    if (PreviousState.VType != VType.TypeName)
                    {
                        Vehicle editableVehicle = db.Vehicles.Find(CurrentRecord.SomeVehicle.VehicleId);
                        db.Entry(editableVehicle).State = EntityState.Modified;
                        editableVehicle.SomeVehicleType = db.VehicleTypes.Find(VType.VehicleTypeId);

                    }

                    if (!compare.ParkingPlaceLogDataCompare(PreviousState, CurrentState))
                    {
                        User user = db.Users.Find(UserId);
                        db.Entry(user).State = EntityState.Modified;
                        ParkingPlaceLog parkingPlaceLog = new ParkingPlaceLog();


                        if (PreviousState.Coast != CurrentState.Coast)
                        {
                            parkingPlaceLog.Money = CurrentRecord.SomeParkingPlaceLog.Money;
                            if (CurrentRecord.SomeParkingPlaceLog.Money > 0)
                                parkingPlaceLog.PayingDate = DateTime.Now;
                        }
                        parkingPlaceLog.DeadLine = new DateTime(ProlongDate.Year, ProlongDate.Month, ProlongDate.Day);
                        parkingPlaceLog.DateOfChange = DateTime.Now;

                        db.ParkingPlaceLogs.Add(parkingPlaceLog);
                        user.ParkingPlaceLogs.Add(parkingPlaceLog);

                        ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);
                        db.Entry(parkingPlace).State = EntityState.Modified;
                        parkingPlace.ParkingPlaceLogs.Add(parkingPlaceLog);
                    }

                    db.SaveChanges();

                    PreviousState = SetState();

                    NextDeadLine = ProlongDate;

                    FillFreeParkingPlacesList();
                    FillHistoryList(CurrentRecord.SomeClient.ClientId, CurrentRecord.SomeParkingPlace.ParkPlaceNumber, StartHistoryDate, EndHistoryDate);
                                        
                    GetLatPayInfo(CurrentRecord.SomeParkingPlaceLog.PayingDate, CurrentRecord.SomeParkingPlaceLog.Money);
                    dialogService.ShowMessage("Ok");
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




        private string toolTipMess;
        public string ToolTipMess
        {
            get { return toolTipMess; }
            set
            {
                if (toolTipMess != value)
                {
                    toolTipMess = value;
                    OnPropertyChanged(nameof(ToolTipMess));
                }
            }
        }

        private RelayCommand changeParkinPlaceCommand;

        public RelayCommand ChangeParkinPlaceCommand => changeParkinPlaceCommand ?? (changeParkinPlaceCommand = new RelayCommand(
                    (obj) =>
                    {
                        EditDataParkPlaceNumber();
                        FillFreeParkingPlacesList();
                    }
                    ));

        private void EditDataParkPlaceNumber()
        {
            using (DBConteiner db = new DBConteiner())
            {
                try
                {

                    ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);
                    db.Entry(parkingPlace).State = EntityState.Modified;
                    parkingPlace.FreeStatus = true;
                    parkingPlace.Released = false;

                    parkingPlace.Vehicles.Remove(CurrentRecord.SomeVehicle);

                    ParkingPlace newParkingPlace = db.ParkingPlaces.Where(par => par.ParkPlaceNumber == FreeparkPlace).FirstOrDefault();
                    db.Entry(newParkingPlace).State = EntityState.Modified;
                    newParkingPlace.FreeStatus = false;
                    newParkingPlace.Released = false;
                    newParkingPlace.Vehicles.Clear();
                    newParkingPlace.Vehicles.Add(CurrentRecord.SomeVehicle);


                    //Client curClient = db.Clients.Find(CurrentRecord.SomeClient.ClientId);
                    //db.Entry(curClient).State = EntityState.Modified;                    

                    ParkingPlaceLog curParkPlaceLog = CurrentRecord.SomeParkingPlaceLog;
                    curParkPlaceLog.ParkingPlaceLogId = 0;
                    db.ParkingPlaceLogs.Add(curParkPlaceLog);
                    curParkPlaceLog.SomeParkingPlace = newParkingPlace;


                    db.SaveChanges();
                    dialogService.ShowMessage("\t\tOk");
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


        private bool ValidationInputData()
        {
            if (CurrentRecord.SomeContacts.Phone == CurrentRecord.TrContacts.Phone)
            {
                dialogService.ShowMessage("Номери телефонів клієнта і довіреної\n особи НЕ можуть будти однаковими");
                return false;
            }




            if (CurrentRecord.SomeClient.OrgName is null || CurrentRecord.SomePerson.SecondName is null ||
                CurrentRecord.SomePerson.FirstName is null || CurrentRecord.SomePerson.Patronimic is null ||
                CurrentRecord.TrustedPerson.SecondName is null || CurrentRecord.TrustedPerson.FirstName is null ||
                CurrentRecord.TrustedPerson.Patronimic is null ||
                CurrentRecord.SomeClient.OrgName == "" || CurrentRecord.SomePerson.SecondName == "" ||
                CurrentRecord.SomePerson.FirstName == "" || CurrentRecord.SomePerson.Patronimic == "" ||
                CurrentRecord.TrustedPerson.SecondName == "" || CurrentRecord.TrustedPerson.FirstName == "" ||
                CurrentRecord.TrustedPerson.Patronimic == ""
                )
            {
                dialogService.ShowMessage("Одне або декілька полів з особистими данними\n" +
                    "вони не повинні бути пустими." +
                    ".\n\t\tВідкорегуйте");
                return false;
            }
            CurrentRecord.SomeClient.OrgName = CurrentRecord.SomeClient.OrgName.Trim();
            CurrentRecord.SomePerson.SecondName = CurrentRecord.SomePerson.SecondName.Trim();
            CurrentRecord.SomePerson.FirstName = CurrentRecord.SomePerson.FirstName.Trim();
            CurrentRecord.SomePerson.Patronimic = CurrentRecord.SomePerson.Patronimic.Trim();

            CurrentRecord.TrustedPerson.SecondName = CurrentRecord.TrustedPerson.SecondName.Trim();
            CurrentRecord.TrustedPerson.FirstName = CurrentRecord.TrustedPerson.FirstName.Trim();
            CurrentRecord.TrustedPerson.Patronimic = CurrentRecord.TrustedPerson.Patronimic.Trim();



            if (RegNumber is null || RegNumber == "" || RegNumber.Length < 8)
            {
                if (dialogService.YesNoDialog("Номер держ реєстрації не задано або задано не корректно.\n" +
                                            "\t\tПродовжити?"))
                    RegNumber = "не задано";
                else
                    return false;
            }

            if (decimal.TryParse(Coast, out decimal result))
                CurrentRecord.SomeParkingPlaceLog.Money = result;
            else
            {
                dialogService.ShowMessage("Значення ціни не корректне\n \t Відкорегуйте");
                return false;
            }

            return true;
        }

        private RelayCommand cencelWindowCommand;
        public RelayCommand CencelWindowCommand => cencelWindowCommand ?? (cencelWindowCommand = new RelayCommand(
                    (obj) =>
                    {

                        CurrentState = SetState();
                        if (CurrentState.TotalCompare(CurrentState, PreviousState))
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

        private RelayCommand printQuitanceCommand;
        public RelayCommand PrintQuitanceCommand => printQuitanceCommand ?? (printQuitanceCommand = new RelayCommand(
                    (obj) =>
                    {
                        if (CurrentRecord.SomeParkingPlaceLog.Money == 0)
                        {
                            if (LastPayDate != null)
                            {
                                if (dialogService.YesNoDialog("\t\tНе зада сума оплати.\nРоздрукувати квитанцію з останнєю оплатою?"))
                                {
                                    CurrentRecord.SomeParkingPlaceLog.Money = LastPaying;
                                    CurrentRecord.SomeParkingPlaceLog.PayingDate = LastPayDate;
                                }
                                else
                                {
                                    dialogService.ShowMessage("Не можна роздрукувати квитанцію\n" +
                                                              "з нульовою сумою");
                                    return;
                                }
                            }                            
                            else
                            {
                                dialogService.ShowMessage("Не можна роздрукувати квитанцію\n" +
                                                          "з нульовою сумою");
                                return;
                            }
                        };
                        PrintBlank printWindow = new PrintBlank(CurrentRecord, lib.GetUserData(UserId), lib.GetCompanyData());//for edit order                  
                        showWindow.ShowDialog(printWindow);
                    }
                    ));


        private void GetLatPayInfo(int vehId)
        {
            decimal coast;
            DateTime? lastDate;
            lib.GetLastVehicleDateAndPay(vehId, out coast, out lastDate);
            LastPayDate = lastDate;
            LastPaying = coast;
            if (!(lastDate is null))
                LastPayDateMessage = "Остання оплата:\n " + lastDate.Value.ToString("dd/MM/yyyy") +
                    "\nЧас:" + lastDate.Value.ToString("HH/mm/ss") + "\nСума : " + coast;

        }

        private void GetLatPayInfo(DateTime? date, decimal coast)

        {
            if (CurrentRecord.SomeParkingPlaceLog.Money > 0)
            {
                LastPayDateMessage = "Остання оплата:\n " + date.Value.ToString("dd/MM/yyyy") +
                                               "\nЧас:" + date.Value.ToString("HH/mm/ss") + "\nСума : " +
                                               coast;
                LastPayDate = date;
                LastPaying = coast;
            }

        }

    }

   
}

