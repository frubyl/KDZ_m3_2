namespace ClassLibrary
{
    public static class WriteJson
    {
        /// <summary>
        /// Запись файла JSON.
        /// </summary>
        /// <param name="path"> Путь, по которому нужно сохранить файл. </param>
        /// <param name="needNewPath"> Нужно лии получить новый путь. </param>
        /// <param name="authors"> Лист для записи. </param>
        public static void WriteJsonAndGetPathOrNot(string path, bool needNewPath, List<Author> authors)
        {
            do
            {
                Console.Clear();
                // Если нужен новый путь, получаем его.
                if (needNewPath)
                {
                    path = GetAndCheckPath();
                    path += ".json";
                }
                // Если новый путь не нужен, используем переданный.
                else
                {
                    // Убираем из абсолютного пути название файла, и получаем директорию для записи через автосохранение.
                    char sep = Path.DirectorySeparatorChar;
                    string[] pathArr = path.Split(sep);
                    string[] newFileName = pathArr[pathArr.Length-1].Split('.');  
                    newFileName[newFileName.Length - 2] += "_tmp";
                    Array.Resize(ref pathArr, pathArr.Length - 1);
                    path = String.Join(sep, pathArr);
                    path += sep + String.Join('.', newFileName);

                }
                var consoleOutputEncoding = Console.OutputEncoding;
                try
                {
                    // Меняем поток вывода и записываем данные.
                    using StreamWriter writer = new StreamWriter(path);
                    Console.SetOut(writer);
                    Methods.PrintAllList(authors);
                    // Возвращаем стандартный поток вывода.
                    StreamWriter streamConsole = new StreamWriter(Console.OpenStandardOutput(), consoleOutputEncoding);
                    Console.SetOut(streamConsole);
                    streamConsole.AutoFlush = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Environment.NewLine}Данные успешно записаны!");
                    Console.ResetColor();
                    Thread.Sleep(2500);
                    return;
                }
                catch (Exception e)
                {
                    // Если возникла ошибка во время исполнения, также меняем вывод на стандартный.
                    StreamWriter streamConsole = new StreamWriter(Console.OpenStandardOutput(), consoleOutputEncoding);
                    Console.SetOut(streamConsole);
                    streamConsole.AutoFlush = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Возникла ошибка, повторите попытку!");
                    Console.ResetColor();
                    Thread.Sleep(3000);
                    continue;
                }
            } while (true);
        }
        /// <summary>
        /// Получение абсолютного пути к файлу.
        /// </summary>
        /// <returns> Абсолютный путь. </returns>
        private static string GetAndCheckPath()
        {
            do
            {
                // Получаем директорию, где находится файл, и проверяем, существует ли она.
                Console.Clear();
                Console.WriteLine($"{Environment.NewLine}Введите директорию, где хотите сохранить файл.{Environment.NewLine}Пример для Windows: C:\\Users\\frubyl\\Desktop\\KDZ\\ClassLibrary{Environment.NewLine}");
                string path = Console.ReadLine();
                path = String.IsNullOrEmpty(path) ? "empty" : path;
                DirectoryInfo di = new DirectoryInfo(path);
                // Если не существует, оповещаем пользователя, запрашиваем директорию еще раз.
                if (!di.Exists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такой директории не существует, повторите попытку!");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                }
                // Если существует, запрашиваем имя файла, формируем абсолютный путь.
                else
                {
                    string fileName = GetAndCheckFileName();
                    path = path + Path.DirectorySeparatorChar + fileName;
                    return path;
                }
            } while (true);
        }
        /// <summary>
        /// Получаем и проверяем имя файла.
        /// </summary>
        /// <returns> Имя файла. </returns>
        private static string GetAndCheckFileName()
        {
            do
            {
                Console.WriteLine($"{Environment.NewLine}Введите имя файла без расширения. Пример: name");
                string fileName = Console.ReadLine();
                if (!ValidateFileName(fileName))
                {
                    Console.WriteLine("Недопустимое имя файла, повторите попытку!");
                    continue;
                }
                else { return fileName; }
            } while (true);
        }

        /// <summary>
        /// Проверяем имя файла.
        /// </summary>
        /// <param name="filename"> Имя файла.</param>
        /// <returns> true, если имя корректно, false иначе.</returns>
        private static bool ValidateFileName(string filename)
        {
            try
            {
                FileStream fs = File.Open(filename, FileMode.Open);
                if (fs != null) fs.Close();
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }
    }
}


  
