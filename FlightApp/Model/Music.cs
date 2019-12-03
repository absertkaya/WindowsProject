namespace FlightApp.Model
{
    public class Music
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Poster { get; private set; }
        public string Source { get { return $"{Id.ToString()}.mp3"; } }

        public Music(int id, string title, string artist, string poster)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Poster = poster;
        }
    }
}
