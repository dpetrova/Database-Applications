using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.EFCodeFirst_Phonebook
{
    public class Contact
    {
        private ICollection<Phone> phones;
        private ICollection<Email> emails;

        public Contact()
        {
            this.phones = new HashSet<Phone>();
            this.emails = new HashSet<Email>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Not valid URL")]
        public string SiteUrl { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        public virtual ICollection<Phone> Phones
        {
            get { return this.phones; }
            set { this.phones = value; }
        }

        public virtual ICollection<Email> Emails
        {
            get { return this.emails; }
            set { this.emails = value; }
        }
       


    }
}
