using CSCI2910_Lab5.models;

namespace CSCI2910_Lab5
{
    public class Program
    {
        static void Main(string[] args)
        {
            AuthorOperations();            
        }

        static List<Author> ReadAuthorFromFile()
        {
            string rootPath = FileRoot.GetDefaultDirectory();
            string authorCSVPath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}author.csv";

            var authorList = new List<Author>();
            //var authorId = 1;
            using (StreamReader sr = new StreamReader(authorCSVPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    List<string> elements = line.Split(",").ToList();
                    authorList.Add(new Author(Convert.ToInt32(elements[0]), elements[1], elements[2]));
                }
            }
            return authorList;
        }

        static void PracticeOperations()
        {
            var authorList = ReadAuthorFromFile();

            string rootPath = FileRoot.GetDefaultDirectory();
            string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(databasePath);

            using (qb)
            {
                Console.WriteLine("\nClearing all authors from database...");
                qb.DeleteAll<Author>();

                Console.WriteLine("Database cleared. Now adding all pokemon from data file...");
                foreach (Author author in authorList)
                {
                    qb.Insert<Author>(author);
                }
                Console.WriteLine($"{authorList.Count} authors were added...");

                Console.Write("Please enter the ID of the author you would like to view: ");
                var id = Convert.ToInt32(Console.ReadLine());
                Author readAuthor = qb.Read<Author>(id);
                Console.WriteLine(readAuthor);

                Console.WriteLine("\n\n===============================================");
                Console.WriteLine("Reading all authors from the database...");
                Console.WriteLine("===============================================");
                var authorListFromDB = qb.ReadAll<Author>();
                foreach (Author author in authorListFromDB)
                {
                    Console.WriteLine(author);
                }

                Console.WriteLine("\nPlease enter the ID of the author you would like to remove from the database: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Deleting pokemon with ID {id}...");
                qb.Delete<Author>(id);
            }
        }

        static void AuthorOperations()
        {
            string rootPath = FileRoot.GetDefaultDirectory();
            string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(databasePath);

            using (qb)
            {
                Console.WriteLine("\n\n===============================================");
                Console.WriteLine("Reading all authors from the database...");
                Console.WriteLine("===============================================");
                var authorListFromDB = qb.ReadAll<Author>();
                foreach (Author author in authorListFromDB)
                {
                    Console.WriteLine(author);
                }

                Console.WriteLine("\n\n===============================================");
                Console.WriteLine("Reading an author based on an Id...");
                Console.WriteLine("===============================================");
                Random rand = new Random();
                int Id = rand.Next(1, 9);
                var authorFromDB = qb.Read<Author>(Id);
                Console.WriteLine(authorFromDB);


                Console.WriteLine("\n\n===============================================");
                Console.WriteLine("Resource Disposed... Ending Program...");
                Console.WriteLine("===============================================");
                qb.Dispose();
            }
        }

        static void AuthorDelete()
        {
             string rootPath = FileRoot.GetDefaultDirectory();
             string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
             QueryBuilder qb = new QueryBuilder(databasePath);

             using (qb)
             {
                 Console.WriteLine("\nPlease enter the ID of the author you would like to remove from the database: ");
                 var id = Convert.ToInt32(Console.ReadLine());
                 Console.WriteLine($"Deleting author with ID {id}...");
                 qb.Delete<Books>(id);
             }
        }

        
    }
}


