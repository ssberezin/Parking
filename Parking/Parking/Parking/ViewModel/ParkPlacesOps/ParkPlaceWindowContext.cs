using Parking.Helpes;
using Parking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.ViewModel.ParkPlacesOps
{
  
    public class ParkPlaceWindowContext: Helpes.ObservableObject
    {
        IDialogService dialogService;
        IShowWindowService showWindow;

       public ObservableCollection<int> FreeParkingPlacesList { get; set; }

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
            FreeParkingPlacesList = new ObservableCollection<int>();
            FillFreeParkPlacesList();
            ProlongDate = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            DefaultPhoto = "default_vehicle_picture.png";
            PreviousState = SetState();//remember current data state for compare in future befor save


            PropertyChanged2 += ChangeProlongData;
        }

        private void ChangeProlongData(object sender, PropertyChangedEventArgs e)
        {
            int res = ProlongDate.Subtract(CurrentRecord.SomeParkingPlaceLog.DeadLine.Value).Days;
            if (res > 0)
                ProlonDaysCount = res.ToString()+" дн.";
        }

            private void FillFreeParkPlacesList()
        {
            FreeParkingPlacesList.Clear();
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
            tmp.OwnerPersMale = CurrentRecord.SomePerson.Male;
            tmp.OwnerPersFemale = CurrentRecord.SomePerson.Female;
            tmp.OwnerPhone = CurrentRecord.SomeContacts.Phone;
            tmp.VPhoto = CurrentRecord.SomeVehicle.VPhoto is null?null:lib.CopyPhoto(CurrentRecord.SomeVehicle.VPhoto);
            tmp.DeadLine = CurrentRecord.SomeParkingPlaceLog.DeadLine.Value;
            tmp.TrustSecondName = CurrentRecord.TrustedPerson.SecondName;
            tmp.TrustFirstName = CurrentRecord.TrustedPerson.FirstName;
            tmp.TrustPatronimic = CurrentRecord.TrustedPerson.Patronimic;
            tmp.TrustPersMale = CurrentRecord.TrustedPerson.Male;
            tmp.TrustPersFemale = CurrentRecord.TrustedPerson.Female;
            tmp.TrustPhone = CurrentRecord.TrContacts.Phone;
            tmp.Adress = CurrentRecord.SomeContacts.Adress;
            tmp.RegNumber = CurrentRecord.SomeVehicle.RegNumber;
            tmp.Color = CurrentRecord.SomeVehicle.Color;
            tmp.VType = CurrentRecord.SomeVehicle.TypeName;
            tmp.ProlongDate = ProlongDate;
            tmp.Coast = Coast;
            tmp.FreeparkPlace = FreeparkPlace;
            return tmp;
        }

    }

   
}

