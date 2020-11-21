using System;

namespace Proxy
{

    public interface IPrinter
    {
        public void SetPrinterName(string name);
        public string GetPrinterName();
        public void Print(string str);

    }
    public class Printer : IPrinter
    {
        private string _name;

        public Printer()
        {
            Console.WriteLine("重い処理");
        }

        public Printer(string name)
        {
            _name = name;
            Console.WriteLine("initialize");
        }

        public string GetPrinterName()
        {
            return _name;
        }

        public void Print(string str)
        {
            Console.WriteLine($"==={_name}===");
            Console.WriteLine(str);
        }

        public void SetPrinterName(string name)
        {
            _name = name;
        }
    }

    public class PrinterProxy : IPrinter
    {
        private string _name;
        private IPrinter _real;

        public PrinterProxy() { }
        public PrinterProxy(string name)
        {
            _name = name;
        }

        public string GetPrinterName()
        {
            return _name;
        }

        public void Print(string str)
        {
            realize();
            _real.Print(str);
        }

        public void SetPrinterName(string name)
        {
            _name = name;
            if (_real != null)
            {
                _real.SetPrinterName(name);
            }
        }

        private void realize()
        {
            if (_real == null)
            {
                _real = new Printer(_name);
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            IPrinter p = new PrinterProxy("Alice");
            p.SetPrinterName("Bob");
            p.Print("Hello world.");

        }
    }
}
