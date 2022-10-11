using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManager
{
    public class Startup
    {
        private bool errorOccurred;
        private string currentDirectory;

        public void Main()
        {
            errorOccurred = false;
            currentDirectory = Directory.GetCurrentDirectory();

            Directory.CreateDirectory(Path.Combine(currentDirectory, "Books"));

            ShowMainMenu();
        }

        //===============================//
        private void ShowMainMenu()
        {
            StringBuilder mainMenuContent = new();

            ErrorMessage(mainMenuContent);
            TitleMessage(mainMenuContent, "BIBLIOTECA");

            mainMenuContent.AppendLine("SELECIONE UMA OPÇÃO ABAIXO:");
            mainMenuContent.AppendLine("1) Pesquisar Ficheiros - Pesquise por ficheiros");
            mainMenuContent.AppendLine("2) Editar Ficheiros - Adicionar ou remover ficheiros");

            mainMenuContent.AppendLine("\nDIGITE UM NÚMERO: ");

            Console.Clear();
            Console.Write(mainMenuContent.ToString());

            try
            {
                uint option = uint.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        ShowBooksMenu();
                        break;

                    case 2:
                        ShowEditBookMenu();
                        break;

                    default:
                        ShowMainMenu();
                        break;
                }

                errorOccurred = false;
            }
            catch (Exception)
            {
                errorOccurred = true;
                ShowMainMenu();
            }
        }

        //===============================//
        private void ShowBooksMenu()
        {
            Book[] books = GetBooks();

            StringBuilder booksMenu = new();
            ErrorMessage(booksMenu);
            TitleMessage(booksMenu, "LIVROS REGISTRADOS");

            booksMenu.AppendLine($"LIVROS: ({books.Length})");
            if(books.Length > 0)
            {
                for (int i = 0; i < books.Length; i++)
                {
                    booksMenu.AppendLine($"{i}) {books[i].Title}");
                }
            }
            else
            {
                booksMenu.AppendLine($"Nenhum livro registrado!\n");
            }

            booksMenu.AppendLine($"{books.Length}) Sair\n");
            booksMenu.AppendLine($"DIGITE UM NÚMERO: ");

            Console.Clear();
            Console.WriteLine(booksMenu.ToString());

            try
            {
                uint option = uint.Parse(Console.ReadLine());
                Book selectedBook = books.ElementAtOrDefault((int)option);
                if(selectedBook != null)
                {
                    errorOccurred = false;
                    BookDetails(selectedBook);
                    return;
                }
                else if(option == books.Length)
                {
                    errorOccurred = false;
                    ShowMainMenu();
                    return;
                }
                else
                {
                    errorOccurred = true;
                    ShowBooksMenu();
                    return;
                }
            }
            catch (Exception)
            {
                errorOccurred = true;
                ShowBooksMenu();
            }
        }
        private void BookDetails(Book book)
        {
            StringBuilder booksMenu = new();
            ErrorMessage(booksMenu);
            TitleMessage(booksMenu, "DETALHES DO LIVRO");

            booksMenu.AppendLine($"- Id: {book.Id}");
            booksMenu.AppendLine($"- Título: {book.Title}");
            booksMenu.AppendLine($"- Author: {book.AuthorName}");
            booksMenu.AppendLine($"- Tipo: {book.Type}");

            booksMenu.AppendLine($"\nSELECIONE UMA OPÇÃO ABAIXO: ");
            booksMenu.AppendLine($"1) Sair\n");

            booksMenu.AppendLine($"DIGITE UM NÚMERO: ");

            Console.Clear();
            Console.Write(booksMenu.ToString());

            try
            {
                uint option = uint.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        ShowBooksMenu();
                        break;

                    default:
                        BookDetails(book);
                        break;
                }

                errorOccurred = false;
            }
            catch (Exception)
            {
                errorOccurred = true;
                ShowMainMenu();
            }
        }

        //===============================//
        private void ShowEditBookMenu()
        {
            StringBuilder editBooksMenu = new();
            ErrorMessage(editBooksMenu);
            TitleMessage(editBooksMenu, "EDIÇÃO DE FICHEIROS");

            editBooksMenu.AppendLine("SELECIONE UMA OPÇÃO ABAIXO:");
            editBooksMenu.AppendLine("1) Adicionar - Adicione um ficheiro");
            editBooksMenu.AppendLine("2) Remover - Remova um ficheiro");
            editBooksMenu.AppendLine("3) Sair\n");

            editBooksMenu.AppendLine($"DIGITE UM NÚMERO: ");

            Console.Clear();
            Console.WriteLine(editBooksMenu.ToString());

            try
            {
                uint option = uint.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        AddBookMenu();
                        break;

                    case 2:
                        RemoveBookMenu();
                        break;

                    case 3:
                        ShowMainMenu();
                        break;

                    default:
                        ShowEditBookMenu();
                        break;
                }

                errorOccurred = false;
            }
            catch (Exception)
            {
                errorOccurred = true;
                ShowEditBookMenu();
            }
        }
        private void AddBookMenu()
        {
            StringBuilder editBooksMenu = new();
            ErrorMessage(editBooksMenu);
            TitleMessage(editBooksMenu, "ADICIONAR UM FICHEIRO");

            Book[] books = GetBooks();

            string id = string.Empty;
            string title = string.Empty;
            string authorName = string.Empty;
            string type = string.Empty;

            do
            {
                Console.WriteLine("\nDigite o ID do livro: ");
                id = Console.ReadLine();
            } while (Array.Find(books, x => x.Id == id) != null);


            Console.WriteLine("\nDigite o TÍTULO do livro: ");
            title = Console.ReadLine();

            Console.WriteLine("\nDigite o NOME DO AUTHOR do livro: ");
            authorName = Console.ReadLine();

            Console.WriteLine("\nDigite o TIPO do livro: ");
            type = Console.ReadLine();

            try
            {
                CreateBook(id, title, authorName, type);
                Console.WriteLine("\nLIVRO CRIADO COM SUCESSO!");
            }
            catch (Exception)
            {
                Console.WriteLine("\nCRIAÇÃO DO LIVRO FALHOU!");
            }

            ShowMainMenu();
        }
        private void RemoveBookMenu()
        {
            Book[] books = GetBooks();

            StringBuilder editBooksMenu = new();
            ErrorMessage(editBooksMenu);
            TitleMessage(editBooksMenu, "ADICIONAR UM FICHEIRO");

            editBooksMenu.AppendLine("SELECIONE UM LIVRO ABAIXO PARA REMOVER:");
            for (int i = 0; i < books.Length; i++)
            {
                editBooksMenu.AppendLine($"{i}) {books[i].Title}");
            }

            editBooksMenu.AppendLine($"{books.Length}) Sair");
            editBooksMenu.AppendLine($"DIGITE UM NÚMERO: ");

            Console.Clear();
            Console.Write(editBooksMenu.ToString());

            try
            {
                uint option = uint.Parse(Console.ReadLine());
                Book selectedBook = books.ElementAtOrDefault((int)option);
                if (selectedBook != null)
                {
                    errorOccurred = false;
                    DeleteBook(selectedBook);
                }

                ShowMainMenu();
            }
            catch (Exception)
            {
                errorOccurred = true;
                ShowBooksMenu();
            }
        }

        //===============================//
        private void TitleMessage(StringBuilder builder, string message)
        {
            builder.AppendLine(new string('=', 20) + "\n");
            builder.AppendLine($"{message}\n");
            builder.AppendLine(new string('=', 20) + "\n");
        }
        private void ErrorMessage(StringBuilder builder)
        {
            if (errorOccurred)
            {
                builder.AppendLine("OH NÃO, ALGO DEU ERRADO!");
                builder.AppendLine("Causas do erro:\n" +
                                   "- Digitou caracteres que não são números\n" +
                                   "- Digitou um menu inexistente.\n\n");
            }
        }

        private void CreateBook(string id, string title, string author, string type)
        {
            using StreamWriter bookStream = new(Path.Combine(currentDirectory, "Books", $"{id}.txt"));
            bookStream.WriteLine($"id:{id}");
            bookStream.WriteLine($"title:{title}");
            bookStream.WriteLine($"author:{author}");
            bookStream.WriteLine($"type:{type}");
            bookStream.Close();
        }
        private void DeleteBook(Book book)
        {
            File.Delete(Path.Combine(currentDirectory, "Books", $"{book.Id}.txt"));
        }
        private Book[] GetBooks()
        {
            string[] booksPaths = Directory.GetFiles(Path.Combine(currentDirectory, "Books"), "*.txt");
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
