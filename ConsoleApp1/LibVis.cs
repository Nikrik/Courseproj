using System;

namespace ConsoleApp1
{
    class LibVis: IReadbleObject, IWritableObject
    {
        private DateTime Visit, Issue, Delivery;
        public int Books;
        public void Write(ISaveManager man)
        {
            man.WriteLine(this.LibVisInfofull());
        }
        public LibVis()
        {

        }
        private LibVis(ILoadManager man)
        {
            this.Visit= DateTime.Parse(man.ReadLine().Split(' ')[2]);
            this.Books = int.Parse(man.ReadLine().Split(' ')[2])-1;
            this.Issue= DateTime.Parse(man.ReadLine().Split(' ')[3]);
            this.Delivery= DateTime.Parse(man.ReadLine().Split(' ')[3]);
        }
        public class Loader : IReadableObjectLoader
        {
            public IReadbleObject Load(ILoadManager man)
            {
                return new LibVis(man);
            }
        }
        ~LibVis()
        {
            Console.WriteLine("Удаление записи о посещении завершено");
        }
        public string LibVisinfo(Book[] books)
        {
            string ret = "";
            ret += Visit.Year.ToString() + "." + Visit.Month.ToString() + "." + Visit.Day.ToString() + "\n";
            ret += Books.ToString() + "\n";
            ret += books[Books].Title + "\n";
            ret += Issue.Year.ToString() + "." + Issue.Month.ToString() + "." + Issue.Day.ToString() + "\n";
            ret += Delivery.Year.ToString() + "." + Delivery.Month.ToString() + "." + Delivery.Day.ToString() + "\n";
            return ret;
        }
        public string LibVisInfofull()
        {
            string ret;
            ret = "Дата посещения: " + Visit.Year.ToString() + "." + Visit.Month.ToString() + "." + Visit.Day.ToString() + "\n";
            ret += "Индекс книги: " + (Books + 1) + "\n";
            ret += "Дата выдачи книги: " + Issue.Year.ToString() + "." + Issue.Month.ToString() + "." + Issue.Day.ToString() + "\n";
            ret += "Дата возвращения книги: " + Delivery.Year.ToString() + "." + Delivery.Month.ToString() + "." + Delivery.Day.ToString();
            return ret;
        }
        public string LibVisInfofull(Book[] books)
        {
            string ret;
            ret = "Дата посещения: " + Visit.Year.ToString() + "." + Visit.Month.ToString() + "." + Visit.Day.ToString() + "\n";
            ret += "Индекс книги: " + (Books + 1) + "\n";
            ret += "Название книги: " + books[Books].Title + "\n";
            ret += "Дата выдачи книги: " + Issue.Year.ToString() + "." + Issue.Month.ToString() + "." + Issue.Day.ToString() + "\n";
            ret += "Дата возвращения книги: " + Delivery.Year.ToString() + "." + Delivery.Month.ToString() + "." + Delivery.Day.ToString() + "\n";
            return ret;
        }
        public void InputData(DateTime Vis, int b, DateTime Iss, DateTime Del)
        {
            Visit = Vis;
            Issue = Iss;
            Delivery = Del;
            Books = b;
            Console.WriteLine("Запись о посещении успешно добавлена");
        }

    }
}
