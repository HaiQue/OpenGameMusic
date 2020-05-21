using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
               new Song
               {
                   Id = 1,
                   Artist = "Faustix",
                   SongName = "Solo",
                   License = Dept.PublicPerformanceLicense
               },
               new Song
               {
                   Id = 2,
                   Artist = "Madison Mars",
                   SongName = "Back to you",
                   License = Dept.PublicPerformanceLicense
               }
               );
        }
    }
}
