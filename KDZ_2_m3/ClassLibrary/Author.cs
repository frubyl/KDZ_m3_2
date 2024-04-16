using System.Text.Json.Serialization;
namespace ClassLibrary
{
    [Serializable]
    public class Author
    {
        private readonly string? _authorId; 

        private readonly string? _name;

        private readonly double? _earnings;  

        private readonly Book[]? _books; 

        public event EventHandler<DateChangeTimeArgs> Updated;

        // Поля, которые может изменить пользователь.
        private string? _nameChange;

        private double? _earningsChange;

        private Book[]? _booksChange;

        // Свойства для инициализации readonly полей при десериализации.
        public string? authorId
        {
            init
            {
                _authorId = value;
            }
            get
            {
                return _authorId;
            }
        }
    
        public string? name
        {
            init
            {
                _name = value;
                _nameChange = value;
            }
            get
            {
                return _name;
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

        public Book[]? books
        {
            init
            {
                _books = value;
                if (_books != null & _books.Length != 0)
                {
                    for (int i = 0; i < _books.Length; i++)
                    {
                        _books[i].author = this;
                    }
                }
                _booksChange = value;
            }
            get
            {
                return _books;
            }
        }

        // Свойства для полей, которые может изменить пользователь, используются для вызова события "Изменение объекта".
        [JsonIgnore]
        public string? NameChange 
        {
            get
            {
                return _nameChange;
            }
            set
            {
                _nameChange = value;
                ObjectChenged(new DateChangeTimeArgs(DateTime.Now));
            }
        }
        [JsonIgnore]
        public double? EarningsChange
        {
            get
            {
                return _earningsChange;
            }
            set
            {
                _earningsChange = value;
                ObjectChenged(new DateChangeTimeArgs(DateTime.Now));
            }
        }
        [JsonIgnore]
        public Book[]? BooksChange
        {
            get
            {
                return _booksChange;
            }
            set
            {
                _booksChange = value;
                if (BooksChange.Length != 0)
                {
                    ObjectChenged(new DateChangeTimeArgs(DateTime.Now));

                }
            }
        }
       
        private void ObjectChenged(DateChangeTimeArgs e) => Updated?.Invoke(this, e);

        /// <summary>
        /// Пересчитывает прибыль автора при изменении прибыли какой-либо книги.
        /// </summary>
        public void RecalculationEarnings()
        {
            if(BooksChange.Length != 0)
            {
                double newEarning = 0;
                foreach(var book in BooksChange)
                {
                    if(book.EarningsChange != null)
                    {
                        newEarning += (double)book.EarningsChange;
                    }
                }
                EarningsChange = newEarning;
            }
        }

        /// <summary>
        /// Выводит объект в формате JSON.
        /// </summary>
        public void ToJSON()
        {
            Console.WriteLine('{');
            Console.WriteLine($"\"authorId\": \"{authorId}\",");
            Console.WriteLine($"\"name\": \"{NameChange}\",");
            Console.WriteLine($"\"earnings\": {EarningsChange}".Replace(',','.') + ",");
            Console.WriteLine("\"books\": [");
            if (BooksChange?.Length != 0)
            {
                for (int i = 0; i < BooksChange.Length - 1; i++)
                {
                    BooksChange[i].ToJSON();
                    Console.WriteLine("\t,");
                }
                BooksChange[BooksChange.Length - 1].ToJSON();
            }              
            Console.WriteLine(']');
            Console.WriteLine('}');
        }

        /// <summary>
        /// Проверяет на null объект.
        /// </summary>
        /// <returns> true если null, false иначе. </returns>
        public bool CheckNullObjectAndValue()
        {
            if(_authorId == null || _books == null || _name == null || _earnings == null || _earnings < 0)
            {
                return true;
            }
            if (_authorId == String.Empty || _name == String.Empty)
            {
                return true;
            }
            foreach(var book in _books)
            {
                if (book.CheckNullObjectAndValue())
                {
                    return true;
                }
            }
            return false;
        }

        public Author(string? authorId, string? name, double? earnings, Book[]? books)
        { 
            this.authorId = authorId;
            this.name = name;
            this.earnings = earnings;
            this.books = books;
        }   

        public Author() { }
    }
}
