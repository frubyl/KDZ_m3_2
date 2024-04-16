namespace ClassLibrary
{
    public static class Methods
    {
        /// <summary>
        /// Подписываем RecalculationEarnings у каждого автора на событие "Изменение дохода книги" каждой книги.
        /// </summary>
        /// <param name="authors"></param>
        public static void LinkRecalculationEarnings(List<Author> authors)
        {
            foreach (var author in authors) 
            { 
                foreach(var book in author.BooksChange)
                {
                    book.EarningsChanged += author.RecalculationEarnings;
                }
            }
        }
        /// <summary>
        /// Подписываем ObjectChangesEventHandler на событие "Изменение данных объекта".
        /// </summary>
        /// <param name="authors"></param>
        /// <param name="autosaver"></param>
        public static void LinkAutosaver(List<Author> authors, AutoSaver autosaver)
        {
            foreach (var author in authors)
            {
                author.Updated += autosaver.ObjectChangesEventHandler;
            }
        }
        /// <summary>
        /// Вывод объектов в формате JSON.
        /// </summary>
        /// <param name="list"> Лист для вывода. </param>
        public static void PrintAllList(List<Author> list)
        {
            Console.Clear();
            Console.WriteLine("[");
            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i].ToJSON();
                Console.WriteLine(",");
            }
            list[list.Count - 1].ToJSON();
            Console.WriteLine("]");
        }
        /// <summary>
        /// Вывод массива книг.
        /// </summary>
        /// <param name="books"> Массив для вывода. </param>
        public static void PrintAuthorsBook(Book[] books)
        {
            for (int i = 0; i < books.Length; i++)
            {
                books[i].ToJSON();
            }
        }
    }
}