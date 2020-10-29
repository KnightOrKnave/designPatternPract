using System;

namespace Singleton
{

    public class Singleton
    {
        private static Singleton singleton = new Singleton();

        private Singleton()
        {
            Console.WriteLine("インスタンスを作成しました");
        }

        public static Singleton getInstance()
        {
            return singleton;
        }
    }

    public class TicketMaker
    {
        private int ticket = 1000;
        public int getNextTicketNumber()
        {
            return ticket++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Singleton obj1 = Singleton.getInstance();
            Singleton obj2 = Singleton.getInstance();

            if(obj1==obj2)
            {
                Console.WriteLine("same");
            }
            else
            {
                Console.WriteLine("different");
            }


            TicketMaker t1 = new TicketMaker();
            TicketMaker t2 = new TicketMaker();
            for(int i=0;i<10;i++)
            {
                Console.WriteLine(t1.getNextTicketNumber());
                Console.WriteLine(t2.getNextTicketNumber());
            }

        }
    }
}
