using System;
using System.Collections.Generic;

namespace Prototype
{
    public interface Product : ICloneable
    {
        public abstract void use(string s);
        public abstract Product createClone();
    }

    public class Manager
    {
        private Dictionary<string, Product> showcase = new Dictionary<string, Product>();
        public void register(string name,Product proto)
        {
            showcase.Add(name, proto);
        }

        public Product create(string protoname)
        {
            Product p = showcase[protoname];
            return p.createClone();
        }
    }

    public class MessageBox : Product
    {
        private char dotchar;
        public MessageBox(char dc)
        {
            this.dotchar = dc;
        }

        public object Clone()
        {
            return this;
        }

        public Product createClone()
        {
            Product p = null;
            try
            {
                p = (Product)Clone();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return p;
        }

        public void use(string s)
        {
            for(int i=0;i<s.Length+4;i++)
            {
                Console.Write(this.dotchar);
            }
            Console.WriteLine("");
            Console.WriteLine($"{this.dotchar} {s} {this.dotchar}");
            for (int i = 0; i < s.Length + 4; i++)
            {
                Console.Write(this.dotchar);
            }
            Console.WriteLine("");
        }
    }

    public class UnderlinePen : Product
    {
        private char ulchar;
        public UnderlinePen(char dc)
        {
            this.ulchar = dc;
        }

        public object Clone()
        {
            return this;
        }

        public Product createClone()
        {
            Product p = null;
            try
            {
                p = (Product)Clone();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return p;
        }

        public void use(string s)
        {
            Console.WriteLine($@"""{s}""");
            Console.Write(" ");
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(this.ulchar);
            }
            Console.WriteLine(" ");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            UnderlinePen upen = new UnderlinePen('~');
            MessageBox mbox = new MessageBox('*');
            MessageBox sbox = new MessageBox('/');
            manager.register("strong message", upen);
            manager.register("warning box", mbox);
            manager.register("slash box", sbox);

            Product p1 = manager.create("strong message");
            Product p2 = manager.create("warning box");
            Product p3 = manager.create("slash box");

            p1.use("Hello,World. ");
            p2.use("Hello,World. ");
            p3.use("Hello,World. ");

        }
    }
}
