using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab8
{



    interface IGeneric<T>
    {
        void Add(T item);
        void Delete(T item);
        void OnScreen();
    }


    

        class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Next { get; set; }
    }

    class CollectionType<T> : IGeneric<T> where T : class
    {
        Node<T>[] Elements;

        public T this[int index]
        {
            get
            {
                return Elements[index].Element;
            }
            set
            {
                Elements[index].Element = value;
            }
        }

        public List<T> Collections;
        public void Add(T value)
        {
            Collections.Add(value);
        }
        public void Delete(T item)
        {
            Collections.Remove(item);
        }
        public void OnScreen()
        {
            foreach (T o in Collections)
            {
                Console.WriteLine(o.ToString());
            }
        }
        public void OutputInfo(string info)
        {
            string path = @"C:\Users\yegor\source\repos\lab8\lab8\TextFile.txt";
            StreamWriter sw = new StreamWriter(path, true);
            foreach (T t in Collections)
            {
                sw.WriteLine(DateTime.Now + $", {info}: Test log message.");
            }

            Console.WriteLine("Log written");
            sw.Close();
        }

        //public void InputInfo(string info)
        //{
        //    string path = @"C:\Users\yegor\source\repos\lab8\lab8\TextFile.txt";
        //    StreamWriter sw = new StreamWriter(path, true);
        //    foreach (T t in Collections)
        //    {
        //        sw.WriteLine(DateTime.Now + $", {info}: Test log message.");
        //    }

        //    Console.WriteLine("Log written");
        //    sw.Close();
        //}

        public CollectionType(int i)
        {
            Elements = new Node<T>[i];
        }

        private Node<T> Head { get; set; }
        private Node<T> Current { get; set; }

        public int Size { get; set; }



        //Удаление элемента списка
        public void DeleteNode(int number)
        {

            if ((Head != null) && (number < Size) && (number >= 0))
            {
                Node<T> n = Head;
                Node<T> prevNode = null;
                if (number == 0)
                {
                    n = n.Next;
                    Head = n;
                }
                if (number == Size)
                {
                    for (int i = 0; i < number - 2; i++)
                    {
                        n = n.Next;
                    }

                    n.Next = null;
                }
                else
                {
                    for (int i = 0; i < number - 1; i++)
                    {
                        prevNode = n;
                        n = n.Next;

                    }
                    n.Next = n.Next.Next;
                }

            }
        }

        //Получение значения "головного элемента" списка
        public Node<T> GetHead()
        {
            return this.Head;
        }




        //Добавление элемента в список

        public void Push(T elem /*, int pos*/)
        {
            Size++;
            var node = new Node<T>() { Element = elem };
            //Node temp = new Node();
            //if (pos == 0)
            //{
            //    node.Next = Head;
            //    Head = node;
            //}
            //if (pos == Size)
            //{
            //    Current.Next = node;
            //}
            //else
            //{
            //    for(int i = 0; i < pos - 1; i++)
            //    {
            //        node = node.Next;
            //    }
            //    temp = node.Next;


            //}
            if (Head == null)
            {
                Head = node;
            }
            else
            {
                Current.Next = node;
            }
            Current = node;
        }


        //Вывод элементов списка в консоль

        public void Output()
        {
            Node<T> n = Head;
            while (n != null)
            {
                Console.WriteLine($"Скейт-бренд: {n.Element}");
                n = n.Next;
            }
        }
    }


    class IndexRestriction : Exception
    {
        public string message = "Вы вышли за пределы списка!";
        public string diagnostics = "Отмените добавление в список необходимого количества элементов!";
        public IndexRestriction(string message) : base(message)
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CollectionType<string> e1 = new CollectionType<string>(5);
            e1.Push("Santa Cruz");
            e1.Push("Carhartt");
            e1.Push("Pass~port");
            e1.Push("Blind");

            try
            {
                CollectionType<string> e2 = new CollectionType<string>(5);
                e2.Push("Supreme");
                e2.Push("FA|Hockey");
                e2.Push("Vans");
                e2.Push("Nike SB");
                e2.Push("Rassvet");
                e2.Push("Pass~port");
                e2.Push("Blind");
                if (e2.Size > 5)
                {
                    throw new IndexRestriction("Вы вышли за пределы списка!");
                }
            }

            catch (IndexRestriction ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source);
                
            }
            finally
            {
               
                    Console.WriteLine("Исключение обработано");
              
            }
            //List e2 = new List();
            //e2.Push("Supreme");
            //e2.Push("FA|Hockey");
            //e2.Push("Vans");
            //e2.Push("Nike SB");
            //e2.Push("Rassvet");



            Console.WriteLine("\nПервый список:");
            e1.Output();
            Console.WriteLine("\nВторой список:");
            //e2.Output();
            Console.WriteLine("\nДобавление еще одного наименования в первый список: ");
            e1.Push("Dime");
            e1.Output();
            //Console.WriteLine($"\nStringCounter: {e2.StringCounter()}");
            //e2.SumOfStrings();
            //e1.FirstAndLastString();
            //e1.LongestAndShortestWord();
            //e1.DeleteLastNode();
            //e2 = e2 + "OKTYABR";
            //e2.Output();
            //List.OwnerInfo();

            Book book1 = new Book("Война и мир 1-й том", 2014, 16);
            Book book2 = new Book("Война и мир 2-ой том", 2014, 16);

            CollectionType<Book> myCollectiion = new CollectionType<Book>(2);
            myCollectiion.Add(book1);
            myCollectiion.Add(book2);


            Console.WriteLine("--------------------------------------------------------");

            bool tmp = false;
            try
            {
                StandartTypes<int, double, byte> myTypes = new StandartTypes<int, double, byte>(13, 55.4, 3);
                myTypes.ShowTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source);
                tmp = true;
            }
            finally
            {
                if (tmp)
                {
                    Console.WriteLine("Исключение обработано");
                }
                else
                {
                    Console.WriteLine("Исключение не обработано либо не произошло");
                }

            }
        }
    }


    //     public static class StatisticOperation
    //    {
    //        internal static void SumOfStrings(this List elem)
    //        {
    //            string Sum = "";
    //            Node head = elem.GetHead();
    //            for (int i = 1; i < elem.Size; i++)
    //            {
    //                Sum += head.Element;
    //                head = head.Next;
    //            }
    //            Console.WriteLine($"Сумма элементов списка: {Sum}");
    //        }

    //        internal static void FirstAndLastString(this List elem)
    //        {
    //            Node head = elem.GetHead();
    //            string first = head.Element;
    //            string last = first;
    //            int key;
    //            Node next = head.Next;
    //            while (next.Next != null)
    //            {
    //                key = String.Compare(first, next.Element);
    //                if (key > 0)
    //                {
    //                    first = next.Element;
    //                }
    //                if (key < 0)
    //                {
    //                    last = next.Element;
    //                }
    //                next = next.Next;
    //            }
    //            Console.WriteLine($"Самое первое слово в алфавитном порядке: {first}");
    //            Console.WriteLine($"Самое последнее слово в алфавитном порядке: {last}");
    //        }
    //        internal static int StringCounter(this List e)
    //        {
    //            return e.Size;
    //        }

    //        internal static void LongestAndShortestWord(this List elem)
    //        {
    //            Node head = elem.GetHead();
    //            string maxword = head.Element;
    //            string minword = head.Element;
    //            Node next = head.Next;
    //            while (next.Next != null)
    //            {
    //                if (next.Element.Length > maxword.Length)
    //                {
    //                    maxword = next.Element;

    //                }
    //                if (next.Element.Length < minword.Length)
    //                {
    //                    minword = next.Element;

    //                }
    //                next = next.Next;
    //            }
    //            Console.WriteLine($"Самое длинное слово в списке: {maxword}");
    //            Console.WriteLine($"Самое короткое слово в списке: {minword}");
    //        }

    //        internal static void DeleteLastNode(this List e)
    //        {
    //             Node head = e.GetHead();

    //             if (head.Element != null)
    //            {
    //             for (int i = 0; i< e.Size - 2; i++)
    //                    {
    //                        head = head.Next;
    //                    }

    //             head.Next = null;
    //            }
    //            e.Output();
    //        }
    //    }
    //}

    public class StandartTypes<T1, T2, T3> where T1 : struct where T2 : struct where T3 : struct
    {
        T1 A { get; set; }
        T2 B { get; set; }
        T3 C { get; set; }

        public StandartTypes(T1 A, T2 B, T3 C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }


        public void ShowTypes()
        {
            Console.WriteLine($"Первый тип {A.GetType()} его значение {A}");
            Console.WriteLine($"Второй тип {B.GetType()} его значение {B}");
            Console.WriteLine($"Третий тип {C.GetType()} его значение {C}");
        }




    }

    interface IBook
    {
        bool Firm_Binding();
        string Pages_Amount();

    }



    interface IPaper
    {
        void Clone();
    }





    public abstract class Publishing_House : IBook
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public abstract string Pages_Amount();
        virtual public bool Firm_Binding()
        {
            return true;
        }

        public abstract void Clone();

    }

    public class Print_Edition : Publishing_House, IPaper
    {

        protected string name;
        protected int year;
        protected int price;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }


        public override string ToString()
        {
            Console.WriteLine(Name + " " + Year + " " + Price + " ");
            return " type " + base.ToString();
        }

        public override void Clone()
        {
            Console.WriteLine("Метод абстрактного класса");
        }

        void IPaper.Clone()
        {
            Console.WriteLine("Метод интерфейса");
        }


        public override bool Firm_Binding()
        {
            return false;
        }

        public override string Pages_Amount()
        {
            return "Количество книг в этом печатном издании не определено";

        }
    }







    sealed public class Book : Print_Edition, IPaper
    {



        public Book(string name, int year, int price)
        {
            this.name = name;
            this.year = year;
            this.price = price;
        }


        public override string ToString()
        {
            Console.WriteLine(Name + " " + Year + " " + Price + " ");
            return " type " + base.ToString();
        }

    }
}
