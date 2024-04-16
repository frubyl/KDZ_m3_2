using System.Diagnostics;

namespace ClassLibrary
{
    /// <summary>
    /// Класс хранит время изменения объекта.
    /// </summary>
    public class DateChangeTimeArgs : EventArgs
    {
        /// <summary>
        /// Время изменения, получаем в конструкторе.
        /// </summary>
        public DateTime ChangeTime { get; init; }

        public DateChangeTimeArgs(DateTime changeTime)
        {
            ChangeTime = changeTime;
        }

        public DateChangeTimeArgs() { }
    }
}
