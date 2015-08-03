using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public class License
    {
        private ICollection<Resource> resources;

        public License()
        {
            this.resources = new HashSet<Resource>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "License name is required")]
        [MaxLength(300, ErrorMessage = "You exceeded max length")]
        public string Name { get; set; }

        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }
    }
}
