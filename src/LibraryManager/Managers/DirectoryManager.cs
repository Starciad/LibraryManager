namespace LibraryManager.Managers
{
    public static class DirectoryManager
    {
        public static string ProgramDirectory { get; private set; }
        public static string BooksDirectory { get; private set; }

        public static void Start()
        {
            ProgramDirectory = Directory.GetCurrentDirectory();
            BooksDirectory = Directory.CreateDirectory(Path.Combine(ProgramDirectory, "Books")).FullName;
        }
    }
}
