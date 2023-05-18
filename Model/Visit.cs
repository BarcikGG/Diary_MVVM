using System;

namespace Diary_MVVM.Model
{
    public class Visit
    {
        public string FIO { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Visit() { }
        public Visit(string fio, DateTime date)
        {
            FIO = fio;
            Date = date;
        }
    }
}
