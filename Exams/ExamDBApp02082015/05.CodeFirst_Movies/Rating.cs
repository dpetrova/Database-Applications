using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.CodeFirst_Movies
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 10)]
        public int Stars { get; set; }

        [Index("IX_UserAndMovie", 1, IsUnique = true)]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Index("IX_UserAndMovie", 2, IsUnique = true)]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
