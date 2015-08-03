using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.EFCodeFirst_Phonebook
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not valid email")]
        public string EmailAddress { get; set; }
    }
}
