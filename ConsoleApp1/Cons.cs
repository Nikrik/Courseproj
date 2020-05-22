using System;

namespace ConsoleApp1
{
    static class Cons
    {
        static public Reader Addreader()
        {
            string FIO, Address, E_mail;
            int prob;
            ulong Telephone, num;
            do
            {
                Console.WriteLine("Введите ФИО");
                FIO = Console.ReadLine();
                prob = FIO.Split(' ').Length;//Разделяем строку пробелом и возвращаем кол-во разделенных строк
                for (int i = 0; i < FIO.Length - 1; i++)//проверка на повторяющиеся пробелы
                {
                    if (FIO[i] == ' ' && FIO[i + 1] == ' ')
                    {
                        prob = 4;
                    }
                }
                if (FIO.Length > 1 && (FIO[0] == ' ' || FIO[FIO.Length - 1] == ' '))//проверка на пробелы в конце и вначале строки, если строка больше 1
                {
                    prob = 4;
                }
                if (!(prob == 3))
                {
                    Console.WriteLine("Неправильно введено ФИО");
                }
            } while (!(prob == 3));
            do
            {
                Console.WriteLine("Введите Адрес");
                Address = Console.ReadLine();
                if (Address.Length == 0)
                {
                    Console.WriteLine("Неправильно введен адрес");
                }
            } while (Address.Length == 0);
            bool att;
            do
            {
                Console.WriteLine("Введите номер телефона (без +, слитно и в полном формате, прим 89123456789)");
                att = ulong.TryParse(Console.ReadLine(), out Telephone);
                if (!att || !(Telephone.ToString().Length == 11))
                {
                    Console.WriteLine("Неправильно введен номер телефона");
                }
            } while (!att || !(Telephone.ToString().Length == 11));
            bool dog = false;
            do
            {
                int i;
                Console.WriteLine("Введите Email");
                E_mail = Console.ReadLine();
                for (i = 0; i < E_mail.Length; i++)
                {
                    if (E_mail[i] == '@')
                    {
                        for (int j = i; j < E_mail.Length; j++)
                        {
                            if (E_mail[j] == '.')
                            {
                                dog = true;
                            }
                        }
                    }
                }
                if (!dog)
                {
                    Console.WriteLine("Неправильно введен Email");
                }
            } while (!dog);
            do
            {
                Console.WriteLine("Введите номер карты читателя");
                if (!ulong.TryParse(Console.ReadLine(), out num) || !(num.ToString().Length == 4 * 4))
                {
                    Console.WriteLine("Неправильно введен номер карты");
                }
            } while (!att || !(num.ToString().Length == 4 * 4));
            DateTime Issue = NewDataTime("выдачи");
            DateTime Validity = NewDataTime("действия карты");
            Reader Readers = new Reader();
            Readers.InputData(num, Issue, Validity);
            Readers.InputData(FIO, Address, Telephone, E_mail);
            return Readers;
        }

        public static LibVis AddVis(Book[] books)
        {
            DateTime Visit, Issue, Delivery;
            int j;
            Visit = NewDataTime("посещения");
            bool t;
            do
            {
                Console.WriteLine("Список книг");
                for (int i = 0; i < books.Length; i++)
                {
                    Console.WriteLine($"{i + 1} {books[i].Title}");
                }
                Console.WriteLine("Введите индекс книги");
                t = int.TryParse(Console.ReadLine(), out j);
                j--;
                if (!t || 0 > j || j >= books.Length)
                {
                    Console.WriteLine("Неправильно выбран индекс");
                }
            } while (!t || 0 > j || j >= books.Length);
            Issue = NewDataTime("получения книги");
            Delivery = NewDataTime("сдачи книги");
            LibVis vis = new LibVis();
            vis.InputData(Visit, j, Issue, Delivery);
            return vis;
        }

        public static void Write(Reader[] Readers, Book[] Books)
        {
            for (int i = 0; i < Readers.Length; i++)
            {
                Console.WriteLine("Читатель");
                Console.WriteLine("Индекс: " + (i + 1));
                Console.Write(Readers[i].ReaderInfofull());
                Console.WriteLine();
                Console.WriteLine("Читательская карта");
                Console.WriteLine("Индекс: " + (i + 1));
                Console.Write(Readers[i].CardInfofull());
                Console.WriteLine("\n");
            }
            for (int i = 0; i < Books.Length; i++)
            {
                Console.WriteLine("Книга");
                Console.WriteLine("Индекс: " + (i + 1));
                Console.Write(Books[i].BookInfofull());
                Console.WriteLine("\n");
            }
            for (int i = 0; i < Readers.Length; i++)
            {
                if (!(Readers[i].Vis.Length == 0))
                {
                    Console.WriteLine("Посещение библиотеки");
                    Console.WriteLine("Индекс посетителя: " + (i + 1));
                    Console.WriteLine("ФИО посетителя: " + Readers[i].FIO);
                    for (int j = 0; j < Readers[i].Vis.Length; j++)
                    {
                        Console.Write(Readers[i].Vis[j].LibVisInfofull(Books));
                        Console.WriteLine();
                    }
                }
            };
        }
        private static DateTime NewDataTime(string msg)
        {
            int Yeari, Monthi, Dayi;
            bool dog;
            Console.WriteLine($"Введите дату {msg}");
            do
            {
                Console.WriteLine($"введите год {msg}");
                dog = int.TryParse(Console.ReadLine(), out Yeari);
                if (!dog || 1 > Yeari || Yeari > 9999)
                {
                    Console.WriteLine("Неправильно введен год");
                }
            } while (!dog || 1 > Yeari || Yeari > 9999);
            do
            {
                Console.WriteLine($"введите месяц {msg} (номер месяца)");
                dog = int.TryParse(Console.ReadLine(), out Monthi);
                if (!dog || 1 > Monthi || Monthi > 12)
                {
                    Console.WriteLine("Неправильно введен номер месяца");
                }
            } while (!dog || 1 > Monthi || Monthi > 12);
            do
            {
                Console.WriteLine($"введите день {msg}");
                dog = int.TryParse(Console.ReadLine(), out Dayi);
                if (!dog || 1 > Dayi || Dayi > DateTime.DaysInMonth(Yeari, Monthi))
                {
                    Console.WriteLine("Неправильно введен день");
                }
            } while (!dog || 1 > Dayi || Dayi > DateTime.DaysInMonth(Yeari, Monthi));
            DateTime ret = new DateTime(Yeari, Monthi, Dayi);
            return ret;
        }

        static public Book AddBook()
        {
            uint Copies, Prise;
            int Yeari;
            string Title, Autor, Summary;
            do
            {
                Console.WriteLine("Введите Название книги");
                Title = Console.ReadLine();
                if (Title.Length == 0)
                {
                    Console.WriteLine("Неправильно введено название книги");
                }
            } while (Title.Length == 0);
            do
            {
                Console.WriteLine("Введите автора");
                Autor = Console.ReadLine();
                if (Autor.Length == 0)
                {
                    Console.WriteLine("Неправильно введен автор");
                }
            } while (Autor.Length == 0);
            bool dog;
            do
            {
                Console.WriteLine("Введите год создания");
                dog = int.TryParse(Console.ReadLine(), out Yeari);
                if (!dog)
                {
                    Console.WriteLine("Неправильно введен год");
                }
            } while (!dog);
            do
            {
                Console.WriteLine("Введите Краткое описание");
                Summary = Console.ReadLine();
                if (!dog)
                {
                    Console.WriteLine("Неправильно введено краткое описание");
                }
            } while (!dog);
            do
            {
                Console.WriteLine("Введите цену в рублях");
                dog = uint.TryParse(Console.ReadLine(), out Prise);
                if (!dog)
                {
                    Console.WriteLine("Неправильно введена цена");
                }
            } while (!dog);
            do
            {
                Console.WriteLine("Введите кол-во копий");
                dog = uint.TryParse(Console.ReadLine(), out Copies);
                if (!dog)
                {
                    Console.WriteLine("Неправильно введено кол-во копий");
                }
            } while (!dog);
            Book Books = new Book();
            Books.InputData(Autor, Title, Yeari, Summary, Copies, Prise);
            return Books;
        }
        public static void ReaderFIOWrite(Reader[] Readers)
        {
            for (int i = 0; i < Readers.Length; i++)
            {
                Console.WriteLine($"{i + 1} {Readers[i].FIO}");
            }
        }
    }
}
