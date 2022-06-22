namespace CSCI2910_Lab5.models
{
    internal class Books : IClassModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string DateOfPublication { get; set; }

        public Books() { }

        public Books(int id, string title, string isbn, string dateOfPublication)
        {
            Id = id;
            Title = title;
            Isbn = isbn;
            DateOfPublication = dateOfPublication;
        }

        public override string ToString()
        {
            string bookString = string.Empty;
            bookString += Id + " ";
            bookString += Title + " ";
            return bookString;
        }
    }
}
