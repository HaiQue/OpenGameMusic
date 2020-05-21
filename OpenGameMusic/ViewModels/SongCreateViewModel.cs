using Microsoft.AspNetCore.Http;
using OpenGameMusic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.ViewModels
{
    public class SongCreateViewModel
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
        public IFormFile Photo { get; set; }
    }
}
