namespace CSCI2910_Lab5.models
{
    internal class BookCategories
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        public BookCategories() { }

        public BookCategories(int bookId, int categoryId)
        {
            BookId = bookId;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            string bookCategoriesString = string.Empty;
            bookCategoriesString += BookId + " ";
            bookCategoriesString += CategoryId + " ";
            return bookCategoriesString;
        }
    }
}
