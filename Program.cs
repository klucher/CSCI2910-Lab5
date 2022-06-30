using CSCI2910_Lab5.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace CSCI2910_Lab5
{
    public class Program
    {
        static void Main(string[] args)
        {
            //AuthorOperations();  
            //BookOperations();
            SerializeAllBooks();
            DeserializeAuthor();

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

        static void AuthorOperations()
        {
            string rootPath = FileRoot.GetDefaultDirectory();
            string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(databasePath);

            using (qb)
            {
                Author a = new Author(99, "Jacob", "Klucher");


                qb.Delete<Author>(99);
                qb.Insert(a);
                a.FirstName = "Jackie";
                a.Surname = "Macon";
                

                Console.WriteLine("\n\n===============================================");
                Console.WriteLine("Reading all authors from the database...");
                Console.WriteLine("===============================================");
                var authorListFromDB = qb.ReadAll<Author>();
                foreach (Author author in authorListFromDB)
                {
                    Console.WriteLine(author);
                }

                qb.Update<Author>(a);

                Console.WriteLine("\n\n===============================================");
                Console.WriteLine("Reading an author based on an Id...");
                Console.WriteLine("===============================================");
                Random rand = new Random();
                int Id = 99;
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

        static void BookOperations()
        {
            string rootPath = FileRoot.GetDefaultDirectory();
            string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(databasePath);

            using (qb)
            {
                Books book = new Books(99, "The Hobbit", "1111111111111", "11/11/1111");
                qb.Delete<Books>(99);
                qb.Insert(book);
                BookAuthors hobbit = new BookAuthors(99, 6);
                qb.Insert(hobbit);
            }
        }

        static void SerializeAllBooks()
        {
            // path to the JSON files
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            
            var outputPath = root + $"{Path.DirectorySeparatorChar}Output";

            var bookList = new List<Books>();

            string rootPath = FileRoot.GetDefaultDirectory();
            string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(databasePath);

            using (qb)
            {
                bookList = qb.ReadAll<Books>();
            }

            // serialize (write) JSON from the object
            var options2 = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(bookList, options2);

            //Console.WriteLine(jsonString);

            // output to JSON file
            using (StreamWriter sw = new StreamWriter(outputPath + $"{Path.DirectorySeparatorChar}books.JSON", false))
            {
                try
                {
                    sw.WriteLine(jsonString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        static void DeserializeAuthor()
        {
            // path to the JSON files
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();

            var outputPath = root + $"{Path.DirectorySeparatorChar}Output";

            string rootPath = FileRoot.GetDefaultDirectory();
            string databasePath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            string fileName = rootPath + $"{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}test.JSON";

            // create options JSONSerializer must follow
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // set JSON string to empty and set up streamreader based on data file's path
            string jsonReadString = string.Empty;
            var authorList = new List<Author>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                jsonReadString = sr.ReadToEnd();
            }

            JArray authorArray = JArray.Parse(jsonReadString);

            foreach (var o in authorArray)
            {
                authorList.Add(JsonConvert.DeserializeObject<Author>(o.ToString()));
            }

            QueryBuilder qb = new QueryBuilder(databasePath);
            using (qb)
            {
                foreach(Author author in authorList)
                {
                    qb.Insert<Author>(author);
                }
            }

        }  
    }
}


