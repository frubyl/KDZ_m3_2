using System.Text.Json.Serialization;
namespace ClassLibrary
{
    public delegate void EarningsChangeHandler();
    public class Book
    {
        private readonly string? _bookId;

        private readonly string? _title;

        private readonly int? _publicationYear;

        private readonly string? _genre;

        private readonly double? _earnings;

        private Author? _author;

        public event EarningsChangeHandler EarningsChanged;

        // Поля, которые можно изиенять.

        private string? _titleChange;

        private int? _publicationYearChange;

        private string? _genreChange;

        private double? _earningsChange;

        // Свойства для инициализации readonly полей при десериализации.
        public string? bookId
        {
            init
            {
                _bookId = value;
            }
            get
            {
                return _bookId;
            }
        }

        public string? title
        {
            init
            {
                _title = value;
                TitleChange = value;
            }
            get
            {
                return _title;
            }
        }

        public int? publicationYear
        {
            init
            {
                _publicationYear = value;
                PublicationYearChange = value;
            }
            get
            {
                return _publicationYear;
            }
        }

        public string? genre
        {
            init
            {
                _genre = value;
                GenreChange = value;
            }
            get
            {
                return _genre;
            }
        }

        public double? earnings
        {
            init
            {
                _earnings = value;
                _earningsChange = value;
            }
            get
            {
                return _earnings;
            }
        }

        [JsonIgnore]
        public Author? author
        {
            set
            {
                _author = value;
            }
            get {
                return _author;
            }
        }

        // Свойства для полей, которые может изменить пользователь, используются для вызова события "Изменение прибыли книги".

        public int? PublicationYearChange
        {
            get
            {
                return _publicationYearChange;
            }
            set
            {
                _publicationYearChange = value;
            }
        }

        public string? TitleChange
        {
            get
            {
                return _titleChange;
            }
            set
            {
                _titleChange = value;
            }
        }

        public string? GenreChange
        {
            get
            {
                return _genreChange;
            }
            set
            {
                _genreChange = value;
            }
        }

        public double? EarningsChange
        {
            get
            {
                return _earningsChange;
            }
            set
            {
                _earningsChange = value;
                BookEarningsChenged();
            }
        }

        private void BookEarningsChenged()
        {
            EarningsChanged?.Invoke();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Environment.NewLine}Только что была пересчитана прибыль всех авторов!");
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Выводит объект в формате JSON.
        /// </summary>
        public void ToJSON()
        {
            Console.WriteLine("\t{");
            Console.WriteLine($"\t\"bookId\": \"{_bookId}\",");
            Console.WriteLine($"\t\"title\": \"{TitleChange}\",");
            Console.WriteLine($"\t\"publicationYear\": {PublicationYearChange},");
            Console.WriteLine($"\t\"genre\": \"{GenreChange}\",");
            Console.WriteLine($"\t\"earnings\": {EarningsChange}".Replace(',', '.'));
            Console.WriteLine("\t}");
        }

        public bool CheckNullObjectAndValue()
        {
            if (bookId == null || _title == null || _genre == null || _publicationYear == null || _earnings == null || _publicationYear <= 0 || _earnings <0)
            {
                return true;
            }
            if (bookId == String.Empty || _title == String.Empty || _genre == String.Empty)
            {
                return true;
            }
            return false;
        }

        public Book(string? bookId, string? title, int? publicationYear, string? genre, double? earnings, Author? author)
        {
            this.bookId = bookId;
            this.title = title;
            this.publicationYear = publicationYear;
            this.genre = genre;
            this.earnings = earnings;
            this.author = author;
        }

        public Book() { }
    }
}
