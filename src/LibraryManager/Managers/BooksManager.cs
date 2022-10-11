namespace LibraryManager.Managers
{
    public static class BooksManager
    {
        public static void CreateBook(string id, string title, string author, string type)
        {
            using StreamWriter bookStream = new(Path.Combine(DirectoryManager.BooksDirectory, $"{id}.txt"));
            bookStream.WriteLine($"id:{id}");
            bookStream.WriteLine($"title:{title}");
            bookStream.WriteLine($"author:{author}");
            bookStream.WriteLine($"type:{type}");
            bookStream.Close();
        }
        public static void DeleteBook(Book book) => File.Delete(Path.Combine(DirectoryManager.BooksDirectory, $"{book.Id}.txt"));
        public static Book[] GetBooks()
        {
            string[] booksPaths = Directory.GetFiles(Path.Combine(DirectoryManager.BooksDirectory), "*.txt");
            Book[] books = new Book[booksPaths.Length];

            for (int i = 0; i < booksPaths.Length; i++)
            {
                books[i] = new Book();
                books[i].GetInformations(File.ReadAllText(booksPaths[i]));
            }

            return books;
        }
    }
}
