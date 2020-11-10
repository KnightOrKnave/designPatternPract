using System;
using System.Runtime.Intrinsics.X86;

namespace Decorator
{
    class Program
    {

        public abstract class Display
        {
            public abstract int getColumns();
            public abstract int getRows();
            public abstract string getRowText(int row);
            public void show()
            {
                for(int i=0;i<getRows();i++)
                {
                    Console.WriteLine(getRowText(i));
                }
            }
        }

        public abstract class Border : Display
        {
            protected Display display;
            protected Border(Display display)
            {
                this.display = display;
            }
        }


        public class StringDisplay : Display
        {
            private string str;

            public StringDisplay(string str)
            {
                this.str = str;
            }
            public override int getColumns()
            {
                return this.str.Length;
            }

            public override int getRows()
            {
                return 1;
            }

            public override string getRowText(int row)
            {
                if(row==0)
                {
                    return this.str;
                }
                else
                {
                    return null;
                }
            }
        }

        public class SideBorder : Border
        {
            private char borderChar;

            public SideBorder(Display disp,char ch):base(disp)
            {
                borderChar = ch;
            }

            public override int getColumns()
            {
                return 1 + display.getColumns() + 1;
            }

            public override int getRows()
            {
                return display.getRows();
            }

            public override string getRowText(int row)
            {
                return $"{borderChar}{display.getRowText(row)}{borderChar}";
            }
        }

        public class FullBorder : Border
        {
            private string makeLine(char ch,int count)
            {
                string buf = new string(ch, count);
                return buf;
            }

            public FullBorder(Display disp):base(disp)
            {
            }
            public override int getColumns()
            {
                return 1 + display.getColumns() + 1;
            }

            public override int getRows()
            {
                return 1 + display.getRows() + 1;
            }

            public override string getRowText(int row)
            {
                if (row == 0) 
                {
                    return $"+{makeLine('-', display.getColumns())}+";

                }
                else if(row == display.getRows() + 1)
                {
                    return $"+{makeLine('-', display.getColumns())}+";
                }
                else
                {
                    return $"|{display.getRowText(row-1)}|";
                }
            }
        }

        static void Main(string[] args)
        {
            Display d1 = new StringDisplay("Hello world.");
            Display d2 = new SideBorder(d1, '#');
            Display d3 = new FullBorder(d2);
            d1.show();
            d2.show();
            d3.show();

            Display d4 = new SideBorder(new FullBorder(new FullBorder(new SideBorder(new FullBorder(new StringDisplay("Hello")), '*'))), '/');
            d4.show();
        }
    }
}
