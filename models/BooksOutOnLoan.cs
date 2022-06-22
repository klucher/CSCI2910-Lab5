namespace CSCI2910_Lab5.models
{
    internal class BooksOutOnLoan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string DateIssued { get; set; }
        public string DueDate { get; set; }
        public string DateReturned { get; set; }

        public BooksOutOnLoan() { }

        public BooksOutOnLoan(int id, int userId, int bookId, string dateIssued, string dueDate, string dateReturned)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            DateIssued = dateIssued;
            DueDate = dueDate;
            DateReturned = dateReturned;
        }

        public override string ToString()
        {
            string booksOutOnLoanString = string.Empty;
            booksOutOnLoanString += Id + " ";
            booksOutOnLoanString += UserId + " ";
            booksOutOnLoanString += BookId + " ";
            return booksOutOnLoanString;
        }
    }
}
