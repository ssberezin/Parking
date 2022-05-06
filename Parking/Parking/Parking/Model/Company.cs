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
   public class Company:Helpes.ObservableObject
    {
        public Company()
        {
            Employes = new ObservableCollection<Employee>();
        }
        public int CompanyId { get; set; }

        [Column("OrganizationName", TypeName = "nvarchar")]
        [MaxLength(50)]
        private string orgName;
        public string OrgName
        {
            get { return orgName; }
            set
            {
                if (value != orgName)
                {
                    orgName = value;
                    OnPropertyChanged(nameof(OrgName));
                }
            }
        }

        [Column("Adress", TypeName = "nvarchar")]
        [MaxLength(150)]
        private string orgAdress;
        public string OrgAdress
        {
            get { return orgAdress; }
            set
            {
                if (value != orgAdress)
                {
                    orgAdress = value;
                    OnPropertyChanged(nameof(OrgAdress));
                }
            }
        }

        [Column("TaxCode", TypeName = "nvarchar")]
        [MaxLength(10)]
        private string taxCode;
        public string TaxCode
        {
            get { return taxCode; }
            set
            {
                if (value != taxCode )
                {
                    taxCode = value;
                    OnPropertyChanged(nameof(TaxCode));
                }
            }
        }

        [Column("RegNumber", TypeName = "int")]
        private int regNumber;
        public int RegNumber
        {
            get { return regNumber; }
            set
            {
                if (value != regNumber)
                {
                    regNumber = value;
                    OnPropertyChanged(nameof(RegNumber));
                }
            }
        }

        public virtual ObservableCollection<Employee> Employes { get; set; }
    }
}
