using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary_MVVM.Model
{
    public class Payments
    {
        public string FIO { get; set; }
        public string Status { get; set; }
        public int Lessons { get; set; }

        public Payments() { }
        public Payments(string fio, string status, int lessons)
        {
            FIO = fio;
            Status = status;
            Lessons = lessons;
        }
    }
}
