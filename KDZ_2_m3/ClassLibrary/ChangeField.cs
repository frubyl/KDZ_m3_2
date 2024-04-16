namespace ClassLibrary
{
    public static class ChangeField
    {
        /// <summary>
        /// Выбор автора и редактирование поля.
        /// </summary>
        /// <param name="authors"> Лист, из которого нужно выбрать автора. </param>
        /// <returns> Лист, где у выбранного автора было изменено поле. </returns>
        public static List<Author> ChangeAndEditAuthor(List<Author> authors)
        {
            // Просмотр всех авторов.
            Methods.PrintAllList(authors);
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Environment.NewLine}Когда будете готовы выбрать автора по ID, нажмите ENTER");
                Console.ResetColor();
            } while (Console.ReadKey().Key != ConsoleKey.Enter);
            // Меню для выбора автора.
            string[] menuItems = new string[authors.Count];
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i] = authors[i].authorId;
            }
            int menuIndex = Menu.CreateMenu(menuItems, "Выберите автора для редактирования");
            // Редактируем выбранного автора.
            authors[menuIndex] = EditAuthor(authors[menuIndex]);
            return authors;

        }
        /// <summary>
        /// Редактирование поля у автора.
        /// </summary>
        /// <param name="author"> Автор, у которого нужно отредактировать поле. </param>
        /// <returns> Автор с измененным полем. </returns>
        private static Author EditAuthor(Author author)
        {
            // Выбор поля дле редактирования.
            string[] menuItems =
            {
                "name",
                "books"
            };
            int menuIndex = Menu.CreateMenu(menuItems, "Выберите поле для редактирования");
            // Редактирование автора.
            switch (menuIndex)
            {
                case 0:
                    author.NameChange = GetNewFieldValue(2);
                    break;
                case 1:
                    author.BooksChange = ChangeAndEditBook(author.BooksChange);
                    break;
            }
            // Выводим результат изменения, только если оно было.
            if (!(menuIndex == 1 & author.books.Length == 0)) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Теперь Ваш объект выглядит так:");
                Console.ResetColor();
                author.ToJSON();
                do
                {
                    Console.WriteLine("Для продолжения нажмите ENTER");
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
            }
            return author;
        }

        /// <summary>
        /// Выбор книги для редактирования.
        /// </summary>
        /// <param name="books"> Массив книг, из которого нужно выбрать книгу. </param>
        /// <returns> Массив книг, где выбранная книга отредактирована. </returns>
        private static Book[] ChangeAndEditBook(Book[] books)
        {
            Console.Clear();
            if (books.Length != 0)
            {
                // Просмотр всех книг.
                Methods.PrintAuthorsBook(books);
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Environment.NewLine}Когда будете готовы выбрать книгу по ID, нажмите ENTER");
                    Console.ResetColor();
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
                // Выбор книги по id.
                string[] menuItems = new string[books.Length];
                for (int i = 0; i < books.Length; i++)
                {
                    menuItems[i] = books[i].bookId;
                }
                int menuIndex = Menu.CreateMenu(menuItems, "Выберите книгу для редактирования");
                books[menuIndex] = EditBook(books[menuIndex]);
                return books;
            }
            // Если у автора нет книг, ничего не меняем.
            else
            {
                Console.WriteLine("У автора нет книг!");
                Thread.Sleep(1500);
                return books;
            }
        }

        /// <summary>
        /// Замена значения поля у книги.
        /// </summary>
        /// <param name="book"> Книга, в котрой меняем поле. </param>
        /// <returns> Книга с измененным полем. </returns>
        private static Book EditBook(Book book)
        {
            string[] menuItems =
            {
            "title",
            "publicationYear", // Нужна проверка на int.
            "genre",
            "earnings" // Нужна проверка на double.
            };
            int menuIndex = Menu.CreateMenu(menuItems, "Выберите поле для редактирования");
            switch (menuIndex)
            {
                case 0:
                    book.TitleChange = GetNewFieldValue(2);
                    break;
                case 1:
                    book.PublicationYearChange = int.Parse(GetNewFieldValue(0));
                    break;
                case 2:
                    book.GenreChange = GetNewFieldValue(2);
                    break;
                case 3:
                    book.EarningsChange = double.Parse(GetNewFieldValue(1));
                    break;
            }
            return book;
        }
        /// <summary>
        /// Получение нового значения для поля с проверкой введенного значения.
        /// </summary>
        /// <param name="numOfCheck"> Номер проверки. </param>
        /// <returns> Строку с допустимым значением. </returns>
        private static string GetNewFieldValue(int numOfCheck)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Введите новое значение: ");
                string value = Console.ReadLine();
                bool flag = true;
                switch (numOfCheck)
                {
                    // Проверка int значения - year.
                    case 0: 
                        if (!int.TryParse(value, out int intValue) || intValue <= 0)
                        {
                            flag = false;
                        }
                        break;
                    // Проверка double значения - earnings.
                    case 1: 
                        if (!double.TryParse(value, out double doubleValue) || doubleValue <= 0)
                        {
                            flag = false;
                        }
                        break;
                }
                // Если недопустимое значение, запрашиваем значение еще раз.
                if (!flag)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Недопустимое значение, повторите попытку!");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    continue;
                }
                return value;
            } while (true);
        }
    }
}
