using System;

namespace ConsoleApp1
{
    class Book : IReadbleObject, IWritableObject
    {
        private uint Copies, Prise;
        private int Year;
        private string Autor, Summary;
        public string Title;
        public Book()
        {

        }
        public void Write(ISaveManager man)
        {
            man.WriteLine(this.BookInfofull());
        }
        public class Loader : IReadableObjectLoader
        {
            public IReadbleObject Load(ILoadManager man)
            {
                return new Book(man);
            }
        }
        Book(ILoadManager man)
        {
            this.Title = man.ReadLine();
            this.Title = this.Title.Substring(this.Title.IndexOf(' ') + 1);
            this.Autor = man.ReadLine();
            this.Autor = this.Autor.Substring(this.Autor.IndexOf(' ') + 1);
            this.Year = int.Parse(man.ReadLine().Split(' ')[2]);
            this.Summary = man.ReadLine();
            this.Summary = this.Summary.Substring(this.Summary.IndexOf(' ') + 1);
            this.Summary = this.Summary.Substring(this.Summary.IndexOf(' ') + 1);
            this.Prise = uint.Parse(man.ReadLine().Split()[1]);
            this.Copies = uint.Parse(man.ReadLine().Split()[1]);
        }
        public string BookInfo()
        {
            string ret = Title + "\n";
            ret += Autor + "\n";
            ret += Year.ToString() + "\n";
            ret += Summary + "\n";
            ret += Prise + "\n";
            ret += Copies + "\n";
            return ret;
        }
        public string BookInfofull()
        {
            string ret = "Название: " + Title + "\n";
            ret += "Автор: " + Autor + "\n";
            ret += "Год написания: " + Year.ToString() + "\n";
            ret += "Краткое описание: " + Summary + "\n";
            ret += "Цена: " + Prise + "\n";
            ret += "Копий: " + Copies;
            return ret;
        }
        public void InputData(string Aut, string Titl, int Ye, string Summ, uint Cop, uint Pr)
        {
            Copies = Cop;
            Prise = Pr;
            Year = Ye;
            Title = Titl;
            Autor = Aut;
            Summary = Summ;
            Console.WriteLine("Книга успешно добавлена");
        }
    }
}
