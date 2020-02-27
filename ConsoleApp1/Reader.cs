using System;

namespace ConsoleApp1
{
    class Reader : LibCard
    {
        public string FIO;
        private string Address, E_mail;
        private ulong Telephone;
        public LibVis[] Vis = new LibVis[0];
        public string ReaderInfo()
        {
            return FIO + "\n" + Address + "\n" + Telephone.ToString() + "\n" + E_mail + "\n";
        }
        public string ReaderInfofull()
        {
            return "ФИО: " + FIO + "\nАдрес: " + Address + "\nТелефон: " + Telephone.ToString() + "\nE-mail: " + E_mail + "\n";
        }
        ~Reader()
        {
            Console.WriteLine("Удаление читателя завершено");
        }
        public void InputData(string f, string addr, ulong tel, string Em)
        {
            FIO = f;
            Address = addr;
            E_mail = Em;
            Telephone = tel;
            Console.WriteLine("Читатель успешно добавлен");
        }
        public void LibVis(Book[] books)
        {
            Array.Resize(ref Vis, Vis.Length + 1);
            Vis[Vis.Length - 1] = new LibVis();
            Vis[Vis.Length - 1] = Cons.AddVis(books);
        }
        public void LibVis(DateTime Visit, int j, DateTime Issue, DateTime Delivery)
        {
            LibVis vis = new LibVis();
            vis.InputData(Visit, j, Issue, Delivery);
            Array.Resize(ref Vis, Vis.Length + 1);
            Vis[Vis.Length - 1] = new LibVis();
            Vis[Vis.Length - 1] = vis;
        }

        public void DelLibVis(int del)
        {
            for (int i = 0; i < Vis.Length; i++)
            {
                if (Vis[i].Books == del)
                {
                    for (int j = 0; j < Vis.Length - 1; j++)
                    {
                        Vis[j] = Vis[j + 1];
                    }
                    Array.Resize(ref Vis, Vis.Length - 1);
                }
            }
        }
    }
}
