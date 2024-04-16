namespace ClassLibrary
{
    public class AutoSaver 
    {
        private static List<Author> _authors;

        private static DateTime? _lastUpdate = null;

        // Путь, по которому находится изначальный файл.
        private static string _path;

        // Обработчик события.
        public void ObjectChangesEventHandler(object sender, DateChangeTimeArgs e)
        {
            // Дублирует изменение в _authors.
            int indexOfChangedObject = _authors.FindIndex(a => a.authorId == ((Author)sender).authorId);
            _authors![indexOfChangedObject] = (Author)sender;
            // Автосохранение.
            if(!(_lastUpdate is null) && ((DateTime)_lastUpdate - e.ChangeTime).TotalSeconds <= 15)
            {
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Запущено автосохранение, новый файл будет находиться в директории, где находится рабочий файл.");
                Console.WriteLine("После автосохранения, вы сможете увидеть, как выглядит измененный объект, и сможете продолжить работу.");
                Console.ResetColor();
                do
                {
                    Console.WriteLine($"{Environment.NewLine}Для продолжения нажмите ENTER");
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
                WriteJson.WriteJsonAndGetPathOrNot(_path, false, _authors);
            }
            _lastUpdate = e.ChangeTime;
        }
        
        public AutoSaver(List<Author> authors, string path) 
        {
            _authors = authors; 
            _path = path;
        }
        public AutoSaver() { }
    }
}
