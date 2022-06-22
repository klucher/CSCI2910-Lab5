namespace CSCI2910_Lab5
{
    internal class FileRoot
    {
        public static string GetDefaultDirectory()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
        }
    }
}
