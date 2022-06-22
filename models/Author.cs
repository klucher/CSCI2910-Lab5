namespace CSCI2910_Lab5.models
{
    internal class Author : IClassModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public Author() { }

        public Author(int id, string firstName, string surname)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
        }

        public override string ToString()
        {
            string authorString = string.Empty;
            authorString += Id + " ";
            authorString += FirstName + " ";
            authorString += Surname + " ";
            return authorString;
        }
    }
}
