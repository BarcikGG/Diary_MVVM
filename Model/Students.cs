using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary_MVVM.Model
{
    public class Students
    {
        public string FIO { get; set; }
        public string Group { get; set; }
        public int Price { get; set; }

        public Students(string fio, string group, int price)
        {
            FIO = fio;
            Group = group;
            Price = price;
        }
    }
}
