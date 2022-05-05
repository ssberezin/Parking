using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parking.ViewModel.ParkPlacesOps
{
  
    public class ParkPlaceWindowContext: Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;

       public ObservableCollection<int> FreeParkingPlacesList { get; set; }
       public ObservableCollection<VehicleType> VehicleTypeList { get; set; }

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

        private string deadLine;
        public string DeadLine
        {
            get { return deadLine; }
            set
            {
                if (deadLine != value)
                {
                    deadLine = value;
                    OnPropertyChanged(nameof(DeadLine));
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

            //if there is some client by this parking place we have an opportunity to change parking place number
            NewDataAddedSaved = CurrentRecord.SomeClient.ClientId == 0 ? false : true;
            CurrentRecord.SomeClient.OrgName = CurrentRecord.SomeClient.OrgName == null ? "фізична особа" : CurrentRecord.SomeClient.OrgName;
            NextDeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            OwnerPhone1 = CurrentRecord.SomeContacts.Phone;
            TrustPhone = CurrentRecord.TrContacts.Phone;
            RegNumber = CurrentRecord.SomeVehicle.RegNumber;
            VType = CurrentRecord.SomeVehicleType;
            NotFree = !CurrentRecord.SomeParkingPlace.FreeStatus.Value;
            NotInPlace = !CurrentRecord.SomeParkingPlace.Released;
            ProlongDate = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;

            Coast = "0";
            FreeParkingPlacesList = new ObservableCollection<int>();
            VehicleTypeList = new ObservableCollection<VehicleType>();
            
            FillFreeParkingPlacesList();
            FillVehicleTypeList();




            DefaultPhoto = "default_vehicle_picture.png";
            PreviousState = SetState();//remember current data state for compare in future befor save

            PropertyChanged2 += ChangeProlongData;
            PropertyChanged3 += NumberValidationPhone1;
            PropertyChanged4 += NumberValidationPhone2;
            PropertyChanged5 += RegNumberValidation;
            PropertyChanged6 += CoastValidation;
        }

        private void ChangeProlongData(object sender, PropertyChangedEventArgs e)
        {
            int res = ProlongDate.Subtract(CurrentRecord.SomeParkingPlaceLog.DeadLine.Value).Days;
            if (res > 0)
                ProlonDaysCount = res.ToString()+" дн.";
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
            if (!(RegNumber is null))
                RegNumber = RegNumber.ToUpper();

            if (regexRegNumer.IsMatch(RegNumber))
            {
                RegNumber = RegNumber.Remove(RegNumber.Length-1,1);                
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
                        MessageForChangeParkPlace = null;
                    }
                    else
                        MessageForChangeParkPlace = "Немає вільних місць";            

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
            tmp.Color = CurrentRecord.SomeVehicle.Color;
            tmp.VType = CurrentRecord.SomeVehicleType.TypeName;

            //trusted person's data
            tmp.TrustPhone = CurrentRecord.TrContacts.Phone;
            
            tmp.ProlongDate = ProlongDate;
            tmp.Coast = Coast;

            return tmp;
        }

        private RelayCommand savedataCommand;

        public RelayCommand SavedataCommand => savedataCommand ?? (savedataCommand = new RelayCommand(
                    (obj) =>
                    {
                        if (CurrentRecord.SomeClient.ClientId == 0)
                            AddnewData();
                        else
                            EditData();
                    }
                    ));

        private void AddnewData()
        {

            CompareStatesForParkingPlace compare = new CompareStatesForParkingPlace();
            if (!ValidationInputData())
                return;
            CurrentState = SetState();

            CurrentRecord.SomeVehicle.RegNumber = RegNumber;
            CurrentRecord.SomeContacts.Phone = OwnerPhone1;
            CurrentRecord.TrContacts.Phone = TrustPhone;


            //next we have to save data to DB
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    lib.GetPersonAndContactsIds(CurrentRecord.SomeContacts.Phone, CurrentRecord.SomePerson, out int? ctnId, out int? persId);

                    Client Cl;
                    Person OwnerPerson;
                    Contacts OwnerContacts;// = db.Contacts.Where(ctn => ctn.Phone == CurrentRecord.SomeContacts.Phone).FirstOrDefault();

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

                    Person TrustPerson;
                    Contacts TrustContacts;// = db.Contacts.Where(ctn => ctn.Phone == CurrentRecord.TrContacts.Phone).FirstOrDefault();

                    if (trCtnId is null || trCtnId == 0 && trPersId == 0)
                    {
                        TrustPerson = CurrentRecord.TrustedPerson;
                        db.Persons.Add(TrustPerson);

                        TrustContacts = CurrentRecord.TrContacts;
                        db.Contacts.Add(TrustContacts);

                        TrustPerson.ContactsData.Add(TrustContacts);

                        OwnerPerson.TrustedPerson = TrustPerson;
                    }
                    else
                    {
                        TrustContacts = db.Contacts.Find(trCtnId);
                        TrustPerson = db.Persons.Find(trPersId);

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
                            Color = CurrentRecord.SomeVehicle.Color,
                            RegNumber = CurrentRecord.SomeVehicle.RegNumber,
                            SomeVehicleType = db.VehicleTypes.Find(VType.VehicleTypeId),
                            VPhoto = CurrentRecord.SomeVehicle.VPhoto
                        };
                        db.Vehicles.Add(newVehicle);
                        newVehicle.ClientOwner = Cl;
                    }
                    else
                    {
                        db.Entry(newVehicle).State = EntityState.Modified;
                        newVehicle.Color = CurrentRecord.SomeVehicle.Color;
                        newVehicle.RegNumber = CurrentRecord.SomeVehicle.RegNumber;
                        newVehicle.SomeVehicleType = db.VehicleTypes.Find(VType.VehicleTypeId);
                        newVehicle.VPhoto = CurrentRecord.SomeVehicle.VPhoto;
                        newVehicle.ClientOwner = Cl;
                    }

                    User user = db.Users.Find(UserId);
                    db.Entry(user).State = EntityState.Modified;

                    ParkingPlaceLog parkingPlaceLog = new ParkingPlaceLog();
                    parkingPlaceLog.DeadLine = new DateTime(ProlongDate.Year, ProlongDate.Month, ProlongDate.Day);
                    parkingPlaceLog.Money = CurrentRecord.SomeParkingPlaceLog.Money;
                    db.ParkingPlaceLogs.Add(parkingPlaceLog);

                    user.ParkingPlaceLogs.Add(parkingPlaceLog);


                    ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);

                    parkingPlaceLog.SomeParkingPlace = parkingPlace;

                    db.Entry(parkingPlace).State = EntityState.Modified;
                    parkingPlace.FreeStatus = false;
                    parkingPlace.Released = false;

                    parkingPlace.SomeClient = Cl;

                    db.SaveChanges();
                    DeadLine = ProlongDate.ToString("dd/MM/yyyy");
                    PreviousState = SetState();
                    NewDataAddedSaved = true;//allow an opportunity of changing parking plase number

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

            CurrentRecord.SomeVehicle.RegNumber = RegNumber;
            CurrentRecord.SomeContacts.Phone = OwnerPhone1;
            CurrentRecord.TrContacts.Phone = TrustPhone;
          
            CurrentState = SetState();
            //next we have to save data to DB
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
                    if (!compare.ParkingPalceDataCompare(PreviousState,CurrentState))
                    {
                        ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);
                        db.Entry(parkingPlace).State = EntityState.Modified;
                        parkingPlace.FreeStatus = CurrentRecord.SomeParkingPlace.FreeStatus;
                        parkingPlace.Released = CurrentRecord.SomeParkingPlace.Released;
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
                        editableVehicle.Color = CurrentRecord.SomeVehicle.Color;
                        editableVehicle.RegNumber = CurrentRecord.SomeVehicle.RegNumber;
                        editableVehicle.VPhoto = CurrentRecord.SomeVehicle.VPhoto;
                    }

                    if (VType.TypeName != CurrentRecord.SomeVehicleType.TypeName)
                    {
                        Vehicle editableVehicle = db.Vehicles.Find(CurrentRecord.SomeVehicle.VehicleId);
                        db.Entry(editableVehicle).State = EntityState.Modified;
                        editableVehicle.SomeVehicleType = db.VehicleTypes.Find(VType.VehicleTypeId);
                        
                    }

                    if (!compare.ParkingPlaceLogDataCompare(PreviousState, CurrentState))
                    {
                        User user = db.Users.Find(UserId);
                        db.Entry(user).State = EntityState.Modified;

                        ParkingPlaceLog parkingPlaceLog = db.ParkingPlaceLogs.Find(CurrentRecord.SomeParkingPlaceLog.ParkingPlaceLogId);
                        db.Entry(parkingPlaceLog).State = EntityState.Modified;
                        parkingPlaceLog.DeadLine = new DateTime(ProlongDate.Year, ProlongDate.Month,ProlongDate.Day );
                        parkingPlaceLog.Money = CurrentRecord.SomeParkingPlaceLog.Money;

                        user.ParkingPlaceLogs.Add(parkingPlaceLog);
                    }

                    db.SaveChanges();

                    PreviousState = SetState();
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

        private RelayCommand changeParkinPlaceCommand;

        public RelayCommand ChangeParkinPlaceCommand => changeParkinPlaceCommand ?? (changeParkinPlaceCommand = new RelayCommand(
                    (obj) =>
                    {

                        EditDataParkPlaceNamber();                      

                    }
                    ));

        private void EditDataParkPlaceNamber()
        {

            
            using (DBConteiner db = new DBConteiner())
            {
                try
                {
            
                     ParkingPlace parkingPlace = db.ParkingPlaces.Find(CurrentRecord.SomeParkingPlace.ParkingPlaceId);
                     db.Entry(parkingPlace).State = EntityState.Modified;
                    parkingPlace.FreeStatus = true;
                    parkingPlace.Released = true;

                    ParkingPlace newParkingPlace = db.ParkingPlaces.Where(par => par.ParkPlaceNumber == FreeparkPlace).FirstOrDefault();
                    db.Entry(newParkingPlace).State = EntityState.Modified;
                    newParkingPlace.FreeStatus = false;
                    newParkingPlace.FreeStatus = false;

                    Client curClient = db.Clients.Find(CurrentRecord.SomeClient.ClientId);
                    db.Entry(curClient).State = EntityState.Modified;
                    curClient.ParkingPlaces.Add(parkingPlace);

                    ParkingPlaceLog curParkPlaceLog = db.ParkingPlaceLogs.Find(CurrentRecord.SomeParkingPlaceLog.ParkingPlaceLogId);
                    db.Entry(curParkPlaceLog).State = EntityState.Modified;
                    curParkPlaceLog.SomeParkingPlace = newParkingPlace;

                    db.SaveChanges();
                    CurrentRecord.SomeParkingPlace = newParkingPlace;
                    CurrentRecord.SomeParkingPlaceLog = curParkPlaceLog;
                    FillFreeParkingPlacesList();

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
                     
            if (CurrentRecord.SomeClient.OrgName is null || CurrentRecord.SomePerson.SecondName is null ||
                CurrentRecord.SomePerson.FirstName is null || CurrentRecord.SomePerson.Patronimic is null ||
                CurrentRecord.TrustedPerson.SecondName is null || CurrentRecord.TrustedPerson.FirstName is null ||
                CurrentRecord.TrustedPerson.Patronimic is null ||
                CurrentRecord.SomeClient.OrgName =="" || CurrentRecord.SomePerson.SecondName == "" ||
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
            if (CurrentRecord.SomeVehicle.Color is null || CurrentRecord.SomeVehicle.Color == "")
            {
                dialogService.ShowMessage("Не заданий колір транспортного засобу." +
                    ".\n\t\tВідкорегуйте");
                return false;
            }
            if (RegNumber is null || RegNumber=="" || RegNumber.Length<8 )
            {
                dialogService.ShowMessage("Значення номера держ. реэстрації НЕ корректне.\n або не задано\n \t\t Відкорегуйте" +
                    ".\n\t\tВідкорегуйте");
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

                        showWindow.CloseWindow(obj as Window);
                    }
                    ));

    }

   
}

