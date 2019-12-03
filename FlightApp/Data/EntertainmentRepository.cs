using FlightApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace FlightApp.Data
{
    public static class EntertainmentRepository
    {
        public static async Task<IList<Movie>> GetAllMoviesAsync()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Entertainment/get_movies"));
            IList<MovieDTO> movies = JsonConvert.DeserializeObject<ObservableCollection<MovieDTO>>(json);
            return movies.Select(m => m.ToMovie()).ToList();
        }

        public static async Task<IList<Music>> GetAllMusicAsync()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Entertainment/get_music"));
            IList<MusicDTO> music = JsonConvert.DeserializeObject<ObservableCollection<MusicDTO>>(json);
            return music.Select(m => m.ToMusic()).ToList();
        }

        #region DTOs
        //Receive
        private class MovieDTO
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime ReleaseDate { get; set; }
            public int Runtime { get; set; }
            public string Description { get; set; }
            public string Director { get; set; }
            public string Poster { get; set; }
            public string Trailer { get; set; }

            public Movie ToMovie()
            {
                return new Movie(Id, Title, ReleaseDate, Runtime, Description, Director, Poster, Trailer);
            }
        }
        private class MusicDTO
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Poster { get; set; }

            public Music ToMusic()
            {
                return new Music(Id, Title, Artist, Poster);
            }
        }
        #endregion
    }
}
