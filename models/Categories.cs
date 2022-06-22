namespace CSCI2910_Lab5.models
{
    internal class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Categories() { }

        public Categories(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            string categoriesString = string.Empty;
            categoriesString += Id + " ";
            categoriesString += Name + " ";
            return categoriesString;
        }
    }
}
