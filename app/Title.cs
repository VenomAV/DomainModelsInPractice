namespace app
{
    public class Title
    {
        private Title(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object? obj)
        {
            var otherTitle = obj as Title;
            return otherTitle != null && Value.Equals(otherTitle.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static Title New(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Title cannot be empty");
            return new Title(value);
        }
    }
}