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
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public Product createClone()
        {
            throw new NotImplementedException();
        }

        public void use(string s)
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
