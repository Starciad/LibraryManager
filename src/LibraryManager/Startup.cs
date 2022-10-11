using System.Text;

using LibraryManager.Managers;

namespace LibraryManager
{
    public class Startup
    {
        private bool errorOccurred;

        public void Main()
        {
            DirectoryManager.Start();

            this.errorOccurred = false;
            this.ShowMainMenu();
        }

        //===============================//
        // MENU PRÍNCIPAL
        private void ShowMainMenu()
        {
            // StringBuilder usada para construir o menu
            StringBuilder mainMenuContent = new();

            this.ErrorMessage(mainMenuContent);
            this.TitleMessage(mainMenuContent, "BIBLIOTECA");

            mainMenuContent.AppendLine("SELECIONE UMA OPÇÃO ABAIXO:");
            mainMenuContent.AppendLine("1) Pesquisar Ficheiros - Pesquise por ficheiros");
            mainMenuContent.AppendLine("2) Editar Ficheiros - Adicionar ou remover ficheiros");
            mainMenuContent.AppendLine("\nDIGITE UM NÚMERO: ");

            //Apagar e escrever a String na tela
            Console.Clear();
            Console.Write(mainMenuContent.ToString());

            //Try & Catch de Opções
            try
            {
                //Dicionario de Opções
                Dictionary<uint, Action> menuOptions = new()
                {
                    [1] = new Action(this.ShowBooksMenu),
                    [2] = new Action(this.ShowEditBookMenu),
                    [3] = new Action(this.ShowMainMenu)
                };

                menuOptions[uint.Parse(Console.ReadLine())].Invoke();
                this.errorOccurred = false;
            }
            catch (Exception)
            {
                this.errorOccurred = true;
                this.ShowMainMenu();
            }
        }

        //===============================//
        // MENUS DE MOSTRAR LIVROS
        private void ShowBooksMenu()
        {
            Book[] books = BooksManager.GetBooks();

            StringBuilder booksMenu = new();
            this.ErrorMessage(booksMenu);
            this.TitleMessage(booksMenu, "LIVROS REGISTRADOS");

            booksMenu.Append("LIVROS: (").Append(books.Length).AppendLine(")");
            if (books.Length > 0)
            {
                for (int i = 0; i < books.Length; i++)
                {
                    booksMenu.Append(i).Append(") ").AppendLine(books[i].Title);
                }
            }
            else
            {
                booksMenu.AppendLine("Nenhum livro registrado!\n");
            }

            booksMenu.Append(books.Length).AppendLine(") Sair\n");
            booksMenu.AppendLine("DIGITE UM NÚMERO: ");

            //Exibir String no Console
            Console.Clear();
            Console.WriteLine(booksMenu.ToString());

            //Try & Catch de Opções
            try
            {
                uint option = uint.Parse(Console.ReadLine());
                Book selectedBook = books.ElementAtOrDefault((int)option);

                this.errorOccurred = false;

                if (selectedBook != null)
                {
                    this.BookDetails(selectedBook);
                    return;
                }

                if (option == books.Length)
                {
                    this.ShowMainMenu();
                    return;
                }

                this.errorOccurred = true;
                this.ShowBooksMenu();
                return;
            }
            catch (Exception)
            {
                this.errorOccurred = true;
                this.ShowBooksMenu();
            }
        }
        private void BookDetails(Book book)
        {
            StringBuilder booksMenu = new();
            this.ErrorMessage(booksMenu);
            this.TitleMessage(booksMenu, "DETALHES DO LIVRO");

            booksMenu.Append("- Id: ").AppendLine(book.Id);
            booksMenu.Append("- Título: ").AppendLine(book.Title);
            booksMenu.Append("- Author: ").AppendLine(book.AuthorName);
            booksMenu.Append("- Tipo: ").AppendLine(book.Type);

            booksMenu.AppendLine("\nSELECIONE UMA OPÇÃO ABAIXO: ");
            booksMenu.AppendLine("1) Sair\n");

            booksMenu.AppendLine("DIGITE UM NÚMERO: ");

            //Exibir String no Console
            Console.Clear();
            Console.Write(booksMenu.ToString());

            //Try & Catch de Opções
            try
            {
                switch (uint.Parse(Console.ReadLine()))
                {
                    case 1:
                        this.ShowBooksMenu();
                        break;

                    default:
                        this.BookDetails(book);
                        break;
                }

                this.errorOccurred = false;
            }
            catch (Exception)
            {
                this.errorOccurred = true;
                this.ShowMainMenu();
            }
        }

        //===============================//
        // MENUS DE EDIÇÃO DOS LIVROS
        private void ShowEditBookMenu()
        {
            StringBuilder editBooksMenu = new();
            this.ErrorMessage(editBooksMenu);
            this.TitleMessage(editBooksMenu, "EDIÇÃO DE FICHEIROS");

            editBooksMenu.AppendLine("SELECIONE UMA OPÇÃO ABAIXO:");
            editBooksMenu.AppendLine("1) Adicionar - Adicione um ficheiro");
            editBooksMenu.AppendLine("2) Remover - Remova um ficheiro");
            editBooksMenu.AppendLine("3) Sair\n");

            editBooksMenu.AppendLine("DIGITE UM NÚMERO: ");

            //Exibir String no Console
            Console.Clear();
            Console.WriteLine(editBooksMenu.ToString());

            //Try & Catch de Opções
            try
            {
                switch (uint.Parse(Console.ReadLine()))
                {
                    case 1:
                        this.AddBookMenu();
                        break;

                    case 2:
                        this.RemoveBookMenu();
                        break;

                    case 3:
                        this.ShowMainMenu();
                        break;

                    default:
                        this.ShowEditBookMenu();
                        break;
                }

                this.errorOccurred = false;
            }
            catch (Exception)
            {
                this.errorOccurred = true;
                this.ShowEditBookMenu();
            }
        }
        private void AddBookMenu()
        {
            StringBuilder editBooksMenu = new();
            this.ErrorMessage(editBooksMenu);
            this.TitleMessage(editBooksMenu, "ADICIONAR UM FICHEIRO");

            Book[] books = BooksManager.GetBooks();
            string id = string.Empty;
            string title = string.Empty;
            string authorName = string.Empty;
            string type = string.Empty;

            //Exibir String no Console
            Console.Clear();
            Console.Write(editBooksMenu.ToString());

            do
            {
                Console.WriteLine("\nDigite o ID do livro: (Deve ser único)");
                id = Console.ReadLine();
            } while (Array.Find(books, x => x.Id == id) != null);

            Console.WriteLine("\nDigite o TÍTULO do livro: ");
            title = Console.ReadLine();

            Console.WriteLine("\nDigite o NOME DO AUTHOR do livro: ");
            authorName = Console.ReadLine();

            Console.WriteLine("\nDigite o TIPO do livro: ");
            type = Console.ReadLine();

            //Try & Catch de Criação
            try
            {
                BooksManager.CreateBook(id, title, authorName, type);
                Console.WriteLine("\nLIVRO CRIADO COM SUCESSO!");
            }
            catch (Exception)
            {
                Console.WriteLine("\nCRIAÇÃO DO LIVRO FALHOU!");
            }

            this.ShowMainMenu();
        }
        private void RemoveBookMenu()
        {
            Book[] books = BooksManager.GetBooks();

            StringBuilder editBooksMenu = new();
            this.ErrorMessage(editBooksMenu);
            this.TitleMessage(editBooksMenu, "ADICIONAR UM FICHEIRO");

            editBooksMenu.AppendLine("SELECIONE UM LIVRO ABAIXO PARA REMOVER:");
            for (int i = 0; i < books.Length; i++)
            {
                editBooksMenu.Append(i).Append(") ").AppendLine(books[i].Title);
            }

            editBooksMenu.Append(books.Length).AppendLine(") Sair");
            editBooksMenu.AppendLine("DIGITE UM NÚMERO: ");

            //Exibir String no Console
            Console.Clear();
            Console.Write(editBooksMenu.ToString());

            //Try & Catch de Opções
            try
            {
                uint option = uint.Parse(Console.ReadLine());
                Book selectedBook = books.ElementAtOrDefault((int)option);
                if (selectedBook != null)
                {
                    this.errorOccurred = false;
                    BooksManager.DeleteBook(selectedBook);
                }

                this.ShowMainMenu();
            }
            catch (Exception)
            {
                this.errorOccurred = true;
                this.ShowBooksMenu();
            }
        }

        //===============================//
        // MÉTODOS AUXILIARES
        private void TitleMessage(StringBuilder builder, string message)
        {
            builder.Append(new string('=', 20)).AppendLine("\n");
            builder.Append(message).AppendLine("\n");
            builder.Append(new string('=', 20)).AppendLine("\n");
        }
        private void ErrorMessage(StringBuilder builder)
        {
            if (this.errorOccurred)
            {
                builder.AppendLine("OH NÃO, ALGO DEU ERRADO!");
                builder.AppendLine("Causas do erro:\n" +
                                   "- Digitou caracteres que não são números\n" +
                                   "- Digitou um menu inexistente.\n\n");
            }
        }
    }
}
