using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Composite
{

    public abstract class Entry
    {
        public abstract string getName();
        public abstract int getSize();
        public Entry add(Entry entry)
        {
            throw new Exception();
        }
        public void printList()
        {
            printList("");
        }
        protected extern void printList(string prefix);
        public string tostring()
        {
            return $"{getName()}({getSize()})";
        }
    }

    public class File : Entry
    {
        private string name;
        private int size;

        public File(string name,int size)
        {
            this.name = name;
            this.size = size;
        }
        public override string getName()
        {
            return this.name;
        }

        public override int getSize()
        {
            return this.size;
        }

        protected override void printList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this.tostring()}");
        }
    }

    public class Directory : Entry
    {
        private string name;
        private IEnumerable<Entry> List;
        
        public Directory(string name)
        {
            this.name = name;
            List = new List<Entry>();
        }
        public override string getName()
        {
            return name;
        }

        public override int getSize()
        {
            int size = 0;
            foreach(var e in List)
            {
                size+=e.getSize();
            }
            return size;
        }

        protected override void printList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this}");
            foreach(Entry e in List)
            {
                e.printList($"{prefix}/{name}");
            }
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
