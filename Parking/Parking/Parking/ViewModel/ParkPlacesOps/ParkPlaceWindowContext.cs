using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private decimal coast;
        public decimal Coast
        {
            get { return coast; }
            set
            {
                if (coast != value)
                {
                    coast = value;
                    OnPropertyChanged2(nameof(Coast));
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

        CompareStates PreviousState { get; set; }
        CompareStates CurrentState { get; set; }

        Library lib;//for using some methodes 

        public int UserId { get; set; } 

        public ParkPlaceWindowContext()
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();
        }

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
            CurrentRecord.SomeClient.OrgName = CurrentRecord.SomeClient.OrgName == null ?"фізична особа": CurrentRecord.SomeClient.OrgName;            
            NextDeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            OwnerPhone1 = CurrentRecord.SomeContacts.Phone;
            TrustPhone = CurrentRecord.TrContacts.Phone;
            RegNumber = CurrentRecord.SomeVehicle.RegNumber;
            VType = CurrentRecord.SomeVehicleType;
            FreeParkingPlacesList = new ObservableCollection<int>();
            VehicleTypeList = new ObservableCollection<VehicleType>();
            FillLists();

            ProlongDate = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            DefaultPhoto = "default_vehicle_picture.png";
            PreviousState = SetState();//remember current data state for compare in future befor save


            PropertyChanged2 += ChangeProlongData;
            PropertyChanged3 += NumberValidationPhone1;
            PropertyChanged4 += NumberValidationPhone2;
            PropertyChanged5 += RegNumberValidation;

        }

        private void ChangeProlongData(object sender, PropertyChangedEventArgs e)
        {
            int res = ProlongDate.Subtract(CurrentRecord.SomeParkingPlaceLog.DeadLine.Value).Days;
            if (res > 0)
                ProlonDaysCount = res.ToString()+" дн.";
        }

        private static readonly Regex regex = new Regex("[^0-9+]+"); //regex that matches disallowed text
        private void NumberValidationPhone1(object sender, PropertyChangedEventArgs e)
        {
            if (regex.IsMatch(OwnerPhone1))
            {
                OwnerPhone1 = CurrentRecord.SomeContacts.Phone;
                dialogService.ShowMessage("Ви намагаєтеся ввести в поле номеру телефона некорректні данні.");
            }
           
        }

        private void NumberValidationPhone2(object sender, PropertyChangedEventArgs e)
        {
            if (regex.IsMatch(TrustPhone))
            {
                TrustPhone = CurrentRecord.TrContacts.Phone;
                dialogService.ShowMessage("Ви намагаєтеся ввести в поле номеру телефона некорректні данні.");
            }

        }

        private static readonly Regex regexRegNumer = new Regex("[^0-9][^A-Z]+"); //regex that matches disallowed text
        private void RegNumberValidation(object sender, PropertyChangedEventArgs e)
        {
            CurrentRecord.SomeVehicle.RegNumber = CurrentRecord.SomeVehicle.RegNumber.ToUpper();
            if (regexRegNumer.IsMatch(CurrentRecord.SomeVehicle.RegNumber))
            {
                RegNumber = CurrentRecord.SomeVehicle.RegNumber;
                dialogService.ShowMessage("Ви намагаєтеся ввести в поле номеру \n держ реэстрації не корректні данні.");
            }

        }

        private void FillLists()
        {
            FreeParkingPlacesList.Clear();
            VehicleTypeList.Clear();
            using (DBConteiner  db = new DBConteiner())
            {
                try
                {
                    var result = db.ParkingPlaces.Where(p=>p.FreeStatus==true).ToList();
                    if (result != null)
                    {
                        foreach (ParkingPlace item in result)
                            FreeParkingPlacesList.Add(item.ParkPlaceNumber);
                        MessageForChangeParkPlace = null ;
                    }
                    else 
                        MessageForChangeParkPlace = "Немає вільних місць";
                    var VTlist = db.VehicleTypes.ToList();
                    if (!(VTlist is null))
                    {
                        foreach (var item in VTlist)
                            VehicleTypeList.Add(
                                new VehicleType
                            { VehicleTypeId=item.VehicleTypeId,
                              TypeName=item.TypeName
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

        private CompareStates SetState()
        {
            CompareStates tmp  = new CompareStates();
            tmp.Owner = CurrentRecord.SomeClient.OrgName;
            tmp.OwnerSecondName = CurrentRecord.SomePerson.SecondName;
            tmp.OwnerFirstName = CurrentRecord.SomePerson.FirstName;
            tmp.OwnerPatronimic = CurrentRecord.SomePerson.Patronimic;
            tmp.OwnerSex = CurrentRecord.SomePerson.Sex;            
            tmp.OwnerPhone = CurrentRecord.SomeContacts.Phone;
            tmp.VPhoto = CurrentRecord.SomeVehicle.VPhoto is null?null:lib.CopyPhoto(CurrentRecord.SomeVehicle.VPhoto);
            tmp.DeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            tmp.TrustSecondName = CurrentRecord.TrustedPerson.SecondName;
            tmp.TrustFirstName = CurrentRecord.TrustedPerson.FirstName;
            tmp.TrustPatronimic = CurrentRecord.TrustedPerson.Patronimic;
            tmp.TrustPersSex = CurrentRecord.TrustedPerson.Sex;            
            tmp.TrustPhone = CurrentRecord.TrContacts.Phone;
            tmp.Adress = CurrentRecord.SomeContacts.Adress;
            tmp.RegNumber = CurrentRecord.SomeVehicle.RegNumber;
            tmp.Color = CurrentRecord.SomeVehicle.Color;
            tmp.VType = CurrentRecord.SomeVehicleType.TypeName;
            tmp.ProlongDate = ProlongDate;
            tmp.Coast = Coast;
            tmp.FreeparkPlace = FreeparkPlace;
            return tmp;
        }


        private void SaveData()
        {
            CurrentState = SetState();
            if (!ValidationInputData())
                return;
            //next we have to save data to DB



        }
        private bool ValidationInputData()
        {
            if (!CurrentState.CompareObjects(PreviousState, CurrentState)) //if there wasn't any changes            
                return false;

            //Phone nubers validations
            string tmpPh = lib.PhoneNumberValidation(OwnerPhone1);
            if (tmpPh is null)
            {
                dialogService.ShowMessage("Поле телефона власника заповнене не корректно.\nВідкорегуйте");
                return false;
            }
            else
                CurrentRecord.SomeContacts.Phone = OwnerPhone1;

            if (lib.PhoneNumberValidation(TrustPhone) is null)
            {
                dialogService.ShowMessage("Поле телефона фласника заповнене не корректно.\nВідкорегуйте");
                return false;
            }
            else
                CurrentRecord.TrContacts.Phone = TrustPhone;
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

