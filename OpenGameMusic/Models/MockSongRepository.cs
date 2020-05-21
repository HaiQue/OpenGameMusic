using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public class MockSongRepository : ISongRepository
    {
        private readonly List<Song> _songList;
        public MockSongRepository()
        {
            _songList = new List<Song>()
            {
                new Song() { Id = 1, Artist = "Hugel", SongName = "They know", License = Dept.FullCClicense},
                new Song() { Id = 2, Artist = "Jack Back", SongName = "Survivor", License = Dept.None},
                new Song() { Id = 3, Artist = "Ramesis B", SongName = "Forever", License = Dept.PublicPerformanceLicense}
            };
        }

        public Song Add(Song song)
        {
            song.Id = _songList.Max(e => e.Id) + 1;
            _songList.Add(song);
            return song;
        }

        public Song Delete(int id)
        {
            Song song = _songList.FirstOrDefault(e => e.Id == id);
            if (song != null)
            {
                _songList.Remove(song);
            }
            return song;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return _songList;
        }
        public Song GetSong(int Id)
        {
            return _songList.FirstOrDefault(e => e.Id == Id);
        }

        public Song Update(Song songChanges)
        {
            Song song = _songList.FirstOrDefault(e => e.Id == songChanges.Id);
            if (song != null)
            {
                song.Artist = songChanges.Artist;
                song.SongName = songChanges.SongName;
                song.License = songChanges.License;
            }
            return song;
        }
    }
}
