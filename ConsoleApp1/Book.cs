using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Book
    {
        private uint Copies, Prise;
        private int Year;
        private string Autor, Summary;
        public string Title;
        ~Book()
        {
            Console.WriteLine("Удаление книги завершено");
        }
        public string BookInfo()
        {
            string ret =Title + "\n";
            ret += Autor + "\n";
            ret += Year.ToString()+"\n";
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
            ret += "Копий: " + Copies + "\n";
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
