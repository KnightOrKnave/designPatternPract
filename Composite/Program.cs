using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Composite
{

    //public abstract class Entry
    //{
    //    public abstract string getName();
    //    public abstract int getSize();
    //    public Entry add(Entry entry)
    //    {
    //        throw new Exception();
    //    }
    //    public void printList()
    //    {
    //        printList("");
    //    }
    //    protected abstract void printList(string prefix);
    //    public string tostring()
    //    {
    //        return $"{getName()}({getSize()})";
    //    }
    //}

    public interface Entry
    {
        public string getName();
        public int getSize();
        public Entry add(Entry entry)
        {
            throw new Exception();
        }
        public void printList()
        {
            printList("");
        }
        public void printList(string prefix) { }
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
        public string getName()
        {
            return this.name;
        }

        public int getSize()
        {
            return this.size;
        }
        public string tostring()
        {
            return $"{getName()}({getSize()})";
        }

        public void printList(string prefix)
        {
            Console.WriteLine($"{prefix}/{tostring()}");
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
        public string getName()
        {
            return name;
        }

        public int getSize()
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
        public string tostring()
        {
            return $"{getName()}({getSize()})";
        }
        public void printList(string prefix)
        {
            Console.WriteLine($"{prefix}/{tostring()}");
            foreach(var e in List)
            {
                e.printList($"{prefix}/{name}");
            }
        }

        //実装しないといけない？
        public void printList()
        {
            printList("");
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
