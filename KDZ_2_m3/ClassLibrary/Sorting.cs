namespace ClassLibrary
{
    public static class Sorting
    {
        /// <summary>
        ///  Сортировка.
        /// </summary>
        /// <param name="authors"> Лист объектов для сортировки. </param>
        /// <returns> Отсортированный лист. </returns>
        public static List<Author> Sort(List<Author> authors)
        {
            // Получаем поле для сортировки.
            string[] menuItems =
            {
                "authorId",
                "name",
                "earnings",
            };
            int numberOfMenuItem = Menu.CreateMenu(menuItems, "Выберите поле для сортировки");
            // Получаем тип сортировки.
            string[] typeOfSort =
            {
                "По возрастанию",
                "По убыванию"
            };
            int numberOFTypeOfSort = Menu.CreateMenu(typeOfSort, "Выберите тип сортировки");
            // Сортировка по возрастанию.
            switch(numberOfMenuItem) 
            {
                case 0:
                    var newList = from a in authors orderby a.authorId ascending select a;
                    authors = new List<Author>(newList);
                    break;
                case 1:
                    newList = from a in authors orderby a.NameChange ascending select a;
                    authors = new List<Author>(newList);
                    break;
                case 2:
                    newList = from a in authors orderby a.EarningsChange ascending select a;
                    authors = new List<Author>(newList);
                    break;
            }
            // Если сортировка по убыванию, используем реверс.
            if(numberOFTypeOfSort == 1)
            {
                authors.Reverse();
            }
            return authors;
        }
    }
}
