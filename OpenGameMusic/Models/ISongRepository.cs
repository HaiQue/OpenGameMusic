using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public interface ISongRepository
    {
        Song GetSong(int Id);

        IEnumerable<Song> GetAllSongs();
    }
}
