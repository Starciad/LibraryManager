namespace LibraryManager
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Type { get; set; }

        public void GetInformations(string bookFile)
        {
            string[] bookInformations = bookFile.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            this.Id = bookInformations[0].Split(':')[1];
            this.Title = bookInformations[1].Split(':')[1];
            this.AuthorName = bookInformations[2].Split(':')[1];
            this.Type = bookInformations[3].Split(':')[1];

            this.Id = this.Id.Substring(0, this.Id.Length - 1);
            this.Title = this.Title.Substring(0, this.Title.Length - 1);
            this.AuthorName = this.AuthorName.Substring(0, this.AuthorName.Length - 1);
            this.Type = this.Type.Substring(0, this.Type.Length - 1);
        }
    }
}
