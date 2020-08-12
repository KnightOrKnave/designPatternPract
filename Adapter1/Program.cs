using System;

namespace Adapter1
{

    public class Banner
    {
        private string str;

        public Banner(string s)
        {
            this.str = s;
        }

        public void showWithParen()
        {
            Console.WriteLine($"({str})");
        }

        public void showWithAster()
        {
            Console.WriteLine($"*{str}*");
        }

    }

    public interface Print
    {
        public void printWeak();
        public void printStrong();
    }

    public class PrintBanner : Banner,Print
    {
        public PrintBanner(string s) : base(s)
        {
        }

        public void printStrong()
        {
            showWithAster();
        }

        public void printWeak()
        {
            showWithParen();
        }
    }

    /*
     * 移譲パターン
     */
    public class PrintBanner2 : Print
    {
        private Banner banner;
        public PrintBanner2(string s)
        {
            this.banner = new Banner(s);
        }
        public void printStrong()
        {
            banner.showWithAster();
        }

        public void printWeak()
        {
            banner.showWithParen();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*
            var van = new Banner("Hello world");
            van.showWithParen();
            van.showWithAster();
            */
            var van = new PrintBanner("Hello world");
            van.printStrong();
            van.printWeak();
        }
    }
}
