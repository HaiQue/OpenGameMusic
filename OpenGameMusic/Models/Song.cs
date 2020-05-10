using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public class Song
    {
        public int Id { get; set; }

        //[Required]
        //[MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Artist { get; set; }

        //[Required]
        public string SongName { get; set; }

        //[Required]
        public string Licence { get; set; }
        //public string PhotoPath { get; set; }
    }
}
