namespace ClassLibrary
{
    public static class Menu
    {
        /// <summary>
        /// Реализация меню.
        /// </summary>
        /// <param name="menuItems"> Массив с пунктами меню. </param>
        /// <param name="question"> Строка для вывода пользователю. </param>
        /// <returns> Индекс выбранного пункта. </returns>
        public static int CreateMenu(string[] menuItems, string question)
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("| Выберите с помощью стрелок пункт меню и нажмите ENTER. |");
            Console.WriteLine($"----------------------------------------------------------{Environment.NewLine}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(question + Environment.NewLine);
            Console.ResetColor();
            // Получаем текущую позицию курсора.
            int row = Console.CursorTop;
            int col = Console.CursorLeft;
            int index = 0;
            while (true)
            {
                VisualizationMenu(menuItems, row, col, index);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < menuItems.Length - 1)
                            index++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                            index--;
                        break;
                    case ConsoleKey.Enter:
                        return index;
                }
            }
        }
        /// <summary>
        /// Визуализация меню.
        /// </summary>
        /// <param name="items"> Массив из строк, которые являются пунктами меню. </param>
        /// <param name="index"> На каком элементе пользователь. </param>
        private static void VisualizationMenu(string[] items, int row, int col, int index)
        {
            // "Фиксируем" курсор.
            Console.SetCursorPosition(col, row);
            for (int i = 0; i < items.Length; i++)
            {
                // Для элемента, который выбрал пользователь с помощью стрелок, меняем цвет.
                if (i == index)
                {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(items[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
