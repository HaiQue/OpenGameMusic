using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public class SQLSongRepository : ISongRepository
    {
        private readonly AppDbContext context;
        public SQLSongRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Song Add(Song song)
        {
            context.Songs.Add(song);
            context.SaveChanges();
            return song;
        }

        public Song Delete(int id)
        {
            Song song = context.Songs.Find(id);
            if (song != null)
            {
                context.Songs.Remove(song);
                context.SaveChanges();
            }
            return song;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return context.Songs;
        }

        public Song GetSong(int Id)
        {
            return context.Songs.Find(Id);
        }

        public Song Update(Song songChanges)
        {
            var song = context.Songs.Attach(songChanges);
            song.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return songChanges;
        }
    }
}
