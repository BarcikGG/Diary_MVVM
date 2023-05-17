using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary_MVVM.Model
{
    public class Visit
    {
        public string FIO { get; set; }
        public DateTime Date { get; set; }

        public Visit() { }
        public Visit(string fio, DateTime date)
        {
            FIO = fio;
            Date = date;
        }
    }
}
