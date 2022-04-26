using Parking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Helpes
{
    public class ParkingPlaceRecord: Helpes.ObservableObject
    {
        public ParkingPlaceRecord()
        {
            
        }


        private ParkingPlace someParkingPlace;
        public ParkingPlace SomeParkingPlace
        {
            get { return someParkingPlace; }
            set
            {
                if (someParkingPlace != value)
                {
                    someParkingPlace = value;
                    OnPropertyChanged(nameof(SomeParkingPlace));
                }
            }
        }

        private Client someClient;
        public Client SomeClient
        {
            get { return someClient; }
            set
            {
                if (someClient != value)
                {
                    someClient = value;
                    OnPropertyChanged(nameof(SomeClient));
                }
            }
        }

        private Person somePerson;
        public Person SomePerson
        {
            get { return somePerson; }
            set
            {
                if (somePerson != value)
                {
                    somePerson = value;
                    OnPropertyChanged(nameof(SomePerson));
                }
            }
        }

        private Contacts someContacts;
        public Contacts SomeContacts
        {
            get { return someContacts; }
            set
            {
                if (someContacts != value)
                {
                    someContacts = value;
                    OnPropertyChanged(nameof(SomeContacts));
                }
            }
        }

        private ParkingPlaceLog someParkingPlaceLog;
        public ParkingPlaceLog SomeParkingPlaceLog
        {
            get { return someParkingPlaceLog; }
            set
            {
                if (someParkingPlaceLog != value)
                {
                    someParkingPlaceLog = value;
                    OnPropertyChanged(nameof(SomeParkingPlaceLog));
                }
            }
        }

        private Vehicle someVehicle;
        public Vehicle SomeVehicle
        {
            get { return someVehicle; }
            set
            {
                if (someVehicle != value)
                {
                    someVehicle = value;
                    OnPropertyChanged(nameof(SomeVehicle));
                }
            }
        }

        //private int parkingPlaceId;
        //public int ParkingPlaceId
        //{
        //    get { return parkingPlaceId; }
        //    set
        //    {
        //        if (parkingPlaceId != value)
        //        {
        //            parkingPlaceId = value;
        //            OnPropertyChanged(nameof(ParkingPlaceId));
        //        }
        //    }
        //}

        //private int parkingPlaceNumber;
        //public int ParkingPlaceNumber
        //{
        //    get { return parkingPlaceNumber; }
        //    set
        //    {
        //        if (parkingPlaceNumber != value)
        //        {
        //            parkingPlaceNumber = value;
        //            OnPropertyChanged(nameof(ParkingPlaceNumber));
        //        }
        //    }
        //}

        //private bool freeStatus;
        //public bool FreeStatus
        //{
        //    get { return freeStatus; }
        //    set
        //    {
        //        if (freeStatus != value)
        //        {
        //            freeStatus = value;
        //            OnPropertyChanged(nameof(FreeStatus));
        //        }
        //    }
        //}

        //private bool released;
        //public bool Released
        //{
        //    get { return released; }
        //    set
        //    {
        //        if (released != value)
        //        {
        //            released = value;
        //            OnPropertyChanged(nameof(Released));
        //        }
        //    }
        //}

        //private int clientId;
        //public int ClientId
        //{
        //    get { return clientId; }
        //    set
        //    {
        //        if (clientId != value)
        //        {
        //            clientId = value;
        //            OnPropertyChanged(nameof(ClientId));
        //        }
        //    }
        //}

        //private string  orgName;
        //public string OrgName
        //{
        //    get { return orgName; }
        //    set
        //    {
        //        if (orgName != value)
        //        {
        //            orgName = value;
        //            OnPropertyChanged(nameof(OrgName));
        //        }
        //    }
        //}

        //private int personId;
        //public int PersonId
        //{
        //    get { return personId; }
        //    set
        //    {
        //        if (personId != value)
        //        {
        //            personId = value;
        //            OnPropertyChanged(nameof(PersonId));
        //        }
        //    }
        //}

        //private string orgName;
        //public string OrgName
        //{
        //    get { return orgName; }
        //    set
        //    {
        //        if (orgName != value)
        //        {
        //            orgName = value;
        //            OnPropertyChanged(nameof(OrgName));
        //        }
        //    }
        //}




    }
}
