using System;

namespace FlightApp.Model
{
    public class Movie
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public int Runtime { get; private set; }
        public string Description { get; private set; }
        public string Director { get; private set; }
        public string Poster { get; private set; }
        public string Trailer { get; private set; }

        public string Source { get { return $"{Id}.mp4"; } }

        public Movie(int id, string title, DateTime releaseDate, int runtime, string description, string director, string poster, string trailer)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Runtime = runtime;
            Description = description;
            Director = director;
            Poster = poster;
            Trailer = trailer;
        }
    }
}