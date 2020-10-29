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
        private int ticket;

        private static TicketMaker ticketmaker = new TicketMaker();

        private TicketMaker()
        {
            this.ticket = 1000;
        }

        public int getNextTicketNumber()
        {
            return ticket++;
        }

        public static TicketMaker getInstance()
        {
            return ticketmaker;
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


            TicketMaker t1 = TicketMaker.getInstance();
            TicketMaker t2 = TicketMaker.getInstance();
            for(int i=0;i<10;i++)
            {
                Console.WriteLine(t1.getNextTicketNumber());
                Console.WriteLine(t2.getNextTicketNumber());
            }

        }
    }
}
