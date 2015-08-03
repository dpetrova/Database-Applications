using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public class Resource
    {
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
        }
        
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Resource name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Type of resource is required")]
        public TypeOfResource ResourceType { get; set; }
        [Required(ErrorMessage = "URL of resource is required")]
        public string Url { get; set; }

        public virtual ICollection<License> Licenses
        {
            get { return this.licenses; }
            set { this.licenses = value; }
        }
    }
}
