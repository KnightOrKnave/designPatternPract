using System;
using System.Collections;
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
        protected abstract void printList(string prefix);
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
        private List<Entry> List;
        
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

        public Entry add(Entry entry)
        {
            List.Add(entry);
            return this;
        }

        protected override void printList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this}");
            foreach(var e in List)
            {
                Console.WriteLine("");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Directory rootdir = new Directory("root");
            Directory bindir = new Directory("bin");
            Directory tmpdir = new Directory("tmp");

            rootdir.add(bindir);
            rootdir.add(tmpdir);
            bindir.add(new File("vi", 1000));
            bindir.add(new File("latex", 1000));
            rootdir.printList();
        }
    }
}
