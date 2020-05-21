using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Artist name cannot exceed 100 characters")]
        public string Artist { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Song name cannot exceed 100 characters")]
        public string SongName { get; set; }
        [Required]
        public Dept? License { get; set; }
        public string PhotoPath { get; set; }
    }
}
