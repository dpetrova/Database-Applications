using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDB.Models
{
    public class News
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string Content { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
