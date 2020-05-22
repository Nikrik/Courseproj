using System;

namespace ConsoleApp1
{
    class LibCard
    {
        protected ulong Number;
        protected DateTime Issue, Validity;

        public void InputData(ulong numcard, DateTime Iss, DateTime Val)
        {
            Number = numcard;
            Issue = Iss;
            Validity = Val;
            Console.WriteLine("Карта читателя успешно добавлена");
        }
        public string CardInfofull()
        {
            string ret = "Номер карты: " + Number.ToString() + "\n";
            ret += "Выдана: " + Issue.Year.ToString() + "." + Issue.Month.ToString() + "." + Issue.Day.ToString() + "\n";
            ret += "Действитльна по: " + Validity.Year.ToString() + "." + Validity.Month.ToString() + "." + Validity.Day.ToString();
            return ret;
        }
        public string CardInfo()
        {
            string ret = Number.ToString() + "\n";
            ret += Issue.Year.ToString() + "." + Issue.Month.ToString() + "." + Issue.Day.ToString() + "\n";
            ret += Validity.Year.ToString() + "." + Validity.Month.ToString() + "." + Validity.Day.ToString() + "\n";
            return ret;
        }
        ~LibCard()
        {
            Console.WriteLine("удаление карты читателя завершено");
        }
    }
}
