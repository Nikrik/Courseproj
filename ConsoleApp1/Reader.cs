using System;

namespace ConsoleApp1
{
    class Reader : LibCard, IReadbleObject, IWritableObject
    {
        public string FIO;
        private string Address, E_mail;
        private ulong Telephone;
        public LibVis[] Vis = new LibVis[0];
        public Reader()
        {

        }

        private Reader(ILoadManager man)
        {
            this.FIO = man.ReadLine();
            this.FIO = this.FIO.Split(' ')[1] + " " + this.FIO.Split(' ')[2]+" " + this.FIO.Split(' ')[3];
            this.Address = man.ReadLine();
            this.Address = this.Address.Substring(this.Address.IndexOf(' ')+1);
            this.E_mail = man.ReadLine();
            this.E_mail = this.E_mail.Substring(this.E_mail.IndexOf(' ')+1);
            this.Telephone = ulong.Parse(man.ReadLine().Split(' ')[1]);
            this.Number = ulong.Parse(man.ReadLine().Split(' ')[2]);
            this.Issue = DateTime.Parse(man.ReadLine().Split(' ')[1]);
            this.Validity = DateTime.Parse(man.ReadLine().Split(' ')[2]);
            Array.Resize(ref Vis, int.Parse(man.ReadLine()));
            for (int i = 0; i < Vis.Length; i++)
            {
                Vis[i] = man.Read(new LibVis.Loader()) as LibVis;
            }
        }
        public class Loader : IReadableObjectLoader
        {
            public IReadbleObject Load(ILoadManager man)
            {
                return new Reader(man);
            }
        }

        public void Write(ISaveManager man)
        {
            man.WriteLine(this.ReaderInfofull());

            man.WriteLine(this.CardInfofull());

            man.WriteLine(Vis.Length.ToString());
            for (int i = 0; i < Vis.Length; i++)
            {
                man.WriteObject(Vis[i]);
            }
        }

        public string ReaderInfo()
        {
            return FIO + "\n" + Address + "\n" + Telephone.ToString() + "\n" + E_mail + "\n";
        }

        public string ReaderInfofull()
        {
            return "ФИО: " + FIO + "\nАдрес: " + Address + "\nE-mail: " + E_mail + "\nТелефон: " + Telephone.ToString();
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
