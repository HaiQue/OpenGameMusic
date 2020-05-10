using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public class MockSongRepository : ISongRepository
    {
        private List<Song> _songList;
        public MockSongRepository()
        {
            _songList = new List<Song>()
            {
                new Song() { Id = 1, Artist = "Hugel", SongName = "They know", Licence = "Full CC licence"},
                new Song() { Id = 2, Artist = "Jack Back", SongName = "Survivor", Licence = "Full CC licence"},
                new Song() { Id = 3, Artist = "Ramesis B", SongName = "Forever", Licence = "Full CC licence"}
            };
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return _songList;
        }
        public Song GetSong(int Id)
        {
            return _songList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
