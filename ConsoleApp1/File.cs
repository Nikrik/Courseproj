using System;
using System.IO;

namespace ConsoleApp1
{
    static class File
    {
        public static void Write(Reader[] Readers, Book[] Books)
        {
            string namef;
            do
            {
                Console.WriteLine("Введите название файла");
                namef = Console.ReadLine();
                if (namef.Length == 0)
                {
                    Console.WriteLine("Пустое имя");
                }
            } while (namef.Length == 0);
            StreamWriter File = new StreamWriter(namef);
            for (int i = 0; i < Readers.Length; i++)
            {
                File.WriteLine("READER");
                File.Write(Readers[i].ReaderInfo());
                File.WriteLine("LIBCARD");
                File.Write(Readers[i].CardInfo());
            }
            for (int i = 0; i < Books.Length; i++)
            {
                File.WriteLine("BOOK");
                File.Write(Books[i].BookInfo());
            }
            for (int i = 0; i < Readers.Length; i++)
            {
                if (!(Readers[i].Vis.Length == 0))
                {
                    for (int j = 0; j < Readers[i].Vis.Length; j++)
                    {
                        File.WriteLine("VISITION");
                        File.WriteLine(i);
                        File.Write(Readers[i].Vis[j].LibVisinfo(Books));
                    }
                }

            }
            File.Close();
            Console.WriteLine("Запись завершена");
        }
        public static DateTime Date(string inp)
        {
            string[] o;
            o = inp.Split('.');
            return new DateTime(int.Parse(o[0]), int.Parse(o[1]), int.Parse(o[2]));
        }
        public static void Read(Reader[] Readersi, Book[] Booksi, out Reader[] Readerso, out Book[] Bookso)
        {
            Readerso = Readersi;
            Bookso = Booksi;
            Console.WriteLine("Введите название файла");
            string buff = Console.ReadLine();
            if (System.IO.File.Exists(buff))
            {
                string title;

                StreamReader File = new StreamReader(buff);
                while (!File.EndOfStream)
                {
                    title = File.ReadLine();
                    if (title == "READER")
                    {
                        Array.Resize(ref Readerso, Readerso.Length + 1);
                        Readerso[Readerso.Length - 1] = new Reader();
                        Readerso[Readerso.Length - 1].InputData(File.ReadLine(), File.ReadLine(), ulong.Parse(File.ReadLine()), File.ReadLine());
                        File.ReadLine();
                        Readerso[Readerso.Length - 1].InputData(ulong.Parse(File.ReadLine()), Date(File.ReadLine()), Date(File.ReadLine()));
                    }
                    if (title == "BOOK")
                    {
                        Array.Resize(ref Bookso, Bookso.Length + 1);
                        Bookso[Bookso.Length - 1] = new Book();
                        Bookso[Bookso.Length - 1].InputData(File.ReadLine(), File.ReadLine(), int.Parse(File.ReadLine()), File.ReadLine(), uint.Parse(File.ReadLine()), uint.Parse(File.ReadLine()));
                    }
                    if (title == "VISITION")
                    {

                        int i = int.Parse(File.ReadLine()) + Readersi.Length;
                        DateTime Visit = Date(File.ReadLine());
                        int j = int.Parse(File.ReadLine()) + Booksi.Length;
                        File.ReadLine();
                        DateTime Issue = Date(File.ReadLine());
                        DateTime Delivery = Date(File.ReadLine());
                        Readerso[i].LibVis(Visit, j, Issue, Delivery);
                    }
                }
                File.Close();
            }
            else
            {
                Console.WriteLine("Файла нет");
            }
        }
    }
}
