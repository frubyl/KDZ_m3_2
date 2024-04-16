using System.Text.Json;
namespace ClassLibrary
{
    public static class ReadJson
    {
        /// <summary>
        /// Получение данных из файла.
        /// </summary>
        /// <param name="path"> Путь, где лежит считанный файл. </param>
        /// <returns> Лист прочитанных объектов. </returns>
        public static List<Author> GetData(out string path)
        {
            path = "";
            do
            {
                try
                {
                    path = GetPath();
                    string jsonString = File.ReadAllText(path);
                    var objects = JsonSerializer.Deserialize<List<Author>>(jsonString);
                    // Если нет подходящих объектов, запрашиваем данные еще раз.
                    if (objects.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{Environment.NewLine}Нет подходящих объектов, повторите попытку!");
                        Console.ResetColor();
                        Thread.Sleep(2500);
                        continue;
                    }
                    // Если есть объекты с пустыми полями, выбрасываем исключение.
                    for (int i = 0; i < objects.Count; i++)
                    {
                        if (objects[i].CheckNullObjectAndValue())
                        {
                            throw new FormatException();
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Environment.NewLine}Данные прочитаны!");
                    Console.ResetColor();
                    Thread.Sleep(2500);
                    return objects;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Environment.NewLine}Данные некорректны, проверьте корректность данных и повторите попытку!");
                    Console.ResetColor();
                    Thread.Sleep(2500);
                    continue;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Environment.NewLine}Возникла ошибка, проверьте корректность данных и повторите попытку!");
                    Console.ResetColor();
                    Thread.Sleep(2500);
                    continue;
                }
            }while (true);
        }
        /// <summary>
        /// Получение абсолютного пути к файлу.
        /// </summary>
        /// <returns> Путь к файлу. </returns>
        private static string GetPath()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Введите путь к файлу: ");
                string path = Console.ReadLine();
                if (!File.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Environment.NewLine}Файл не найден, повторите попытку!");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    continue;
                }
                return path;
            }while (true);
        }
    }
}
