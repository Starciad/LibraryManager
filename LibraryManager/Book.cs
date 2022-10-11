using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Id = bookInformations[0].Split(':')[1];
            Title = bookInformations[1].Split(':')[1];
            AuthorName = bookInformations[2].Split(':')[1];
            Type = bookInformations[3].Split(':')[1];

            Id = Id.Substring(0, Id.Length - 1);
            Title = Title.Substring(0, Title.Length - 1);
            AuthorName = AuthorName.Substring(0, AuthorName.Length - 1);
            Type = Type.Substring(0, Type.Length - 1);
        }
    }
}
