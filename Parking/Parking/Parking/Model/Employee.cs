using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model
{
    public class Employee: Helpes.ObservableObject
    {

        public Employee()
        {
            Users = new ObservableCollection<User>();
            EmployeePositions = new ObservableCollection<EmployeePosition>();
            
        }
        public int EmployeeId { get; set; }

        private decimal salary;
        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (salary != value)
                {
                    salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }

        

        [Column(TypeName = "datetime2")]

        private DateTime? hireDate;
        public DateTime? HireDate
        {
            get { return hireDate; }
            set
            {
                if (hireDate != value)
                {
                    hireDate = value;
                    OnPropertyChanged(nameof(HireDate));
                }
            }
        }

        [Column(TypeName = "datetime2")]

        private DateTime? fireDate;
        public DateTime? FireDate
        {
            get { return fireDate; }
            set
            {
                if (fireDate != value)
                {
                    fireDate = value;
                    OnPropertyChanged(nameof(FireDate));
                }
            }
        }

        [Column("Description", TypeName = "nvarchar")]
        [MaxLength(500)]
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public virtual Person SomePerson { get; set; }
        public virtual Company OwnerCompany { get; set; }
        public virtual ObservableCollection<User> Users { get; set; }       
        public virtual ObservableCollection <EmployeePosition> EmployeePositions { get; set; }
    }
}
