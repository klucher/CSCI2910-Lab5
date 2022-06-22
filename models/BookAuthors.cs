namespace CSCI2910_Lab5.models
{
    internal class BookAuthors
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public BookAuthors() { }

        public BookAuthors(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }

        public override string ToString()
        {
            string bookAuthorString = string.Empty;
            bookAuthorString += BookId + " ";
            bookAuthorString += AuthorId + " ";
            return bookAuthorString;
        }
    }
}
