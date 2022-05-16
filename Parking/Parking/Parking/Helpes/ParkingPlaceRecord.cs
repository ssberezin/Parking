using Parking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Helpes
{
    public class ParkingPlaceRecord : Helpes.ObservableObject
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

        private Person trustedPerson;
        public Person TrustedPerson
        {
            get { return trustedPerson; }
            set
            {
                if (trustedPerson != value)
                {
                    trustedPerson = value;
                    OnPropertyChanged(nameof(trustedPerson));
                }
            }
        }

        private Contacts trContacts;
        public Contacts TrContacts
        {
            get { return trContacts; }
            set
            {
                if (trContacts != value)
                {
                    trContacts = value;
                    OnPropertyChanged(nameof(trContacts));
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

        //SomeVehicleType
        private VehicleType someVehicleType;
        public VehicleType SomeVehicleType
        {
            get { return someVehicleType; }
            set
            {
                if (someVehicleType != value)
                {
                    someVehicleType = value;
                    OnPropertyChanged(nameof(SomeVehicleType));
                }
            }
        }

       

        private bool femaleOwnPers;
        public bool FemaleOwnPers
        {
            get { return femaleOwnPers; }
            set
            {
                if (value != femaleOwnPers)
                {
                    femaleOwnPers = value;
                    OnPropertyChanged(nameof(FemaleOwnPers));
                }
            }
        }

        private bool femaleTrustPers;
        public bool FemaleTrustPers
        {
            get { return femaleTrustPers; }
            set
            {
                if (value != femaleTrustPers)
                {
                    femaleTrustPers = value;
                    OnPropertyChanged(nameof(FemaleTrustPers));
                }
            }
        }


      

    }
}