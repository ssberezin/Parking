using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model
{
    public class Contacts : Helpes.ObservableObject
    {
        public Contacts()
            {

            }

        public int ContactsId { get; set; }

        [Column("Adress", TypeName = "nvarchar")]
        [MaxLength(500)]
        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                if (value != adress)
                {
                    adress = value;
                    OnPropertyChanged(nameof(Adress));
                }
            }
        }

        [Column("Phone", TypeName = "nvarchar")]
        [MaxLength(13)]
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != phone)
                {
                    phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        public virtual Person SomePerson { get; set; }

    }
}
