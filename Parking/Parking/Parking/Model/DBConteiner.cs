using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Parking.Helpes;
using System.Windows;

namespace Parking.Model
{
    class DBConteiner: DbContext
    {
        IDialogService dialogService;
        IShowWindowService showWindow;
        public DBConteiner() : base("name=ParkingDB")
        {
            showWindow = new DefaultShowWindowService();
            dialogService = new DefaultDialogService();

            Database.SetInitializer<DBConteiner>
            (new DropCreateDatabaseIfModelChanges<DBConteiner>());
            
        }


        public virtual DbSet<Company> OwnerCompany { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
        public virtual DbSet<ParkingPlace> ParkingPlaces { get; set; }
        public virtual DbSet<ParkingPlaceLog> ParkingPlaceLogs { get; set; }
        public virtual DbSet<Person> Persons { get; set; }     
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleColor> Colors { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }

    }
}
