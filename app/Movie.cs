namespace app
{
    public class Movie
    {
        private Movie(Title title)
        {
            Title = title;
        }

        public Title Title { get; }

        public override bool Equals(object obj)
        {
            var otherMovie = obj as Movie;
            return otherMovie != null && Title.Equals(otherMovie.Title);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public static Movie New(Title title)
        {
            if (title == null)
            {
                throw new DomainException("Movie must have a title");
            }
            return new Movie(title);
        }
    }
}