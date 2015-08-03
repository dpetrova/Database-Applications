using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.EFCodeFirst_Phonebook
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Not valid phone number")]
        public string PhoneNumber { get; set; }
    }
}
