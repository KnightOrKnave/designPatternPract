using System;

namespace Bridge
{
    class Program
    {

        public class Display
        {
            private DisplayImpl impl;
            public Display(DisplayImpl impl)
            {
                this.impl = impl;
            }

            public void open()
            {
                impl.rawOpen();
            }

            public void print()
            {
                impl.rawPrint();
            }

            public void close()
            {
                impl.rawClose();
            }

            public void display()
            {
                open();
                print();
                close();
            }
        }

        public class CountDisplay :Display
        {
            public CountDisplay(DisplayImpl impl):base(impl)
            {

            }

            public void multiDisplay(int times)
            {
                open();
                for(int i=0;i<times;i++)
                {
                    print();
                }
                close();
            }
        }
        public abstract class DisplayImpl
        {
            public abstract void rawOpen();
            public abstract void rawPrint();
            public abstract void rawClose();
        }

        public class StringDisplayImpl : DisplayImpl
        {
            private string str;

            public StringDisplayImpl(string s)
            {
                this.str = s;
            }
            public override void rawClose()
            {
                printLine();
            }

            public override void rawOpen()
            {
                printLine();
            }

            public override void rawPrint()
            {
                Console.WriteLine($"|{str}|");
            }

            private void printLine()
            {
                Console.Write("+");
                for(int i=0;i<str.Length;i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine("+");
            }
        }




        static void Main(string[] args)
        {
            Display d1 = new Display(new StringDisplayImpl("Hello world."));
            CountDisplay d2 = new CountDisplay(new StringDisplayImpl("Hello world."));

            d1.display();
            d2.multiDisplay(5);
        }
    }
}
