namespace Diary_MVVM.Model
{
    public class Students
    {
        public string FIO { get; set; }
        public string Group { get; set; }
        public int Price { get; set; }

        public Students() { }
        public Students(string fio, string group, int price)
        {
            FIO = fio;
            Group = group;
            Price = price;
        }
    }
}
