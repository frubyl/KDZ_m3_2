using ClassLibrary;
class Program
{
    static void Main(string[] args)
    {
        string[] menuItems =
            {
            "Ввести данные",
            "Отсортировать данные по одному из полей",
            "Изменить данные объекта",
            "Вывести данные в System.Console",
            "Сохраните данные",
            "Выйти из программы"
            };
        int menuItemIndex = Menu.CreateMenu(menuItems, "Чтобы проводить операции над данными, введите их, выбрав первый пункт меню");
        List<Author> authors = new List<Author>();
        string path = "";
        AutoSaver saver;
        // При первом выводе меню обязательно получаем данные.
        switch (menuItemIndex)
        {
            case 0:
                authors = ReadJson.GetData(out path);
                saver = new AutoSaver(authors, path);
                Methods.LinkRecalculationEarnings(authors);
                Methods.LinkAutosaver(authors, saver);
                break;
            // Если выбран любой другой пункт, оповещаем пользователя, что нужно ввести данные.
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Данные не введены, сейчас Вам предложат ввести данные.");
                Console.ResetColor();
                Thread.Sleep(2500);
                authors = ReadJson.GetData(out path);
                saver = new AutoSaver(authors, path);
                Methods.LinkAutosaver(authors, saver);
                Methods.LinkRecalculationEarnings(authors);
                break;
        };
        do
        {
            menuItemIndex = Menu.CreateMenu(menuItems, "Для вывода результата выберите соответствующий пункт меню после операции над данными.");
            switch (menuItemIndex)
            {
                case 0:
                    authors = ReadJson.GetData(out path);
                    saver = new AutoSaver(authors, path);
                    Methods.LinkRecalculationEarnings(authors);
                    Methods.LinkAutosaver(authors, saver);
                    break;
                case 1:
                    authors = Sorting.Sort(authors);
                    Console.WriteLine("Данные отсортированы!");
                    Thread.Sleep(2500);
                    break;
                case 2:
                    authors = ChangeField.ChangeAndEditAuthor(authors);
                    break;
                case 3:
                    Methods.PrintAllList(authors);
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{Environment.NewLine}Для продолжения нажмите ENTER");
                        Console.ResetColor();
                    } while (Console.ReadKey().Key != ConsoleKey.Enter);
                    break;
                case 4:
                    WriteJson.WriteJsonAndGetPathOrNot(path, true, authors);
                    break;
                case 5: return;
            }
        } while (true);
    }
}


 