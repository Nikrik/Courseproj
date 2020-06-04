using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {            
            Book[] Books = new Book[0];
            Reader[] Readers = new Reader[0];
            Console.WriteLine("Программа базы данных библиотеки");
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Команды:");
                Console.WriteLine("Readfile - добавить данные с файла");
                Console.WriteLine("Writefile - записать все данные в файл");
                Console.WriteLine("Add - Добавить запись в БД");
                Console.WriteLine("Delete - Удалить запись из БД");
                Console.WriteLine("Write - Вывести информацию в консоль");
                Console.WriteLine("Exit - Выход из программы");
                string otv = Console.ReadLine().ToLower();
                Console.WriteLine();
                switch (otv)
                {
                    case "readfile":
                        {
                            //File.Read(Readers, Books, out Readers, out Books);
                            Console.WriteLine("Введите название файла");
                            string buff = Console.ReadLine();
                            if (System.IO.File.Exists(buff))
                            {
                                LoadManager load = new LoadManager(buff);
                                Logger log = new Logger(new FileInfo("log.log").AppendText());
                                LoadLogger loadLogger = new LoadLogger(load, log);
                                load.BeginRead();
                                Array.Resize(ref Readers, int.Parse(load.ReadLine()));
                                for (int i = 0; i < Readers.Length; i++)
                                {
                                    Readers[i]=load.Read(new Reader.Loader()) as Reader;
                                }
                                Array.Resize(ref Books, int.Parse(load.ReadLine()));
                                for (int i = 0; i < Books.Length; i++)
                                {
                                    Books[i] = load.Read(new Book.Loader()) as Book;
                                }
                                load.EndRead();
                                Console.WriteLine("Чтение успешно завершено");
                            }
                        }
                        break;
                    case "writefile":
                        if (Books.Length == 0 && Readers.Length == 0)
                        {
                            Console.WriteLine("Нет записей");
                        }
                        else
                        {
                            //File.Write(Readers, Books);
                            Console.WriteLine("Введите название файла");
                            string buff = Console.ReadLine();
                            SaveManager save = new SaveManager(buff);
                            save.WriteLine(Readers.Length.ToString());
                            for (int i = 0; i < Readers.Length; i++)
                            {
                                save.WriteObject(Readers[i]);
                            }
                            save.WriteLine(Books.Length.ToString());
                            for (int i = 0; i < Books.Length; i++)
                            {
                                save.WriteObject(Books[i]);
                            }
                            Console.WriteLine("Запись успешно завершена");
                        }
                        break;
                    case "add":
                        Console.WriteLine("Reader - Добавить запись о читателе");
                        Console.WriteLine("Book - добавить запись о книге");
                        Console.WriteLine("Libvis - добавить запись посещении");
                        otv = Console.ReadLine().ToLower();
                        switch (otv)
                        {
                            case "reader":
                                {
                                    Array.Resize(ref Readers, Readers.Length + 1);
                                    Readers[Readers.Length - 1] = new Reader();
                                    Readers[Readers.Length - 1] = Cons.Addreader();
                                }
                                break;
                            case "book":
                                {
                                    Array.Resize(ref Books, Books.Length + 1);
                                    Books[Books.Length - 1] = new Book();
                                    Books[Books.Length - 1] = Cons.AddBook();
                                }
                                break;
                            case "libvis":
                                int i;
                                bool t;
                                if (Books.Length == 0 || Readers.Length == 0)
                                {
                                    Console.WriteLine("Нет записи о книгах или читателях, посещение невозможно");
                                }
                                else
                                {
                                    Console.WriteLine("Список читателей");
                                    Cons.ReaderFIOWrite(Readers);
                                    do
                                    {
                                        Console.WriteLine("Выберите читателя по индексу");
                                        t = int.TryParse(Console.ReadLine(), out i);
                                        i--;
                                        if (!t || 0 > i || i >= Readers.Length)
                                        {
                                            Console.WriteLine("Неправильно выбран индекс");
                                        }
                                    } while (!t || 0 > i || i >= Readers.Length);
                                    Readers[i].LibVis(Books);
                                }
                                break;
                            default:
                                break;
                        }

                        break;
                    case "delete":
                        Console.WriteLine("Reader - удалить запись о читателе");
                        Console.WriteLine("Book - удалить запись о книге");
                        Console.WriteLine("Libvis - удалить запись посещении");
                        otv = Console.ReadLine().ToLower();
                        switch (otv)
                        {
                            case "reader":
                                {
                                    Console.WriteLine("Список читателей");
                                    Cons.ReaderFIOWrite(Readers);
                                    bool t;
                                    int i;
                                    do
                                    {
                                        Console.WriteLine("Выберите читателя по индексу");
                                        t = int.TryParse(Console.ReadLine(), out i);
                                        i--;
                                        if (!t || 0 > i || i >= Readers.Length)
                                        {
                                            Console.WriteLine("Неправильно выбран индекс");
                                        }
                                    } while (!t || 0 > i || i >= Readers.Length);
                                    Console.WriteLine("Вы действительно хотите удалить запись о" + Readers[i].FIO + "И все прилагающие к нему записи о посещениях? Да/Нет");
                                    otv = Console.ReadLine().ToLower();
                                    if (otv == "да")
                                    {
                                        for (; i < Readers.Length - 1; i++)
                                        {
                                            Readers[i] = Readers[i + 1];
                                        }
                                        Array.Resize(ref Readers, Readers.Length - 1);
                                    }
                                }
                                break;
                            case "book":
                                {
                                    Console.WriteLine("Список книг");
                                    int i;
                                    for (i = 0; i < Books.Length; i++)
                                    {
                                        Console.WriteLine($"{i + 1} {Books[i].Title}");
                                    }
                                    bool t;
                                    do
                                    {
                                        Console.WriteLine("Выберите книгу по индексу");
                                        t = int.TryParse(Console.ReadLine(), out i);
                                        i--;
                                        if (!t || 0 > i || i >= Books.Length)
                                        {
                                            Console.WriteLine("Неправильно выбран индекс");
                                        }
                                    } while (!t || 0 > i || i >= Books.Length);
                                    Console.WriteLine("Вы действительно хотите удалить запись о " + Books[i].Title + " И все прилагающие к нему записи о посещениях? Да/Нет");
                                    otv = Console.ReadLine().ToLower();
                                    if (otv == "да")
                                    {
                                        for (int j = i; j < Books.Length - 1; j++)
                                        {
                                            Books[j] = Books[j + 1];
                                        }
                                        Array.Resize(ref Books, Books.Length - 1);
                                    }
                                    for (int j = 0; j < Readers.Length; j++)
                                    {
                                        Readers[i].DelLibVis(i);
                                    }
                                    break;
                                }
                            case "libvis":
                                {
                                    Console.WriteLine("Список читателей");
                                    Cons.ReaderFIOWrite(Readers);
                                    bool t;
                                    int i;
                                    do
                                    {
                                        Console.WriteLine("Выберите читателя по индексу");
                                        t = int.TryParse(Console.ReadLine(), out i);
                                        i--;
                                        if (!t || 0 > i || i >= Readers.Length)
                                        {
                                            Console.WriteLine("Неправильно выбран индекс");
                                        }
                                    } while (!t || 0 > i || i >= Readers.Length);
                                    Console.WriteLine("Список Посещений");
                                    for (int k = 0; k < Readers[i].Vis.Length; k++)
                                    {
                                        Console.WriteLine((k + 1) + " " + Readers[i].Vis[k].LibVisInfofull(Books));
                                    }
                                    int j;
                                    do
                                    {
                                        Console.WriteLine("Выберите посещение по индексу");
                                        t = int.TryParse(Console.ReadLine(), out j);
                                        j--;
                                        if (!t || 0 > j || j >= Readers[i].Vis.Length)
                                        {
                                            Console.WriteLine("Неправильно выбран индекс");
                                        }
                                    } while (!t || 0 > j || j >= Readers[i].Vis.Length);
                                    Readers[i].DelLibVis(j);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "write":
                        if (Books.Length == 0 && Readers.Length == 0)
                        {
                            Console.WriteLine("Нет записей");
                        }
                        else
                        {
                            Cons.Write(Readers, Books);
                        }
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Неправильно Введенная команда");
                        break;
                }
            }
        }
    }
}