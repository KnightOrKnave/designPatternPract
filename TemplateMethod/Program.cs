using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace TemplateMethod
{
    class Program
    {

        public abstract class AbstractDisplay
        {
            public abstract void open();
            public abstract void print();
            public abstract void close();
            public void display()
            {
                open();
                for(int i=0;i<5;i++)
                {
                    print();
                }
                close();
            }
        }

        public class CharDisplay : AbstractDisplay
        {
            private char ch;
            public CharDisplay(char ch)
            {
                this.ch = ch;
            }

            public override void close()
            {
                Console.Write(">>");
            }

            public override void open()
            {
                Console.Write("<<");
            }

            public override void print()
            {
                Console.Write(ch);
            }
        }

        public class StringDisplay : AbstractDisplay
        {
            private string st;
            private int stringLen;
            public StringDisplay(string st)
            {
                this.st = st;
                this.stringLen = st.Length;
            }

            public override void close()
            {
                printCap();
            }

            public override void open()
            {
                printCap();
            }

            public override void print()
            {
                Console.WriteLine($"|{st}|");
            }

            private void printCap()
            {
                Console.Write("+");
                for (int i = 0; i < this.stringLen; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine("+");

            }
        }

        static void Main(string[] args)
        {
            var d1 = new CharDisplay('H');
            d1.display();
            Console.WriteLine("");
            var d2 = new StringDisplay("Hello, world.");
            d2.display();
            Console.WriteLine("");
            var d3 = new StringDisplay("Learn from yesterday.");
            d3.display();
        }
    }
}
